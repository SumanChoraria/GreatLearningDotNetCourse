using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SumanChoraria_Sprint1.Models;
using SumanChoraria_Sprint1.Models.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SumanChoraria_Sprint1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserModel>
    {
        public UserController(IUserRepository userRepository) : base(userRepository)
        {

        }
    }
}
