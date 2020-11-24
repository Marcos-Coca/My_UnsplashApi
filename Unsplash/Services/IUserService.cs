using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unsplash.Models.Request;
using Unsplash.Models.ViewModels;

namespace Unsplash.Services
{
    public interface IUserService
    {
        UserViewModel Auth(AuthRequest user);
    }
}
