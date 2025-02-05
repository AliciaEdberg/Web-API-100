namespace SoftwareCatalog.Api.Vendors;

public class VendorEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? WebsiteUrl { get; set; }
    public DateTime DateAdded { get; set; }
}