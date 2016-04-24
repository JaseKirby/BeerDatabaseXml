using System;
using System.IO;
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

        public static string ConvertCamelCase(string s)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            s = r.Replace(s, " ");
            return s;
        }

        public static string CreateAngularExpression(string objName, string propName)
        {
            string dboBr = "{{";
            string dbcBr = "}}";
            string s = String.Format("{0}{1}.{2}{3}", dboBr, objName, propName, dbcBr);
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

    }
}
