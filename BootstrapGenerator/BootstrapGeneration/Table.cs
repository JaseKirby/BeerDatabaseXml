using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BootstrapGenerator.BootstrapGeneration
{
    public class Table : AView
    {
        protected List<string> tableClassAttributes = new List<string>() { "table" };
        protected List<Object> generationObjs { get; private set; }
        public Table(BootstrapGenerator generator, List<Object> generationObjs, List<string> tableClassAttributes = null) : base(generator)
        {
            this.generationObjs = generationObjs;
            if (tableClassAttributes != null)
                this.tableClassAttributes.AddRange(tableClassAttributes);
        }

        protected override void GenerateHtml()
        {
            Functions.CreateXmlElementSetText(Doc, StartNode, "h1", GenerationObjName + "s");

            XmlElement table = Doc.CreateElement("table");
            string attrs = Functions.JoinStringList(tableClassAttributes);
            table.SetAttribute("class", attrs);

            StartNode.AppendChild(table);

            XmlElement thead = Doc.CreateElement("thead");
            table.AppendChild(thead);

            XmlElement tr = Doc.CreateElement("tr");
            thead.AppendChild(tr);

            var props = GenerationObj.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (Generator.configuration.IsPropertyTypeSupported(prop.PropertyType))
                {
                    string colName = Functions.ConvertCamelCase(prop.Name);
                    Functions.CreateXmlElementSetText(Doc, tr, "th", colName);
                }
            }

            XmlElement tbody = Doc.CreateElement("tbody");
            table.AppendChild(tbody);

            foreach(var obj in generationObjs)
            {
                var objProps = obj.GetType().GetProperties();
                foreach(var p in objProps)
                {
                    if (Generator.configuration.IsPropertyTypeSupported(p.PropertyType))
                    {
                        //how to fill in actual prop values here is the question, toString() may not work
                        //use function I just added GetPropValue() in Functions
                    }
                }
            }
        }
    }
}
