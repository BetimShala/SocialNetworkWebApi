﻿using SocialNetwork.Data;
using SocialNetwork.Helpers.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialNetwork.Helpers.Implementation
{
    public abstract class CrudService<T> : ContextBase<T>, ICrudService<T> where T : class, new()
    {
        public CrudService(DataContext context) : base(context)
        {
        }

        public void Add(T entity)
        {
            _ctx.Add(entity);
            SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _ctx.AddRange(entities);
        }

        public T Get(int id)
        {
            return _ctx.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _ctx.ToList();
        }

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return _ctx.Where(predicate);
        }

        public void Remove(T entity)
        {
            _ctx.Remove(entity);
            SaveChanges();
        }

        public void Update(T entity)
        {
            _ctx.Update(entity);
            SaveChanges();
        }
    }
}
