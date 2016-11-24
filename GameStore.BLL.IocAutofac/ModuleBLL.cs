using Autofac;
using GameStore.DAL.EF;
using GameStore.DAL.EF.Context;
using GameStore.DAL.Interfaces;

namespace GameStore.BLL.IocAutofac
{
    public class ModuleBLL : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(MainContext)).As(typeof(IContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork<MainContext>)).As(typeof(IUnitOfWork)).InstancePerRequest();
        }
    }
}