using AutoMapper;
using Grpc.Net.Client;
using GrpcProto;
using Microsoft.AspNetCore.Mvc;
using Rest.Models;

namespace Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private const string _grpcServerUrl = "https://localhost:7279";

        private IMapper _mapper;
        private GrpcService.GrpcServiceClient _client;
        private GrpcChannel _channel;

        public StudentController(IMapper mapper) 
        {
            _mapper = mapper;

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            _channel = GrpcChannel.ForAddress(_grpcServerUrl, new GrpcChannelOptions { HttpHandler = handler });
            _client = new GrpcService.GrpcServiceClient(_channel);
        }

        [HttpPost("add_student")]
        public async Task<ActionResult> AddStudent(AddStudentModel student)
        {
            var request = _mapper.Map<Student>(student);
            var reply = await _client.AddStudentAsync(request);

            return Ok();
        }

        [HttpGet("{course}/students")]
        public async Task<ActionResult<StudentModel[]>> GetStudentsByCourse(int course)
        {
            var request = new StudentByCourseRquest { Course = course };

            var reply = await _client.GetStudentsByCourseAsync(request);

            StudentModel[] result = _mapper.Map<StudentModel[]>(reply.Students);

            return Ok(result);
        }

        ~StudentController()
        {
            _channel.Dispose();
        }
    }
}
