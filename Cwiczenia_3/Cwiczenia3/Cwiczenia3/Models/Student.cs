using Cwiczenia3.Models.CustomExceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cwiczenia3.Models
{
    [Serializable]
    public class Student
    {
        public const int StudentNumOfFields = 9;
        public const int StudentIndexNumberPosition = 4;

        public static List<Student> StudentsExtension = new List<Student>();

        #region arrtibutes
        [Required(ErrorMessage = "Imie jest wymagane")]
        [JsonProperty("IMIE")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [JsonProperty("NAZWISKO")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Kierunek jest wymagany")]
        [JsonProperty("KIERUNEK")]
        public string StudiesName { get; set; }

        [Required(ErrorMessage = "Tryb jest wymagany")]
        [JsonProperty("TRYB")]
        public string StudiesMode { get; set; }

        [Required(ErrorMessage = "Numer indeksu jest wymagany")]
        [JsonProperty("NUMER_INDEKSU")]
        public int IndexNumber { get; set; }

        [Required(ErrorMessage = "Data urodzenia jest wymagana")]
        [JsonProperty("DATA_URODZENIA")]
        public string BirthDate { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$",
            ErrorMessage = "Wrong e-mail adress input")]
        [Required(ErrorMessage = "Adres email jest wymagany")]
        [JsonProperty("ADRES_EMAIL")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Imie Matki jest wymagane")]
        [JsonProperty("IMIE_MATKI")]
        public string MotherFirstName { get; set; }

        [Required(ErrorMessage = "Imie Ojca jest wymagane")]
        [JsonProperty("IMIE_OJCA")]
        public string FatherFirstName { get; set; }
        #endregion attributes

        public static void RecreateStudentExtension(List<string[]> convertedDataSet)
        {
            if (convertedDataSet.Count == 0)
            {
                return;
            }

           foreach(var dataSet in convertedDataSet)
            {
                try
                {
                    Student.CheckDataToRecreateStudent(dataSet);
                    Student.CheckIndexNumForDuplication((string)dataSet.GetValue(StudentIndexNumberPosition));
                }
                //temp solution, in current assigment there is no need to log exception in extension recreation process.
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                Student student = Student.RecreateStudentFromTable(dataSet);

                Student.AddStudentToExtension(student);
            }


        }
        public static Student RecreateStudentFromTable(string[] fieldsWithDataToRecreate)
        {

            Student student = null;
           
                student = new Student
                {
                    FirstName = fieldsWithDataToRecreate[0],
                    LastName = fieldsWithDataToRecreate[1],
                    StudiesName = fieldsWithDataToRecreate[2],
                    StudiesMode = fieldsWithDataToRecreate[3],
                    IndexNumber = int.Parse(fieldsWithDataToRecreate[StudentIndexNumberPosition]),
                    BirthDate = fieldsWithDataToRecreate[5],
                    EmailAddress = fieldsWithDataToRecreate[6],
                    MotherFirstName = fieldsWithDataToRecreate[7],
                    FatherFirstName = fieldsWithDataToRecreate[8]
                };
            

            return student;
        }
        public static void CheckDataToRecreateStudent(string[] dataset)
        {

            if (dataset.Length != StudentNumOfFields)
            {
                throw new NotEnoughDataException();
            }

            bool containsEmptyValues = false;
            foreach (string field in dataset)
            {
                if (field.Equals(""))
                {
                    containsEmptyValues = true;
                }
            }

            if (containsEmptyValues)
            {
                throw new NotEnoughDataException();
            }

        }
        public static void CheckIndexNumForDuplication(string indexNumber)
        {
            if (StudentsExtension != null && StudentsExtension.Count != 0)
            {
                if (StudentsExtension.Exists(stud => stud.IndexNumber == int.Parse(indexNumber)))
                {
                    throw new RecordDuplicationException();
                }
            }
        }
        public static void CheckIndexNumForDuplication(int indexNumber)
        {
            if (StudentsExtension != null && StudentsExtension.Count != 0)
            {
                if (StudentsExtension.Exists(stud => stud.IndexNumber == indexNumber))
                {
                    throw new RecordDuplicationException();
                }
            }
        }
        public static void AddStudentToExtension(Student student)
        {
            StudentsExtension.Add(student);
        }
        public static void RemoveStudentFromExtension(Student student)
        {
            StudentsExtension.Remove(student);
        }
        public static List<Student> GetStudentsExtension()
        {
            return StudentsExtension;
        }
        public static bool IsStundentUnderGivenIndexExtist(int indexNumber)
        {
            return StudentsExtension.Exists(stud => stud.IndexNumber == indexNumber);
        }
        public string GetStudentDataInCSVFormat()
        {
            return $"{FirstName},{LastName},{StudiesName},{StudiesMode},{IndexNumber},{BirthDate},{EmailAddress},{MotherFirstName},{FatherFirstName}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            
            Student temp = obj as Student;
            return (
            FirstName.Equals(temp.FirstName) &&
            LastName.Equals(temp.LastName) &&
            StudiesName.Equals(temp.StudiesName) &&
            StudiesMode.Equals(temp.StudiesMode) &&
            IndexNumber == temp.IndexNumber &&
            BirthDate.Equals(temp.BirthDate) &&
            EmailAddress.Equals(temp.EmailAddress) &&
            MotherFirstName.Equals(temp.MotherFirstName) &&
            FatherFirstName.Equals(temp.FatherFirstName)
                );
            
        }
    }


}
