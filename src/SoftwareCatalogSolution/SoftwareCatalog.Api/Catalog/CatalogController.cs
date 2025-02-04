using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog;
/*
POST http://localhost:1337/vendors/{vendorId}/opensource
Content-Type: application/json

{
  "name": "Visual Studio Code"
}
*/
public class CatalogController(IDocumentSession session) : ControllerBase
{
    // GET /catalog/pizza -> 404
    [HttpGet("/catalog/{id:guid}")]
    public async Task<ActionResult> GetItemById(Guid id)
    {
        var savedEntity = await session.Query<CatalogItemEntity>().Where(c => c.Id == id).SingleOrDefaultAsync();

        if (savedEntity == null)
        {
            return NotFound();
        }
        else
        {
            var response = new CatalogItemResponseDetailsModel
            {
                Id = savedEntity.Id,
                Licence = savedEntity.Licence,
                Name = savedEntity.Name,
                Vendor = savedEntity.Vendor
            };
            return Ok(savedEntity);
        }
    }

    [HttpPost("/vendors/microsoft/opensource")]
    public async Task<ActionResult> AddItemToCatalogAsync(
        [FromBody] CatalogItemRequestModel request)
    {

        // TODO: 1. Do Some validation
        // TODO : 2 save it to the database.
        var entityToSave = new CatalogItemEntity
        {
            Id = Guid.NewGuid(),
            Licence = CatalogItemLicenceTypes.OpenSource,
            Name = request.Name,
            Vendor = "Microsoft"
        };

        session.Store(entityToSave);
        await session.SaveChangesAsync(); // Do the actual work and save into the database

        // Step 3: send them the response
        // fake this for right now
        var fakeResponse = new CatalogItemResponseDetailsModel
        {
            Id = entityToSave.Id,
            Licence = entityToSave.Licence,
            Name = entityToSave.Name,
            Vendor = entityToSave.Vendor
        };
        return StatusCode(201, fakeResponse);
    }
}


public record CatalogItemRequestModel
{
    public string Name { get; set; } = string.Empty;

}

public enum CatalogItemLicenceTypes { OpenSource, Free, Paid }
public record CatalogItemResponseDetailsModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
    public CatalogItemLicenceTypes Licence { get; set; }

}


public class CatalogItemEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
    public CatalogItemLicenceTypes Licence { get; set; }
}