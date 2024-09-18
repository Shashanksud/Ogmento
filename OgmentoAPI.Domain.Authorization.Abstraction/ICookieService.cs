using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgmentoAPI.Domain.Authorization.Abstractions
{
    public interface ICookieService
    {
        void SetAuthToken(string token);
    }
}
