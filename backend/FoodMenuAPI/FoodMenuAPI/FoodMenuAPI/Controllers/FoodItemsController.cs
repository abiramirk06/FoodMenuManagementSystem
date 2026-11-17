
using FoodMenuAPI.Models;
using FoodMenuAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenuAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FoodItemsController : ControllerBase
{
    private readonly IFoodItemService _service;

    public FoodItemsController(IFoodItemService service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);

        if (item == null)
            return NotFound("Food item not found.");

        return Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(FoodItem foodItem)
    {
        try
        {
            var createdItem = await _service.CreateAsync(foodItem);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdItem.Id },
                createdItem);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, FoodItem foodItem)
    {
        try
        {
            var updatedItem = await _service.UpdateAsync(id, foodItem);

            if (updatedItem == null)
                return NotFound("Food item not found.");

            return Ok(updatedItem);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound("Food item not found.");

        return Ok("Food item deleted successfully.");
    }
}
