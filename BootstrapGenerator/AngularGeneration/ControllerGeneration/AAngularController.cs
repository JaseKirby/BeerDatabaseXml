using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapGenerator.AngularGeneration
{
    public enum ControllerTypes
    {
        Singular, Multiple
    }
    public enum HttpControllerTypes
    {
        GetSingle, GetMultiple, PostSingle, PutSingle, DeleteSingle
    }
    public abstract class AAngularController
    {
        AAngularView view { get; set; }
        protected string controllerName { get; set; }
        protected List<string> controllerLines { get; set; }

        protected AAngularController(AAngularView view)
        {
            this.view = view;
            controllerLines = new List<string>();
            controllerName = String.Format("{0}{1}Ctrl.js", view.GenerationObjName, view.ViewName);
        }

        protected void CreateFirstLine(List<string> services)
        {
            string servicesStr = String.Join(", ", services);
            string startLine = String.Format("app.controller('{0}', function({1}){", controllerName, servicesStr);
            controllerLines.Add(startLine);
        }

        protected void SaveFile()
        {
            string path = String.Format(@"{0}\{1}", view.OutputPath, controllerName);
            File.WriteAllLines(path, controllerLines);
        }
    }
}
