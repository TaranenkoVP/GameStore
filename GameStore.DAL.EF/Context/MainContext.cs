﻿using System.Data.Entity;
using GameStore.DAL.Models;

namespace GameStore.DAL.EF.Context
{
    public class MainContext : DbContext, IContext
    {
        public MainContext() : base("DefaultConnection")
        {
//#if DEBUG
//            Database.Log = e => { Debug.WriteLine(e); };
//#endif
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MainContextInitializer());
        }

        public virtual IDbSet<Game> Game { get; set; }
        public virtual IDbSet<Comment> Comment { get; set; }
        public virtual IDbSet<Genre> Genre { get; set; }
        public virtual IDbSet<PlatformType> PlatformType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Configurations.Add(new GameConfiguration());

            //modelBuilder.Entity<Genre>()
            //    .Property(x => x.Name)
            //    .IsRequired()
            //    .HasMaxLength(450)
            //    .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));

            //modelBuilder.Entity<PlatformType>()
            //    .Property(x => x.Type)
            //    .IsRequired()
            //    .HasMaxLength(450)
            //    .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));


            //modelBuilder.Entity<Game>()
            //    .HasKey(a => a.Key);

            //modelBuilder.Entity<Game>()
            //    .Property(a => a.Key)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //modelBuilder.Entity<Game>()
            //    .HasMany(p => p.Genres)
            //    .WithMany(c => c.Games);

            //modelBuilder.Entity<Game>()
            //    .HasMany(p => p.PlatformTypes)
            //    .WithMany(c => c.Games);

            //modelBuilder.Entity<Game>()
            //    .HasMany(p => p.Comments)
            //    .WithRequired(p => p.Game);

            //modelBuilder.Entity<Game>()
            //    .HasMany(p => p.Comments);

            //modelBuilder.Entity<Comment>()
            //    .HasKey(a => a.Id);

            //modelBuilder.Entity<Genre>()
            //    .HasKey(a => a.Name);

            //modelBuilder.Entity<PlatformType>()
            //    .HasKey(a => a.Type);
        }
    }
}