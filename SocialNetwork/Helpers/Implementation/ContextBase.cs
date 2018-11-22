using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Helpers.Implementation
{
    public abstract class ContextBase<T> where T : class, new()
    {
        protected readonly DataContext _context;
        public ContextBase(DataContext context)
        {
            _context = context;
        }
        
        protected DbSet<T> _ctx => _context.Set<T>();

        protected void SaveChanges() => _context.SaveChanges();
    }
}
