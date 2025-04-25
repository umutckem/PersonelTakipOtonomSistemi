using PersonelTakipOtonomSistemi.Dtos;
using System.Net.Http.Json;
using System.Text.Json;
using System.Net;


namespace PersonelTakipOtonomSistemi.Services
{

    public static class UrlHelper
    {
        private static string BaseUrl = "https://localhost:7100";
        public static string PersonelUrl = $"{BaseUrl}/Personel";
    }

    public abstract class BaseService
    {
        protected HttpClient _client;
        protected JsonSerializerOptions _serializerOptions;

        public BaseService()
        {
#if DEBUG && ANDROID
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _client = new HttpClient();
#endif

            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
    }

    public class HttpsClientHandlerService
    {

        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new Xamarin.Android.Net.AndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            TrustOverrideForUrl = IsHttpsLocalhost
        };
        return handler;
#else
     throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }

#if IOS
    public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
    {
        return url.StartsWith("https://localhost");
    }
#endif
    }

    public class PersonelServices : BaseService , IPersonelServices
    {


        async Task <List<Personel>> IPersonelServices.GetTumPersoneller()
        {
            Uri uri = new Uri(UrlHelper.PersonelUrl);
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var sonuc = JsonSerializer.Deserialize<List<Personel>>(content, _serializerOptions);
                return sonuc;
            }
            return new List<Personel>();
        }

         async Task IPersonelServices.PersonelEkle(PersonelOlusturDto personel)
        {
            Uri uri = new Uri(UrlHelper.PersonelUrl);
            JsonContent content = JsonContent.Create(personel);

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

            }
        }

        async Task IPersonelServices.PersonelGuncelle(PersonelGuncelleDto personel)
        {

            JsonContent jsonContent = JsonContent.Create(personel);

            await _client.PutAsync(UrlHelper.PersonelUrl, jsonContent);
        }
        async public Task PersonelSil(int id)
        {
            Uri uri = new Uri($"{UrlHelper.PersonelUrl}?id={id}");

            HttpResponseMessage response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {

            }
        }


    }
}
