using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ISignalRService.Models;
using System.Net;
using System.Net.Sockets;

namespace ISignalRService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string DNS = Dns.GetHostName();
            string IP = Dns.GetHostEntry(DNS).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork).ToString();
            ViewBag.Message = "DNS: " + DNS + " IP:" + IP;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
