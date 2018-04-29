using System.Data.Entity;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FailTracker.Web.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FailTracker.Web.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
	    public ApplicationDbContext()
	        : base("DefaultConnection")
	    {
	        this.Database.Log = s => Debug.WriteLine(s);
	        counter++;
	        x = counter;
	    }

	    protected override void Dispose(bool disposing)
	    {
	        base.Dispose(disposing);
	        counter--;
	    }

	   public static int counter = 0;
	    public int x;
        public DbSet<Issue> Issues { get; set; }
		public DbSet<LogAction> Logs { get; set; }
	    protected override void OnModelCreating(DbModelBuilder modelBuilder)
	    {
	        modelBuilder.Entity<ApplicationUser>()
	            .HasMany(u => u.Assignments).WithRequired(i => i.AssignedTo);

	        base.OnModelCreating(modelBuilder);
	    }
    }
   
}