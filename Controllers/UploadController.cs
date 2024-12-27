using System.IO;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITDocumentation.Controllers
{
    [DisableRequestSizeLimit]
    [EnableCors("http://localhost:5206/itdocs/")]
    public partial class UploadController : Controller
    {
        private readonly IWebHostEnvironment environment;
        string prodPath = @"\\v00483021\DocumentsUploaded\";
        string devPath = @"\\v00483021\DocumentsUploadedTest\";

        public UploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;

        }


        [HttpPost("upload/image")]
        public IActionResult Image(IFormFile file)
        {

            string path = environment.WebRootPath + "/ImagesUploaded";
            if (!Directory.Exists(path))
            {
                createDirectory(path);
            }

            try
            {
                var fileName = $"upload-{DateTime.Today.ToString("yyyy-MM-dd")}-{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    // Save the file
                    file.CopyTo(stream);

                    // Return the URL of the file
                    var url = Url.Content($"~/{fileName}");

                    return Ok(new { Url = url });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("upload/image/{parent}/{pageID}")]
        public IActionResult ImageSpecific(IFormFile file, string parent, int pageID)
        {

            string path = environment.WebRootPath + "/ImagesUploaded/" + parent + "/" + pageID;
            if (!Directory.Exists(path))
            {
                createDirectory(path);
            }

            try
            {
                var fileName = $"ver-{DateTime.Today.ToString("yyyy-MM-dd")}-{file.FileName}";

                using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {

                    file.CopyTo(stream);

                    var url = Url.Content($"~/{fileName}");

                    return Ok(new { Url = url });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        void DeleteOldFiles()
        {
            foreach (var file in Directory.GetFiles(environment.WebRootPath))
            {
                var fileName = Path.GetFileName(file);

                if (fileName.StartsWith("upload-") && !fileName.StartsWith($"upload-{DateTime.Today.ToString("yyyy-MM-dd")}"))
                {
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch
                    {

                    }
                }
            }
        }


        void createDirectory(string path)
        {
            try
            {
               
                Directory.CreateDirectory(path);


            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }

        [HttpPost("upload/multiple/{parent}/{pageID}")]
        public IActionResult Multiple(IFormFile[] files, string parent, int pageID)
        {

            string path = environment.WebRootPath + "/DocumentsUploaded/" + parent + "/" + pageID;
            if (!Directory.Exists(path))
            {
                createDirectory(path);
            }

            for (int i = 0; i < files.Length; i++)
            {
                IFormFile file = files[i];


                try
                {

                    var fileName = $"ver-{DateTime.Today.ToString("yyyy-MM-dd")}-{file.FileName}";
                    string url = Url.Content($"~/{fileName}");



                    using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {

                        if (!System.IO.File.Exists(url))
                        {
                            file.CopyTo(stream);
                        }




                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    return StatusCode(500, ex.Message);
                }

            }
            return Ok();
        }


        [HttpPost("upload/worksheet")]
        public IActionResult Worksheet(IFormFile file)
        {

            string path = environment.WebRootPath + @"\Worksheets";
            var fileName = "";

            if (!Directory.Exists(path))
            {
                createDirectory(path);
            }


            fileName = $"{file.FileName.Replace(" ", "")}";
            if (System.IO.File.Exists(path + file.FileName))
            {
                fileName = $"{file.FileName.Replace(" ", "")}";
            }

            try
            {
                string url = Url.Content($"~/{fileName}");
                using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {

                    if (!System.IO.File.Exists(url))
                    {
                        file.CopyTo(stream);
                        url = Url.Content($"~/{fileName}");

                        return Ok(new { Url = url });
                    }

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
            return Ok();
        }


        [HttpPost("upload/single/{parent}/{pageID}")]
        public IActionResult Single(IFormFile file, string parent, int pageID)
        {
            
            
            string path = environment.WebRootPath + @"\DocumentsUploaded\" + parent + @"\" + pageID;
            //string path = this.devPath + parent + @"\" + pageID;
            var fileName = "";

            if (!Directory.Exists(path))
            {
                createDirectory(path);
            }

            //if (!System.IO.File.Exists(path + file.FileName))
            //{

            fileName = $"ver-{DateTime.Today.ToString("yyyy-MM-dd")}-{file.FileName}";
            if (System.IO.File.Exists(path + file.FileName))
            {
                fileName = $"ver-{DateTime.Today.ToString("yyyy-MM-dd")}-{DateTime.Today.ToString("mm:ss.fff")}-{file.FileName}";
            }

            try
                {
                   
                   // fileName = $"ver-{DateTime.Today.ToString("yyyy-MM-dd")}-{file.FileName}";

                   // Console.WriteLine("FileName " + fileName);
                    string url = Url.Content($"~/{fileName}");



                    using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {

                        if (!System.IO.File.Exists(url))
                        {
                            file.CopyTo(stream);
                            url = Url.Content($"~/{fileName}");

                            return Ok(new { Url = url });
                        }




                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            //}

            return Ok();
        }

        [HttpPost("upload/single/{tmp}")]
        public IActionResult Single(IFormFile file, string tmp)
        {

            string path = environment.WebRootPath + "/DocumentsUploaded/" + tmp;
            //string path = this.devPath + tmp;
            var fileName = "";


            if (!Directory.Exists(path))
            {
                createDirectory(path);
            }
            fileName = $"ver-{DateTime.Today.ToString("yyyy-MM-dd")}-{file.FileName}";
            if (System.IO.File.Exists(path + file.FileName)){
                fileName = $"ver-{DateTime.Today.ToString("yyyy-MM-dd")}-{DateTime.Today.ToString("mm:ss.fff")}-{file.FileName}";
            }


            //if (!System.IO.File.Exists(path + file.FileName))
            //{

                try
                {

                    //fileName = $"ver-{DateTime.Today.ToString("yyyy-MM-dd")}-{file.FileName}";
                    string url = Url.Content($"~/{fileName}");


                    using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {


                        if (!System.IO.File.Exists(url))
                        {
                            file.CopyTo(stream);
                            url = Url.Content($"~/{fileName}");

                            return Ok(new { Url = url });
                        }


                    }
                }
                catch (Exception ex)
                {
                    //Console.Write(ex.ToString());
                    return StatusCode(500, ex.Message);
                }
            //}

            return Ok();
        }



        [HttpPost("upload/multiple/{tmp}")]
        public IActionResult Multiple(IFormFile[] files, string tmp)
        {

            string path = environment.WebRootPath + "/DocumentsUploaded/" + tmp;
           
            if (!Directory.Exists(path))
            {
                createDirectory(path);
            }

            for (int i = 0; i < files.Length; i++)
            {
                IFormFile file = files[i];

                try
                {

                    var fileName = $"ver-{DateTime.Today.ToString("yyyy-MM-dd")}-{file.FileName}";
                    string url = Url.Content($"~/{fileName}");


                    using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {


                        file.CopyTo(stream);


                    }
                }
                catch (Exception ex)
                {
                    
                    return StatusCode(500, ex.Message);
                }

            }
            return Ok();
        }

        [HttpPost("upload/{id}")]
        public IActionResult Post(IFormFile[] files, int id)
        {
            try
            {
                // Put your code here
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/specific")]
        public IActionResult Specific(IFormFile myName)
        {
            try
            {
                // Put your code here
                return Ok(new { Completed = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}