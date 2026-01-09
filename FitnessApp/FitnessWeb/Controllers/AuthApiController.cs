using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FitnessWeb.Models;
using FitnessWeb.Data;
using FitnessWeb.DTOs;

namespace FitnessWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly FitnessContext _context;

        public AuthApiController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, FitnessContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Member");

                var member = new Member
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Phone = model.Phone
                };
                _context.Member.Add(member);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Registration successful" });
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (!await _userManager.IsInRoleAsync(user, "Member"))
                {
                    await _signInManager.SignOutAsync();
                    return Unauthorized("Acces permis doar clienților! Adminii și Antrenorii trebuie să folosească site-ul Web.");
                }

                return Ok(new { message = "Login successful" });
            }

            return Unauthorized("Email sau parolă greșită.");
        }
    }
}