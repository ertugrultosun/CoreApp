using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreApp.Models
{
    public partial class MainContext : DbContext
    {
        public MainContext()
        {
        }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdditionList> AdditionList { get; set; }
        public virtual DbSet<Additions> Additions { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<BankAccount> BankAccount { get; set; }
        public virtual DbSet<Credentials> Credentials { get; set; }
        public virtual DbSet<CredentialTypes> CredentialTypes { get; set; }
        public virtual DbSet<CurActivity> CurActivity { get; set; }
        public virtual DbSet<FinActivity> FinActivity { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Notebook> Notebook { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<RolePermissions> RolePermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserList> UserList { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=D:\\Projects\\CoreApp\\CoreApp\\CoreApp.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdditionList>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Addid)
                    .HasColumnName("addid")
                    .HasColumnType("int");

                entity.Property(e => e.Cuserid)
                    .HasColumnName("cuserid")
                    .HasColumnType("int");

                entity.Property(e => e.Noteid)
                    .HasColumnName("noteid")
                    .HasColumnType("int");

                entity.HasOne(d => d.Add)
                    .WithMany(p => p.AdditionList)
                    .HasForeignKey(d => d.Addid);

                entity.HasOne(d => d.Cuser)
                    .WithMany(p => p.AdditionList)
                    .HasForeignKey(d => d.Cuserid);

                entity.HasOne(d => d.Note)
                    .WithMany(p => p.AdditionList)
                    .HasForeignKey(d => d.Noteid);
            });

            modelBuilder.Entity<Additions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cdate)
                    .HasColumnName("cdate")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Additions)
                    .HasForeignKey(d => d.Userid);
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("nvarchar(350)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("nvarchar(50)");
            });

            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Bankid)
                    .HasColumnName("bankid")
                    .HasColumnType("int");

                entity.Property(e => e.Cdate)
                    .HasColumnName("cdate")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.BankAccount)
                    .HasForeignKey(d => d.Bankid);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BankAccount)
                    .HasForeignKey(d => d.Userid);
            });

            modelBuilder.Entity<Credentials>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Identifier).IsRequired();

                entity.HasOne(d => d.CredentialType)
                    .WithMany(p => p.Credentials)
                    .HasForeignKey(d => d.CredentialTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Credentials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CredentialTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<CurActivity>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Accid)
                    .HasColumnName("accid")
                    .HasColumnType("int");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("float");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Cuserid)
                    .HasColumnName("cuserid")
                    .HasColumnType("int");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Notebookid)
                    .HasColumnName("notebookid")
                    .HasColumnType("int");

                entity.Property(e => e.Parity)
                    .HasColumnName("parity")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("nvarchar(50)");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.CurActivity)
                    .HasForeignKey(d => d.Accid);

                entity.HasOne(d => d.Cuser)
                    .WithMany(p => p.CurActivity)
                    .HasForeignKey(d => d.Cuserid);

                entity.HasOne(d => d.Notebook)
                    .WithMany(p => p.CurActivity)
                    .HasForeignKey(d => d.Notebookid);
            });

            modelBuilder.Entity<FinActivity>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Accid)
                    .HasColumnName("accid")
                    .HasColumnType("int");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("float");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Cuserid)
                    .HasColumnName("cuserid")
                    .HasColumnType("int");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Notebookid)
                    .HasColumnName("notebookid")
                    .HasColumnType("int");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("nvarchar(50)");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.FinActivity)
                    .HasForeignKey(d => d.Accid);

                entity.HasOne(d => d.Cuser)
                    .WithMany(p => p.FinActivity)
                    .HasForeignKey(d => d.Cuserid);

                entity.HasOne(d => d.Notebook)
                    .WithMany(p => p.FinActivity)
                    .HasForeignKey(d => d.Notebookid);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Notebookid)
                    .HasColumnName("notebookid")
                    .HasColumnType("int");

                entity.Property(e => e.Senddate)
                    .HasColumnName("senddate")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int");

                entity.HasOne(d => d.Notebook)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.Notebookid);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.Userid);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cdate)
                    .HasColumnName("cdate")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Ctype)
                    .HasColumnName("ctype")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Cuserid)
                    .HasColumnName("cuserid")
                    .HasColumnType("int");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(350)");

                entity.Property(e => e.Notebookid)
                    .HasColumnName("notebookid")
                    .HasColumnType("int");

                entity.Property(e => e.Tdate)
                    .HasColumnName("tdate")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("nvarchar(50)");

                entity.HasOne(d => d.Cuser)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.Cuserid);

                entity.HasOne(d => d.Notebook)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.Notebookid);
            });

            modelBuilder.Entity<Notebook>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Cdate)
                    .HasColumnName("cdate")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Cuserid)
                    .HasColumnName("cuserid")
                    .HasColumnType("int");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(350)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("nvarchar(50)");

                entity.HasOne(d => d.Cuser)
                    .WithMany(p => p.Notebook)
                    .HasForeignKey(d => d.Cuserid);
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<RolePermissions>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId });

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<UserList>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Joindate)
                    .HasColumnName("joindate")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Notebookid)
                    .HasColumnName("notebookid")
                    .HasColumnType("int");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int");

                entity.HasOne(d => d.Notebook)
                    .WithMany(p => p.UserList)
                    .HasForeignKey(d => d.Notebookid);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserList)
                    .HasForeignKey(d => d.Userid);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Created).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
