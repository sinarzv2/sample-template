using Domain.Common.DependencyLifeTime;

namespace Application.Services.DataInitializer
{
    public interface IDataInitializer : IScopedService
    {
        void InitializeData();
    }
}
