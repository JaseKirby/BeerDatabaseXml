using System;
using System.Collections.Generic;
using System.Xml;

namespace BootstrapGenerator.AngularGeneration.ViewGeneration
{
    public class AngularDetails : AAngularView
    {
        public AngularDetails(BootstrapGenerator generator) : base(generator)
        {
            ViewName = "Details";
        }

        public override void GenerateController(List<string> services = null, bool isHttp = false)
        {
            throw new NotImplementedException();
        }

        protected override void GenerateHtml()
        {
            ScopeObjectString = String.Format("$scope.{0} = ", GenerationObjName);
            ScopeObjectString += "{};";

            Functions.CreateXmlElementSetText(Doc, StartNode, "h1", GenerationObjName + " Details");

            XmlElement detailList = Doc.CreateElement("dl");
            StartNode.AppendChild(detailList);

            var props = GenerationObj.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (Generator.configuration.IsPropertyTypeSupported(prop.PropertyType))
                {
                    XmlElement dataTitle = Doc.CreateElement("dt");
                    string dtStr = Functions.ConvertCamelCase(prop.Name);
                    dataTitle.InnerText = dtStr;
                    detailList.AppendChild(dataTitle);

                    XmlElement data = Doc.CreateElement("dd");
                    string angDataExp = Functions.CreateAngularExpression(GenerationObjName, prop.Name);
                    data.InnerText = angDataExp;
                    detailList.AppendChild(data);
                }
            }
        }
    }
}
