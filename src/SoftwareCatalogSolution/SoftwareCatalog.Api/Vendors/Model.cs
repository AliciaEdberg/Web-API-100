

using FluentValidation;

namespace SoftwareCatalog.Api.Vendors
{
    public record VendorRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }
    }

    public class VendorRequestModelValidator : AbstractValidator<VendorRequestModel>
    {
        public VendorRequestModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(100);

            RuleFor(x => x.WebsiteUrl)
                .Must(url => string.IsNullOrEmpty(url) || url.StartsWith("https://"))
                .WithMessage("Website URL must start with 'https://'");
        }
    }

    public record VendorResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? WebsiteUrl { get; set; }
        public DateTime DateAdded { get; set; }
    }
}