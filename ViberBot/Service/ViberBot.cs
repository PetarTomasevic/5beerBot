using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViberBot.Enum;
using ViberBot.Interface;
using ViberBot.Model;

namespace ViberBot.Service
{
    public class ViberBot : IViberBot, IDisposable
    {
        private readonly ILogger _logger;
        private bool _disposed;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public const Int32 MaxBroadcastMessageReceivers = 300;
        private readonly HMACSHA256 _hashAlgorithm;
        private const string BaseApiUrl = "https://chatapi.viber.com/pa/";
        private string _authToken = "";
        private static JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        public ViberBot(ILoggerFactory logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger.CreateLogger(nameof(ViberBot));
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _authToken = _configuration["XViberAuthToken"];
            _httpClient = httpClientFactory.CreateClient("ViberBot");
            _httpClient.DefaultRequestHeaders.Add("X-Viber-Auth-Token", new[] { _authToken });
            _hashAlgorithm = new HMACSHA256(Encoding.UTF8.GetBytes(_authToken));
        }

        /// <summary>
        /// Set Web Hook
        /// </summary>
        /// <param name="webhookParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<EventTypeEnum>> SetWebhookAsync(WebhookParams webhookParams)
        {
            var result = new SetWebhookApiResponse();
            try
            {
                if (webhookParams == null)
                    throw new ArgumentNullException(nameof(webhookParams));

                result = await MakeApiRequestAsync<SetWebhookApiResponse>("set_webhook", webhookParams);
                _logger.LogInformation("this thing Works " + nameof(SetWebhookAsync));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), nameof(SetWebhookAsync));
            }
            return result?.EventTypes;
        }

        /// <summary>
        /// REmove web hook
        /// </summary>
        /// <returns></returns>
        public async Task RemoveWebhookAsync()
        {
            try
            {
                var webhookParams = new WebhookParams("");
                await MakeApiRequestAsync<SetWebhookApiResponse>("set_webhook", webhookParams);
            }
            catch (Exception ex)
            { }
        }

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<Int64?> SendMessageAsync(MessageBase message)
        {
            SendMessageApiResponse result = null;
            try
            {
                var apiMethod = message.IsBroadcasting ? "broadcast_message" : "send_message";
                result = await MakeApiRequestAsync<SendMessageApiResponse>(apiMethod, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), nameof(SendMessageAsync));
            }
            return result?.MessageToken;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiMethod"></param>
        /// <param name="apiData"></param>
        /// <returns></returns>
        private async Task<T> MakeApiRequestAsync<T>(String apiMethod, Object apiData = null)
            where T : BaseApiResponse, new()
        {
            try
            {
                var requestJson = apiData == null
                    ? "{}"
                    : JsonConvert.SerializeObject(apiData, _jsonSettings);

                var viberUrl = BaseApiUrl + apiMethod;

                var response = _httpClient.PostAsync(viberUrl, new StringContent(requestJson)).GetAwaiter().GetResult();
                var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var result = JsonConvert.DeserializeObject<T>(responseJson);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), nameof(MakeApiRequestAsync));
                return null;
            }
        }

        #region Take care of memory disposal

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }

                _httpClient.Dispose();
                _hashAlgorithm.Dispose();
                _disposed = true;
            }
        }

        #endregion Take care of memory disposal
    }
}