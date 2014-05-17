using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ExposeAssociationTable
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }

    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }

    //public class UserRole
    //{
    //    public Guid UserId { get; set; }
    //    public Guid RoleId { get; set; }
    //    public User User { get; set; }
    //    public Role Role { get; set; }
    //}

    public class MyContext : DbContext //not I have a web.config connection string pointing to localhost
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserRole>().HasKey(u => new { u.RoleId, u.UserId });
            modelBuilder.Entity<User>().HasMany(x => x.Roles).WithMany(x => x.Users).Map(m =>
            {
                m.ToTable("UserRoles");
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
            });
           

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; } 
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
