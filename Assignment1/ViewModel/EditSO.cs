using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Models;

namespace Assignment1.ViewModel
{
    public class EditSO
    {
        public EditSO() { }
        public EditSO(SalesOrderDetail sod)
        {
            this.SalesOrderDetailId = sod.SalesOrderDetailId;
            this.OrderQty = sod.OrderQty;
            this.ProductId = sod.ProductId;
            this.UnitPrice = sod.UnitPrice;
            this.UnitPriceDiscount = sod.UnitPriceDiscount;
        }

        public int SalesOrderDetailId { get; set; }
        public short OrderQty { get;  set; }
        public int ProductId { get;  set; }
        public decimal UnitPrice { get;  set; }
        public decimal UnitPriceDiscount { get;  set; }

        public int SalesOrderId { get; set; }

        internal SalesOrderDetail ToEntity(SalesOrderDetail sod)
        {

            //sod.SalesOrderId = this.SalesOrderId;
            sod.OrderQty = this.OrderQty;
            //sod.ProductId = this.ProductId;
            sod.UnitPrice = this.UnitPrice;
            sod.ModifiedDate = DateTime.Now;
            //sod.Rowguid = Guid.NewGuid();
            sod.UnitPriceDiscount = this.UnitPriceDiscount;

            return sod;
        }
    }
}
