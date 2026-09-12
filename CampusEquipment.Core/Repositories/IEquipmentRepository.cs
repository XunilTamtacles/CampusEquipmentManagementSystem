using CampusEquipment.Core.DTOs;
using CampusEquipment.Infrastructure.Entities;

namespace CampusEquipment.Core.Repositories
{
	public interface IEquipmentRepository
	{
		Task<IEnumerable<Equipment>> GetAll();
		Task<Equipment?> GetById(int id);
		Task Add(Equipment equipment);
		Task Update(Equipment equipment);
		Task Delete(int id);
	}
}