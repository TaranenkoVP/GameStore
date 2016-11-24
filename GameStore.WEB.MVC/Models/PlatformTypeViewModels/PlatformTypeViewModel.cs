using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Entities;
using GameStore.WEB.Infrastructure.Mappers;

namespace GameStore.WEB.MVC.Models.PlatformTypeViewModels
{
    public class PlatformTypeViewModel : IMapFrom<PlatformTypeBll>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Type")]
        [StringLength(450, ErrorMessage = "The Type must be between 3 and 450 symbols!", MinimumLength = 3)]
        public string Type { get; set; }
    }
}