using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface ICourseRepository
{
    List<Course> GetSchedule(int boootcampClassId);
}
