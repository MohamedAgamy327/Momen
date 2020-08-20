using System.IO;

namespace Utilities.StaticHelpers
{
    public static class FolderOperations
    {
        public static void DeleteFolder(string floder)
        {
            Directory.Delete($"{Directory.GetCurrentDirectory()}/Content/{floder}", true);
        }
    }
}
