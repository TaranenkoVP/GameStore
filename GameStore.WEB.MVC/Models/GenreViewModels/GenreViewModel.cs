using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Entities;
using GameStore.WEB.Infrastructure.Mappers;

namespace GameStore.WEB.MVC.Models.GenreViewModels
{
    public class GenreViewModel : IMapFrom<GenreBll>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(450, ErrorMessage = "The Name must be between 3 and 450 symbols!", MinimumLength = 3)]
        public string Name { get; set; }

        public int? ParentGenreId { get; set; }
    }
}