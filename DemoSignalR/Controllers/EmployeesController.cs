using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoSignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace DemoSignalR.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly demoSignalRContext _context;
        private readonly IHubContext<SignalrServer> _siganlrHub; 
        public EmployeesController(demoSignalRContext context, IHubContext<SignalrServer> siganlrHub)
        {
            _context = context;
            _siganlrHub = siganlrHub;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View();
        }
        //public JsonResult GetMessages()
        //{
        //    List<Employee> messages = new List<Employee>();
        //    Repository r = new Repository();
        //    messages = r.GetAllMessages();
        //    return Json(messages);
        //}
        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync()
            {
              var res = _context.Employees.ToList();
           
            await _siganlrHub.Clients.All.SendAsync("LoadEmployees");
            return Ok(res);
        }
        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmpName,Salary,DeptName,Designation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                await _siganlrHub.Clients.All.SendAsync("LoadEmployees");
                return View();
               // return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int EmpId, [Bind("EmpId,EmpName,Salary,DeptName,Designation")] Employee employee)
        {
            if (EmpId != employee.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    await _siganlrHub.Clients.All.SendAsync("LoadEmployees");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmpId))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int EmpId)
        {
            var employee = await _context.Employees.FindAsync(EmpId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            await _siganlrHub.Clients.All.SendAsync("LoadEmployees");
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }
    }
}
