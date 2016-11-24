using Autofac;

namespace GameStore.BLL.IocAutofac
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            //builder.RegisterAssemblyTypes(Assembly.Load("DAL.EF"))
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();

            ////TODO fix generic reflection
            //builder.RegisterType(typeof(GenericRepository<Comment>))
            //    .As(typeof(IGenericRepository<Comment>))
            //    .InstancePerRequest();
        }
    }
}