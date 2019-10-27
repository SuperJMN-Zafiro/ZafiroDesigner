using Designer.Core;
using Designer.Extensions;
using Zafiro.Core;

namespace Designer
{
    public class ServiceFactory : IServiceFactory
    {
        public IDictionaryBasedService Create(string packageFamilyName, string serviceName)
        {
            return new WindowsAppService(packageFamilyName, serviceName);
        }
    }
}