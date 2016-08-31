using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class SuppliersController : ApiController
    {
        private NORTHWNDEntities _db;

        public SuppliersController()
        {
            _db = new NORTHWNDEntities();
        }

        //GET: api/suppliers/salesAndMarketing
        [HttpGet, Route("api/suppliers/salesAndMarketing"), ResponseType(typeof(IQueryable<Supplier>))]
        public IHttpActionResult GetSalesAndMarketing()
        {
            //throw new NotImplementedException("Write a query to return all Suppliers that are marketing managers or sales representatives that have a fax number");
            var result = from supply in _db.Suppliers
                         where (supply.ContactTitle == "Marketing Manager" || supply.ContactTitle == "Sales Representative") && supply.Fax != null
                         select supply;

            return Ok(result);
        }

        //GET: api/suppliers/search
        [HttpGet, Route("api/suppliers/search"), ResponseType(typeof(IQueryable<Supplier>))]
        public IHttpActionResult SearchSuppliers(string term)
        {
            //throw new NotImplementedException("Write a query to return all Suppliers containing the 
            //'term' variable in their address. The list should ordered alphabetically by company 
            //name.");
            var result = from supply in _db.Suppliers
                         where supply.Address.Contains(term)
                         select supply;

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
