using TufanFramework.Core.Repository;

namespace testt
{
    public interface IStoreRepository : IRepository
    {
        string Get();
        void Create();
    }
}