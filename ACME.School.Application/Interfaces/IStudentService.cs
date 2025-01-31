namespace ACME.School.Application.Interfaces
{
    using ACME.School.Application.DTOs;

    public interface IStudentService
    {
        StudentDTO CreateStudent(string firstMidName,
            string lastName,
            DateTime birthDate);
    }
}
