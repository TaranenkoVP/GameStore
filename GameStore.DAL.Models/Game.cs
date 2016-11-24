using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Models
{
    public class Game : BaseModel<int>
    {
        [Index(IsUnique = true)]
        [StringLength(450, MinimumLength = 3)]
        public string Key { get; set; }

        [Required]
        [StringLength(450, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(450)]
        public string Description { get; set; }

        [StringLength(450)]
        public string DownloadPath { get; set; }

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public int? PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<PlatformType> PlatformTypes { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}