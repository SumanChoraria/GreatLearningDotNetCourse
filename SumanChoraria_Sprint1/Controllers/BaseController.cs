using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SumanChoraria_Sprint1.Models;
using SumanChoraria_Sprint1.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SumanChoraria_Sprint1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController<T> : ControllerBase where T : BaseModel
    {
        private readonly IBaseRepository<T> _baseRepository;
        
        public BaseController(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

       
        [HttpPost]
        [Route("Create")]

        public virtual IActionResult Create(T entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest();

                if (_baseRepository.Create(entity))
                {
                    return Ok(new
                    {
                        message = "A new entity has been created"
                    });
                }
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }

       
        [HttpPut]
        [Route("Update")]

        public virtual IActionResult Update(int id, T entity)
        {
            try
            {
                if (id != entity.Id)
                    return BadRequest();

                if (_baseRepository.Update(id, entity))
                {
                    return Ok(new
                    {
                        message = "An existing entity has been updated"
                    });
                }
            }
            catch (Exception)
            {

            }
            return BadRequest();
        }

        
        [HttpGet]
        [Route("GetAll")]

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return _baseRepository.GetAll().ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }

       
        [HttpGet]
        [Route("GetById")]

        public virtual IActionResult GetById(int id)
        {
            try
            {
                var entity = _baseRepository.GetById(id);
                if (entity == null)
                    return NotFound();

                return Ok(entity);
            }
            catch (Exception)
            {

            }
            return NotFound();
        }
    }
}
