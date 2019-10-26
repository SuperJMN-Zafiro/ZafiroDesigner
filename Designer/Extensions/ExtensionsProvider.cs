using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.Foundation;
using Designer.Core;
using DynamicData;
using ReactiveUI;
using Zafiro.Core;

namespace Designer.Extensions
{
    public class ExtensionsProvider : ReactiveObject, IExtensionsProvider
    {
        private readonly IServiceFactory factory;
        private readonly IFilePicker picker;
        private readonly CompositeDisposable refreshers = new CompositeDisposable();
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        private ReadOnlyObservableCollection<ImportExtensionViewModel> extensions;
        private readonly IObservable<IChangeSet<ImportExtensionViewModel, string>> observableChangeset;

        public IObservable<IChangeSet<ImportExtensionViewModel, string>> ObservableChangeset => observableChangeset;

        public ExtensionsProvider(string contract, IServiceFactory factory, IFilePicker picker)
        {
            this.factory = factory;
            this.picker = picker;
            var catalog = AppExtensionCatalog.Open(contract);
            var source = new SourceCache<AppExtension, string>(a => a.Id);

            Connect = ReactiveCommand.CreateFromTask(async () =>
            {
                var initial = await catalog.FindAllAsync();

                source.Edit(x =>
                {
                    x.Clear();
                    var findAllAsync = initial;
                    x.AddOrUpdate(findAllAsync);

                    refreshers.Dispose();

                    Observable
                        .FromEventPattern<AppExtensionPackageInstalledEventArgs>(catalog, "PackageInstalled")
                        .Subscribe(args => x.AddOrUpdate(args.EventArgs.Extensions))
                        .DisposeWith(refreshers);

                    Observable
                        .FromEventPattern<AppExtensionPackageUninstallingEventArgs>(catalog, "PackageUninstalling")
                        .Subscribe(args => { })
                        .DisposeWith(refreshers);

                });
                return Unit.Default;
            });

            Func<Task<byte[]>> GetLogo(AppExtension appExtension)
            {
                return async () =>
                {
                    var open = await appExtension.AppInfo.DisplayInfo.GetLogo(new Size(1, 1)).OpenReadAsync();
                    var stream = open.AsStreamForRead();
                    return await stream.ReadBytes();
                };
            }

            Func<Task<IDictionaryBasedService>> GetService(AppExtension appExtension)
            {
                return async () =>
                {
                    var connInfo = await appExtension.GetConnectionInfo();
                    return factory.Create(connInfo.Item1, connInfo.Item2); 
                };
            }

            observableChangeset = source
                .Connect()
                .Transform(ext => new ImportExtensionViewModel(ext.DisplayName, ext.Description, GetLogo(ext), GetService(ext), picker));

            observableChangeset
                .Bind(out extensions)
                .Subscribe()
                .DisposeWith(disposables);

            Connect.Execute()
                .Subscribe()
                .DisposeWith(disposables);
        }

        public ReactiveCommand<Unit, Unit> Connect { get; set; }

        public ReadOnlyObservableCollection<ImportExtensionViewModel> Extensions
        {
            get => extensions;
            set => extensions = value;
        }
    }
}