﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Data.Models
{
    public class Item : FullAuditModel
    {
        [StringLength(ModelConstants.MAX_NAME_LENGTH)]
        [Required]
        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
        [Range(ModelConstants.MINIMUM_QUANTITY, ModelConstants.MAXIMUM_QUANTITY)]
        public int Quantity { get; set; }
        [StringLength(ModelConstants.MAX_DESCRIPTION_LENGTH)]
        public string Description { get; set; }
        [StringLength(ModelConstants.MAX_NOTES_LENGTH, MinimumLength = 10)]
        public string Notes { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime? PurchasedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        [Range(ModelConstants.MINIMUM_PRICE, ModelConstants.MAXIMUM_PRICE)]
        public decimal? PurchasePrice { get; set; }
        [Range(ModelConstants.MINIMUM_PRICE, ModelConstants.MAXIMUM_PRICE)]
        public decimal? CurrentOrFinalPrice { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Player> Players { get; set; } = new List<Player>();
        public virtual List<ItemGenre> ItemGenres { get; set; } = new List<ItemGenre>();
    }
}
