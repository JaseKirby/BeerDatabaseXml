using System;
using System.Collections.Generic;
using System.Xml;

namespace BootstrapGenerator.AngularGeneration.ViewGeneration
{
    public class AngularForm : AAngularView
    {
        public AngularForm(BootstrapGenerator generator) : base(generator)
        {
            ViewName = "Form";
        }

        public override void GenerateController(List<string> services = null, bool isHttp = false)
        {
            throw new NotImplementedException();
        }

        protected override void GenerateHtml()
        {
            ScopeObjectString = String.Format("$scope.{0} = ", GenerationObjName);
            ScopeObjectString += "{};";

            Functions.CreateXmlElementSetText(Doc, StartNode, "h1", "Create " + GenerationObjName);

            XmlElement form = Doc.CreateElement("form");
            StartNode.AppendChild(form);

            var props = GenerationObj.GetType().GetProperties();
            foreach(var prop in props)
            {
                if (generator.configuration.IsPropertyTypeSupported(prop.PropertyType))
                {
                    XmlElement formGroup = Doc.CreateElement("div");
                    formGroup.SetAttribute("class", "form-group");
                    form.AppendChild(formGroup);

                    string lblName = Functions.ConvertCamelCase(prop.Name);
                    XmlElement label = Doc.CreateElement("label");
                    label.InnerText = lblName;
                    formGroup.AppendChild(label);

                    XmlElement input = Doc.CreateElement("input");
                    input.SetAttribute("type", "text");
                    input.SetAttribute("class", "form-control");
                    string angModelProp = Functions.CreateAngularPropView(GenerationObjName, prop.Name);
                    input.SetAttribute("ng-model", angModelProp);
                    formGroup.AppendChild(input);
                }
            }
        }
    }
}
