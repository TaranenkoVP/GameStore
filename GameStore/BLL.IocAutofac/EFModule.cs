using System.Data.Entity;
using Autofac;
using DAL.EF;
using DAL.EF.Infrastructure;
using DAL.EF.Repositories;
using DAL.Interfaces;
using DAL.Interfaces.Repositories;
using DAL.Models;

namespace BLL.IocAutofac
{
    public class EFModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //TODO fix generic reflection
            builder.RegisterType(typeof (GenericRepository<Comment>))
                .As(typeof (IGenericRepository<Comment>))
                .InstancePerRequest();
            //builder.RegisterModule(new RepositoryModule());
            builder.RegisterType(typeof (MainContext)).As(typeof (DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof (CommentUnitOfWork)).As(typeof (ICommentUnitOfWork)).InstancePerRequest();
            builder.RegisterType(typeof (GameUnitOfWork)).As(typeof (IGameUnitOfWork)).InstancePerRequest();
        }
    }
}