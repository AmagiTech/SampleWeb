using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.Models.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public CategoryDetailDto CategoryDetail { get; set; }

    }
}
