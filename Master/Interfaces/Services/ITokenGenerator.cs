using System;

using Master.Models;
using Master.Interfaces.Models;

namespace Master.Interfaces.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(IUser user);
    }
}