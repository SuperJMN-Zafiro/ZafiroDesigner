using System;
using System.Reactive;
using System.Threading.Tasks;
using Designer.Domain.Models;
using ReactiveUI;

namespace Designer.Core
{
    public class ExtensionViewModel : ReactiveObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; }

        public ExtensionViewModel(string name, string description, byte[] logo)
        {
            Name = name;
            Description = description;
            Logo = logo;
            Import = ReactiveCommand.CreateFromTask(LoadBytes);
            Import.Subscribe(project => { });
        }

        private Task<Project> LoadBytes()
        {
            return Task.FromResult(new Project());
        }

        public ReactiveCommand<Unit, Project> Import { get; set; }
    }
}