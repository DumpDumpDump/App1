using Newtonsoft.Json;

namespace Reader
{
    public static class ReaderHelper
    {
        public static T ObjectConverter<T>(dynamic objectToBeCoverted)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(objectToBeCoverted));
        }
    }
}
