using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CustomersController : ApiController
    {
        private NORTHWNDEntities _db;

        public CustomersController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/customers/city/London
        [HttpGet, Route("api/customers/city/{city}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAll(string city)
        {
            //throw new NotImplementedException("Write a query to return all customers in the given city");
            var cityResult = from customer in _db.Customers
                                  where customer.City == "London"
                                  select customer;

            return Ok(cityResult);
        }

        // GET: api/customers/mexicoSwedenGermany
        [HttpGet, Route("api/customers/mexicoSwedenGermany"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAllFromMexicoSwedenGermany()
        {
            //throw new NotImplementedException("Write a query to return all customers from Mexico, Sweden and Germany.");
            var countriesResult = from customer in _db.Customers
                                  where customer.Country == "Mexico" || customer.Country == "Sweden" || customer.Country == "Germany"
                                  select customer;

            return Ok(countriesResult);
        }

        // GET: api/customers/shippedUsing/Speedy Express
        [HttpGet, Route("api/customers/shippedUsing/{shipperName}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersThatShipWith(string shipperName)
        {
            //throw new NotImplementedException("Write a query to return all customers with orders that shipped using the given shipperName.");
            var orderResult = from order in _db.Orders
                              join shipper in _db.Shippers
                              on order.ShipVia equals shipper.ShipperID
                              where shipper.CompanyName == shipperName && order.ShippedDate != null
                              group order by order.CustomerID into customerShipped
                              select customerShipped;

            return Ok(orderResult);
        }

        // GET: api/customers/withoutOrders
        [HttpGet, Route("api/customers/withoutOrders"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersWithoutOrders()
        {
            //throw new NotImplementedException("Write a query to return all customers with no orders in the Orders table.");
            var orderResult = from customer in _db.Customers
                              join order in _db.Orders
                              on customer.CustomerID equals order.CustomerID
                              into a
                              from order in a.DefaultIfEmpty()
                              where order == null
                              select customer;

            return Ok(orderResult);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
