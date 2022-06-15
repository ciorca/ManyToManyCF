﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManyToManyCF
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmployeeDBContext employeeDBContext = new EmployeeDBContext();
            GridView1.DataSource = (from student in employeeDBContext.Students
                                    from course in student.Courses
                                    select new
                                    {
                                        StudentName = student.StudentName,
                                        CourseName = course.CourseName
                                    }).ToList();
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            EmployeeDBContext employeeDBContext = new EmployeeDBContext();
            Course WCFCourse = employeeDBContext.Courses.FirstOrDefault(x => x.CourseID == 4);
            employeeDBContext.Students.Include("Courses").FirstOrDefault(x => x.StudentID == 1).Courses.Add(WCFCourse);
            employeeDBContext.SaveChanges();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            EmployeeDBContext employeeDBContext = new EmployeeDBContext();
            Course SQLServerCourse = employeeDBContext.Courses.FirstOrDefault(x => x.CourseID == 3);
            employeeDBContext.Students.Include("Courses").FirstOrDefault(x => x.StudentID == 2).Courses.Remove(SQLServerCourse);
            employeeDBContext.SaveChanges();
        }
    }
}