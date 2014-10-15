using System;
using System.Linq;

using NHibernate.Linq;

namespace UnitTestFriendlyDal
{
    public interface IDataStore : IDisposable
    {
        IQueryable<T> Query<T>();
        T Get<T>(object id);
        object Save(object transientObject);
    }

    public class DataStore : IDataStore
    {

        NHibernate.ISessionFactory _sessionFactory;
        NHibernate.ISession _session;
        NHibernate.ITransaction _transaction;

        public DataStore(NHibernate.ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }


        IQueryable<T> IDataStore.Query<T>()
        {
            return _session.Query<T>();
        }


        T IDataStore.Get<T>(object id)
        {
            return _session.Get<T>(id);
        }

        void IDisposable.Dispose()
        {
            // Because transaction is a cross-cutting concern
            _transaction.Commit();
            _transaction.Dispose();
            _session.Dispose();
        }

        public object Save(object transientObject)
        {
            return _session.Save(transientObject);
        }

    }



    /// <summary>
    /// cross-cutting concern    
    /// MakeCacheable replaces Cacheable, so IQueryable detection provider can be done here
    /// Can't use NHibernate's built-in .Cacheable on non-NHibernate IQueryable, it will throw an error    
    /// </summary>
    public static class NHibernateLinqExtensionMethods
    {
        public static IQueryable<T> MakeCacheable<T>(this IQueryable<T> query)
        {
            if (query.Provider.GetType() == typeof(NHibernate.Linq.DefaultQueryProvider))
                query = query.Cacheable();

            return query;
        }


    }

}



//public static IQueryable<T> DoFetch<T, TRelated>(
//    this IQueryable<T> query
//    , System.Linq.Expressions.Expression<Func<T,TRelated>> relatedObjectSelector )
//{
//    if (query.Provider.GetType() == typeof(NHibernate.Linq.DefaultQueryProvider))
//        query = query.Fetch(relatedObjectSelector);

//    return query;
//}
