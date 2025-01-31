namespace ACME.School.Application.Configs
{
    using ACME.School.Application.DTOs;
    using ACME.School.Domain.Entities;
    using AutoMapper;

    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Enrollment, EnrollmentDTO>().ReverseMap();
        }
    }
}
