namespace linearAPI.Repo.Database
{
    public interface ILinearAccess<TType>
    {
        void Create(TType entity);
        void CreateList(IList<TType> entities);
        TType? Delete(string id);
        IDictionary<string, TType>? DeleteAll();
        IDictionary<string, TType>? DeleteSeveral(List<string> ids);
        TType? Read(string id);
        IList<TType> ReadAll();
    }
}