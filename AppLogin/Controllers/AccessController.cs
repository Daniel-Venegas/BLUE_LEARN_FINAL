using AppLogin.Context;
using AppLogin.Model;
using AppLogin.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AppLogin.Controllers
{
    public class AccessController : Controller
    {
        private readonly BLDbContext _dbContext;
        public AccessController(BLDbContext bLDbContext)
        {
            _dbContext = bLDbContext;

        }

        private string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir el password en un array de bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                // Calcular el hash
                byte[] hash = sha256.ComputeHash(bytes);
                // Convertir el hash en una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AgricultoresVM modelo)
        {
            if (modelo.password != modelo.ConfirmPassword)
            {
                string encryptedPassword = EncryptPassword(modelo.password);
                ViewData["Mensaje"] = "Las contraseñas no coincides";
                return View();
            }

            Agricultores agricultores = new Agricultores()
            {
                IdJugador = modelo.IdJugador,
                Nombres = modelo.Nombres,
                Apellidos = modelo.Apellidos,
                Direccion = modelo.Direccion,
                Contacto = modelo.Contacto,
                password = modelo.password
            };
            await _dbContext.Agricultores.AddAsync(agricultores);
            await _dbContext.SaveChangesAsync();

            if (agricultores.IdAgricultor != 0)
            {
                return RedirectToAction("login", "Access");
            }
            ViewData["Mensaje"] = "Error: No se pudo crear el usuario";

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        /*public async Task<IActionResult> Login(LoginVM modelo)
        {
            Agricultores? agricultoresToFind = await _dbContext.Agricultores
                                              .Where(a =>
                                              a.Contacto == modelo.Contacto &&
                                              a.password == modelo.password
                                              ).FirstOrDefaultAsync();

            if (agricultoresToFind == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, agricultoresToFind.Nombres)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );
            return RedirectToAction("Index", "Home");
        }*/

        public async Task<IActionResult> Login(LoginVM modelo)
        {
            string encryptedPassword = EncryptPassword(modelo.password);
            Agricultores agricultorToFind = await _dbContext.Agricultores
                                                .Where(a =>
                                                a.Contacto == modelo.Contacto &&
                                                a.password == modelo.password)
                                                .FirstOrDefaultAsync();

            if (agricultorToFind == null || agricultorToFind.Eliminado)
            {
                ViewData["Mensaje"] = "Los datos son incorrectos";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, agricultorToFind.Nombres)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );
            return RedirectToAction("Index", "Home");
        }

    }
}
