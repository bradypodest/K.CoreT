using Autofac;
using Autofac.Extensions.DependencyInjection;
using K.Core.IServices;
using K.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace K.Core.Tests
{
    public class DI_Test
    {

        [Fact]
        public void DI_Connet_Test()
        {
            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();
            builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();

            //指定已扫描程序集中的类型注册为提供所有其实现的接口。
            var assemblysServices = Assembly.Load("K.Core.Services");
            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();
            var assemblysRepository = Assembly.Load("K.Core.Repository");
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();


            //将services填充到Autofac容器生成器中
            //builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            Assert.True(ApplicationContainer.ComponentRegistry.Registrations.Count() > 0);
        }
    }
}
