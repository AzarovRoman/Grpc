using AutoMapper;
using Grpc.Core;
using Grpc.Dal.Entities;
using Grpc.Dal.Interfaces;
using GrpcProto;

namespace Grpc.Services
{
    public class StudentService : GrpcService.GrpcServiceBase
    {
        private IMapper _mapper;
        private IStudentRepository _studentRepository;

        public StudentService(IMapper mapper, IStudentRepository studenRep)
        {
            _mapper = mapper;
            _studentRepository = studenRep;
        }

        public override async Task<AddStudentResponse> AddStudent(Student request, ServerCallContext context)
        {
            AddStudentResponse addedStudent = _mapper.Map<AddStudentResponse>(await _studentRepository.AddStudent(_mapper.Map<StudentEntity>(request)));

            if (addedStudent.Id > 0)
                return addedStudent;
            else
                throw new Exception();
        }

        public override async Task<StudentsByCourseResponse> GetStudentsByCourse(StudentByCourseRquest request, ServerCallContext context)
        {
            var result = new StudentsByCourseResponse();
            result.Students.AddRange(_mapper.Map<List<Student>>(await _studentRepository.GetStudentsByCourse(request.Course)));

            return result;
        }
    }
}
