using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BootstrapGenerator.AngularGeneration.ControllerGeneration
{
    public abstract class AAngularController
    {
        protected AAngularView view { get; set; }
        protected string controllerName { get; set; }
        protected string controllerFileName { get; set; }
        protected List<string> controllerLines { get; set; }
        protected List<string> services { get; set; }
        protected string scopeObjString { get;}

        protected AAngularController(AAngularView view, List<string> services = null)
        {
            this.view = view;
            this.services = new List<string>() { "$scope" };
            if (services != null)
                this.services.AddRange(services);
            controllerLines = new List<string>();
            controllerName = String.Format("{0}{1}Ctrl", view.GenerationObjName, view.ViewName);
            controllerFileName = String.Format("{0}.js", controllerName);
            scopeObjString = Functions.StripScopeObjString(view.ScopeObjectString);
            Generate();
        }

        protected void Generate()
        {
            CreateFirstLines();
            CreateBody();
            CreateEndLine();
            AddControllerReferenceToView();
            SaveFile();

        }

        protected void CreateFirstLines()
        {
            string servicesStr = String.Join(", ", services);
            string startLine = String.Format("app.controller('{0}', function({1})", controllerName, servicesStr);
            startLine += "{";
            controllerLines.Add(startLine);
            controllerLines.Add("\t" + view.ScopeObjectString);
        }

        protected abstract void CreateBody();

        protected void CreateEndLine()
        {
            controllerLines.Add("});");
        }

        protected void AddControllerReferenceToView()
        {
            XmlElement scriptTag = view.Doc.CreateElement("script");
            scriptTag.InnerText = " ";
            scriptTag.SetAttribute("src", controllerFileName);
            ((XmlElement)view.StartNode).SetAttribute("ng-controller", controllerName);
            if (view.IsTemplateView)
            {
                XmlNode body = view.Doc.SelectSingleNode("//body");
                body.AppendChild(scriptTag);
            }else
            {
                view.StartNode.AppendChild(scriptTag);
            }
            view.SaveView(false);
        }

        protected void SaveFile()
        {
            string path = String.Format(@"{0}\{1}", view.Generator.OutputPath, controllerFileName);
            File.WriteAllLines(path, controllerLines);
            Console.WriteLine("Angular controller for {1}{2} view generated at: {3}", controllerName, 
                view.GenerationObjName, view.ViewName, path);
        }

        public string GetClosingTagNoColon()
        {
            return ")}";
        }
        public string GetClosingTagColon()
        {
            return ")};";
        }
    }
}
