// See https://aka.ms/new-console-template for more information
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Course.Protobuf.Test;

Console.WriteLine("Welcome to protobuf test");

var emp = new Employee();

emp.FirstName = "Zyad";

emp.LastName = "Elhad";

emp.IsRetired = false;

var birthdate = new DateTime(2000, 07, 20);

birthdate = DateTime.SpecifyKind(birthdate, DateTimeKind.Utc);

emp.BirthDate = Timestamp.FromDateTime(birthdate);

emp.MaritalStatus = Employee.Types.MaritalStatus.Married;

emp.PreviousEmployers.Add("Halan");
emp.PreviousEmployers.Add("Dsquares");

using (var output = File.Create("emp.dat"))
{
    emp.WriteTo(output);
}

Employee empfromFile;

using (var input = File.OpenRead("emp.dat"))
{
    empfromFile = Employee.Parser.ParseFrom(input);
}

Console.WriteLine(empfromFile.FirstName);

Console.WriteLine("Protobuf test complete :) ");
