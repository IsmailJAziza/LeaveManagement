using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Data;
using LeaveManagement.Services;
using LeaveManagement.Models.LeaveTypes;
using AutoMapper;
using LeaveManagement.MappingProfiles;

namespace LeaveManagement.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private const string NameExisitValidationMessage= "This leave is alreaed Exist";

        public LeaveTypesController(ILeaveTypeRepository repository, IMapper mapper)
        {
           _leaveTypeRepository = repository;
            _mapper = mapper;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            var data = await _leaveTypeRepository.GettAll();
            var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
            return View(viewData);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeRepository.Details(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            var viewData = _mapper.Map<LeaveTypeReadOnlyVM>(leaveType);
            return View(viewData);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {
            if (await _leaveTypeRepository.LeaveTypeNameExists(leaveTypeCreate.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExisitValidationMessage);
            }
            if (ModelState.IsValid)
            {   
                var leaveType = _mapper.Map<LeaveType>(leaveTypeCreate);
                await _leaveTypeRepository.Create(leaveType);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeCreate);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeRepository.Details(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            var viewData = _mapper.Map<LeaveTypeEditVM>(leaveType);
            return View(viewData);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,LeaveTypeEditVM leaveTypeEdit)
        {
            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }
            
            if ( await _leaveTypeRepository.LeaveTypeNameExistsForEdit(leaveTypeEdit.Name,leaveTypeEdit.Id))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExisitValidationMessage);
            }

            if (ModelState.IsValid)
            {
               
                try
                {
                    var leaveType = _mapper.Map<LeaveType>(leaveTypeEdit);
                    await _leaveTypeRepository.Edit(leaveType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _leaveTypeRepository.LeaveTypeExists(leaveTypeEdit.Id))
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
            return View(leaveTypeEdit);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeRepository.Details(id);

            var viewData = _mapper.Map<LeaveTypeReadOnlyVM>(leaveType);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveType = await _leaveTypeRepository.Details(id);
            if (leaveType != null)
            {

                await _leaveTypeRepository.DeleteAsync(leaveType);

            }


            return RedirectToAction(nameof(Index));
        }


    }
}
