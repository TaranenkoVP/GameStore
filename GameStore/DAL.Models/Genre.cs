using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Genre : BaseModel<int>
    {
        public Genre()
        {
            SubGenres = new HashSet<Genre>();
            Games = new HashSet<Game>();
        }

        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public int? ParentGenreId { get; set; }

        public virtual Genre ParentGenre { get; set; }

        public virtual ICollection<Genre> SubGenres { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}