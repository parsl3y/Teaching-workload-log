using Domain.Entity;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassController : Controller
{
    private readonly IMongoCollection<Classes>? _classes;

    public ClassController(MongoDbService mongoDbService)
    {
        _classes = mongoDbService.Database?.GetCollection<Classes>("classes");
    }

    [HttpGet]
    public async Task<IEnumerable<Classes>> Get()
    {
        return await _classes.Find(FilterDefinition<Classes>.Empty).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Classes> GetById(string id)
    {
        var filter = Builders<Classes>.Filter.Eq(x => x.Id, id);
        var classes = await _classes.Find(filter).FirstOrDefaultAsync();
    
        if (classes == null)
        {
            throw new KeyNotFoundException($" {id} not found.");
        }
    
        return classes;
    }

    [HttpPost]
    public async Task<ActionResult<Classes>> Post(Classes classes)
    {
        await _classes.InsertOneAsync(classes);
        return CreatedAtAction(nameof(GetById), new { id = classes.Id }, classes);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Classes>> Put(string id, Classes classes)
    {
        var filter = Builders<Classes>.Filter.Eq(x => x.Id, id);
        /*var update = Builders<Classes>.Update
            .Set(x => x.ClassName, classes.ClassName)
            .Set(x => x.TotalClassNumber, classes.TotalClassNumber)
            .Set(x => x.ClassNumberToday, classes.ClassNumberToday)
            .Set(x => x.ClassRoom, classes.ClassRoom)
            .Set(x => x.ClassDate, classes.ClassDate);
        await _classes.UpdateOneAsync(filter, update);*/
        
        await _classes.ReplaceOneAsync(filter, classes);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Classes>> Delete(string id)
    {
        var filter = Builders<Classes>.Filter.Eq(x => x.Id, id);
        await _classes.DeleteOneAsync(filter);
        return Ok();
    }
}