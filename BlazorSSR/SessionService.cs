using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace BlazorSSR
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SessionService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public T Get<T>(string cacheKey)
        {
            if (_contextAccessor.HttpContext.Session == null)
                return default(T);

            bool cacheExists = _contextAccessor.HttpContext.Session.Keys.Any(c=>c==cacheKey);
            if (cacheExists)
            {
                var item = (T)JsonSerializer.Deserialize<T>(_contextAccessor.HttpContext.Session.GetString(cacheKey));
                return item;
            }
            return default(T);
        }

        public T Set<T>(string cacheKey, T item)
        {
            if (_contextAccessor.HttpContext.Session == null)
                return item;

            if (item != null)
            {
                var serialized=  JsonSerializer.Serialize(item);
                _contextAccessor.HttpContext.Session.SetString(cacheKey, serialized);                
            }
            
            return item;
        }
    }
}
