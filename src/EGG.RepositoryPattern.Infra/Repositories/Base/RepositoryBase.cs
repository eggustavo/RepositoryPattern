using EGG.RepositoryPattern.Domain.Entities.Base;
using EGG.RepositoryPattern.Domain.Interfaces;
using EGG.RepositoryPattern.Domain.Interfaces.Annotations;
using EGG.RepositoryPattern.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EGG.RepositoryPattern.Infra.Repositories.Base
{
    public class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
        where TEntity : EntityBase<TId>, IAggregateRoot
    {
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(EGGContext context)
        {
            DbSet = context.Set<TEntity>();
        }

        #region List

        public IEnumerable<TEntity> List(bool tracking)
        {
            return tracking
                ? DbSet.AsEnumerable()
                : DbSet.AsNoTracking().AsEnumerable();
        }

        public IEnumerable<TEntity> List(Func<TEntity, object> order, bool ascending, bool tracking)
        {
            return tracking
                ? ascending
                    ? DbSet.OrderBy(order).AsEnumerable()
                    : DbSet.OrderByDescending(order).AsEnumerable()
                : ascending
                    ? DbSet.AsNoTracking().OrderBy(order).AsEnumerable()
                    : DbSet.AsNoTracking().OrderByDescending(order).AsEnumerable();
        }

        public IEnumerable<TEntity> List(Func<TEntity, object> order, bool ascending, bool tracking, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            if (navigationProperties.Any())
                return tracking
                    ? ascending
                        ? Include(DbSet, navigationProperties).OrderBy(order).AsEnumerable()
                        : Include(DbSet, navigationProperties).OrderByDescending(order).AsEnumerable()
                    : ascending
                        ? Include(DbSet, navigationProperties).AsNoTracking().OrderBy(order).AsEnumerable()
                        : Include(DbSet, navigationProperties).AsNoTracking().OrderByDescending(order).AsEnumerable();

            return List(order, ascending, tracking);
        }

        public IEnumerable<TEntity> List(Func<TEntity, object> order, bool ascending, bool tracking, params string[] navigationProperties)
        {
            if (navigationProperties.Any())
                return tracking
                    ? ascending
                        ? Include(DbSet, navigationProperties).OrderBy(order).AsEnumerable()
                        : Include(DbSet, navigationProperties).OrderByDescending(order).AsEnumerable()
                    : ascending
                        ? Include(DbSet, navigationProperties).AsNoTracking().OrderBy(order).AsEnumerable()
                        : Include(DbSet, navigationProperties).AsNoTracking().OrderByDescending(order).AsEnumerable();

            return List(order, ascending, tracking);
        }

        public IEnumerable<TEntity> List(bool tracking, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            if (navigationProperties.Any())
                return tracking
                    ? Include(DbSet, navigationProperties).AsEnumerable()
                    : Include(DbSet, navigationProperties).AsEnumerable();

            return List(tracking);
        }

        public IEnumerable<TEntity> List(bool tracking, params string[] navigationProperties)
        {
            if (navigationProperties.Any())
                return tracking
                    ? Include(DbSet, navigationProperties).AsEnumerable()
                    : Include(DbSet, navigationProperties).AsEnumerable();

            return List(tracking);
        }

        #endregion

        #region Get By Id

        public TEntity Get(TId id, bool tracking)
        {
            return tracking
                ? DbSet.Find(id)
                : DbSet.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
        }

        public TEntity Get(TId id, bool tracking, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            if (navigationProperties.Any())
                return tracking
                    ? Include(DbSet, navigationProperties).FirstOrDefault(p => p.Id.Equals(id))
                    : Include(DbSet, navigationProperties).FirstOrDefault(p => p.Id.Equals(id));

            return Get(id, tracking);
        }

        public TEntity Get(TId id, bool tracking, params string[] navigationProperties)
        {
            if (navigationProperties.Any())
                return tracking
                    ? Include(DbSet, navigationProperties).FirstOrDefault(p => p.Id.Equals(id))
                    : Include(DbSet, navigationProperties).FirstOrDefault(p => p.Id.Equals(id));

            return Get(id, tracking);
        }

        #endregion

        #region Get By...

        public TEntity Get(Func<TEntity, bool> where, bool tracking)
        {
            return tracking
                ? DbSet.FirstOrDefault(where)
                : DbSet.AsNoTracking().FirstOrDefault(where);
        }

        public TEntity Get(Func<TEntity, bool> where, bool tracking, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            if (navigationProperties.Any())
                return tracking
                    ? Include(DbSet, navigationProperties).FirstOrDefault(where)
                    : Include(DbSet, navigationProperties).FirstOrDefault(where);

            return Get(where, tracking);
        }

        public TEntity Get(Func<TEntity, bool> where, bool tracking, params string[] navigationProperties)
        {
            if (navigationProperties.Any())
                return tracking
                    ? Include(DbSet, navigationProperties).FirstOrDefault(where)
                    : Include(DbSet, navigationProperties).FirstOrDefault(where);

            return Get(where, tracking);
        }

        #endregion

        #region Verification

        public bool IsExists(Func<TEntity, bool> where)
        {
            return DbSet.Any(where);
        }

        #endregion

        #region Crud Operation

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        #endregion

        #region Navigation Properties

        private IQueryable<TEntity> Include(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            foreach (var property in navigationProperties)
                query = query.Include(property);

            return query;
        }

        private IQueryable<TEntity> Include(IQueryable<TEntity> query, params string[] navigationProperties)
        {
            foreach (var property in navigationProperties)
                query = query.Include(property);

            return query;
        }

        #endregion
    }
}
