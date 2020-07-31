using System;
using System.Threading.Tasks;

namespace TechMarket.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IShoppingCartRepository ShoppingCartRepository { get; }
        IOrderRepository Orders { get; }
        Task<int> CommitAsync();
    }
}
