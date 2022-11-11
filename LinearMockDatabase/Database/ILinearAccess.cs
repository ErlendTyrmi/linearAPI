namespace LinearMockDatabase.Repo.Database
{
    public interface ILinearAccess<TType>
    {
        TType? Create(TType entity);
        IList<TType> CreateList(IList<TType> entities);
        void Delete(string id);
        void DeleteAll();
        void DeleteList(List<string> ids);
        TType? Read(string id);
        IList<TType>? ReadList(IList<string> ids);
        IList<TType> ReadAll();
    }
}