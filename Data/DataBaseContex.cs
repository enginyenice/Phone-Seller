using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelefonSatis.Models;

namespace TelefonSatis.Data
{
    public class DataBaseContex : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<User> Users { get; set; }

        public DataBaseContex(DbContextOptions<DataBaseContex> options) :base(options)
        {

        }
    }
}
