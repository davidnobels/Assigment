using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.ViewModel
{
    public class SODViewModel
    {
        public Guid SalesOrderDetailId { get; internal set; }
        public short OrderQty { get; internal set; }
        public int ProductId { get; internal set; }
        public decimal UnitPrice { get; internal set; }
        public decimal UnitPriceDiscoutn { get; internal set; }
    }
}
