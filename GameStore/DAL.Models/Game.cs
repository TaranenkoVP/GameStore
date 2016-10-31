using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Game : BaseModel<int>
    {
        public Game()
        {
            Comments = new HashSet<Comment>();
            Genres = new HashSet<Genre>();
            PlatformTypes = new HashSet<PlatformType>();
        }

        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string DownloadPath { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<PlatformType> PlatformTypes { get; set; }
    }
}