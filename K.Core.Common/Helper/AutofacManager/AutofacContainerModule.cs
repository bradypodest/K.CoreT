using System;
using System.Collections.Generic;
using System.Text;

namespace K.Core.Common.Helper.AutofacManager
{
    public class AutofacContainerModule
    {
        public static TService GetService<TService>() where TService : class
        {
            return typeof(TService).GetService() as TService;
        }
    }
}
