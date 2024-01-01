
using Microsoft.EntityFrameworkCore;
using ReactWeb.Models;

namespace ReactWeb.Models
{
    public class StudentInfoDetailContext : DbContext
    {
        public StudentInfoDetailContext(DbContextOptions<StudentInfoDetailContext> options) : base(options)
        {

        }
        public DbSet<StudentInfoDetail> StudentInfoDetail { get; set; }
        

    }

}
