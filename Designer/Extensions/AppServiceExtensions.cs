using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;
using Windows.Foundation.Collections;
using Cimbalino.Toolkit.Extensions;
using Zafiro.Core;

namespace Designer.Extensions
{
    public static class AppServiceExtensions
    {
        public static Payload ToPayload(this ValueSet set)
        {
            var dic = set.Select(pair => pair);

            return new Payload(dic);
        }

        public static ValueSet ToValueSet(this Payload payload)
        {
            var set = new ValueSet();
            set.AddRange(payload);
            return set;
        }

        public static async Task<(string, string)> GetConnectionInfo(this AppExtension appExtension)
        {
            var packageFamilyname = appExtension.Package.Id.FamilyName;
            var props = await appExtension.GetExtensionPropertiesAsync();
            var serviceName = (string)((PropertySet)props["Service"])["#text"];


            return (packageFamilyname, serviceName);
        }
    }
}