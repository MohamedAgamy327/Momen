using Microsoft.AspNetCore.Http;
using System.IO;

namespace Utilities.StaticHelpers
{
    public static class FileOperations
    {
        public static async void WriteFile(string floder, IFormFile file)
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), $"Content/{floder}"));
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), $"Content/{floder}"));
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Content/{floder}", file.FileName);
            using FileStream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream).ConfigureAwait(true);
        }

        public static void DeleteFile(string floder,  string fileName)
        {
            File.Delete($"{Directory.GetCurrentDirectory()}/Content/{floder}/{fileName}");
        }
    }
}
