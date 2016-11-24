using System.ComponentModel.DataAnnotations;
using GameStore.BLL.Entities;
using GameStore.WEB.Infrastructure.Mappers;

namespace GameStore.WEB.MVC.Models.CommentViewModels
{
    public class CommentViewModel : IMapFrom<CommentBll>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(450, ErrorMessage = "The name must be between 3 and 450 symbols!", MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(450, ErrorMessage = "The body must be between 3 and 450 symbols!", MinimumLength = 3)]
        public string Body { get; set; }

        [Required]
        public int GameId { get; set; }

        public int? AuthorId { get; set; }
    }
}