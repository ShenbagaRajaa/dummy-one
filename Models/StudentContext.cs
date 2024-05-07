
using Microsoft.EntityFrameworkCore;

namespace dummy.Models;

public class StudentContext:DbContext
{
    public StudentContext(DbContextOptions<StudentContext> options):base(options){

    }
    public DbSet<StudentShenbaORM> StudentShenbaORM{get;set;}
}
