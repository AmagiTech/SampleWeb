﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sample.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Data.Models
{
    [Table("ItemGenres")]
    [Index(nameof(ItemId), nameof(GenreId), IsUnique = true)]
    public class ItemGenre : IIdentityModel
    {
        public int Id { get; set; }

        public virtual int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public virtual int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
