using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberBreakfast.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Context
{
    public class BreakfastContext : DbContext
    {
        public DbSet<BreakfastEntity> Breakfasts {get; set;}

        public BreakfastContext(DbContextOptions<BreakfastContext> options): base(options)
        {
        }
    }
}