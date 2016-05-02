using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapGenerator.AngularGeneration.ControllerGeneration
{
    public class HttpGetController : AAngularHttpController
    {
        public HttpGetController(AAngularView view, List<string> services = null) : base(view, services)
        {
            HttpVerb = "get";
        }

        protected override void AddErrorBody()
        {
            throw new NotImplementedException();
        }

        protected override void AddFirstHttpLine()
        {
            string firstLine = String.Format("\t$http.{0}('/api/URL_ROUTE_HERE')");
            controllerLines.Add(firstLine);
        }

        protected override void AddSuccessBody()
        {
            
        }
    }
}
