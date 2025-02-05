using SoftwareCatalog.Api.Vendors;
using SoftwareCatalog.Tests.Catalog;

namespace SoftwareCatalog.Tests.Vendor
{
    public class AddingAVendor(CatalogSystemTestFixture fixture) : IClassFixture<CatalogSystemTestFixture>
    {
        [Theory]
        [InlineData("Microsoft", "https://www.microsoft.com")]
        [InlineData("Apple", "https://www.apple.com")]
        [InlineData("Amazon", null)] // Testing without a URL
        public async Task CanAddAVendor(string vendorName, string? vendorUrl)
        {
            var vendorToPost = new VendorRequestModel
            {
                Name = vendorName,
                WebsiteUrl = vendorUrl
            };

            var resource = "/vendors";
            var response = await fixture.Host.Scenario(api =>
            {
                api.Post.Json(vendorToPost).ToUrl(resource);
                api.StatusCodeShouldBe(201);
            });

            var responseFromThePost = response.ReadAsJson<VendorResponseModel>();
            Assert.NotNull(responseFromThePost);
            Assert.Equal(vendorName, responseFromThePost.Name);
            Assert.Equal(vendorUrl, responseFromThePost.WebsiteUrl);

            var getResponse = await fixture.Host.Scenario(api =>
            {
                api.Get.Url($"/vendors/{responseFromThePost.Id}");
            });

            var responseFromGet = getResponse.ReadAsJson<VendorResponseModel>();
            Assert.NotNull(responseFromGet);
            Assert.Equal(responseFromThePost, responseFromGet);
        }

        [Theory]
        [InlineData("A", "https://www.example.com")] // Name too short
        [InlineData("ThisNameIsWayTooLongForOurSystemToHandleAndShouldBeRejectedBecauseItExceedsTheMaximumAllowedLength", "https://www.example.com")]
        [InlineData("ValidName", "http://www.example.com")] // Invalid URL
        public async Task CannotAddInvalidVendor(string vendorName, string? vendorUrl)
        {
            var vendorToPost = new VendorRequestModel
            {
                Name = vendorName,
                WebsiteUrl = vendorUrl
            };

            var resource = "/vendors";
            await fixture.Host.Scenario(api =>
            {
                api.Post.Json(vendorToPost).ToUrl(resource);
                api.StatusCodeShouldBe(400); // Expecting a Bad Request status
            });
        }
    }
}