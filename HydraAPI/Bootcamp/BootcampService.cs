using System;
using System.Globalization;
using HydraAPI.Bootcamp.DTO;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using HydraAPI.Shared;
using HydraAPI.Shared.Enum;

namespace HydraAPI.Bootcamp;

public class BootcampService
{
    private readonly IBootcampClass _bootcampClassRepository;

    public BootcampService(IBootcampClass bootcampClassRepository)
    {
        _bootcampClassRepository = bootcampClassRepository;
    }

    public List<BootcampDTO> Get() => 
        _bootcampClassRepository.Get()
        .Select(
            static bootcamp => new BootcampDTO(){
                BootcampId = bootcamp.Id,
                Description = bootcamp.Description??"Not Available",
                StartDate = bootcamp.StartDate.ToString("yyyy-MM-dd"),
                EndDate = bootcamp.EndDate?.ToString("yyyy-MM-dd"),
            }
        ).ToList();

    public BootcampPageDTO Get(int pageNumber, int pageSize, int batchBootcamp, string bootcampName){
        var model = _bootcampClassRepository.Get(pageNumber, pageSize, batchBootcamp, bootcampName)
            .Select(
                bootcamp => new BootcampDTO(){
                 BootcampId = bootcamp.Id,
                 Description = bootcamp.Description??"Not Set",
                 StartDate = bootcamp.StartDate.ToString("yyyy-MM-dd"),
                 EndDate = bootcamp.EndDate?.ToString("yyyy-MM-dd"),
                }
            );
        return new BootcampPageDTO(){
            BootcampClasses = model.ToList(),
            Pagination = new PaginationDTO(){
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _bootcampClassRepository.Count(batchBootcamp, bootcampName)
            }
        };
    }

    public BootcampDTO Get(int id){
        var model = _bootcampClassRepository.Get(id);
        return new BootcampDTO(){
            BootcampId = model.Id,
            Description = model.Description??"Not Set",
            StartDate = model.StartDate.ToString("yyyy-MM-dd"),
            EndDate = model.EndDate?.ToString("yyyy-MM-dd"),
        };
    }

    public void Insert(BootcampInsertDTO request) => 
        _bootcampClassRepository.Insert(new BootcampClass{
          Description = request.Description,
          StartDate = request.StartDate,
          EndDate = request.EndDate  
        });
}
