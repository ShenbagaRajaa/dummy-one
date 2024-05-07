using Microsoft.EntityFrameworkCore;

namespace dummy;

public class DepartmentContext:DbContext
{
    public DepartmentContext(DbContextOptions<DepartmentContext> option):base(option){

    }
    public DbSet<DepartmentsShenba> DepartmentsShenba {get;set;}
}
