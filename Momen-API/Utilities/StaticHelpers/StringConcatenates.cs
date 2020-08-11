namespace Utilities.StaticHelpers
{
    public static class StringConcatenates
    {
        public static string NotEqualIds(int id, int modelId)
        {
            return $"id: {id} isn't equal model.Id: {modelId}";
        }
        public static string NotExist(string resource, int id)
        {
            return $"{resource} with id: {id} doesn't exist";
        }
        public static string Exist(string property, string value)
        {
            return $"resource with {property}: {value} is existed";
        }
        public static string Exist(int id, string list)
        {
            return $"resource with id: {id} is existed in {list}";
        }
    }
}
