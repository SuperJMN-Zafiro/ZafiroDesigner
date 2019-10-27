using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Zafiro.Core;

namespace Designer.Extensions
{
    public class WindowsAppService : IDictionaryBasedService
    {
        private readonly string packageFamilyName;
        private readonly string appServiceName;

        public WindowsAppService(string packageFamilyName, string appServiceName)
        {
            this.packageFamilyName = packageFamilyName;
            this.appServiceName = appServiceName;
        }

        public async Task<Payload> Request(Payload payload)
        {
            using (var connection = new AppServiceConnection {AppServiceName = appServiceName, PackageFamilyName = packageFamilyName })
            {
                var status = await connection.OpenAsync();
                if (status == AppServiceConnectionStatus.Success)
                {
                    var result = await connection.SendMessageAsync(payload.ToValueSet());
                    if (result.Status == AppServiceResponseStatus.Success)
                    {
                        return result.Message.ToPayload();
                    }

                    throw new ServiceException($"The response indicates a failure. Reason: {result.Status}");
                }

                throw new ServiceException($"Couldn't connect to the service. Reason: {status}");
            }
        }
    }
}