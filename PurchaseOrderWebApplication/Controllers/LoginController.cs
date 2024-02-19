using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PurchaseOrderWebApplication.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace PurchaseOrderWebApplication.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        string connectionString = "Server=192.168.0.23,1427;Initial Catalog=interns;Integrated Security=False;user id=Interns;password=test;Connection Timeout=10;";
        [HttpGet]
        public   IActionResult Login()
        {
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            string? username = loginUser.username;
            string? passwd = loginUser.passwd;
            var displayTable = "select * from loginuser where username = @param_username and passwd = @param_passwd";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var loginuserValue = await connection.QueryFirstOrDefaultAsync<LoginUser>(displayTable, new { param_username = username, param_passwd = passwd });
                if (loginuserValue != null)
                {
                    string? TableName = loginuserValue.TableAccessName;
                    if (TableName != "PurchaseOrder")
                    {
                        ViewData["Message"] = "Invalid table access";
                        return View();
                        //return await Task.Run(() => View());
                    }
                    // put authentication code here. 
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, value: loginUser.username), };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", TableName);
                    // return await Task.Run(() => RedirectToAction("Index", TableName));
                }
            }
            ViewData["Message"] = "Invalid username and password";
            return await Task.Run(() => View());
        }
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Logout(LoginUser loginUser)
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "PurchaseOrder");
        }
    }
}