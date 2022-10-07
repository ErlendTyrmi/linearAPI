namespace linearAPI.Entities.BaseEntity
{
    /// <summary>
    /// The stuff that all Linear entities need to have
    /// </summary>
    public interface ILinearEntity
    {
        public string Id { get; set; }

        public DateTime ModifiedTime { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
