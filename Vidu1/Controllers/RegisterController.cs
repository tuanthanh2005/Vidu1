using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using Vidu1.Models;

namespace Vidu1.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IWebHostEnvironment _hosting;

        public RegisterController(IWebHostEnvironment hosting)
        {
            _hosting = hosting;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult XuLy(PersonViewModel m, IFormFile FHinh)
        {
            if (FHinh != null && FHinh.Length > 0)
            {
                // Tạo tên file duy nhất
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(FHinh.FileName);

                // Tạo đường dẫn đến thư mục wwwroot/images
                string uploadPath = Path.Combine(_hosting.WebRootPath, "images");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Ghi file
                string filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    FHinh.CopyTo(stream);
                }

                // Lưu tên file vào model
                m.Picture = fileName;
            }

            return View(m);
        }
    }
}
