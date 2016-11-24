using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Models
{
    public class Genre : BaseModel<int>
    {
        [Index(IsUnique = true)]
        [StringLength(450, MinimumLength = 3)]
        public string Name { get; set; }

        public int? ParentGenreId { get; set; }

        public virtual Genre ParentGenre { get; set; }

        public virtual ICollection<Genre> SubGenres { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}