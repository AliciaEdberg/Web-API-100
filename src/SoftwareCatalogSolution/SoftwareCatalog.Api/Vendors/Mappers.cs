namespace SoftwareCatalog.Api.Vendors;


public static class VendorMappingExtensions
{
    public static VendorEntity ToVendorEntity(this VendorRequestModel model)
    {
        return new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            WebsiteUrl = model.WebsiteUrl,
            DateAdded = DateTime.UtcNow
        };
    }

    public static VendorResponseModel ToVendorResponseModel(this VendorEntity entity)
    {
        return new VendorResponseModel
        {
            Id = entity.Id,
            Name = entity.Name,
            WebsiteUrl = entity.WebsiteUrl,
            DateAdded = entity.DateAdded
        };
    }
}

