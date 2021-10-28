using GlassLewis.WebApp.MVC.Extensions;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GlassLewis.WebApp.MVC.Service
{
    public abstract class Service
    {
        protected StringContent GetContent(object data)
        {
            return new StringContent(
                    JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                    "application/json");
        }

        protected async Task<T> DeserealizeObjetResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var content = await responseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(content, options);
        }

        protected bool ResponseErrorsHandler(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpResponseException(response.StatusCode);

                case 400:
                        return false;

            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
