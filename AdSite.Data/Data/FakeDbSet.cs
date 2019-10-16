using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdSite.Data.Data
{
    public class FakeDbSet<TEntity> : IDbSet<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// Static constructor.  Determines the which properties are key properties
        /// </summary>
        static FakeDbSet()
        {
            var type = typeof(TEntity);

            foreach (var property in type
                .GetProperties()
                .Where(v => v.GetCustomAttributes(false).OfType<KeyAttribute>().Any()))
            {
                keys.Add(property);
            }
        }

        /// <summary>
        /// Contains PropertyInfo objects for each of the key properties
        /// </summary>
        private readonly static List<PropertyInfo> keys = new List<PropertyInfo>();

        /// <summary>
        /// The data we will query against in a List object
        /// </summary>
        private IList<TEntity> _data;

        /// <summary>
        /// The data we will query against in a IQueryable object
        /// </summary>
        private IQueryable<TEntity> _queryable;

        /// <summary>
        /// A dictionary to look up the current status of an object
        /// </summary>
        private Dictionary<TEntity, EntityStatus> _entityStatus =
            new Dictionary<TEntity, EntityStatus>();

        /// <summary>
        /// This is the query provider for our FakeDbSet
        /// </summary>
        private IQueryProvider _provider;

        /// <summary>
        /// Observable collection of data
        /// </summary>
        private ObservableCollection<TEntity> _local;

        /// <summary>
        /// List of logged activities
        /// </summary>
        private List<LogItem> _loggerData = new List<LogItem>();

        /// <summary>
        /// Type for logging actions
        /// </summary>
        public class LogItem
        {
            public string Identifier { get; set; }
            public Expression Expression { get; set; }
        }

        /// <summary>
        /// Constructor.  Expects an IList of entity type
        /// that becomes the data store
        /// </summary>
        /// <param name="data"></param>
        public FakeDbSet(IList<TEntity> data)
        {
            _data = data;
            _entityStatus.Clear();
            foreach (var item in data)
            {
                _entityStatus[item] = EntityStatus.Normal;
            }
            _queryable = data.AsQueryable();

            // The fake provider wraps the real provider (for "List<TEntity")
            // so that it can log activities
            _provider = new FakeDbSetProvider(_queryable.Provider, (u, v) => Logger(u, v));

            _local = new ObservableCollection<TEntity>(data);
        }

        /// <summary>
        /// Logger function that is passed to the Fake DbSet Provider
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="expression"></param>
        private void Logger(string identifier, Expression expression)
        {
            _loggerData.Add(new LogItem
            {
                Identifier = identifier,
                Expression = expression
            });
        }

        /// <summary>
        /// Expose the logged data
        /// </summary>
        public IList<LogItem> LoggedData { get { return _loggerData; } }

        /// <summary>
        /// Implements that "Add" function of IdbSet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Add(TEntity entity)
        {
            _data.Add(entity);
            _entityStatus[entity] = EntityStatus.Added;
            return entity;
        }

        /// <summary>
        /// Implements the Attach function of IdbSet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Attach(TEntity entity)
        {
            return entity;
        }

        /// <summary>
        /// Doesn't implement the Create Derived Entity function of IdbSet
        /// </summary>
        /// <typeparam name="TDerivedEntity"></typeparam>
        /// <returns></returns>
        public TDerivedEntity Create<TDerivedEntity>()
            where TDerivedEntity : class, TEntity
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements the Create Function of IdbSet
        /// </summary>
        /// <returns></returns>
        public TEntity Create()
        {
            return new TEntity();
        }

        /// <summary>
        /// Implements the Find function of IdbSet.
        /// Depends on the keys collection being
        /// set to the key types of this entity
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public TEntity Find(params object[] keyValues)
        {
            if (keyValues.Length != keys.Count)
            {
                throw new ArgumentException(
                    string.Format("Must supply {0} key values", keys.Count),
                    "keyValues"
                    );
            }

            var query = _queryable;

            var parameterExpression = Expression.Parameter(typeof(TEntity), "v");

            for (int i = 0; i < keys.Count; i++)
            {
                var equalsExpression = Expression.Equal(
                    // key property
                    Expression.Property(parameterExpression, keys[i]),
                    // key value
                    Expression.Constant(keyValues[i], keys[i].PropertyType)
                    );

                var whereClause = (Expression<Func<TEntity, bool>>)Expression.Lambda(
                    equalsExpression,
                    new ParameterExpression[] { parameterExpression }
                    );

                query = query.Where(whereClause);
            }

            var result = query.ToList();

            return result.SingleOrDefault();
        }

        /// <summary>
        /// Local observable collection
        /// </summary>
        public ObservableCollection<TEntity> Local
        {
            get { return _local; }
        }

        /// <summary>
        /// Implements the Remove function of IDbSet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Remove(TEntity entity)
        {
            _data.Remove(entity);
            _entityStatus[entity] = EntityStatus.Deleted;
            return entity;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _queryable.GetEnumerator();
        }

        public Type ElementType
        {
            get { return _queryable.ElementType; }
        }

        public Expression Expression
        {
            get { return _queryable.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return _queryable.Provider; }
        }

        public enum EntityStatus
        {
            None,
            Added,
            Deleted,
            Normal
        }

        /// <summary>
        /// Wraps the passed-in IQueryProvider with a Logging call so we can observe activities
        /// </summary>
        public class FakeDbSetProvider : IQueryProvider
        {
            private Action<string, Expression> _logger;
            private IQueryProvider _provider;

            public FakeDbSetProvider(IQueryProvider provider, Action<string, Expression> logger)
            {
                _logger = logger;
                _provider = provider;
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                _logger("CreateQuery", expression);
                return _provider.CreateQuery<TElement>(expression);
            }

            public IQueryable CreateQuery(Expression expression)
            {
                _logger("CreateQuery", expression);
                return _provider.CreateQuery(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                _logger("Execute", expression);
                return _provider.Execute<TResult>(expression);
            }

            public object Execute(Expression expression)
            {
                _logger("Execute", expression);
                return _provider.Execute(expression);
            }
        }
    }
}