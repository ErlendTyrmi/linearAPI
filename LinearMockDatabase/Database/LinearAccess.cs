using Common.Interfaces;
using LinearEntities.Entities.BaseEntity;
using System.Text.Json;

namespace LinearMockDatabase.Repo.Database
{
    /// <summary>
    /// General repo class for Linear Entities.
    /// Type arg must implement ILinearEntity.
    /// </summary>
    public class LinearAccess<TType> : ILinearAccess<TType>
    {
        private readonly LinearFileHandler fileHandler;
        readonly string EntityName = typeof(TType).Name;
        private readonly object Writelock = new();

        public LinearAccess(string directoryPathArg)
        {
            if (directoryPathArg == null)
            {
                throw new ArgumentNullException(nameof(directoryPathArg));
            }
            fileHandler = new LinearFileHandler(directoryPathArg);

            // Make sure TType is linear entity
            if (typeof(TType).GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() != typeof(ILinearEntity)))
            {
                throw new ArgumentException($"{nameof(TType)} does not implement {nameof(ILinearEntity)}");
            }
        }

        public TType Create(TType entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            var id = ((ILinearEntity)entity).Id;
            var entities = ReadAllAsDictionary();
            entities ??= new Dictionary<string, TType>();

            if (entities.ContainsKey(id))
            {
                entities[id] = entity;
            }
            else
            {
                setId(entity);
                entities.Add(id, entity);
            }

            var serialized = JsonSerializer.Serialize(entities);
            lock (Writelock)
            {
                fileHandler.Write(EntityName, serialized);
            }

            return entity;
        }

        public IList<TType> CreateList(IList<TType> entities)
        {
            if (entities.Count < 1) throw new ArgumentException("Received an empty list.", nameof(entities));

            var entitiesDict = ReadAllAsDictionary();
            entitiesDict ??= new Dictionary<string, TType>();

            foreach (var entity in entities)
            {

                // Ignore null entities
                if (entity == null) throw new ArgumentNullException(nameof(entities), "Found a null entity in list");

                setId(entity);

                var id = ((ILinearEntity)entity).Id;

                if (entitiesDict.ContainsKey(id))
                {
                    entitiesDict[id] = entity;
                }
                else
                {
                    entitiesDict.Add(id, entity);
                }
            }

            var serialized = JsonSerializer.Serialize(entitiesDict);
            lock (Writelock)
            {
                fileHandler.Write(EntityName, serialized);
            }

            return entities;
        }

        public TType? Read(string id)
        {
            IDictionary<string, TType>? entities = ReadAllAsDictionary();

            if (entities == null) return default;

            foreach (var key in entities)
            {
                TType? entity = key.Value;
                if (entity == null) continue;

                //ILinearEntity entity = (ILinearEntity)rawEntity;
                if (((ILinearEntity)entity).Id == id) return entity;
            }

            return default;
        }

        public IList<TType>? ReadList(IList<string> ids)
        {
            var entities = new List<TType>();
            IDictionary<string, TType>? entityDict = ReadAllAsDictionary();

            if (entityDict == null) return default;

            foreach (var key in entityDict)
            {
                TType? entity = key.Value;
                if (entity == null) continue;

                //ILinearEntity entity = (ILinearEntity)rawEntity;
                if (ids.Contains(((ILinearEntity)entity).Id)) entities.Add(entity);
            }

            return entities;
        }

        public IList<TType> ReadAll()
        {
            var entityDict = ReadAllAsDictionary();
            var entityList = new List<TType>();

            if (entityDict == null || entityDict.Count == 0) return entityList;

            foreach (var entry in entityDict)
            {
                entityList.Add(entry.Value);
            }
            return entityList;
        }

        public void Delete(string id)
        {

            var entityDictionary = ReadAllAsDictionary();
            if (entityDictionary != null && entityDictionary.ContainsKey(id))
            {
                if (entityDictionary.Remove(id))
                {
                    // Success
                    var serialized = JsonSerializer.Serialize(entityDictionary);
                    lock (Writelock)
                    {
                        fileHandler.Write(EntityName, serialized);
                    }
                }
            }
        }

        public void  DeleteList(List<string> ids)
        {
            var entityDictionary = ReadAllAsDictionary();
            if (entityDictionary != null)
            {
                ids.ForEach((it) =>
                {
                    if (entityDictionary.ContainsKey(it))
                    {
                        entityDictionary.Remove(it);
                    }
                });

                var slimmed = JsonSerializer.Serialize(entityDictionary);
                lock (Writelock)
                {
                    fileHandler.Write(EntityName, slimmed);
                }
            }
        }

        public void DeleteAll()
        {
            fileHandler.Write(EntityName, "{}");
        }

        // PRIVATE METHODS

        private IDictionary<string, TType>? ReadAllAsDictionary()
        {
            string? entityString = fileHandler.ReadAsString(EntityName);

            if (entityString == null) return null;

            Dictionary<string, TType>? entities = JsonSerializer.Deserialize<Dictionary<string, TType>>(entityString);
            return entities;
        }

        private void setId(TType newEntity)
        {
            ILinearEntity? entity = newEntity as ILinearEntity;
            if (entity == null) throw new Exception("Can't generate ID for a null entity.");
            if (entity.Id.Length < 1) entity.Id = Guid.NewGuid().ToString();
        }
    }
}

