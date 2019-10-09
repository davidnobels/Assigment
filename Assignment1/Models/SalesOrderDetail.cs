using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class SalesOrderDetail
    {
        //public SalesOrderDetail(int salesOrderDetailId)
        //{
        //    SalesOrderDetailId = salesOrderDetailId;    
        //}

        [MaxLength(32)]
        public int SalesOrderId { get; set; }
        [Key]
        public int SalesOrderDetailId { get; set; }
        public short OrderQty { get; set; }
        public int ProductId { get; set; }
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }
        [DataType(DataType.Currency)]
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual SalesOrderHeader SalesOrder { get; set; }
    }
}
