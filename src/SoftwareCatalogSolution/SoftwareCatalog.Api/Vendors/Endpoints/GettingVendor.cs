using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors.Endpoints
{
    [ApiController]
    [Route("vendors")]
    public class GettingVendorDetails : ControllerBase
    {
        private readonly IDocumentSession _session;

        public GettingVendorDetails(IDocumentSession session)
        {
            _session = session;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetVendorById(Guid id)
        {
            var vendor = await _session.Query<VendorEntity>()
                .Where(v => v.Id == id)
                .SingleOrDefaultAsync();

            if (vendor == null)
            {
                return NotFound();
            }

            var response = vendor.ToVendorResponseModel();
            return Ok(response);
        }
    }
}