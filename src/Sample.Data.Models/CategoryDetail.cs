using Sample.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Data.Models
{
    public class CategoryDetail : IIdentityModel
    {
        [Key, ForeignKey("Category")]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(ModelConstants.MAX_NAME_LENGTH)]
        public string ColorValue { get; set; }
        [Required]
        [StringLength(ModelConstants.MAX_NAME_LENGTH)]
        public string ColorName { get; set; }

        public virtual Category Category { get; set; }
    }
}
