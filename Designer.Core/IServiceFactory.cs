using Zafiro.Core;

namespace Designer.Core
{
    public interface IServiceFactory
    {
        IDictionaryBasedService Create(string packageFamilyName, string serviceName);
    }
}