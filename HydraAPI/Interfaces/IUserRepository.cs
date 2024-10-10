using System;
using HydraAPI.Models;

namespace HydraAPI.Interfaces;

public interface IUserRepository
{
    List<User> GetAll();
    User GetByUsername(string username);
    string GetRoleName(string username);
    void Register(User user);
    void Update(User user);
}
