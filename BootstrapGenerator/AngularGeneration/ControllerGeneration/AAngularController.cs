using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapGenerator.AngularGeneration
{
    public abstract class AAngularController
    {
        protected AAngularView view { get; set; }
        protected string controllerName { get; set; }
        protected string controllerFileName { get; set; }
        protected List<string> controllerLines { get; set; }
        protected List<string> services { get; set; }
        protected string jsObjectString { get; set; }

        protected AAngularController(AAngularView view, List<string> services = null)
        {
            this.view = view;
            if (services != null)
                this.services = services;
            else
                services = new List<string>() { "$scope" };
            controllerLines = new List<string>();
            controllerName = String.Format("{0}{1}Ctrl", view.GenerationObjName, view.ViewName);
            controllerName = String.Format("{0}Ctrl.js", controllerName);
            Generate();
        }

        protected abstract void Generate();

        protected void CreateFirstLine()
        {
            string servicesStr = String.Join(", ", services);
            string startLine = String.Format("app.controller('{0}', function({1}){", controllerName, servicesStr);
            controllerLines.Add(startLine);
        }

        protected abstract void CreateBody();

        protected void CreateEndLine()
        {
            controllerLines.Add("});");
        }

        protected void SaveFile()
        {
            string path = String.Format(@"{0}\{1}", view.OutputPath, controllerName);
            File.WriteAllLines(path, controllerLines);
        }

        protected void AddScriptTagToHtml()
        {

        }
    }
}
