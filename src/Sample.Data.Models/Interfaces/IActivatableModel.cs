using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.Models.Interfaces
{
    public interface IActivatableModel
    {
        public bool IsActive { get; set; }
    }
}
