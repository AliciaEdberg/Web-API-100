using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Catalog;
/*
POST http://localhost:1337/vendors/{vendorId}/opensource
Content-Type: application/json

{
  "name": "Visual Studio Code"
}
*/
public class CatalogController : ControllerBase
{
    [HttpPost("/vendors/microsoft/opensource")]
    public async Task<ActionResult> AddItemToCatalogAsync(
        [FromBody] CatalogItemRequestModel request)
    {
        // fake this for right now
        var fakeResponse = new CatalogItemResponseDetailsModel
        {
            Id = Guid.NewGuid(),
            Licence = CatalogItemLicenceTypes.OpenSource,
            Name = request.Name,
            Vendor = "Microsoft"
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
