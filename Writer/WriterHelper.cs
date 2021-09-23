using Newtonsoft.Json;

namespace Writer
{
    public static class WriterHelper
    {
        public static T ObjectConverter<T>(dynamic objectToBeCoverted)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(objectToBeCoverted));
        }
    }
}
