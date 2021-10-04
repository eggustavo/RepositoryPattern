using EGG.RepositoryPattern.Domain.Entities.Base;
using EGG.RepositoryPattern.Domain.Interfaces.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EGG.RepositoryPattern.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity, TId>
        where TEntity : EntityBase<TId>, IAggregateRoot
    {
        #region List

        IEnumerable<TEntity> List(bool tracking);
        IEnumerable<TEntity> List(Func<TEntity, object> order, bool ascending, bool tracking);
        IEnumerable<TEntity> List(Func<TEntity, object> order, bool ascending, bool tracking, params Expression<Func<TEntity, object>>[] navigationProperties);
        IEnumerable<TEntity> List(Func<TEntity, object> order, bool ascending, bool tracking, params string[] navigationProperties);
        IEnumerable<TEntity> List(bool tracking, params Expression<Func<TEntity, object>>[] navigationProperties);
        IEnumerable<TEntity> List(bool tracking, params string[] navigationProperties);

        #endregion

        #region Get By Id

        TEntity Get(TId id, bool tracking);
        TEntity Get(TId id, bool tracking, params Expression<Func<TEntity, object>>[] navigationProperties);
        TEntity Get(TId id, bool tracking, params string[] navigationProperties);

        #endregion

        #region Get By...

        TEntity Get(Func<TEntity, bool> where, bool tracking);
        TEntity Get(Func<TEntity, bool> where, bool tracking, params Expression<Func<TEntity, object>>[] navigationProperties);
        TEntity Get(Func<TEntity, bool> where, bool tracking, params string[] navigationProperties);

        #endregion

        #region Verification

        bool IsExists(Func<TEntity, bool> where);

        #endregion

        #region Crud Operation

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        #endregion
    }
}
