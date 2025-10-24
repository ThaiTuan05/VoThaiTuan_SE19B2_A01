using System.Linq.Expressions;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly InMemoryDatabase _database;
        protected readonly List<T> _dbSet;

        public Repository(InMemoryDatabase database)
        {
            _database = database;
            _dbSet = GetDbSet();
        }

        private List<T> GetDbSet()
        {
            return typeof(T).Name switch
            {
                nameof(Models.Customer) => (List<T>)(object)_database.Customers,
                nameof(Models.RoomType) => (List<T>)(object)_database.RoomTypes,
                nameof(Models.RoomInformation) => (List<T>)(object)_database.Rooms,
                nameof(Models.BookingReservation) => (List<T>)(object)_database.Bookings,
                nameof(Models.BookingDetail) => (List<T>)(object)_database.BookingDetails,
                _ => throw new NotSupportedException($"Type {typeof(T).Name} is not supported")
            };
        }

        // Read operations
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.FirstOrDefault(GetIdPredicate(id));
        }

        private Func<T, bool> GetIdPredicate(int id)
        {
            return typeof(T).Name switch
            {
                nameof(Models.Customer) => (T entity) => ((Models.Customer)(object)entity).CustomerID == id,
                nameof(Models.RoomType) => (T entity) => ((Models.RoomType)(object)entity).RoomTypeID == id,
                nameof(Models.RoomInformation) => (T entity) => ((Models.RoomInformation)(object)entity).RoomID == id,
                nameof(Models.BookingReservation) => (T entity) => ((Models.BookingReservation)(object)entity).BookingReservationID == id,
                nameof(Models.BookingDetail) => (T entity) => ((Models.BookingDetail)(object)entity).BookingReservationID == id,
                _ => throw new NotSupportedException($"Type {typeof(T).Name} is not supported")
            };
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate.Compile()).ToList();
        }

        public virtual T? FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate.Compile());
        }

        public virtual int Count()
        {
            return _dbSet.Count;
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Count(predicate.Compile());
        }

        public virtual bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate.Compile());
        }

        // Create operations
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        // Update operations
        public virtual void Update(T entity)
        {
            var index = _dbSet.FindIndex(e => GetIdPredicate(GetEntityId(entity))(e));
            if (index >= 0)
            {
                _dbSet[index] = entity;
            }
        }

        private int GetEntityId(T entity)
        {
            return typeof(T).Name switch
            {
                nameof(Models.Customer) => ((Models.Customer)(object)entity).CustomerID,
                nameof(Models.RoomType) => ((Models.RoomType)(object)entity).RoomTypeID,
                nameof(Models.RoomInformation) => ((Models.RoomInformation)(object)entity).RoomID,
                nameof(Models.BookingReservation) => ((Models.BookingReservation)(object)entity).BookingReservationID,
                nameof(Models.BookingDetail) => ((Models.BookingDetail)(object)entity).BookingReservationID,
                _ => throw new NotSupportedException($"Type {typeof(T).Name} is not supported")
            };
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        // Delete operations
        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbSet.Remove(entity);
            }
        }

        public virtual void RemoveById(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        // Save operations (no-op for in-memory database)
        public virtual async Task<int> SaveChangesAsync()
        {
            await Task.CompletedTask;
            return 1; // Simulate successful save
        }

        public virtual int SaveChanges()
        {
            return 1; // Simulate successful save
        }
    }
}
