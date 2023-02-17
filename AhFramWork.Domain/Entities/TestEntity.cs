using AhFramWork.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AhFramWork.Domain.Entities;

namespace AhFramWork.Domain.Entities
{
    public class TestEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public TestEntity(string name, int age)
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            CreatedAt = DateTime.Now;
            LastUpdate = null;
            Name = name;
            Age = age;
        }
    }
   
}
