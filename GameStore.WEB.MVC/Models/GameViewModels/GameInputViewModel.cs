using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Entities;
using GameStore.WEB.Infrastructure.Mappers;
using GameStore.WEB.MVC.Models.GenreViewModels;
using GameStore.WEB.MVC.Models.PlatformTypeViewModels;

namespace GameStore.WEB.MVC.Models.GameViewModels
{
    public class GameInputViewModel : IMapFrom<GameBll>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Key")]
        [StringLength(450, ErrorMessage = "The Key must be between 3 and 450 symbols!", MinimumLength = 3)]
        public string Key { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(450, ErrorMessage = "The Name must be between 3 and 450 symbols!", MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(450)]
        public string Description { get; set; }

        [StringLength(450)]
        public string DownloadPath { get; set; }

        public ICollection<GenreViewModel> Genres { get; set; }

        public ICollection<PlatformTypeViewModel> PlatformTypes { get; set; }
    }
}