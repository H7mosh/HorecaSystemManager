namespace sacmy.Server.Service
{
    public class FileService
    {
        private const string TargetPath = @"C:\assets\TaskAttachment";

        public async Task<string> UploadFileAsync(IFormFile file, string taskTitle, string targetPath, int? fileNumber = null)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (string.IsNullOrWhiteSpace(taskTitle))
                throw new ArgumentNullException(nameof(taskTitle));

            if (string.IsNullOrWhiteSpace(targetPath))
                throw new ArgumentNullException(nameof(targetPath));

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
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                // Combine the directory path with the filename to get the exact path
                var exactPath = Path.Combine(targetPath, filename);

                // Create and copy the file to the specified path
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return filename;
        }
    }

}
