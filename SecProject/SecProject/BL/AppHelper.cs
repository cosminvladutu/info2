using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SecProject.BL
{
    public class AppHelper
    {
        public static string Server = "server";
        public static string DbName = "dbName";
        public static string StardogReasoningMode = "stardogReasoningMode";
        public static string Username = "username";
        public static string Password = "password";

        public static Dictionary<string, string> ReadConfigs()
        {
            Dictionary<string, string> configs = new Dictionary<string, string>();
            try
            {
                configs.Add(AppHelper.Server, ConfigurationManager.AppSettings[AppHelper.Server]);
                configs.Add(AppHelper.DbName, ConfigurationManager.AppSettings[AppHelper.DbName]);
                configs.Add(AppHelper.Password, ConfigurationManager.AppSettings[AppHelper.Password]);
                configs.Add(AppHelper.StardogReasoningMode, ConfigurationManager.AppSettings[AppHelper.StardogReasoningMode]);
                configs.Add(AppHelper.Username, ConfigurationManager.AppSettings[AppHelper.Username]);
            }
            catch (Exception ex)
            {

                throw;
            }


            return configs;
        }
    }
}