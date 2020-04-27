using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Domain
{
    public interface IConnectionContext
    {
        Domain.Models.ProductsManagerContext Get();
    }

    public class ConnectionContext : IConnectionContext
    {
        private readonly string _connectionString;
        public ConnectionContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProductsManagerContext Get()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Domain.Models.ProductsManagerContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new Domain.Models.ProductsManagerContext(optionsBuilder.Options);
        }
    }
}
