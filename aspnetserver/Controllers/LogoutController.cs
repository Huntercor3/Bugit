using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace aspnetserver.Controllers
{
    public class LogoutController : Controller
    {
        public static async Task<int> LogoutUser(CookieContainer cookieContainer)
        {
            var cookies = cookieContainer.GetCookies(new Uri("https://purple-ground-019dc9c0f.1.azurestaticapps.net/"));
            foreach (Cookie co in cookies)
            {
                co.Expires = DateTime.Now.Subtract(TimeSpan.FromDays(1));
            }
            return 200;
        }
    }
}