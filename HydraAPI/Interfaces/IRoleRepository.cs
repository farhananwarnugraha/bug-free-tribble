using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface IRoleRepository
{
    List<Role> GetAllRole();
    Role GetRoleById(int id);
    Role GetRoleByUsername(string username);
    void AddRole(Role role);
    void UpdateRole(Role role);
    void DeleteRole(int id);
}
