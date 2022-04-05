﻿using Extensions.Password;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Workers.Application.Services.Interfaces;
using Workers.Domain.Models;

namespace Workers.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/employee")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _employeeService.Create(employee);
            return LocalRedirect("~/employee/all");
        }

        //    [HttpGet("edit/{id}")]
        //    public async Task<IActionResult> Edit(int id)
        //    {
        //        var employee = await _db.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        //        return View(employee);
        //    }

        //    [HttpPost("edit")]
        //    public async Task<IActionResult> EditEmployee(Employee employee)
        //    {
        //        var userFromDb = await _db.Employees.SingleOrDefaultAsync(x => x.Id == employee.Id);
        //        if (userFromDb == null)
        //            return LocalRedirect("~/employee/all");
        //        userFromDb.FirstName = employee.FirstName;
        //        userFromDb.LastName = employee.LastName;
        //        // userFromDb.Login = employee.Login;
        //        userFromDb.Email = employee.Email;
        //        await _db.SaveChangesAsync();
        //        return LocalRedirect("~/employee/all");
        //    }
        //    [HttpGet("delete/{id}")]
        //    public async Task<IActionResult> Delete(int id)
        //    {
        //        var employee = await _db.Employees.SingleOrDefaultAsync(x => x.Id == id);
        //        if (employee == null)
        //            return LocalRedirect("~/employee/all");
        //        return View(employee);
        //    }

        //    [HttpPost("delete")]
        //    public async Task<IActionResult> DeleteEmployee(Employee employee)
        //    {
        //        var employeeForDelete = await _db.Employees.SingleOrDefaultAsync(x => x.Id == employee.Id);
        //        if (employee == null)
        //        {
        //            return LocalRedirect("~/employee/all");
        //        }
        //        _db.Remove(employeeForDelete);
        //        await _db.SaveChangesAsync();
        //        return LocalRedirect("~/employee/all");
        //    }
    }
}
