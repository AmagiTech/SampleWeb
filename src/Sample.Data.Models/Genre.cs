using System.ComponentModel.DataAnnotations;

namespace Sample.Data.Models
{
    public class Genre : FullAuditModel
    {
        [Required]
        [StringLength(ModelConstants.MAX_NAME_LENGTH)]
        public string Name { get; set; }

        public virtual List<ItemGenre> GenreItems { get; set; } = new List<ItemGenre>();
    }
}
