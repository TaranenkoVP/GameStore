using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Comment : BaseModel<int>
    {
        public string Name { get; set; }

        public string Body { get; set; }

        [Required]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public int? AuthorId { get; set; }

        public virtual Comment Author { get; set; }
    }
}