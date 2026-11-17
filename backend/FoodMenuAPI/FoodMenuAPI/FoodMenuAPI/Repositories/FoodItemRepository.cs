
using FoodMenuAPI.Data;
using FoodMenuAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuAPI.Repositories;

public class FoodItemRepository : IFoodItemRepository
{
    private readonly AppDbContext _context;

    public FoodItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FoodItem>> GetAllAsync()
    {
        return await _context.FoodItems.ToListAsync();
    }

    public async Task<FoodItem?> GetByIdAsync(int id)
    {
        return await _context.FoodItems.FindAsync(id);
    }

    public async Task<FoodItem> AddAsync(FoodItem foodItem)
    {
        await _context.FoodItems.AddAsync(foodItem);
        await _context.SaveChangesAsync();

        return foodItem;
    }

    public async Task<FoodItem?> UpdateAsync(int id, FoodItem foodItem)
    {
        var existingItem = await _context.FoodItems.FindAsync(id);

        if (existingItem == null)
            return null;

        existingItem.Name = foodItem.Name;
        existingItem.Category = foodItem.Category;
        existingItem.Description = foodItem.Description;
        existingItem.Price = foodItem.Price;
        existingItem.IsAvailable = foodItem.IsAvailable;

        await _context.SaveChangesAsync();

        return existingItem;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var foodItem = await _context.FoodItems.FindAsync(id);

        if (foodItem == null)
            return false;

        _context.FoodItems.Remove(foodItem);
        await _context.SaveChangesAsync();

        return true;
    }
}
