using CampusEquipment.Core.DTOs;
using CampusEquipment.Infrastructure.Entities;

namespace CampusEquipment.Core.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department?> GetById(int id);
        Task Add(Department department);
        Task Update(Department department);
        Task Delete(int id);
    }
}