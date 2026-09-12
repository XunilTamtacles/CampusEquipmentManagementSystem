namespace CampusEquipment.Core.DTOs
{
    public class EquipmentDto
    {
        public int EquipmentId { get; set; }
        public string AssetCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public string? Brand { get; set; }
    }
}