using System.Linq;
using Designer.Core;
using Designer.Core.Persistence;
using Designer.Core.Tools;
using Designer.Domain.ViewModels;
using Designer.Extensions;
using Grace.DependencyInjection;
using Zafiro.Core;
using Zafiro.Uwp.Controls;

namespace Designer
{
    public class Composition
    {
        private static readonly DependencyInjectionContainer Container;

        static Composition()
        {
            Container = new DependencyInjectionContainer();
            Container.Configure(registrationBlock =>
            {
                var toolType = typeof(Tool);
                var assembly = typeof(RectangleTool).Assembly;

                registrationBlock.Export(assembly.ExportedTypes
                        .Where(TypesThat.AreBasedOn<Tool>())
                        .Where(x => !x.IsAbstract))
                    .ByTypes(type => new[] { toolType });

                registrationBlock.Export<UwpFilePicker>().As<IFilePicker>().Lifestyle.Singleton();
                registrationBlock.Export<DesignContext>().As<IDesignContext>().Lifestyle.Singleton();
                registrationBlock.Export<ViewModelFactory>().As<IViewModelFactory>().Lifestyle.Singleton();
                registrationBlock.Export<ProjectStore>().As<IProjectStore>().Lifestyle.Singleton();
                registrationBlock.ExportFactory(() => new ExtensionsProvider("superjmn.suppadesigner")).As<IExtensionsProvider>().Lifestyle.Singleton();
                registrationBlock.Export<ImportExtensionsViewModel>().Lifestyle.Singleton();
            });
        }

        public static MainViewModel Root => Container.Locate<MainViewModel>();

        public ImportExtensionsViewModel Import => Container.Locate<ImportExtensionsViewModel>();
    }
}