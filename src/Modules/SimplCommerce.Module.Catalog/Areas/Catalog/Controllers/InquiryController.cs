using System;
using Microsoft.AspNetCore.Mvc;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Catalog.Models;
using SimplCommerce.Module.Core.Services;

namespace SimplCommerce.Module.Catalog.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class InquiryController : Controller
    {
        private readonly IRepository<PackagingInquiry> _inquiryRepository;
        private readonly IEmailSender _emailSender;

        public InquiryController(IRepository<PackagingInquiry> inquiryRepository, IEmailSender emailSender)
        {
            _inquiryRepository = inquiryRepository;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> SubmitQuote(PackagingInquiry model)
        {
            if (model == null)
            {
                return BadRequest("Invalid inquiry data.");
            }

            try
            {
                model.CreatedOn = DateTimeOffset.Now;
                _inquiryRepository.Add(model);
                _inquiryRepository.SaveChanges();

                var subject = $"Packaging inquiry for {model.ProductSku ?? model.ProductName}";
                var body = $"Product: {model.ProductName} ({model.ProductSku})\n" +
                           $"Company: {model.CompanyName}\n" +
                           $"Contact: {model.ContactName} <{model.ContactEmail}>\n" +
                           $"Quantity: {model.QuantityNeeded}\n" +
                           $"Box Dimensions: {model.BoxDimensions}\n" +
                           $"Message: {model.Message}\n";

                try
                {
                    await _emailSender.SendEmailAsync("pimplenilesh@gmail.com", subject, body);
                }
                catch (Exception ex)
                {
                    // Log email error but don't fail the response - inquiry was saved
                    System.Diagnostics.Debug.WriteLine($"Email error: {ex.Message}");
                }

                return Ok(new { message = "Quote request received successfully" });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error submitting quote: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
