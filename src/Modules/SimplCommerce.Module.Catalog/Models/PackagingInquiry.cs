using System;
using System.ComponentModel.DataAnnotations;
using SimplCommerce.Infrastructure.Models;

namespace SimplCommerce.Module.Catalog.Models
{
    public class PackagingInquiry : EntityBase
    {
        [StringLength(450)]
        public string ProductSku { get; set; }

        [StringLength(450)]
        public string ProductName { get; set; }

        [StringLength(450)]
        public string CompanyName { get; set; }

        [StringLength(450)]
        public string ContactName { get; set; }

        [StringLength(450)]
        public string ContactEmail { get; set; }

        public string BoxDimensions { get; set; }

        public int QuantityNeeded { get; set; }

        public string Message { get; set; }
        
        public DateTimeOffset CreatedOn { get; set; }
    }
}
