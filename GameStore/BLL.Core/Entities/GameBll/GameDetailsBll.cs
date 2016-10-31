using System.Collections.Generic;
using AutoMapper;
using Bll.Core.Entities;
using Bll.Core.Entities.Common;
using BLL.Infrastructure.Mappers;
using DAL.Models;

namespace BLL.Core.Entities.GameBll
{
    public class GameDetailsBll : BaseModelBll<int>, IMapFrom<Game>, IHaveCustomMappings
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DownloadPath { get; set; }

        public ICollection<GenreBll> Genres { get; set; }

        public ICollection<PlatformTypeBll> PlatformTypes { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<GameDetailsBll, Game>()
                .ForMember(r => r.Genres, opts => opts.Ignore())
                .ForMember(r => r.PlatformTypes, opts => opts.Ignore());
        }
    }
}