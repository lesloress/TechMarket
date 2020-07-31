using System.Collections.Generic;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;

namespace TechMarket.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<OrderDTO> GetOrderById(int id);
        Task<OrderDTO> SaveOrder(OrderDTO orderDto, string cartId);
        Task<IEnumerable<OrderDTO>> GetNotShippedOrders();
        Task MarkShipped(OrderDTO orderDto);
    }
}
