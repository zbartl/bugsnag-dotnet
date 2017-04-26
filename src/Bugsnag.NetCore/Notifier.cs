using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Bugsnag.NetCore.Payload;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.NetCore
{
    internal class Notifier
    {
        public const string Name = ".NET Bugsnag Notifier";
        public static readonly Uri Url = new Uri("https://github.com/bugsnag/bugsnag-net");

        public static readonly string Version =
            Assembly.GetEntryAssembly().GetName().Version.ToString(3);

        private static readonly JsonSerializerSettings JsonSettings =
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

        private Configuration Config { get; set; }
        private NotificationFactory Factory { get; set; }

        public Notifier(Configuration config)
        {
            Config = config;
            Factory = new NotificationFactory(config);
        }

        public async Task Send(Event errorEvent)
        {
            var notification = Factory.CreateFromError(errorEvent);
            if (notification != null)
                await Send(notification);
        }

        private async Task Send(Notification notification)
        {
            try
            {
                await SendJson(JsonConvert.SerializeObject(notification, JsonSettings));
            }
            catch (Exception e)
            {
                Logger.Warning(string.Format("Bugsnag failed to serialise error report with exception: {0}", e.ToString()));
            }
        }

        private async Task SendJson(string json)
        {
            var reportSent = false;
            try
            {
                //  Post JSON to server:
                using (var client = new HttpClient())
                {
                    client.BaseAddress = Config.EndpointUrl;
                    var response = await client.PostAsync(Config.EndpointUrl,
                        new StringContent(json, Encoding.UTF8, "application/json"));
                    response.EnsureSuccessStatusCode(); // Throw in not success

                    reportSent = true;
                }
            }
            catch (Exception e)
            {
                Logger.Warning("Bugsnag failed to send error report with exception: " + e.ToString());
            }
            if (!reportSent && Config.StoreOfflineErrors)
            {
                try
                {
                    Logger.Error(json);
                }
                catch (Exception e)
                {
                    Logger.Warning("Bugsnag failed to persist error report with exception: " + e.ToString());
                }
            }
        }
    }
}
