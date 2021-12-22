﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Web.Infrastructure.Context;
using Workers.Web.Infrastructure.Models;

namespace Workers.Web.Controllers
{
    [Route("employee")]
    public class EmployeeController : Controller
    {
        private readonly WorkerDbContext _db;   
        public EmployeeController(WorkerDbContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _db.Employees.ToListAsync(); 
            return View(employees);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();
            return LocalRedirect("~/employee/all");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _db.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return View(employee);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            var userFromDb = await _db.Employees.SingleOrDefaultAsync(x => x.Id == employee.Id);
            if (userFromDb == null)
                return LocalRedirect("~/employee/all");
            userFromDb.FirstName = employee.FirstName;
            userFromDb.LastName = employee.LastName;
            userFromDb.Login = employee.Login;
            userFromDb.Email = employee.Email;
            await _db.SaveChangesAsync();
            return LocalRedirect("~/employee/all");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var employee = await _db.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if(employee == null)
                return LocalRedirect("~/employee/all");
            return View(employee);
        }

        public IActionResult GetProjectEmployees()
        {
            var es = _db.ProjectEmployees
                .Include(x => x.Employee).Include(x => x.Position)
                .FirstOrDefaultAsync(x => x.Id == 1);
            return Ok();
        }
    }
}
