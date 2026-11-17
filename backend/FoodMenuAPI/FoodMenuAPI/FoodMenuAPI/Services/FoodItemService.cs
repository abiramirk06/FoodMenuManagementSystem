
using FoodMenuAPI.Models;
using FoodMenuAPI.Repositories;

namespace FoodMenuAPI.Services;

public class FoodItemService : IFoodItemService
{
    private readonly IFoodItemRepository _repository;

    public FoodItemService(IFoodItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<FoodItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<FoodItem?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<FoodItem> CreateAsync(FoodItem foodItem)
    {
        if (foodItem.Price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        return await _repository.AddAsync(foodItem);
    }

    public async Task<FoodItem?> UpdateAsync(int id, FoodItem foodItem)
    {
        if (foodItem.Price <= 0)
            throw new ArgumentException("Price must be greater than zero.");

        return await _repository.UpdateAsync(id, foodItem);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
