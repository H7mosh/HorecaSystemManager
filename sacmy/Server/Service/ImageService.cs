namespace sacmy.Server.Service
{
    public class ImageService
    {

        private readonly string _imageDirectory = "C:/assets/AppImage";

        public async Task<string> SaveImageAsync(IFormFile imageFile, string path)
        {
            string _imageDirectory = path;
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid image file");
            }

            if (!Directory.Exists(_imageDirectory))
            {
                Directory.CreateDirectory(_imageDirectory);
            }

            var extension = Path.GetExtension(imageFile.FileName);
            var fileName = $"{DateTime.Now:yyyy_MM_dd_HHmmssfff}{extension}";
            var filePath = Path.Combine(_imageDirectory, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public async Task<string> ReplaceImageAsync(IFormFile newImageFile, string existingFileName)
        {
            if (newImageFile == null || newImageFile.Length == 0)
            {
                throw new ArgumentException("Invalid image file");
            }

            string fileName;

            // If there's an existing file, use its name
            if (!string.IsNullOrEmpty(existingFileName))
            {
                var existingFilePath = Path.Combine(_imageDirectory, existingFileName);

                // Delete the existing image if it exists
                if (File.Exists(existingFilePath))
                {
                    File.Delete(existingFilePath);
                }

                fileName = existingFileName; // Keep the old file name
            }
            else
            {
                // If no existing file, generate a new name
                var extension = Path.GetExtension(newImageFile.FileName);
                fileName = $"{DateTime.Now:yyyy_MM_dd_HHmmssfff}{extension}";
            }

            var filePath = Path.Combine(_imageDirectory, fileName);

            // Save the new image with the old file name
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await newImageFile.CopyToAsync(fileStream);
            }

            return fileName;
        }

        // ---------------------------------------------------
        // 2. NEW METHOD that keeps the original file name
        // ---------------------------------------------------
        public async Task<string> UploadFileAsync(IFormFile file, string path)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            // If no path is provided, fall back to the default directory
            string directory = string.IsNullOrWhiteSpace(path) ? _imageDirectory : path;

            // Ensure the directory exists
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Keep the original file name from the upload
            var originalFileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(directory, originalFileName);

            // NOTE: If a file with the same name already exists, it will be overwritten.
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return originalFileName;
        }

        public async Task<bool> RemoveImageAsync(string fileName, string path = null)
        {
            try
            {
                // If no path is provided, fall back to the default directory
                string directory = string.IsNullOrWhiteSpace(path) ? _imageDirectory : path;

                var filePath = Path.Combine(directory, fileName);

                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
