﻿using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class EmployeesController : ApiController
    {
        private NORTHWNDEntities _db;

        public EmployeesController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/employees
        [HttpGet, Route("api/employees"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployees()
        {
            //throw new NotImplementedException("Write a query to return all employees");
            var employeesResults = from employee in _db.Employees
                                   select employee;

            return Ok(employeesResults);
        }

        // GET: api/employees/title/Sales Manager
        [HttpGet, Route("api/employees/title/{title}"), ResponseType(typeof(IQueryable<Employee>))]
        public IHttpActionResult GetEmployeesByTitle(string title)
        {
            //throw new NotImplementedException("Write a query to return all employees with the given Title");
            var titleResult = from employee in _db.Employees
                              where employee.Title == title
                              select employee;

            return Ok(titleResult);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
