using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using SecProject.Models;

namespace SecProject
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            //Authentication via services

            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "47762179",
            //    consumerSecret: "GyMiOEKKh5e63wNrO6hhu3iwbI0psaiIggNV6FoLiw");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "383934385073634",
                appSecret: "6377ee3d234a64a6f16ed773ffacb3cf");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
