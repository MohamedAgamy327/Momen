namespace Utilities.StaticHelpers
{
    public static class StringConcatenates
    {
        public static string NotEqualIds(int id, int modelId)
        {
            return $"id: {id} isn't equal model.Id: {modelId}";
        }

        public static string NotExist(int id)
        {
            return $"resource with id: {id} doesn't exist";
        }
    }
}
