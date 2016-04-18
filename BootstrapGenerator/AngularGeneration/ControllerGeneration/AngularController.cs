using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapGenerator.AngularGeneration.ControllerGeneration
{
    public class AngularController : AAngularController
    {
        public AngularController(AAngularView view, List<string> services = null) : base(view, services)
        {
        }

        protected override void CreateBody()
        {
            throw new NotImplementedException();
        }

        protected override void Generate()
        {
            throw new NotImplementedException();
        }
    }
}
