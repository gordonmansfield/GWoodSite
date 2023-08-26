using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GW.DataAccess.Data;
using GW.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GW.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> contextSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.contextSet = _context.Set<T>();
            _context.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            contextSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            contextSet.RemoveRange(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = contextSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
                _context.Products.Include(u => u.Category).Include(u => u.CategoryId);
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = contextSet;
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
                _context.Products.Include(u => u.Category).Include(u => u.CategoryId);
            }
            return query.ToList();
        }
    }
}
