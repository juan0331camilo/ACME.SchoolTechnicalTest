namespace ACME.School.Application.DTOs
{
    public class CourseDTO
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public decimal RegistrationFee { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
