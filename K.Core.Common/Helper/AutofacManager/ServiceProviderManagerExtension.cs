using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Common.Helper.AutofacManager
{
    public static class ServiceProviderManagerExtension
    {
        public static object GetService(this Type serviceType)
        {
            // HttpContext.Current.RequestServices.GetRequiredService<T>(serviceType);
            return HttpContext.Current.RequestServices.GetService(serviceType);
        }

    }
}
