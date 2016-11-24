using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.WEB.Infrastructure.Mappers;
using GameStore.WEB.MVC.Models.GenreViewModels;
using GameStore.WEB.MVC.Models.PlatformTypeViewModels;

namespace GameStore.WEB.MVC.Models.GameViewModels
{
    public class GameDetailsViewModel : IMapFrom<GameBll>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(450, MinimumLength = 3)]
        public string Key { get; set; }

        [Required]
        [StringLength(450, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(450)]
        public string Description { get; set; }

        [StringLength(450)]
        public string DownloadPath { get; set; }

        public ICollection<GenreViewModel> Genres { get; set; }

        public ICollection<PlatformTypeViewModel> PlatformTypes { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<GameDetailsViewModel, GameBll>()
                .ForMember(r => r.Genres, opts => opts.Ignore())
                .ForMember(r => r.PlatformTypes, opts => opts.Ignore());
        }
    }
}