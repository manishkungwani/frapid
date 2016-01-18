using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Hosting;
using Frapid.Messaging;
using Newtonsoft.Json;

namespace Frapid.MandrillApp
{
    public class Config : IEmailConfig
    {
        public string ApiKey { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public bool Enabled { get; set; }

        public static Config Get(string catalog)
        {
            string path = "~/Catalogs/{0}/Configs/SMTP/Mandrill.json";
            path = string.Format(CultureInfo.InvariantCulture, path, catalog);
            path = HostingEnvironment.MapPath(path);

            if (path == null || !File.Exists(path))
            {
                return new Config();
            }

            string contents = File.ReadAllText(path, Encoding.UTF8);

            var config = JsonConvert.DeserializeObject<Config>(contents);
            return config;
        }
    }
}