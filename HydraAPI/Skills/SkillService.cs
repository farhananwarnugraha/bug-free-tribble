using System;
using HydraAPI.Interfaces;
using HydraAPI.Skills.DTO;

namespace HydraAPI.Skills;

public class SkillService
{
    private readonly ISkillRepository skillRepository;

    public SkillService(ISkillRepository skillRepository)
    {
        this.skillRepository = skillRepository;
    }

    public List<SkillDTO> Get() {
        var model = skillRepository.GetAll()
        .Select(skill => new SkillDTO(){
            SkillId = skill.Id,
            SkillName = skill.Name
        });
        return model.ToList();
    }
}
