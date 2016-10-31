using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class PlatformType : BaseModel<int>
    {
        public PlatformType()
        {
            Games = new HashSet<Game>();
        }

        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Type { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}