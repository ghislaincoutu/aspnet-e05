using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspnet05.Data;
using aspnet05.Models;

namespace aspnet05.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
  private readonly AppDbContext _context;

  public WeatherController(AppDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Weather>>> GetAll()
  {
    return await _context.Weathers.ToListAsync();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Weather>> Get(int id)
  {
    var weather = await _context.Weathers.FindAsync(id);

    if (weather == null)
      return NotFound();

    return weather;
  }

  [HttpPost]
  public async Task<ActionResult<Weather>> Create(Weather weather)
  {
    _context.Weathers.Add(weather);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(Get), new { id = weather.Id }, weather);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, Weather weather)
  {
    if (id != weather.Id)
      return BadRequest();

    _context.Entry(weather).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var weather = await _context.Weathers.FindAsync(id);

    if (weather == null)
      return NotFound();

    _context.Weathers.Remove(weather);
    await _context.SaveChangesAsync();

    return NoContent();
  }
}