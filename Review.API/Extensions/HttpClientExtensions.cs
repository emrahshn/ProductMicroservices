using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Review.API.Extensions
{
    public static class HttpClientExtensions
    {

        /// <summary>
        /// Generic method of HttpClient which is using as a asynchronously string reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
