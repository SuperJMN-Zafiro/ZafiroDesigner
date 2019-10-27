using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Designer.Domain.ViewModels;
using ReactiveUI;
using Zafiro.Core;
using Document = Designer.Domain.Models.Document;

namespace Designer.Core
{
    public class MainViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<bool> isBusy;
        private readonly ObservableAsPropertyHelper<Project> project;
        private readonly IProjectMapper mapper;
        private readonly IProjectStore projectStore;
        private bool isImportVisible;

        public MainViewModel(IFilePicker filePicker, IProjectMapper mapper,
            IProjectStore projectStore, ImportExtensionsViewModel importViewModel)
        {
            this.mapper = mapper;
            this.projectStore = projectStore;

            var saveExtensions = new[]
            {
                new KeyValuePair<string, IList<string>>(Constants.FileFormatName,
                    new List<string> {Constants.FileFormatExtension})
            };

            Load = ReactiveCommand.CreateFromObservable(() =>
                LoadProject(filePicker, new[] {Constants.FileFormatExtension}));

            New = ReactiveCommand.Create(CreateNewDocument);

            Save = ReactiveCommand.CreateFromObservable(() => SaveProject(filePicker, Project, saveExtensions));

            var projects = Load.Merge(New).Merge(importViewModel.ImportedProjects.Do(_ => IsImportVisible = false));
            project = projects
                .Select(mapper.Map)
                .ToProperty(this, model => model.Project);

            isBusy = Load.IsExecuting.Merge(Save.IsExecuting).Merge(importViewModel.IsBusy).ToProperty(this, x => x.IsBusy);

            New.Execute().Subscribe();

            ShowImport = ReactiveCommand.Create(() => IsImportVisible = true);
            HideImport = ReactiveCommand.Create(() => IsImportVisible = false);
        }

        private static Domain.Models.Project CreateNewDocument()
        {
            return new Domain.Models.Project
            {
                Documents = new List<Document>
                {
                    new Document
                    {
                        Name = "New document",
                    }
                }
            };
        }

        public ReactiveCommand<Unit, bool> ShowImport { get; set; }

        public ReactiveCommand<Unit, bool> HideImport { get; set; }

        public bool IsImportVisible
        {
            get => isImportVisible;
            set => this.RaiseAndSetIfChanged(ref isImportVisible, value);
        }

        public bool IsBusy => isBusy.Value;

        public ReactiveCommand<Unit, Project> Save { get; }

        public Project Project => project.Value;

        public ReactiveCommand<Unit, Domain.Models.Project> New { get; }

        public ReactiveCommand<Unit, Domain.Models.Project> Load { get; }

        
        private IObservable<Domain.Models.Project> LoadProject(IFilePicker filePicker, string[] loadExtensions)
        {
            return filePicker.Pick("Load", loadExtensions)
                .SelectMany(file => LoadProject(file, projectStore));
        }

        private async Task<Domain.Models.Project> LoadProject(ZafiroFile file, IProjectStore loader)
        {
            if (loader == null)
            {
                throw new InvalidOperationException("No plugins to load this file type!");
            }

            using (var stream = await file.OpenForRead())
            {
                return await loader.Load(stream);
            }
        }

        private IObservable<Project> SaveProject(IFilePicker filePicker, Project project,
            KeyValuePair<string, IList<string>>[] loadExtensions)
        {
            return filePicker.PickSave("Save", loadExtensions)
                .SelectMany(file => SaveProject(project, file));
        }

        private async Task<Project> SaveProject(Project project, ZafiroFile file)
        {
            using (var stream = await file.OpenForWrite())
            {
                var model = mapper.Map(project);
                await projectStore.Save(model, stream);
                await stream.FlushAsync();
                return project;
            }
        }
    }
}