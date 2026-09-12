using CampusEquipment.Infrastructure.Entities;
using CampusEquipment.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CampusEquipment.Web.Controllers;

public class EquipmentController : Controller
{
    private readonly IEquipmentService _service;

    public EquipmentController(IEquipmentService service)
    {
        _service = service;
    }

   
    public async Task<IActionResult> Index(EquipmentFilter filter)
    {
        var items = await _service.SearchAsync(filter);
        await PopulateFilterListsAsync(filter);
        ViewBag.Filter = filter;
        return View(items);
    }

    
    public async Task<IActionResult> Details(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    
    public async Task<IActionResult> Create()
    {
        await PopulateDepartmentsAsync();
        ViewBag.Statuses = new SelectList(EquipmentStatus.All);
        return View(new Equipment());
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Equipment equipment)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDepartmentsAsync(equipment.DepartmentId);
            ViewBag.Statuses = new SelectList(EquipmentStatus.All, equipment.Status);
            return View(equipment);
        }

        try
        {
            await _service.CreateAsync(equipment);
            return RedirectToAction(nameof(Index));
        }
        catch (BusinessRuleException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await PopulateDepartmentsAsync(equipment.DepartmentId);
            ViewBag.Statuses = new SelectList(EquipmentStatus.All, equipment.Status);
            return View(equipment);
        }
    }

   
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();

        await PopulateDepartmentsAsync(item.DepartmentId);
        ViewBag.Statuses = new SelectList(EquipmentStatus.All, item.Status);
        return View(item);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Equipment equipment)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDepartmentsAsync(equipment.DepartmentId);
            ViewBag.Statuses = new SelectList(EquipmentStatus.All, equipment.Status);
            return View(equipment);
        }

        try
        {
            await _service.UpdateAsync(equipment);
            return RedirectToAction(nameof(Index));
        }
        catch (BusinessRuleException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            await PopulateDepartmentsAsync(equipment.DepartmentId);
            ViewBag.Statuses = new SelectList(EquipmentStatus.All, equipment.Status);
            return View(equipment);
        }
    }

  
    public async Task<IActionResult> Retire(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

     
    [HttpPost, ActionName("Retire")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RetireConfirmed(int id)
    {
        await _service.RetireAsync(id);
        return RedirectToAction(nameof(Index));
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateDepartmentsAsync(int? selectedId = null)
    {
        var departments = await _service.GetDepartmentsAsync();
        ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name", selectedId);
    }

    private async Task PopulateFilterListsAsync(EquipmentFilter filter)
    {
        var departments = await _service.GetDepartmentsAsync();
        ViewBag.DepartmentFilter = new SelectList(departments, "DepartmentId", "Name", filter.DepartmentId);
        ViewBag.StatusFilter = new SelectList(EquipmentStatus.All, filter.Status);
    }
}