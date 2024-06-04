using Spartacus.Domain.Entities.User;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Spartacus.Helpers
{
    public static class MediaHelper
    {
        public static string SaveImageByUser(HttpPostedFileBase image, UTable user)
        {
            var newFileName = DateTime.Now.ToString($"yyyy/MMddHHmmss-{user.Id}-");

            var fileName = image.FileName;
            var extension = Path.GetExtension(fileName).ToLower();
            var maxLength = 50 - newFileName.Length - extension.Length;
            var tmp = (fileName.Length > maxLength) ? fileName.ToLower().Substring(0, maxLength) + extension : fileName.ToLower();
            newFileName += tmp;

            if (image?.ContentLength > 0)
            {
                // Check if the file is an image, no gifs
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (allowedExtensions.Contains(extension))
                {
                    if (user.FileName != null)
                    {
                        var savedPath = HttpContext.Current.Server.MapPath("~/Content/Users/" + user.FileName);
                        File.Delete(savedPath);
                    }

                    (string year, string name) = (newFileName.Split('/').First(), newFileName.Split('/').Last());
                    var uploadsDir = HttpContext.Current.Server.MapPath("~/Content/Users/" + year);

                    if (!Directory.Exists(uploadsDir))
                    {
                        Directory.CreateDirectory(uploadsDir);
                    }

                    var path = Path.Combine(uploadsDir, name);
                    image.SaveAs(path);

                    return newFileName;
                }
            }
            return null;
        }
    }
}
