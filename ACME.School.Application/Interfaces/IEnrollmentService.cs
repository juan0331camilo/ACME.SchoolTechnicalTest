using ACME.School.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.School.Application.Interfaces
{
    public interface IEnrollmentService
    {
        EnrollmentDTO AddEnrrolmentStudent(int studentId, int courseId, decimal payment);
    }
}
