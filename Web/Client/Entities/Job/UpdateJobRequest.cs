using System.ComponentModel.DataAnnotations;

namespace Web.Client.Entities.Job
{
    public class UpdateJobRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int Positions { get; set; }
    }
}
