using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;
using System.Collections.Generic;

namespace Payment_Api.Context
{
    public class VendaContext : DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> options) : base(options)
        {

        }

        public DbSet<Venda> Produtos { get; set; }
    }
}
