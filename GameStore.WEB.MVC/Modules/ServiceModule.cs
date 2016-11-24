using System.Reflection;
using Autofac;
using GameStore.WEB.MVC.Modules;
using Module = Autofac.Module;

namespace GameStore.Web.MVC.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("GameStore.BLL.Services"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterModule(new AutoMapperModule());
        }
    }
}