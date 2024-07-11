using Grpc;
using Grpc.Net.Client;

var handler = new HttpClientHandler();
handler.ServerCertificateCustomValidationCallback =
    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

using var channel = GrpcChannel.ForAddress("https://localhost:7279", new GrpcChannelOptions { HttpHandler = handler});
var client = new GrpcService.GrpcServiceClient(channel);

await Test();

async Task Test()
{
    for (int i = 1; i < 11; i++)
    {
        var addStudentResponse = await client.AddStudentAsync(new Student
        {
            Name = $"StudentName_{i}",
            Course = i % 3
        });
    }

    var studentsResponse = await client.GetStudentsByCourseAsync(new StudentByCourseRquest
    {
        Course = 1
    });

    Console.WriteLine("Первокурсники:");
    foreach (var student in studentsResponse.Students)
    {
        Console.WriteLine($"Name: {student.Name}, Course: {student.Course}");
    }

    var studentsSecondResponse = await client.GetStudentsByCourseAsync(new StudentByCourseRquest
    {
        Course = 2
    });

    Console.WriteLine();
    Console.WriteLine("Второкурсники:");
    foreach (var student in studentsSecondResponse.Students)
    {
        Console.WriteLine($"Name: {student.Name}, Course: {student.Course}");
    }
}

async Task AddStudents()
{

   

}