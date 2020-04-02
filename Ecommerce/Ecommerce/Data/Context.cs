using Ecommerce.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Tables
{
    public abstract class Context : DbContext
    {
        protected readonly IConfiguration configuration;

        public Context(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleType> ArticleTypes { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<LotHistory> LotHistories { get; set; }
        public virtual DbSet<UserArticle> UserArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ArticleType>().HasData(
                new ArticleType { Name = "CPU", ArticleTypeId = 1 },
                new ArticleType { Name = "Teclado", ArticleTypeId = 2 },
                new ArticleType { Name = "Monitor", ArticleTypeId = 3 },
                new ArticleType { Name = "Notebook", ArticleTypeId = 4 },
                new ArticleType { Name = "Mouse", ArticleTypeId = 5 }
                );
        }
    }
}
