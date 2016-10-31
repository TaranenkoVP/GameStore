using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace BLL.IocAutofac
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            builder.RegisterAssemblyTypes(Assembly.Load("DAL.EF"))
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}