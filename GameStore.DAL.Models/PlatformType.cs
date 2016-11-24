using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Models
{
    public class PlatformType : BaseModel<int>
    {
        [Index(IsUnique = true)]
        [StringLength(450, MinimumLength = 3)]
        public string Type { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}