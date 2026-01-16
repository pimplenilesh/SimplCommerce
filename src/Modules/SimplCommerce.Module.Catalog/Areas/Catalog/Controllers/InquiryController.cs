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
        public IActionResult SubmitQuote(PackagingInquiry model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            model.CreatedOn = DateTimeOffset.Now;
            _inquiryRepository.Add(model);

            var subject = $"Packaging inquiry for {model.ProductSku ?? model.ProductName}";
            var body = $"Product: {model.ProductName} ({model.ProductSku})\n" +
                       $"Company: {model.CompanyName}\n" +
                       $"Contact: {model.ContactName} <{model.ContactEmail}>\n" +
                       $"Quantity: {model.QuantityNeeded}\n" +
                       $"BoxDimensions: {model.BoxDimensions}\n" +
                       $"Message: {model.Message}\n";

            try
            {
                _emailSender.SendEmailAsync("pimplenilesh@gmail.com", subject, body);
            }
            catch
            {
                // swallow email errors for now
            }

            return Content("Quote Received");
        }
    }
}
