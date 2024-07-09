using AutoMapper;
using Grpc.Core;
using Grpc.Dal.Entities;
using Grpc.Dal.Interfaces;

namespace Grpc.Services
{
    public class StudentService : GrpcService.GrpcServiceBase
    {
        private readonly ILogger<StudentService> _logger;

        private IMapper _mapper;
        private IStudentRepository _studentRepository;

        public StudentService(ILogger<StudentService> logger, IMapper mapper, IStudentRepository studenRep)
        {
            _logger = logger;
            _mapper = mapper;
            _studentRepository = studenRep;
        }

        public override async Task<AddStudentResponse> AddStudent(Student request, ServerCallContext context)
        {
            return _mapper.Map<AddStudentResponse>(await _studentRepository.AddStudent(_mapper.Map<StudentEntity>(request)));
        }

        public override async Task<StudentsByCourseResponse> GetStudentsByCourse(StudentByCourseRquest request, ServerCallContext context)
        {
            var result = new StudentsByCourseResponse();
            result.Students.AddRange(_mapper.Map<List<Student>>(await _studentRepository.GetStudentsByCourse(request.Course)));

            return result;
        }
    }
}
