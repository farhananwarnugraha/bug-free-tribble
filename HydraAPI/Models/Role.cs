using System;
using System.Collections.Generic;

namespace HydraAPI.Models
{
    public partial class Role
    {
        public Role()
        {
            Usernames = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<User> Usernames { get; set; }
    }
}
