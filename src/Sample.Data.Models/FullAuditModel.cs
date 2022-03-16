using Sample.Data.Models.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Data.Models
{
    public abstract class FullAuditModel : IIdentityModel, IAuditedModel, IActivatableModel, ISoftDeletable
    {
        [Key]
        public int Id { get; set; }
        [StringLength(ModelConstants.MAX_USERID_LENGTH)]
        [Column(TypeName = "VARCHAR")]
        public string? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(ModelConstants.MAX_USERID_LENGTH)]
        [Column(TypeName = "VARCHAR")]
        public string? LastModifiedUserId { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
