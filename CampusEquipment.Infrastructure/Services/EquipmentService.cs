public class EquipmentService : IEquipmentService
{
	private readonly IEquipmentRepository _equipmentRepository;

	public EquipmentService(IEquipmentRepository equipmentRepository)
	{
		_equipmentRepository = equipmentRepository;
	}

	public async Task<EquipmentDto> CreateEquipment(CreateEquipmentDto dto)
	{
		// Rule 5: Required fields
		if (string.IsNullOrWhiteSpace(dto.AssetCode))
			throw new ArgumentException("Asset Code is required.");

		if (string.IsNullOrWhiteSpace(dto.Name))
			throw new ArgumentException("Equipment Name is required.");

		if (string.IsNullOrWhiteSpace(dto.Category))
			throw new ArgumentException("Category is required.");

		if (string.IsNullOrWhiteSpace(dto.Status))
			throw new ArgumentException("Status is required.");

		if (dto.DepartmentId <= 0)
			throw new ArgumentException("Department is required.");

		// Rule 1: Asset Code must be unique
		var equipment = await _equipmentRepository.GetAll();

		if (equipment.Any(e =>
			e.AssetCode.Equals(
				dto.AssetCode,
				StringComparison.OrdinalIgnoreCase)))
		{
			throw new InvalidOperationException(
				"Asset Code already exists.");
		}

		// Rule 2: Retired cannot be assigned
		if (dto.Status == "Retired")
		{
			// Nothing to do when creating as Retired.
			// It simply cannot later be assigned.
		}

		// Rule 3: UnderMaintenance cannot be assigned
		if (dto.Status == "UnderMaintenance")
		{
			// Nothing to do when creating as UnderMaintenance.
			// It simply cannot later be assigned.
		}

		var newEquipment = new Equipment
		{
			AssetCode = dto.AssetCode,
			Name = dto.Name,
			Category = dto.Category,
			Status = dto.Status,
			DepartmentId = dto.DepartmentId,
			Brand = dto.Brand
		};

		await _equipmentRepository.Add(newEquipment);

		return new EquipmentDto
		{
			EquipmentId = newEquipment.EquipmentId,
			AssetCode = newEquipment.AssetCode,
			Name = newEquipment.Name,
			Category = newEquipment.Category,
			Status = newEquipment.Status,
			DepartmentId = newEquipment.DepartmentId,
			Brand = newEquipment.Brand
		};
	}
}