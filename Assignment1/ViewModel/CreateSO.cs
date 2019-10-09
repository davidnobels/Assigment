using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models;

namespace Assignment1.ViewModel
{
    public class CreateSO
    {
        [Required]
        public int SalesOrderId { get; set; }
        [Required]
        public short OrderQty { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal UnitPriceDiscount { get; set; }

        internal SalesOrderDetail ToEntity()
        {
            return new SalesOrderDetail
            {
                SalesOrderId = this.SalesOrderId,
                OrderQty = this.OrderQty,
                ProductId = this.ProductId,
                UnitPrice = this.UnitPrice,
                ModifiedDate = DateTime.Now,
                Rowguid = Guid.NewGuid(),
                UnitPriceDiscount = this.UnitPriceDiscount
            };
        }

    }
}
