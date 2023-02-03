using Cwiczenia3.Models;
using Cwiczenia3.Models.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Cwiczenia3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(Student.GetStudentsExtension());
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudents(int indexNumber) 
        {
            
            if (!Student.IsStundentUnderGivenIndexExtist(indexNumber))
            {
                return StatusCode(400);
            }
            else
            {
                return Ok(Student.GetStudentsExtension().Find(stud => stud.IndexNumber == indexNumber));
            }

        }

        [HttpPost]
        public IActionResult InsertStudent(Student student)
        {

            try
            {
                Student.CheckIndexNumForDuplication(student.IndexNumber.ToString());
                Student.AddStudentToExtension(student);
                SaveStudentToCSVFile(student, Program.DataSetLocation);
                return Ok(student);

            }
            catch (RecordDuplicationException r)
            {
                Console.WriteLine(r.Message);
                return StatusCode(400);
            }

        }

        [HttpPut("{indexNumber}")]
        public IActionResult UpdateStudent(Student student)
        {
            Student tempStudent = UpdateStudentData(student, Program.DataSetLocation);
            if (tempStudent == null)
            {
                return StatusCode(400);
            }
            else
            {
                return Ok(tempStudent);
            }
        }

        [HttpDelete("{indexNumber}")]
        public IActionResult DeleteStudent(int indexNumber)
        {
            //Do nothing if provided student does not exist
            if (!Student.IsStundentUnderGivenIndexExtist(indexNumber))
            {
                return StatusCode(400);
            }
            

            Student studentForDeletion = Student.GetStudentsExtension().Find(stud => stud.IndexNumber == indexNumber);

            string text = System.IO.File.ReadAllText(Program.DataSetLocation);
            string updatedCSVData = "";
            //create new csv file with updated data
            using (StreamReader sr = new StreamReader(Program.DataSetLocation))
            {

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    if (!studentForDeletion.Equals(Student.RecreateStudentFromTable(fields)))
                    {
                        updatedCSVData += line + "\n";
                    }
                   
                }
            }
            //delete student from extension
            Student.RemoveStudentFromExtension(studentForDeletion);

            //save file as ..New.csv
            System.IO.File.WriteAllText("DATA_SET/daneNew.csv", updatedCSVData);

            //replace orginal data with updated, delete temporary new file
            try
            {
                System.IO.File.Replace("DATA_SET/daneNew.csv", Program.DataSetLocation, null);
                System.IO.File.Delete("DATA_SET/daneNew.csv");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Ok($"Student with index number: {indexNumber} successfully deleted");
        }

        private void SaveStudentToCSVFile(Student student, string DataSetLocation)
        {
            if (System.IO.File.Exists(DataSetLocation))
            {
                using (StreamWriter streamWriter = System.IO.File.AppendText(DataSetLocation))
                {
                    streamWriter.WriteLine(student.GetStudentDataInCSVFormat());
                }
            }
        }

        private Student UpdateStudentData(Student student, string DataSetPath) //attributes validation delegated do HttpPut method
        {
            //Do nothing if provided student does not exist
            if (!Student.IsStundentUnderGivenIndexExtist(student.IndexNumber))
            {
                return null;
            }

            Student studentFound = Student.GetStudentsExtension().Find(stud => stud.IndexNumber == student.IndexNumber);

            string text = System.IO.File.ReadAllText(DataSetPath);
            string updatedCSVData = "";
            //create new csv file with updated data
            using (StreamReader sr = new StreamReader(DataSetPath))
            {

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    if (studentFound.Equals(Student.RecreateStudentFromTable(fields)))
                    {
                        updatedCSVData += student.GetStudentDataInCSVFormat() + "\n";
                    }
                    else
                    {
                        updatedCSVData += line + "\n";
                    }
                }
            }
            //save file as ..New.csv
            System.IO.File.WriteAllText("DATA_SET/daneNew.csv", updatedCSVData);

            //replace orginal data with updated, delete temporary new file
            try
            {
                System.IO.File.Replace("DATA_SET/daneNew.csv", DataSetPath, null);
                System.IO.File.Delete("DATA_SET/daneNew.csv");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //update student data and return it
            studentFound.FirstName = student.FirstName;
            studentFound.LastName = student.LastName;
            studentFound.StudiesName = student.StudiesName;
            studentFound.StudiesMode = student.StudiesMode;
            studentFound.BirthDate = student.BirthDate;
            studentFound.EmailAddress = student.EmailAddress;
            studentFound.MotherFirstName = student.MotherFirstName;
            studentFound.FatherFirstName = student.FatherFirstName;

            return studentFound;

        }

        
    }
}
