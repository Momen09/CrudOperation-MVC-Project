using SimpleProject.Services.Interfaces;

namespace SimpleProject.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            // Constructor logic if needed
        }

        public bool DeletePhysicalFile(string path)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot"+path);
            if(File.Exists(directoryPath))
            {
               
                    File.Delete(directoryPath);
                    return true; // File deleted successfully
                
             
            }
            return false; // File not found
        }

        public async Task<string> Upload(IFormFile file, string location)
        {
            
                try {
                    var path = _webHostEnvironment.WebRootPath + location;
                    var extension = Path.GetExtension(file.FileName);//to change FIleName
                    var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //save
                    using (FileStream fileStream = File.Create(path + fileName))
                    {
                        await file.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return $"{location}/{fileName}";
                    }
                }
                catch 
                {
                    // Log the exception or handle it as needed
                    return "Error uploading file";
                }
        }
    }
}
