using CampusEquipment.Core.DTOs;

namespace CampusEquipment.Core.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetDepartments();
        Task<DepartmentDto?> GetDepartmentById(int id);
    }
}