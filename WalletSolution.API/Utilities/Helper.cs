using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WalletSolution.API.Models;
using WalletSolution.Common.General;

namespace WalletSolution.API.Utilities;
public class Helper
{
    public static string GenerateToken(UserDto userDto, JwtSettings settings)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
        var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userDto.Email),
            new Claim(ClaimTypes.Email, userDto.Email),
            new Claim(ClaimTypes.Role, userDto.Roles[0])
        };       

        var token = new JwtSecurityToken(
            settings.Issuer,
            settings.Audience,
            claims,
            expires:  DateTime.Now.AddMinutes(60),
            signingCredentials: credential);
        return new JwtSecurityTokenHandler().WriteToken(token);        
    }

    public static string GetFileName(string fileName)
    {
        fileName = Path.GetFileName(fileName);
        return string.Concat(Path.GetFileNameWithoutExtension(fileName),"_",Guid.NewGuid().ToString().AsSpan(0,4),Path.GetExtension(fileName));
    }
    public static bool IsValidType(IFormFile file)
    {
        var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        string[] allowedExtension = { "png", "jpg", "jpeg" };
        return allowedExtension.Contains(extension);
    }
    public static string WriteFile(IFormFile file)
    {
        string filename = "";
        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        filename = DateTime.Now.Ticks.ToString() + extension;

        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
        using (var stream = new FileStream(exactpath, FileMode.Create))
        {
             file.CopyTo(stream);
        }
        return filename;
    }
}
