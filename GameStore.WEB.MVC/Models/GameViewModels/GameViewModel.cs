using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Entities;
using GameStore.WEB.Infrastructure.Mappers;

namespace GameStore.WEB.MVC.Models.GameViewModels
{
    public class GameViewModel : IMapFrom<GameBll>
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
    }
}