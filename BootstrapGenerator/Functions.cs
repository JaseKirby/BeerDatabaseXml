using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace BootstrapGenerator
{
    public static class Functions
    {
        public static string GetProjectPathString()
        {
            return Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        }

        public static string ConvertCamelCase(string s, bool captalizeFirstLetter = false)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            s = r.Replace(s, " ");
            if (captalizeFirstLetter)
                s = CapitalizeFirstLetter(s);
            return s;
        }

        public static string CapitalizeFirstLetter(string s)
        {
            return s.First().ToString().ToUpper() + s.Substring(1);
        }

        public static string CreateAngularExpression(string objName, string propName)
        {
            string dboBr = "{{";
            string dbcBr = "}}";
            string s = String.Format("{0}{1}{2}", dboBr, CreateAngularPropView(objName, propName), dbcBr);
            return s;
        }

        public static string CreateAngularPropView(string objName, string propName)
        {
            return String.Format("{0}.{1}", objName, propName);
        }

        public static void CreateXmlElement(XmlDocument doc, XmlNode parent, string name)
        {
            XmlElement ele = doc.CreateElement(name);
            parent.AppendChild(ele);
        }
        public static void CreateXmlElementSetText(XmlDocument doc, XmlNode parent, string name, string text)
        {
            XmlElement ele = doc.CreateElement(name);
            XmlText txt = doc.CreateTextNode(text);
            ele.AppendChild(txt);
            parent.AppendChild(ele);
        }

        public static string StripScopeObjString(string scopeObjString)
        {
            string[] rStrs = { "=", "{", "}", "[", "]", " " };
            foreach(string s in rStrs)
            {
                scopeObjString = scopeObjString.Replace(s, String.Empty);
            }
            return scopeObjString;
        }

        public static string JoinStringList(List<string> lst)
        {
            return String.Join(" ", lst);
        }

        public static string GetPropValue(object src, string propName)
        {
            var val = src.GetType().GetProperty(propName).GetValue(src, null);
            return Convert.ToString(val);
        }
    }
}
