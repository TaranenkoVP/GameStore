namespace Bll.Core.Entities.Common
{
    public abstract class BaseModelBll<TKey>
    {
        public TKey Id { get; set; }
    }
}