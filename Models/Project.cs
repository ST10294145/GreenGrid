namespace GreenGrid.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Ongoing", "Completed"
    }
}
