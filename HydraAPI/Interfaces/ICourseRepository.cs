using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface ICourseRepository
{
    List<Course> GetSchedule(int boootcampClassId);
    List<Course> GetAllCourse();
    Course GetCourse(string courseId);
    void Add(Course course);
    void Update(Course course);
    void Delete(Course course);
}
