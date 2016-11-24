namespace GameStore.BLL.Entities.Common
{
    public abstract class BaseModelBll<TKey>
    {
        public TKey Id { get; set; }
    }
}