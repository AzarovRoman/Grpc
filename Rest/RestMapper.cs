using AutoMapper;
using GrpcProto;
using Rest.Models;

namespace Rest
{
    public class RestMapper : Profile
    {
        public RestMapper()
        {
            CreateMap<Student, StudentModel>().ReverseMap();
            CreateMap<Student, AddStudentModel>().ReverseMap();
        }
    }
}
