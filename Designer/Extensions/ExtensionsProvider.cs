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
using Zafiro.Core.Files;
using Zafiro.Core.Mixins;
using DisposableMixins = System.Reactive.Disposables.DisposableMixins;

namespace Designer.Extensions
{
    public class ExtensionsProvider : ReactiveObject, IExtensionsProvider
    {
        private readonly CompositeDisposable refreshers = new CompositeDisposable();
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        private ReadOnlyObservableCollection<ImportViewModel> extensions;

        public IObservable<IChangeSet<ImportViewModel, string>> ObservableChangeset { get; }

        public ExtensionsProvider(string contract, IServiceFactory factory, IFilePicker picker)
        {
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

                    DisposableMixins.DisposeWith(Observable
                            .FromEventPattern<AppExtensionPackageInstalledEventArgs>(catalog, "PackageInstalled")
                            .Subscribe(args => x.AddOrUpdate(args.EventArgs.Extensions)), refreshers);

                    DisposableMixins.DisposeWith(Observable
                            .FromEventPattern<AppExtensionPackageUninstallingEventArgs>(catalog, "PackageUninstalling")
                            .Subscribe(args => { }), refreshers);

                });
                return Unit.Default;
            });

            Func<Task<byte[]>> GetLogo(AppExtension appExtension)
            {
                return async () =>
                {
                    var open = await appExtension.AppInfo.DisplayInfo.GetLogo(new Size(1, 1)).OpenReadAsync();
                    var stream = open.AsStreamForRead();
                    return await StreamMixin.ReadBytes(stream);
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

            ObservableChangeset = source
                .Connect()
                .Transform(ext => new ImportViewModel(ext.DisplayName, ext.Description, GetLogo(ext), GetService(ext), picker));

            DisposableMixins.DisposeWith(ObservableChangeset
                    .Bind(out extensions)
                    .Subscribe(), disposables);

            DisposableMixins.DisposeWith(Connect.Execute()
                    .Subscribe(), disposables);
        }

        public ReactiveCommand<Unit, Unit> Connect { get; set; }

        public ReadOnlyObservableCollection<ImportViewModel> Extensions
        {
            get => extensions;
            set => extensions = value;
        }
    }
}