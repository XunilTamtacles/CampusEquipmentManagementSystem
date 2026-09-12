using CampusEquipment.Core.DTOs;

namespace CampusEquipment.Core.Services
{
	public interface IEquipmentService
	{
		Task<IEnumerable<EquipmentDto>> GetEquipment();
		Task<EquipmentDto?> GetEquipmentById(int id);
		Task<EquipmentDto> CreateEquipment(CreateEquipmentDto dto);
		Task<EquipmentDto?> UpdateEquipment(int id, UpdateEquipmentDto dto);
		Task<bool> DeleteEquipment(int id);
	}
}