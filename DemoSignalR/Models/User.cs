using System;
using System.Collections.Generic;

#nullable disable

namespace DemoSignalR.Models
{
    public partial class User
    {
        public User()
        {
            CaBenhs = new HashSet<CaBenh>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CaBenh> CaBenhs { get; set; }
    }
}
