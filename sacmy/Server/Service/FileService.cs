namespace sacmy.Server.Service
{
    public class FileService
    {
        private const string TargetPath = @"C:\assets\TaskAttachment";

        public async Task<string> UploadFileAsync(IFormFile file, string taskTitle, int? fileNumber = null)
        {
            string filename = "";
            try
            {
                // Get the file extension
                var extension = Path.GetExtension(file.FileName);

                // Create a unique filename using the task title and current milliseconds
                string baseFilename = $"{taskTitle}-{DateTime.Now:yyyy-MM-dd-HH-mm-ss-fffffff}";
                if (fileNumber.HasValue)
                {
                    baseFilename += $"-{fileNumber.Value}";
                }
                filename = $"{baseFilename}{extension}";

                // Ensure the directory exists
                if (!Directory.Exists(TargetPath))
                {
                    Directory.CreateDirectory(TargetPath);
                }

                // Combine the directory path with the filename to get the exact path
                var exactPath = Path.Combine(TargetPath, filename);

                // Create and copy the file to the specified path
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as needed
                // For debugging, you can log the exception message
                Console.WriteLine(ex.Message);
            }

            return filename;
        }
    }

}
