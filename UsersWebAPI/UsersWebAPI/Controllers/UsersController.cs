using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersWebAPI.Data;
using UsersWebAPI.Models;
using UsersWebAPI.Models.Domain;

namespace UsersWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AuthDBContext dBContext;

        public UsersController(AuthDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await dBContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            var user = await dBContext.Users.FindAsync(id);

            return (user == null) ? NotFound() : user;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> AddUser(AddRequestUserDTO request)
        {
            var domainModelUser = new User
            {
                Id = Guid.NewGuid(),
                firstName = request.firstName,
                lastName = request.lastName,
                email = request.email,
                phone = request.phone,
                role = request.role
            };

            dBContext.Users.Add(domainModelUser);
            await dBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = domainModelUser.Id }, domainModelUser);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            dBContext.Entry(user).State = EntityState.Modified;

            try
            {
                await dBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await dBContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            dBContext.Users.Remove(user);
            await dBContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id)
        {
            return dBContext.Users.Any(e => e.Id == id);
        }
    }
}
