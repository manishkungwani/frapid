﻿using System.Linq;
using Frapid.ApplicationState.Cache;
using Frapid.DataAccess;

namespace WebsiteBuilder.DAL
{
    public class Content
    {
        public static Models.Content Get(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                return GetDefault();
            }

            const string sql = "SELECT * FROM wb.contents WHERE alias=@0;";
            return Factory.Get<Models.Content>(AppUsers.GetCatalog(), sql, alias).FirstOrDefault();
        }

        public static Models.Content GetDefault()
        {
            const string sql = "SELECT * FROM wb.contents WHERE is_default;";
            return Factory.Get<Models.Content>(AppUsers.GetCatalog(), sql).FirstOrDefault();
        }
    }
}