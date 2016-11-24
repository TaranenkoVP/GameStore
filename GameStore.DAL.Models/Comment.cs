using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.DAL.Models
{
    public class Comment : BaseModel<int>
    {
        [Required]
        [StringLength(450, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(450, MinimumLength = 3)]
        public string Body { get; set; }

        [Required]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int? AuthorId { get; set; }

        public virtual Comment Author { get; set; }

        public virtual ICollection<Comment> ChildComments { get; set; }
    }
}