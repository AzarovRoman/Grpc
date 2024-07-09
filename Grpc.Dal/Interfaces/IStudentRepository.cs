using Grpc.Dal.Entities;

namespace Grpc.Dal.Interfaces
{
    public interface IStudentRepository
    {
        Task<StudentEntity> AddStudent(StudentEntity student);
        Task<List<StudentEntity>> GetStudentsByCourse(int course);
    }
}