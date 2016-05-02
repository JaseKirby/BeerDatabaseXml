using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapGenerator.AngularGeneration.ControllerGeneration
{
    public abstract class AAngularHttpController : AAngularController
    {
        public string HttpVerb { get; set; }
        public AAngularHttpController(AAngularView view, List<string> services = null) : base(view, services)
        {
        }

        protected override void CreateBody()
        {
            controllerLines.Add("");
            AddFirstHttpLine();
            AddSuccessLine();
            AddSuccessBody();
            AddErrorLine();
            AddErrorBody();
        }

        protected abstract void AddFirstHttpLine();

        protected void AddSuccessLine()
        {
            controllerLines.Add("\t\t.success(function (data, status, headers, config) {");
        }
        protected abstract void AddSuccessBody();
        protected void AddErrorLine()
        {
            controllerLines.Add("\t\t.error(function (data, status, header, config) {");
        }
        protected abstract void AddErrorBody();
    }
}
