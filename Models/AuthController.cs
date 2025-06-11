// using Microsoft.AspNetCore.Mvc;
// using FROASTERY.Models;
// using Microsoft.EntityFrameworkCore;
// using System.Threading.Tasks;
// using System.Security.Cryptography;
// using System.Text;
// using System;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;
// using System.Collections.Generic;
// using System.Security.Claims;
// @inject AppDbContext DbContext
// [ApiController]
// [Route("api/auth")]
// public class AuthController : ControllerBase
// {
//     private readonly AppDbContext _dbContext;
//     private readonly IConfiguration _configuration;

//     public AuthController(AppDbContext dbContext, IConfiguration configuration)
//     {
//         _dbContext = dbContext;
//         _configuration = configuration;
//     }

//     [HttpPost("login")]
//     public async Task<IActionResult> Login([FromBody] LoginModel model)
//     {
//         if (!ModelState.IsValid)
//         {
//             return BadRequest(ModelState);
//         }

//         var user = await _dbContext.NguoiDungs.FirstOrDefaultAsync(u => u.Email == model.Email);

//         if (user == null)
//         {
//             return Unauthorized(); // Người dùng không tồn tại
//         }

//         // Xác thực mật khẩu (sử dụng PasswordHelper bạn đã có)
//         if (!PasswordHelper.VerifyPassword(model.Password, user.MatKhau))
//         {
//             return Unauthorized(); // Mật khẩu không đúng
//         }

//         // Tạo JWT token
//         var token = GenerateJwtToken(user);

//         return Ok(new { Token = token });
//     }

//     private string GenerateJwtToken(NguoiDung user)
//     {
//         var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//         var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//         var claims = new List<Claim>
//         {
//             new Claim(ClaimTypes.NameIdentifier, user.MaNguoiDung.ToString()),
//             new Claim(ClaimTypes.Name, user.Email),
//             // Thêm các claims khác về người dùng (vai trò, quyền, ...) nếu cần
//         };

//         var tokenDescriptor = new SecurityTokenDescriptor
//         {
//             Subject = new ClaimsIdentity(claims),
//             Expires = DateTime.UtcNow.AddHours(2), // Thời gian hết hạn của token
//             SigningCredentials = credentials,
//             Issuer = _configuration["Jwt:Issuer"],
//             Audience = _configuration["Jwt:Audience"]
//         };

//         var tokenHandler = new JwtSecurityTokenHandler();
//         var token = tokenHandler.CreateToken(tokenDescriptor);

//         return tokenHandler.WriteToken(token);
//     }
// }

// // Helper class cho việc băm và xác minh mật khẩu (giả sử bạn đã có)
// public static class PasswordHelper
// {
//     public static string HashPassword(string password)
//     {
//         // Triển khai logic băm mật khẩu mạnh (ví dụ: Argon2, bcrypt)
//         // Đây chỉ là một ví dụ đơn giản và KHÔNG AN TOÀN cho môi trường production
//         using var sha256 = SHA256.Create();
//         var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//         return Convert.ToBase64String(hashedBytes);
//     }

//     public static bool VerifyPassword(string enteredPassword, string hashedPassword)
//     {
//         // Triển khai logic xác minh mật khẩu đã băm
//         // Cần đảm bảo logic này tương ứng với logic băm
//         using var sha256 = SHA256.Create();
//         var enteredHashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
//         return Convert.ToBase64String(enteredHashBytes) == hashedPassword;
//     }
// }