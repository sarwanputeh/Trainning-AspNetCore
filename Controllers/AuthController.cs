using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using OrixNetCoreApp.Areas.Identity.Data;
using OrixNetCoreApp.ModelDto;

namespace OrixNetCoreApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment) { 
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }
        //ท่สวนลงทะเบียน
        //api/auth/register
        [HttpPost]
        //Register(RegisterDto registerDto)เพิ่มข้อมูลในตารางเมื่อลงทะเบียนสำเร็จ

        [RequestSizeLimit(10 * 1024 * 1024)] //10MB
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            //เช็คอีเมล์ซ่ำ
            var useEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (useEmail != null) return Conflict(new {Message = "อีเมลล์นี้มีผู้ใช้งานแล้ว"});
            
           
            
            
            //if (!ModelState)

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };


            //Upload photo
            if (registerDto.Photo != null)
            {
                var base64array = Convert.FromBase64String(registerDto.Photo);
                var newFileName = Guid.NewGuid().ToString() + ".png";
                //path for upload
                var uploadPath = Path.Combine($"{_webHostEnvironment.WebRootPath}/upload/{newFileName}");
                //upload file to path
                await System.IO.File.WriteAllBytesAsync(uploadPath, base64array);

                user.Photo = newFileName;
            }


            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "เกิดข้อผิดพลาด ลงทะเบียนไม่สำเร็จ" });
            }
            return Created("", new { message = "ลงทะเบียนสำเร็จ" });
        }
        //api/auth/Login
        [HttpPost]
        //Register(RegisterDto registerDto)เพิ่มข้อมูลในตารางเมื่อลงทะเบียนสำเร็จ
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            //เช็คอีเมล์ซ่ำ
            var useEmail = await _userManager.FindByEmailAsync(loginDto.Email);
            if (useEmail == null) return Conflict(new { Message = "ไม่พบอีเมลล์นี้ในระบบ" });




            //if (!ModelState)


            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { Message = "รหัสผานไม่ถูกต้อง" });
            }
            return Created("", new { message = "เข้าสู่ระบบสำเร็จ" });
        }
    }
}
