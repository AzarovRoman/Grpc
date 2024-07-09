using AutoMapper;
using Grpc.Dal.Entities;

namespace Grpc
{
    public class GrpcMapper : Profile
    {
        public GrpcMapper()
        {
            CreateMap<Student, StudentEntity>().ReverseMap();
            CreateMap<AddStudentResponse, StudentEntity>().ReverseMap();
            CreateMap<StudentsByCourseResponse, StudentEntity>().ReverseMap();
        }
    }
}
