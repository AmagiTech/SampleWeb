using System.ComponentModel.DataAnnotations;

namespace Sample.Data.Models
{
    public class Player : FullAuditModel
    {
        [Required]
        [StringLength(ModelConstants.MAX_NAME_LENGTH)]
        public string Name { get; set; }

        [StringLength(ModelConstants.MAX_NOTES_LENGTH)]
        public string Description { get; set; }

        public virtual List<Item> Items { get; set; } = new List<Item>();
    }
}
