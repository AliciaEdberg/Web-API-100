using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Vendors.Endpoints
{
    [ApiController]
    [Route("vendors")]
    public class AddingAVendor : ControllerBase
    {
        private readonly IDocumentSession _session;
        private readonly IValidator<VendorRequestModel> _validator;

        public AddingAVendor(IDocumentSession session, IValidator<VendorRequestModel> validator)
        {
            _session = session;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult> AddVendorAsync([FromBody] VendorRequestModel request)
        {
            var validationResults = await _validator.ValidateAsync(request);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToDictionary());
            }

            var entityToSave = request.ToVendorEntity();
            _session.Store(entityToSave);
            await _session.SaveChangesAsync();

            var response = entityToSave.ToVendorResponseModel();
            return StatusCode(201, response);
        }
    }
}