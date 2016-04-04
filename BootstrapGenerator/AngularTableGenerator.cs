using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BootstrapGenerator
{
    public class AngularTableGenerator
    {
        XmlDocument doc { get; set; }
        Object obj { get; set; }
        string[] classAttributes { get; set; }
        string outputPath { get; set; }

        public AngularTableGenerator(Object obj, string outputPath, bool fullPage, string[] tableClassAttributes = null)
        {
            this.obj = obj;
            this.outputPath = outputPath;
            doc = new XmlDocument();
            if (fullPage)
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"BootstrapDefault.xml");
                doc.Load(path);
                CreateFullPageTable();
            }
            else
            {
                CreatePartialPageTable();
            }
        }

        void CreatePartialPageTable()
        {
            XmlNode div = doc.CreateElement("div");
            doc.AppendChild(div);

            CreateTable(div);
        }

        void CreateFullPageTable()
        {
            XmlNode divContainer = doc.SelectSingleNode("//div");
            CreateTable(divContainer);
        }

        void CreateTable(XmlNode startNode)
        {
            GlobalMethods.CreateXmlElementSetText(doc, startNode, "h1", obj.GetType().Name + "s");

            XmlElement table = doc.CreateElement("table");
            if(classAttributes != null)
            {
                string attrs = "table" + String.Join(" ", classAttributes);
                table.SetAttribute("class", attrs);
            }else
            {
                table.SetAttribute("class", "table");
            }
            startNode.AppendChild(table);

            XmlElement thead = doc.CreateElement("thead");
            table.AppendChild(thead);

            XmlElement tr = doc.CreateElement("tr");
            thead.AppendChild(tr);

            var props = obj.GetType().GetProperties();
            foreach(var prop in props)
            {
                if(prop.GetType() == typeof(string) || prop.GetType() == typeof(int) || prop.GetType() ==typeof(DateTime))
                {
                    string colName = GlobalMethods.ConvertCamelCase(prop.Name);
                    GlobalMethods.CreateXmlElementSetText(doc, tr, "th", colName);
                }
            }

            XmlElement tbody = doc.CreateElement("tbody");
            table.AppendChild(tbody);

            XmlElement trBody = doc.CreateElement("tr");
            string angName = obj.GetType().Name;
            string value = String.Format("{0} in {1}s", angName, angName);
            trBody.SetAttribute("ng-repeat", value);
            tbody.AppendChild(trBody);

            foreach(var prop in props)
            {
                string angText = GlobalMethods.CreateAngularPropView(angName, prop.Name);
                GlobalMethods.CreateXmlElementSetText(doc, trBody, "td", angText);
            }

            doc.Save(outputPath + @"\" + angName + "Index.html");
        }
    }
}
