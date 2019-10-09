using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1.Models;
using Assignment1.ViewModel;

namespace Assignment1.Controllers
{
    public class SalesOrderDetailsController : Controller
    {
        private readonly AdventureWorksLT2012Context _context;

        public SalesOrderDetailsController(AdventureWorksLT2012Context context)
        {
            _context = context;
        }

        // GET: SalesOrderDetails
        public async Task<IActionResult> Index(string sortDate)
        {
            ViewData["DateSortParm"] = sortDate == "Date" ? "date_desc" : "Date";
            var adventureWorksLT2012Context = _context.SalesOrderDetail.Include(s => s.Product).Include(s => s.SalesOrder);
            var sod = from s in _context.SalesOrderDetail
                      select s;
            switch (sortDate)
            {
                case "Date":
                    sod = sod.OrderBy(s => s.ModifiedDate);
                    break;
                case "date_desc":
                    sod = sod.OrderByDescending(s => s.ModifiedDate);
                    break;
            }
            return View(await adventureWorksLT2012Context.ToListAsync());
            
        }

        // GET: SalesOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderDetail = await _context.SalesOrderDetail
                .Include(s => s.Product)
                .Include(s => s.SalesOrder)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.SalesOrderDetailId == id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }

            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeader, "SalesOrderId", "SalesOrderNumber");
            return View();
        }

        // POST: SalesOrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // public async Task<IActionResult> Create([Bind("SalesOrderId,SalesOrderDetailId,OrderQty,ProductId,UnitPrice,UnitPriceDiscount,LineTotal,Rowguid,ModifiedDate")] SalesOrderDetail salesOrderDetail);
        public async Task<IActionResult> Create([Bind]CreateSO command)
        {
            if (ModelState.IsValid)
            {
                var order = _context.SalesOrderHeader.First(o => o.SalesOrderId == command.SalesOrderId);
                order.SalesOrderDetail.Add(command.ToEntity());

                await _context.SaveChangesAsync();

                                //_context.SalesOrderDetail.Add(salesOrderDetail);
                //_context.SaveChanges();
                return RedirectToAction(nameof(Index));
               // return RedirectToAction("Index");
            }
            //ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", salesOrderDetail.ProductId);
            //ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeader, "SalesOrderId", "SalesOrderNumber", salesOrderDetail.SalesOrderId);
            //return View(salesOrderDetail);
            return View(command);
        }

        // GET: SalesOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderDetail = _context.SalesOrderDetail
                .Include(s => s.Product)
                .Include(s => s.SalesOrder)
                .AsNoTracking()
                .First(o => o.SalesOrderDetailId == id);

            if (salesOrderDetail == null)
            {
                return NotFound();
            }

            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", salesOrderDetail.ProductId);
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeader, "SalesOrderId", "SalesOrderNumber", salesOrderDetail.SalesOrderId);
            return View(new EditSO {
                //SalesOrderId = salesOrderDetail.SalesOrderId,
                SalesOrderDetailId = salesOrderDetail.SalesOrderDetailId,
                UnitPrice = salesOrderDetail.UnitPrice,
               // ProductId = salesOrderDetail.ProductId,
                OrderQty = salesOrderDetail.OrderQty,
                UnitPriceDiscount = salesOrderDetail.UnitPriceDiscount,
            });

            //var sod = await _context.sods.FindAsync(id);
            //    //.Include(s => s.Product)
            //    //.Include(s => s.SalesOrder)
            //    //.AsNoTracking()
            //    //.FirstOrDefaultAsync(m => m.SalesOrderDetailId == id);
            //if (sod == null)
            //{
            //    return NotFound();
            //}

            //return View(new EditSO(sod));

        }

        //POST: SalesOrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("SalesOrderId,SalesOrderDetailId,OrderQty,ProductId,UnitPrice,UnitPriceDiscount,LineTotal,Rowguid,ModifiedDate")] SalesOrderDetail salesOrderDetail)
        public async Task<IActionResult> Edit(int id, [Bind] EditSO command)
        {
            var sod = _context.SalesOrderDetail.First(o => o.SalesOrderDetailId == id);

            if (id != command.SalesOrderDetailId || sod == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.SalesOrderDetail.Update(command.ToEntity(sod));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesOrderDetailExists(sod.SalesOrderDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", salesOrderDetail.ProductId);
            //ViewData["SalesOrderId"] = new SelectList(_context.SalesOrderHeader, "SalesOrderId", "SalesOrderNumber", salesOrderDetail.SalesOrderId);
            return View();
        }



        // GET: SalesOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderDetail = await _context.SalesOrderDetail
                //.Include(s => s.Product)
                //.Include(s => s.SalesOrder)
                .FirstOrDefaultAsync(m => m.SalesOrderDetailId == id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }

            return View(salesOrderDetail);
        }

        // POST: SalesOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrderDetail = _context.SalesOrderDetail.First(o=> o.SalesOrderDetailId == id);
            _context.SalesOrderDetail.Remove(salesOrderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesOrderDetailExists(int id)
        {
            return _context.SalesOrderDetail.Any(e => e.SalesOrderId == id);
        }
    }
}
