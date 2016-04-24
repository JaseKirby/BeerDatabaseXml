using BootstrapGenerator.AngularGeneration.ControllerGeneration;
using System;
using System.Collections.Generic;
using System.Xml;

namespace BootstrapGenerator.AngularGeneration.ViewGeneration
{
    public class AngularTable : AAngularView
    {
        protected List<string> tableClassAttributes { get; set; }

        public AngularTable(BootstrapGenerator generator) : base(generator)
        {
            ViewName = "Table";
        }

        public AngularTable(BootstrapGenerator generator, List<string> tableClassAttributes) : base(generator)
        {
            ViewName = "Table";
            this.tableClassAttributes = tableClassAttributes;
        }

        protected override void GenerateHtml()
        {
            ScopeObjectString = String.Format("$scope.{0} = [];", GenerationObjName);

            Functions.CreateXmlElementSetText(Doc, StartNode, "h1", GenerationObjName + "s");

            XmlElement table = Doc.CreateElement("table");
            if (tableClassAttributes != null)
            {
                string attrs = "table " + String.Join(" ", tableClassAttributes);
                table.SetAttribute("class", attrs);
            }
            else
            {
                table.SetAttribute("class", "table");
            }

            StartNode.AppendChild(table);

            XmlElement thead = Doc.CreateElement("thead");
            table.AppendChild(thead);

            XmlElement tr = Doc.CreateElement("tr");
            thead.AppendChild(tr);

            var props = GenerationObj.GetType().GetProperties();
            foreach(var prop in props)
            {
                if(generator.configuration.IsPropertyTypeSupported(prop.PropertyType))
                {
                    string colName = Functions.ConvertCamelCase(prop.Name);
                    Functions.CreateXmlElementSetText(Doc, tr, "th", colName);
                }
            }

            XmlElement tbody = Doc.CreateElement("tbody");
            table.AppendChild(tbody);

            XmlElement trBody = Doc.CreateElement("tr");
            string value = String.Format("{0} in {1}s", GenerationObjName, GenerationObjName);
            trBody.SetAttribute("ng-repeat", value);
            tbody.AppendChild(trBody);

            foreach(var prop in props)
            {
                string angText = Functions.CreateAngularExpression(GenerationObjName, prop.Name);
                Functions.CreateXmlElementSetText(Doc, trBody, "td", angText);
            }
        }

        public override void GenerateController(List<string> services = null, bool isHttp = false)
        {
            ControllerFactory controllerFactory = new ControllerFactory(this);
            if (isHttp)
                controllerFactory.createController(ControllerFactory.ControllerType.GetHttp);
            else
                controllerFactory.createController(ControllerFactory.ControllerType.NoHttp);
        }
    }
}
