using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Designer.Domain.Models;
using Newtonsoft.Json;
using ReactiveUI;
using Zafiro.Core;
using Zafiro.Core.Files;
using Zafiro.Core.Mixins;

namespace Designer.Core
{
    public class ImportViewModel : ReactiveObject
    {
        private readonly IFilePicker picker;
        private IDictionaryBasedService service;
        private byte[] logo;
        public string Name { get; }
        public string Description { get; }

        public ImportViewModel(string name, string description, Func<Task<byte[]>> getLogo, Func<Task<IDictionaryBasedService>> getService, IFilePicker picker)
        {
            this.picker = picker;
            Observable.FromAsync(getLogo).Subscribe(bytes => Logo = bytes);
            Observable.FromAsync(getService).Subscribe(service => this.service = service);
            Name = name;
            Description = description;
            Import = ReactiveCommand.CreateFromTask(ImportProject);
        }

        public byte[] Logo
        {
            get => logo;
            set => this.RaiseAndSetIfChanged(ref logo, value);
        }

        private async Task<Project> ImportProject()
        {
            var fileTypes = await service.Request(new Payload(new Dictionary<string, object>()) {["Command"] = "FileTypes"});
            var asArrays = fileTypes.Select(x => x.Value).Cast<string[]>();
            var extensions = asArrays.SelectMany(types => types);

            var file = await picker.Pick("Import", extensions.ToArray());

            if (file == null)
            {
                return null;
            }

            var items = new Dictionary<string, object>()
            {
                ["Command"] = "Import",
                ["Data"] = await GetData(file)
            };

            var result = await service.Request(new Payload(items));

            var data = (string) result["Result"];
            var project = JsonConvert.DeserializeObject<Project>(data, new JsonSerializerSettings() { TypeNameHandling  = TypeNameHandling.All});

            return project;
        }

        private async Task<byte[]> GetData(ZafiroFile file)
        {
            using (var stream = await file.OpenForRead())
            {
                return await stream.ReadBytes();
            }
        }

        public ReactiveCommand<Unit, Project> Import { get; set; }
    }
}