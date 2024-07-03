using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loading_Related_Data_Eager_Loading.Configurations
{
    public class EmployeeData : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(new Employee[]
            {
                new(){ Id = 1, RegionId=1, Name= "Alperen", Surname="Güneş", Salary= 1500,},
                new(){ Id = 2, RegionId=2,Name= "Mehmet", Surname="Yılmaz", Salary= 2000},
                new(){ Id = 3, RegionId=3,Name= "Ayşe", Surname="Kara", Salary= 2500},
                new(){ Id = 4, RegionId=4,Name= "Fatma", Surname="Kara", Salary= 3000},

            });
        }
    }
    public class OrderData : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(new Order[]
            {
                new(){ Id = 1, OrderDate= DateTime.Now, EmployeeId= 1},
                new(){ Id = 2, OrderDate= DateTime.Now, EmployeeId= 1},
                new(){ Id = 3, OrderDate= DateTime.Now, EmployeeId= 2},
                new(){ Id = 4, OrderDate= DateTime.Now, EmployeeId= 2},
                new(){ Id = 5, OrderDate= DateTime.Now, EmployeeId= 3},
                new(){ Id = 6, OrderDate= DateTime.Now, EmployeeId= 3},
                new(){ Id = 7, OrderDate= DateTime.Now, EmployeeId= 4},
                new(){ Id = 8, OrderDate= DateTime.Now, EmployeeId= 4},
            });
        }
    }

    public class RegionData: IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasData(new Region[]
            {
                new(){ Id = 1, Name= "Marmara"},
                new(){ Id = 2, Name= "Ege"},
                new(){ Id = 3, Name= "Karadeniz"},
                new(){ Id = 4, Name= "Akdeniz"},
            });
        }
    }
}
