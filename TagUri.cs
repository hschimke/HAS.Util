// Copyright 2010 Henry A Schimke.
// See LICENSE for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HAS.Util
{
    public class TagUri
    {
        protected const string TAG_SCHEME = "tag";

        public static Uri Create(string authority_part, string authority_date_part, params string[] specifics)
        {
            string format = "{{scheme}}:{{authority}},{{authority_date}}{{combined_specifics}}";
            string specific_format = ":{{part}}";

            StringBuilder sb = new StringBuilder();

            foreach (string p in specifics)
            {
                sb.Append(specific_format.ApplyTemplate("part", p));
            }

            Uri new_uri = new Uri(
                format.ApplyTemplate(new
                {
                    scheme = TAG_SCHEME,
                    authority = authority_part,
                    authority_date = authority_date_part,
                    combined_specifics = sb.ToString()
                })
            );

            return new_uri;
        }

        public static Uri CreateWithFragment(string authority_part, string authority_date_part, string fragment, params string[] specifics)
        {
            string base_uri = Create(authority_part, authority_date_part, specifics).ToString();

            string fragment_format = "#{{part}}";

            string final_uri_string = base_uri + fragment_format.ApplyTemplate("part", fragment);

            return new Uri(final_uri_string);
        }
    }
}
