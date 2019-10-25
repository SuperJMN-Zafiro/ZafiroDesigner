using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Windows.ApplicationModel.AppExtensions;
using Designer.Core;
using DynamicData;
using ReactiveUI;

namespace Designer.Extensions
{
    public class ExtensionsProvider : ReactiveObject, IExtensionsProvider
    {
        private readonly CompositeDisposable refreshers = new CompositeDisposable();
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        private ReadOnlyObservableCollection<ExtensionViewModel> extensions;
        private readonly IObservable<IChangeSet<ExtensionViewModel, string>> observableChangeset;

        public IObservable<IChangeSet<ExtensionViewModel, string>> ObservableChangeset => observableChangeset;

        public ExtensionsProvider(string contract)
        {
            var catalog = AppExtensionCatalog.Open(contract);
            var source = new SourceCache<AppExtension, string>(a => a.Id);

            //Invoke = ReactiveCommand.CreateFromTask(async () =>
            //{
            //    var connection = new AppServiceConnection();

            //    var props = await SelectedExtension.GetExtensionPropertiesAsync();
            //    var serviceName = (string)((PropertySet)props["Service"])["#text"];

            //    connection.PackageFamilyName = SelectedExtension.Package.Id.FamilyName;
            //    connection.AppServiceName = serviceName;

            //    var status = await connection.OpenAsync();
            //    if (status == AppServiceConnectionStatus.Success)
            //    {
            //        var valueSet = new ValueSet();
            //        valueSet["Command"] = "Import";
            //        byte[] bytes = await GetBytes();
            //        valueSet["Data"] = bytes;
            //        var message = await connection.SendMessageAsync(valueSet);
            //    }
            //}, this.WhenAnyValue(x => x.SelectedExtension).Select(x => x != null));

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

            observableChangeset = source
                .Connect()
                .Transform(ext => new ExtensionViewModel(ext.DisplayName, ext.Description, null));

            observableChangeset
                .Bind(out extensions)
                .Subscribe()
                .DisposeWith(disposables);

            Connect.Execute()
                .Subscribe()
                .DisposeWith(disposables);
        }

        public ReactiveCommand<Unit, Unit> Connect { get; set; }

        public ReadOnlyObservableCollection<ExtensionViewModel> Extensions
        {
            get => extensions;
            set => extensions = value;
        }
    }
}