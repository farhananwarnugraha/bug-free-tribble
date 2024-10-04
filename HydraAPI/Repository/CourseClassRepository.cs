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
}
