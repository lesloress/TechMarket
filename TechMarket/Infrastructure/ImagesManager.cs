using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TechMarket.Infrastructure
{
    public static class ImagesManager
    {
        public static string CreateFileName(IFormFile file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string fileExt = Path.GetExtension(file.FileName);
            return fileName + DateTime.Now.ToString("yymmssfff") + fileExt;
        }

        public static async Task SaveImageToFolder(
            string wwwRoot, 
            string folderName, 
            string fileName, 
            IFormFile file)
        {
            string path = Path.Combine(wwwRoot + folderName, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
