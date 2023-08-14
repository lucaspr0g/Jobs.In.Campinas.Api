namespace Web.Client.Entities.Job
{
    public class JobResponse
    {
        public IEnumerable<JobEntity> Jobs { get; set; }

        public int TotalPages { get; set; }
    }

	public class JobEntity
	{
		public string? Id { get; set; }

		public string? Title { get; set; }

		public string? Description { get; set; }

		public int Positions { get; set; }

		public string? CreatedOn { get; set; }

		public string? Time { get; set; }

		public string? Location { get; set; }

		public string? Requirements { get; set; }

		public string? Contact { get; set; }

		public decimal? Salary { get; set; }
	}
}
