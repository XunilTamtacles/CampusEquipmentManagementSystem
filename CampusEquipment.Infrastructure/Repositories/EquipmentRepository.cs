namespace CampusEquipment.Core.Repositories
{
	public class EquipmentRepository : IEquipmentRepository
	{
		private readonly AppDbContext _context;
		public EquipmentRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Equipment>> GetAll()
		{
			return await _context.Equipment.ToListAsync();
		}
		public async Task<Equipment?> GetById(int id)
		{
			return await _context.Equipment.FindAsync(id);
		}
		public async Task Add(Equipment equipment)
		{
			await _context.Equipment.AddAsync(equipment);
			await _context.SaveChangesAsync();
		}
		public async Task Update(Equipment equipment)
		{
			var existingEquipment = await _context.Equipment.FindAsync(equipment.EquipmentId);
			if (existingEquipment != null)
			{
				existingEquipment.Name = equipment.Name;
				existingEquipment.DepartmentId = equipment.DepartmentId;
				await _context.SaveChangesAsync();
			}
		}
		public async Task Delete(int id)
		{
			var equipment = await _context.Equipment.FindAsync(id);
			if (equipment != null)
			{
				_context.Equipment.Remove(equipment);
				await _context.SaveChangesAsync();
			}
		}
	}
}