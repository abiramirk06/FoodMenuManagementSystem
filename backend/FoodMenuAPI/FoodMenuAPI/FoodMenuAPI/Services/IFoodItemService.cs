
using FoodMenuAPI.Models;

namespace FoodMenuAPI.Services;

public interface IFoodItemService
{
    Task<List<FoodItem>> GetAllAsync();

    Task<FoodItem?> GetByIdAsync(int id);

    Task<FoodItem> CreateAsync(FoodItem foodItem);

    Task<FoodItem?> UpdateAsync(int id, FoodItem foodItem);

    Task<bool> DeleteAsync(int id);
}
