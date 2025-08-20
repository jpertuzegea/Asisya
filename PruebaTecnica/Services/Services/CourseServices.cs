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
    public class CourseServices : ICourseServices
    {

        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitofwork;


        public CourseServices(IConfiguration _configuration, IUnitOfWork _unitofwork)
        {
            configuration = _configuration;
            unitofwork = _unitofwork;
        }

        /// <inheritdoc />
        public async Task<ResultModel<Course[]>> CourseList()
        {
            ResultModel<Course[]> ResultModel = new ResultModel<Course[]>();

            try
            {
                IEnumerable<Course> List = await unitofwork.GetRepository<Course>().Get();

                ResultModel.HasError = false;
                ResultModel.Messages = "Course Listados Con Exito";
                ResultModel.Data = List.ToArray();

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Listando Course";
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> CourseAdd(Course CourseModel)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();
            Course[] List = null;

            try
            {
                ResultModel<Course[]> Result = await CourseList();
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

                Course Course = List.FirstOrDefault(x => x.CourseName == CourseModel.CourseName);

                if (Course != null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Nombre ya Existe";
                    ResultModel.Data = null;
                    return ResultModel;
                }

                unitofwork.GetRepository<Course>().Add(CourseModel);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Course No Creado";
                    ResultModel.Data = null;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "Course Creado Con Exito";
                ResultModel.Data = null;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Guardar Course: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<Course>> GetCourseByCourseId(int Id)
        {
            ResultModel<Course> ResultModel = new ResultModel<Course>();

            try
            {
                var Course = (await unitofwork.GetRepository<Course>().Get(x => x.CourseId == Id)).FirstOrDefault();

                ResultModel.HasError = false;
                ResultModel.Messages = "Course Encontrado Con Exito";
                ResultModel.Data = Course;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Obteniendo Course";
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> CourseUpdate(Course CourseModel)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();

            try
            {
                ResultModel<Course> Result = await GetCourseByCourseId((int)CourseModel.CourseId);
                Course Course;

                if (!Result.HasError)
                {
                    Course = Result.Data;

                    if (Course != null)
                    {
                        Course.CourseId = CourseModel.CourseId;
                        Course.CourseName = CourseModel.CourseName;
                        Course.Description = CourseModel.Description;
                        Course.TeacherId = CourseModel.TeacherId;

                        unitofwork.GetRepository<Course>().Update(Course);

                        if (!unitofwork.SaveChanges())
                        {
                            ResultModel.HasError = true;
                            ResultModel.Messages = "Course No Modificado";
                            ResultModel.Data = null;
                            return ResultModel;
                        }

                        ResultModel.HasError = false;
                        ResultModel.Messages = "Course Modificar Con Exito";
                        ResultModel.Data = null;

                        return ResultModel;

                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Course NO Encontrado";
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
                ResultModel.Messages = $"Error Técnico Al Modificar Course: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

        /// <inheritdoc />
        public async Task<ResultModel<string>> CourseDelete(int CourseId)
        {
            ResultModel<string> ResultModel = new ResultModel<string>();

            try
            {
                ResultModel<Course> Result = await GetCourseByCourseId(CourseId);
                Course Course;

                if (!Result.HasError)
                {
                    Course = Result.Data;

                    if (Course != null)
                    {
                        unitofwork.GetRepository<Course>().Remove(Course);

                        if (!unitofwork.SaveChanges())
                        {
                            ResultModel.HasError = true;
                            ResultModel.Messages = "Course No Eliminado";
                            ResultModel.Data = null;
                            return ResultModel;
                        }

                        ResultModel.HasError = false;
                        ResultModel.Messages = "Course Eliminado Con Exito";
                        ResultModel.Data = null;

                        return ResultModel;

                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "Course NO Encontrado";
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
                ResultModel.Messages = $"Error Técnico Al Eliminar Course: {Error.Message}";
                ResultModel.Data = null;
                ResultModel.ExceptionMessage = Error.ToString();

                return ResultModel;
            }
        }

         
    }
}
