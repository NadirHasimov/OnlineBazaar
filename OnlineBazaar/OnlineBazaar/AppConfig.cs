using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OnlineBazaar
{
    public static class AppConfig
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["StoreDB"].ConnectionString; }
        }
    }
}