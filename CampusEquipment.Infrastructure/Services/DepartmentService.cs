using CampusEquipment.Core.DTOs;
using CampusEquipment.Core.Repositories;
using CampusEquipment.Core.Services;

namespace CampusEquipment.Infrastructure.Services
{
	public class DepartmentService : IDepartmentService
	{
		private readonly IDepartmentRepository _departmentRepository;

		public DepartmentService(
			IDepartmentRepository departmentRepository)
		{
			_departmentRepository = departmentRepository;
		}

		public async Task<IEnumerable<DepartmentDto>> GetDepartments()
		{
			var departments = await _departmentRepository.GetAll();

			return departments.Select(d => new DepartmentDto
			{
				DepartmentId = d.DepartmentId,
				Name = d.Name
			});
		}

		public async Task<DepartmentDto?> GetDepartmentById(int id)
		{
			var department = await _departmentRepository.GetById(id);

			if (department == null)
				return null;

			return new DepartmentDto
			{
				DepartmentId = department.DepartmentId,
				Name = department.Name
			};
		}
	}
}