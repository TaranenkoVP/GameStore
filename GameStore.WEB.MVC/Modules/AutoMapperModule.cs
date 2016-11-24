using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Autofac;
using AutoMapper;
using GameStore.BLL.Infrastructure.Mappers;
using GameStore.WEB.Infrastructure.Mappers;
using Module = Autofac.Module;

namespace GameStore.WEB.MVC.Modules
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).As<Profile>();

            //builder.Register(context => new MapperConfiguration(cfg =>
            //{
            //    foreach (var profile in context.Resolve<IEnumerable<Profile>>())
            //    {
            //        cfg.AddProfile(profile);
            //    }
            //})).AsSelf().SingleInstance();

            //builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
            //    .As<IMapper>()
            //    .InstancePerLifetimeScope();



            var autoMapperProfileTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract));

            var autoMapperProfiles = autoMapperProfileTypes.Select(p => (Profile)Activator.CreateInstance(p));
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in autoMapperProfiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();




            //builder.Register(c => new AutoMapperConfigWeb(Assembly.GetExecutingAssembly().GetName().Name).Configuration)
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            //builder.Register(c => new AutoMapperConfigBLL("GameStore.BLL.Entities").Configuration)
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            //builder.Register(c => c.Resolve<IConfigurationProvider>().CreateMapper()).As<IMapper>();
        }
    }
}