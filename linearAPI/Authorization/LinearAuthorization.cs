﻿using Database.CookieAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linearAPI.Authorization
{
    public static class LinearAuthentication
    {
        public static bool AuthenticateCredentials(LinearCredentials credentials)
        {

            if (credentials == null) return false;

            if (credentials.username == null) return false;

            if (credentials.username.Length < 1) return false;

            if (credentials.username == credentials.password) return true;

            return false;
        }
    }
}