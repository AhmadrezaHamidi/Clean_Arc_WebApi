using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AhFramWork.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
  

}
