using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Storage.Settings
{
    public class MongoDBSettings
    {
        public string Host { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public bool IsSSL { get; set; }
    }
}
