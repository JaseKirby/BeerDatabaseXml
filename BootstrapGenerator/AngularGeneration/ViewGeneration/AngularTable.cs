using System;
using System.Xml;

namespace BootstrapGenerator.AngularGeneration
{
    public class AngularTable : AAngularView
    {
        public AngularTable(BootstrapGenerator generator) : base(generator)
        {
            ViewName = "Table";
        }

        protected override void GenerateHtml(XmlNode startNode)
        {
            GlobalMethods.CreateXmlElementSetText(doc, startNode, "h1", GenerationObjName + "s");

            XmlElement table = doc.CreateElement("table");
            //if(classAttributes != null)
            //{
            //    string attrs = "table" + String.Join(" ", classAttributes);
            //    table.SetAttribute("class", attrs);
            //}
            //else
            //{
                table.SetAttribute("class", "table");
            //}
            startNode.AppendChild(table);

            XmlElement thead = doc.CreateElement("thead");
            table.AppendChild(thead);

            XmlElement tr = doc.CreateElement("tr");
            thead.AppendChild(tr);

            var props = GenerationObj.GetType().GetProperties();
            foreach(var prop in props)
            {
                if(generator.configuration.IsPropertyTypeSupported(prop.PropertyType))
                {
                    string colName = GlobalMethods.ConvertCamelCase(prop.Name);
                    GlobalMethods.CreateXmlElementSetText(doc, tr, "th", colName);
                }
            }

            XmlElement tbody = doc.CreateElement("tbody");
            table.AppendChild(tbody);

            XmlElement trBody = doc.CreateElement("tr");
            string value = String.Format("{0} in {1}s", GenerationObjName, GenerationObjName);
            trBody.SetAttribute("ng-repeat", value);
            tbody.AppendChild(trBody);

            foreach(var prop in props)
            {
                string angText = GlobalMethods.CreateAngularPropView(GenerationObjName, prop.Name);
                GlobalMethods.CreateXmlElementSetText(doc, trBody, "td", angText);
            }

            SaveFile();
        }

        public override void GenerateController() { }
    }
}
