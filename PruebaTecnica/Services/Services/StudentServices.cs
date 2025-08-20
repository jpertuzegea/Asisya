//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Agosto 2025</date>
//-----------------------------------------------------------------------

using Infraestructure.Entities;
using Infraestructure.Interfaces;
using Infraestructure.Models;
using Interfaces.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Services.Services
{
    public class StudentServices : IStudentServices
    {

        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitofwork;


        public StudentServices(IConfiguration _configuration, IUnitOfWork _unitofwork)
        {
            configuration = _configuration;
            unitofwork = _unitofwork;
        }

        /// <inheritdoc />
        public async Task<ResultModel<Student[]>> StudentList()
        {
            ResultModel<Student[]> ResultModel = new ResultModel<Student[]>();

            try
            {
                IEnumerable<Student> List = await unitofwork.GetRepository<Student>().Get();

                ResultModel.HasError = false;
                ResultModel.Messages = "Student Listados Con Exito";
                ResultModel.Data = List.ToArray();

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Listando Student";
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> StudentAdd(Student StudentModel)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();
            Student[] List = null;

            try
            {
                ResultModel<Student[]> Result = await StudentList();
                if (!Result.HasError)
                {
                    List = Result.Data;
                }
                else
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = Result.Messages;
                    ResultModel.Data = Result.ExceptionMessage;
                    return ResultModel;
                }

                Student Student = List.FirstOrDefault(x => x.IdentificationNumber == StudentModel.IdentificationNumber);

                if (Student != null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Identificacion ya Existe";
                    ResultModel.Data = null;
                    return ResultModel;
                }

                unitofwork.GetRepository<Student>().Add(StudentModel);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Student No Creado";
                    ResultModel.Data = null;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "Student Creado Con Exito";
                ResultModel.Data = null;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Guardar Student: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<Student>> GetStudentByStudentId(int Id)
        {
            ResultModel<Student> ResultModel = new ResultModel<Student>();

            try
            {
                var Student = (await unitofwork.GetRepository<Student>().Get(x => x.StudentId == Id)).FirstOrDefault();

                ResultModel.HasError = false;
                ResultModel.Messages = "Student Encontrado Con Exito";
                ResultModel.Data = Student;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Obteniendo Student";
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> StudentUpdate(Student StudentModel)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();

            try
            {
                ResultModel<Student> Result = await GetStudentByStudentId((int)StudentModel.StudentId);
                Student Student;

                if (!Result.HasError)
                {
                    Student = Result.Data;

                    if (Student != null)
                    {
                        Student.IdentificationNumber = StudentModel.IdentificationNumber;
                        Student.FirstName = StudentModel.FirstName;
                        Student.LastName = StudentModel.LastName;
                        Student.BirthDate = StudentModel.BirthDate;
                        Student.Email = StudentModel.Email;

                        unitofwork.GetRepository<Student>().Update(Student);

                        if (!unitofwork.SaveChanges())
                        {
                            ResultModel.HasError = true;
                            ResultModel.Messages = "Student No Modificar";
                            ResultModel.Data = null;
                            return ResultModel;
                        }

                        ResultModel.HasError = false;
                        ResultModel.Messages = "Student Modificar Con Exito";
                        ResultModel.Data = null;

                        return ResultModel;

                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Student NO Encontrado";
                        ResultModel.Data = null;

                        return ResultModel;
                    }

                }
                else
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = Result.Messages;
                    ResultModel.Data = Result.ExceptionMessage;
                    return ResultModel;
                }
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Modificar Student: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> StudentDelete(int StudentId)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();

            try
            {
                ResultModel<Student> Result = await GetStudentByStudentId(StudentId);
                Student Student;

                if (!Result.HasError)
                {
                    Student = Result.Data;

                    if (Student != null)
                    {
                        unitofwork.GetRepository<Student>().Remove(Student);

                        if (!unitofwork.SaveChanges())
                        {
                            ResultModel.HasError = true;
                            ResultModel.Messages = "Student No Eliminado";
                            ResultModel.Data = null;
                            return ResultModel;
                        }

                        ResultModel.HasError = false;
                        ResultModel.Messages = "Student Eliminado Con Exito";
                        ResultModel.Data = null;

                        return ResultModel;

                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Student NO Encontrado";
                        ResultModel.Data = null;

                        return ResultModel;
                    }

                }
                else
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = Result.Messages;
                    ResultModel.Data = Result.ExceptionMessage;
                    return ResultModel;
                }
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Eliminar Student: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }




    }
}
