using System;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HydraAPI.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly HydraContext _context;

    public RoleRepository(HydraContext context)
    {
        _context = context;
    }

    public void AddRole(Role role)
    {
        _context.Roles.Add(role);
        _context.SaveChanges();
    }

    public void DeleteRole(int id)
    {
        throw new NotImplementedException();
    }


    public List<Role> GetAllRole()
    {
        return _context.Roles.ToList();
    }

    public Role GetRoleById(int id)
    {
        return _context.Roles.Find(id) ?? throw new Exception("Role Not Found");
    }

    public Role GetRoleByUsername(string username)
    {
        return _context.Roles
            .Include(r => r.Usernames)
            .FirstOrDefault(r => r.Name == username) ?? throw new Exception("Role Not Found");
    }

    public void UpdateRole(Role role)
    {
        _context.Update(role);
        _context.SaveChanges();
    }
}
