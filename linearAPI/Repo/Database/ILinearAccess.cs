namespace linearAPI.Repo.Database
{
    public interface ILinearAccess<TType>
    {
        void Create(TType entity);
        void CreateList(IList<TType> entities);
        TType? Delete(string id);
        IDictionary<string, TType>? DeleteAll();
        TType? Read(string id);
        IList<TType> ReadAll();
    }
}