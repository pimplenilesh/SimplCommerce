using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Infrastructure.Web.SmartTable;
using SimplCommerce.Module.Catalog.Models;

namespace SimplCommerce.Module.Catalog.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    [Authorize(Roles = "admin")]
    [Route("api/inquiries")]
    public class InquiryApiController : Controller
    {
        private readonly IRepository<PackagingInquiry> _inquiryRepository;

        public InquiryApiController(IRepository<PackagingInquiry> inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        [HttpPost("grid")]
        public async Task<IActionResult> List([FromBody] SmartTableParam param)
        {
            var query = _inquiryRepository.Query();

            if (param.Search.PredicateObject != null)
            {
                dynamic search = param.Search.PredicateObject;
                if (search.ProductName != null)
                {
                    string productName = search.ProductName;
                    query = query.Where(x => x.ProductName.Contains(productName));
                }
            }

            var inquiries = query.ToSmartTableResult(
                param,
                x => new
                {
                    x.Id,
                    x.ProductName,
                    x.ProductSku,
                    x.CompanyName,
                    x.ContactName,
                    x.ContactEmail,
                    x.QuantityNeeded,
                    x.BoxDimensions,
                    x.Message,
                    x.CreatedOn
                });

            return Json(inquiries);
        }
    }
}
