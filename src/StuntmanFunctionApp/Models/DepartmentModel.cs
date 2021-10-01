namespace StuntmanFunctionApp.Models
{
    public class DepartmentModel
    {
        public int Id { get; }
        public int ExternalId { get; set; }
        public string DisplayName { get; set; }
        public string ManagerExternalId { get; set; }
    }
}
