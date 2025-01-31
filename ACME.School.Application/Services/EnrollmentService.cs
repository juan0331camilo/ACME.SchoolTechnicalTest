namespace ACME.School.Application.Services
{
    using ACME.School.Application.DTOs;
    using ACME.School.Application.Interfaces;
    using ACME.School.Domain.Data;
    using ACME.School.Domain.Entities;
    using AutoMapper;

    public class EnrollmentService(IMapper mapper,
        ACMEContext ACMEContext) : IEnrollmentService
    {
        private readonly UnitOfWork unitOfWork = new(ACMEContext);

        public EnrollmentDTO AddEnrrolmentStudent(int studentId, int courseId, decimal payment)
        {
            Course course = unitOfWork.Repository<Course>().GetByID(courseId);
            if (course.RegistrationFee >= payment)
            {
                Enrollment enrollment = new()
                {
                    CourseID = course.CourseID,
                    StudentID = studentId,
                    Grade = Grade.A
                };
                unitOfWork.Repository<Enrollment>().Insert(enrollment);
                unitOfWork.Save();

                EnrollmentDTO enrollmentDTO = mapper.Map<EnrollmentDTO>(enrollment);
                enrollmentDTO.Payment = payment;

                return enrollmentDTO;
            }
            else
                throw new Exception("El monto pagado no aplica para el curso seleccionado");
        }
    }
}
