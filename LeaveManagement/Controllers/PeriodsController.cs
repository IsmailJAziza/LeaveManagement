using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Data;
using LeaveManagement.Data.DataModel;
using LeaveManagement.Services.Interface;
using LeaveManagement.Models.Period;
using LeaveManagement.Models.LeaveTypes;
using Microsoft.AspNetCore.Authorization;

namespace LeaveManagement.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class PeriodsController : Controller
    {
        private readonly IPeriodRepository _periodRepository;
        private const string NameExisitValidationMessage = "This leave is alreaed Exist";

        public PeriodsController(IPeriodRepository periodRepository)
        {
            _periodRepository = periodRepository;
        }

        // GET: Periods
        public async Task<IActionResult> Index()
        {
            return View(await _periodRepository.GettAll());
        }

        // GET: Periods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _periodRepository.Get<PeriodReadOnlyVM>(id.Value);
                
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // GET: Periods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Periods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeriodCreateVM model)
        {

            if (await _periodRepository.CheckIfPeriodNameExists(model.Name))
            {
                ModelState.AddModelError(nameof(PeriodCreateVM.Name), NameExisitValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _periodRepository.Create(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Periods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period =  await _periodRepository.Get<PeriodEditVM>(id.Value);
            if (period == null)
            {
                return NotFound();
            }
            return View(period);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PeriodEditVM model)
        {

            if (await _periodRepository.CheckIfPeriodNameExistsForEdit(model))
            {
                ModelState.AddModelError(nameof(PeriodEditVM.Name), NameExisitValidationMessage);
            }
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _periodRepository.Edit(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_periodRepository.PeriodExists(model.Id))
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
            return View(model);
        }

        // GET: Periods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _periodRepository.Get<PeriodReadOnlyVM>(id.Value);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var period = await _periodRepository.Get<PeriodReadOnlyVM>(id);
            if (period != null)
            {
                await _periodRepository.DeleteAsync(id);
            }

            
            return RedirectToAction(nameof(Index));
        }

     
    }
}
