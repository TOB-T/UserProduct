using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProductAPI.Core.Entities;

namespace UserProductAPI.Infrastructure.Interface
{
    public interface ITokenInterface
    {
        string GenerateToken(User user);
        bool ValidateToken(string token, out User user);
    }
}
