using Microsoft.EntityFrameworkCore;

namespace ITDocumentation.Data
{

    public class ApplicationDbContext : DbContext
    {

        public DbSet<Department> Department { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<ProjectPage> ProjectPage { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Subdepartment> Subdepartment { get; set; }
        public DbSet<SinglePage> SinglePage { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<PendingDocument> PendingDocument { get; set; }
        public DbSet<DocumentApproval> DocumentApproval { get; set; }
        public DbSet<UserSubdepartment> UserSubdepartment { get; set; }
        public DbSet<Downtime> Downtime { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Server> Server { get; set; }
        public DbSet<Database> Databases { get; set; }

        public DbSet<SYM> SYMS { get; set; }

        public ApplicationDbContext()
        {


        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    }
}