namespace CampusEquipment.Core.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetById(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task Add(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.DepartmentId);
            if (existingDepartment != null)
            {
                existingDepartment.Name = department.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }

    }
}