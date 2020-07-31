using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechMarket.BLL.DTO;
using TechMarket.BLL.Interfaces;
using TechMarket.DAL.Entities;
using TechMarket.DAL.Interfaces;

namespace TechMarket.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            return _mapper.Map<IEnumerable<OrderDTO>>(await _unitOfWork.Orders.GetAllAsync());
        }

        public async Task<OrderDTO> SaveOrder(OrderDTO orderDto, string cartId)
        {
            Order order = _mapper.Map<Order>(orderDto);
            var cartItems = await _unitOfWork.ShoppingCartRepository
                .Find(s => s.ShoppingCartId == cartId);
            order.CartItems = _mapper.Map<IEnumerable<ShoppingCartItem>>(cartItems);
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CommitAsync();
            return orderDto;
        }

        public async Task<IEnumerable<OrderDTO>> GetNotShippedOrders()
        {
            return _mapper.Map<IEnumerable<OrderDTO>>(
                await _unitOfWork.Orders.Find(o => o.Shipped == false));
        }

        public async Task MarkShipped(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.Shipped = true;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            return _mapper.Map<OrderDTO>(
                //await _unitOfWork.Orders.SingleOrDefaultAsync(o => o.Id == id)
                await _unitOfWork.Orders.GetByIdAsync(id)
                ) ;
        }
    }
}
