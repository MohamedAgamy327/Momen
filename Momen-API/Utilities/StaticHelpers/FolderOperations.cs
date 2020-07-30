using System.IO;

namespace Utilities.StaticHelpers
{
    public static class FolderOperations
    {
        public static void DeleteFolder(string floder, int id)
        {
            Directory.Delete($"{Directory.GetCurrentDirectory()}/Content/{floder}/{id}", true);
        }
    }
}
