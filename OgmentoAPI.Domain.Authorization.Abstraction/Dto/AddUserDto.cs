using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Authorization.Abstractions.Dto
{
    public class AddUserDto:UserDetailsDto
    {
        public string Password { get; set; }
    }
}
