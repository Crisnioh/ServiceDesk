using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ServiceDesk.Models;


namespace ServiceDesk.Controllers
{
    public class LoginController : Controller
    {

        private TicketsContext context = new TicketsContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Usuario y contraseña son obligatorios.";
                return View();
            }

            // Generar el hash de la contraseña ingresada
            var hashedPassword = HashPassword(password);

            // Imprimir valores para depuración
            System.Diagnostics.Debug.WriteLine($"Username ingresado: {email}");
            System.Diagnostics.Debug.WriteLine($"Hash generado: {hashedPassword}");

            // Buscar el usuario por email y hash de contraseña
            var user = context.Usuario.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);

            if (user != null)
            {
                System.Diagnostics.Debug.WriteLine($"Usuario encontrado: {user.Email}");
                // Iniciar sesión
                Session["UserId"] = user.Id;
                Session["Username"] = user.Email;
                return RedirectToAction("Index", "Home");
            }

            System.Diagnostics.Debug.WriteLine("Usuario no encontrado o contraseña incorrecta.");
            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario user)
        {
            if (ModelState.IsValid)
            {
                user.Password = HashPassword(user.Password);
                System.Diagnostics.Debug.WriteLine("Hash en registro: " + user.Password);
                context.Usuario.Add(user);
                context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }
        private string HashPassword(string password)
        {
            // Validar si la contraseña es nula o vacía
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("La contraseña no puede ser nula o vacía.");
            }

            // Crear una instancia de SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir la contraseña en un arreglo de bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Computar el hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower(); // Formato hexadecimal en minúsculas
            }
        }

    }
}