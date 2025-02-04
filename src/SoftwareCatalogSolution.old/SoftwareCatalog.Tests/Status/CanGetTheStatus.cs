

using Alba;
using SoftwareCatalog.Api.Status;

namespace SoftwareCatalog.Tests.Status;
public class CanGetTheStatus
{

    [Fact]
    public async Task GetsA200WhenGettingTheStatus()
    {
        // Given
        var expectedStatus = new StatusResponse(DateTimeOffset.Now, "Looks Good!");
        // This will start up our API, with our configuration (Program)
        var host = await AlbaHost.For<Program>();

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/status");
            api.StatusCodeShouldBeOk();

        });

        var body = response.ReadAsJson<StatusResponse>();
        Assert.NotNull(body); // did we get a response?


        Assert.Equal(expectedStatus, body);

    }
}
