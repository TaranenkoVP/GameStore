using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace GameStore.BLL.Infrastructure.Mappers
{
    public class AutoMapperBllProfile : Profile
    {
        public AutoMapperBllProfile()
        {
            RegisterMappings("GameStore.BLL.Entities");
        }

        private void RegisterMappings(params string[] assemblies)
        {
            var types = new List<Type>();
            foreach (var assembly in assemblies.Select(Assembly.Load))
                types.AddRange(assembly.GetExportedTypes());

            LoadStandardMappings(types);
            //LoadCustomMappings();
        }

        private void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                from i in t.GetInterfaces()
                where i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMapFrom<>)) &&
                      !t.IsAbstract &&
                      !t.IsInterface
                select new
                {
                    Source = i.GetGenericArguments()[0],
                    Destination = t
                }).ToArray();

            foreach (var map in maps)
            {
                CreateMap(map.Source, map.Destination).MaxDepth(3);
                CreateMap(map.Destination, map.Source).MaxDepth(3);
#if Debug
                Debug.WriteLine(map.Source + "   " + map.Destination);
#endif
            }
        }

        private void LoadCustomMappings()
        {
        }
    }
}