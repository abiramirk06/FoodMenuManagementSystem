
using FoodMenuAPI.Models;

namespace FoodMenuAPI.Repositories;

public interface IFoodItemRepository
{
    Task<List<FoodItem>> GetAllAsync();

    Task<FoodItem?> GetByIdAsync(int id);

    Task<FoodItem> AddAsync(FoodItem foodItem);

    Task<FoodItem?> UpdateAsync(int id, FoodItem foodItem);

    Task<bool> DeleteAsync(int id);
}
