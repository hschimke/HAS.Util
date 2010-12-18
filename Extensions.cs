// Copyright 2010 Henry A Schimke.
// See LICENSE for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HAS.Util
{
    public static class Extensions
    {
        #region Required Local Functions

        private const string template_leadin = "{{";
        private const string template_leadout = "}}";

        private static StringBuilder SetApplyTemplate(StringBuilder target, string template, string value, string prefix)
        {
            target.Replace(prefix + template_leadin + template + template_leadout, value);
            return target;
        }

        #endregion

        #region Extension Methods

        public static string ApplyTemplate(this String str, string template_name, string template_value, string template_prefix)
        {
            StringBuilder sb = new StringBuilder(str);
            SetApplyTemplate(sb, template_name, template_value, template_prefix);
            return sb.ToString();
        }

        public static string ApplyTemplate(this String str, Dictionary<string, object> template_applicables, string template_prefix)
        {
            StringBuilder sb = new StringBuilder(str);

            foreach (var item in template_applicables)
            {
                SetApplyTemplate(sb, item.Key, System.Convert.ToString(item.Value), template_prefix);
            }

            return sb.ToString();
        }

        public static string ApplyTemplate(this String str, object template_applicables, string template_prefix)
        {
            System.Reflection.PropertyInfo[] prop_list = template_applicables.GetType().GetProperties();

            StringBuilder sb = new StringBuilder(str);

            foreach (var prop in prop_list)
            {
                SetApplyTemplate(sb, prop.Name, System.Convert.ToString(prop.GetValue(template_applicables, null)), template_prefix);
            }

            return sb.ToString();
        }

        public static string ApplyTemplate(this String str, string template_name, string template_value)
        {
            return ApplyTemplate(str, template_name, template_value, String.Empty);
        }

        public static string ApplyTemplate(this String str, Dictionary<string, object> template_applicables)
        {
            return ApplyTemplate(str, template_applicables, String.Empty);
        }

        public static string ApplyTemplate(this String str, object template_applicables)
        {
            return ApplyTemplate(str, template_applicables, String.Empty);
        }

        public static string StripHtml(this String str, bool aggressive)
        {
            string pattern = aggressive ? @"<(.|\n)*?>" : @"</?(?i:script|embed|object|frameset|frame|iframe|meta|link|style|param|comment|listing|noscript|plaintext|xmp)(.|\n)*?>";

            return Regex.Replace(str, pattern, String.Empty);
        }

        #endregion

        #region Required External Functions

        public static Dictionary<string, object> CreateTemplateSet()
        {
            return new Dictionary<string, object>();
        }

        #endregion
    }
}
