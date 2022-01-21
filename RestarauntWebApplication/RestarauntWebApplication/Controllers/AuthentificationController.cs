using Microsoft.AspNetCore.Mvc;
using RestarauntWebApplication.Models;
using RestarauntWebApplication.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestarauntWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly RestarauntContext _context;
        public AuthentificationController(RestarauntContext context)
        {
            _context = context;
        }
        // GET: api/<AuthentificationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthentificationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthentificationController>
        [HttpPost]
        //ActionResult чтобы отслеживать данные, void не работает
        public ActionResult Post(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Workers.FirstOrDefault(p => p.WorkerLogin.Equals(loginModel.Login) && p.WorkerPassword.Equals(loginModel.Password));
                var a = user.WorkerPosition;
                if (user != null)
                {
                    return Ok(a);
                }
                else
                {
                    
                    ModelState.AddModelError("ErrorAuth", "Неверный логин или пароль");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<AuthentificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthentificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
