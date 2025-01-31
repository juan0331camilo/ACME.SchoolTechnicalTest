namespace ACME.School.Application.Services
{
    using ACME.School.Application.DTOs;
    using ACME.School.Application.Interfaces;
    using ACME.School.Domain.Data;
    using ACME.School.Domain.Entities;
    using AutoMapper;

    public class StudentService(IMapper mapper,
        ACMEContext ACMEContext) : IStudentService
    {
        private readonly UnitOfWork unitOfWork = new(ACMEContext);

        public StudentDTO CreateStudent(string firstMidName,
            string lastName,
            DateTime birthDate)
        {
            DateTime currentDate = DateTime.UtcNow;

            int age = (currentDate.Year - birthDate.Year);
            if (age >= 18)
            {
                Student student = new()
                {
                    BirthDate = birthDate,
                    LastName = lastName,
                    FirstMidName = firstMidName,
                    EnrollmentDate = currentDate
                };
                unitOfWork.Repository<Student>().Insert(student);
                unitOfWork.Save();
                return mapper.Map<StudentDTO>(student);
            }
            else
                throw new Exception("debes ser mayor de edad.");
        }
    }
}
