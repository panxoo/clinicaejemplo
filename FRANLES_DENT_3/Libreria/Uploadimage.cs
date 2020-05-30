using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Libreria
{
    public class Uploadimage
    {
        public async Task copiarImagenAsync(IFormFile AvatarImage, String fileName, IHostingEnvironment environment, String carpeta, String imagen)
        {
            if (AvatarImage == null)
            {
                String archivoOrigen;

                String destFileName = environment.ContentRootPath + "/wwwroot/images/" + carpeta + "/" + fileName;
                if (imagen == null)
                {
                    archivoOrigen = environment.ContentRootPath + "/wwwroot/images/" + carpeta + "/default.png";
                    File.Copy(archivoOrigen, destFileName, true);
                }
                else
                {
                    archivoOrigen = environment.ContentRootPath + "/wwwroot/images/" + carpeta + "/" + imagen + ".png";
                    if (fileName != imagen + ".png")
                    {
                        File.Copy(archivoOrigen, destFileName, true);
                        File.Delete(archivoOrigen);
                    }
                }
            }
            else
            {
                var filePath = Path.Combine(environment.ContentRootPath, "wwwroot/images/" + carpeta, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await AvatarImage.CopyToAsync(stream);
                }
            }
        }
        public void deleteImagen(IHostingEnvironment environment, String carpeta, String imagen)
        {
            var archivoOrigen = environment.ContentRootPath + "/wwwroot/images/" + carpeta + "/" + imagen + ".png";
            File.Delete(archivoOrigen);
        }
    }
}
