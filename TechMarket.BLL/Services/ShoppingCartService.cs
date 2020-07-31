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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShoppingCartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddToCart(ShoppingCartItemDTO itemDto)
        {
            ShoppingCartItem item = _mapper.Map<ShoppingCartItem>(itemDto);

            ShoppingCartItem shoppingCartItem = await _unitOfWork
                .ShoppingCartRepository.GetByCartAndProductIds(itemDto.ShoppingCartId, itemDto.ProductId);

            if (shoppingCartItem == null)
            {
                await _unitOfWork.ShoppingCartRepository.AddAsync(item);
            } 
            else
            {
                shoppingCartItem.Quantity += 1;
                _unitOfWork.ShoppingCartRepository.Update(shoppingCartItem);
            }

            await _unitOfWork.CommitAsync();
        }
        public async Task<IEnumerable<ShoppingCartItemDTO>> GetAllShoppingCartItems(string shoppingCartId)
        {
            return _mapper.Map<IEnumerable<ShoppingCartItemDTO>>(
                await _unitOfWork.ShoppingCartRepository.GetByCartIdWithProductAsync(shoppingCartId));
        }

        public async Task RemoveById(int cartItemId)
        {
            var item = await _unitOfWork.ShoppingCartRepository.GetByIdAsync(cartItemId);
            if (item != null)
            {
                _unitOfWork.ShoppingCartRepository.Remove(item);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task ClearCart(string shoppingCartId)
        {
            var items = await _unitOfWork.ShoppingCartRepository
                .Find(s => s.ShoppingCartId == shoppingCartId);
            if (items.Count() > 0)
            {
                _unitOfWork.ShoppingCartRepository.RemoveRange(items);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<bool> IsNotEmpty(string cartId)
        {
            return await _unitOfWork.ShoppingCartRepository.IsNotEmpty(cartId);
        }

    }
}
