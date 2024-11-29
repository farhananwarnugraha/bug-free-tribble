using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface IBootcampClass
{
    List<BootcampClass> Get();
    List<BootcampClass> Get(int pageNumber, int pageSize, int batchBootcamp, string bootcampName);
    List<BootcampClass> BetBootcampPlaned(int pageNumber, int pageSize, int batchBootcamp, string bootcampName);
    List<BootcampClass> GetBootcampPlaned();
    BootcampClass GetBootcampPlaned(int batchBootcamp);
    List<BootcampClass> GetBootcampActive(int pageNumber, int pageSize, int batchBootcamp, string bootcampName);
    List<BootcampClass> GetBootcampCompleted(int pageNumber, int pageSize, int batchBootcamp, string bootcampName);
    BootcampClass GetDetailBootcamp(int batchBootcamp);
    BootcampClass Get(int bootcampId);
    int Count(int batchBootcamp, string bootcampName);
    int CountBootcampActive(int batchBootcamp, string bootcampDescription);
    int CountBootcampPlaned(int batchBootacamp, string bootcampDescription);
    int CountBootcampCompleted(int batchBootacamp, string bootcampDescription);
    void Insert(BootcampClass bootcampClass);
    void Update(BootcampClass bootcampClass);
    void Delete(int id);
}
