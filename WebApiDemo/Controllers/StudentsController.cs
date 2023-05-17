using StudentDataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace WebApiDemo.Controllers
{
    public class StudentsController : ApiController
    {
        public IEnumerable<Student> Get()
        {
            using (UserDetailsEntities entities = new UserDetailsEntities())
            {
                
                return entities.Students.ToList();
            }
        }
        public Student Get(int id) {
            using(UserDetailsEntities entities = new UserDetailsEntities())
            {
                return entities.Students.FirstOrDefault(e=> e.Id == id);
            } 
        }

        public HttpResponseMessage Post([FromBody] Student student)
        {
            try
            {
                using (UserDetailsEntities entity = new UserDetailsEntities())
                {
                    entity.Students.Add(student);
                    entity.SaveChanges();
                }
                var message = Request.CreateResponse(HttpStatusCode.Created, student);
                message.Headers.Location = new Uri(Request.RequestUri + student.Id.ToString());
                return message;
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (
                    
                    
                    
                    
                    UserDetailsEntities entities = new UserDetailsEntities())
                {
                    var student = entities.Students.FirstOrDefault(e => e.Id == id);
                    if (student == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Student with ID= " + id + " not found.");
                    }
                    else
                    {
                        entities.Students.Remove(student);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Done");

                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ex);
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Student student) {
            try
            {
                using (UserDetailsEntities entities = new UserDetailsEntities())
                {
                    var stu = entities.Students.FirstOrDefault(e => e.Id == id);
                    if (stu == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Student with ID= " + id + " not found.");
                    }
                    else
                    {
                        //stu.Id = student.Id;
                        stu.StudentName = student.StudentName;
                        stu.Gender = student.Gender;
                        stu.Course = student.Course;
                        stu.Age = student.Age;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Update Mudinchu");
                    }
                }
            } 
            catch (Exception ex) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
