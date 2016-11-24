using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Models
{
    public class Publisher : BaseModel<int>
    {
        [Index(IsUnique = true)]
        [StringLength(40)]
        [Column(TypeName = "nvarchar")]
        public string CompanyName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string HomePage { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}