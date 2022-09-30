using Database.Entities;
using linearAPI.Entities;
using System.Text.Json;

namespace Database.LinearDatabase
{
    /// <summary>
    /// General repo class for Linear Entities.
    /// Type arg must implement ILinearEntity.
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    public class LinearRepo<TType>
    {
        readonly LinearFileHandler fileHandler = new LinearFileHandler(null);
        readonly string EntityName = nameof(TType);

        public void Create(TType entity)
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
                entities.Add(id, entity);
            }

            var serialized = JsonSerializer.Serialize(entities);
            fileHandler.WriteAsString(EntityName, serialized);
        }

        // Optional
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

        public IList<TType> ReadAll()
        {
            var entityDict = ReadAllAsDictionary();
            var entityList = new List<TType>();

            if (entityDict == null || entityDict.Count == 0) return entityList;

            foreach (var entry in entityDict) {
                entityList.Add(entry.Value);
            }
            return entityList;
        }

        // PRIVATE METHODS

        private IDictionary<string, TType>? ReadAllAsDictionary()
        {
            string? entityString = fileHandler.ReadAsString(EntityName);

            if (entityString == null) return null;

            Dictionary<string, TType>? entities = JsonSerializer.Deserialize<Dictionary<string, TType>>(entityString);
            return entities;
        }
    }
}
