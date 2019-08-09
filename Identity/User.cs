using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.WebAPI.Identity
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName="nvarcher(150)")]
        public string FullName { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}