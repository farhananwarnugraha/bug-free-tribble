using System;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraAPI.Repository;

public class CourseClassRepository : ICourseRepository
{
    private readonly HydraContext _dbContext;

    public CourseClassRepository(HydraContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Course course)
    {
        _dbContext.Add(course);
        _dbContext.SaveChanges();
    }

    public void Delete(Course course)
    {
        throw new NotImplementedException();
    }

    public List<Course> GetAllCourse()
    {
        throw new NotImplementedException();
    }

    public Course GetCourse(string courseId)
    {
        return _dbContext.Courses.FirstOrDefault(c => c.Id == courseId) ?? throw new Exception("Course Not Found");
    }

    public List<Course> GetSchedule(int boootcampClassId)
    {
        return _dbContext.Courses
        .Include(c => c.TrainerSkillDetail)
            .ThenInclude(td => td.Trainer)
        .Include(c => c.TrainerSkillDetail)
            .ThenInclude(td => td.Skill)
        .Where(
            schedule => schedule.BootcampClassId == boootcampClassId
        )
        .ToList();
    }

    public void Update(Course course)
    {
        _dbContext.Update(course);
        _dbContext.SaveChanges();
    }
}
