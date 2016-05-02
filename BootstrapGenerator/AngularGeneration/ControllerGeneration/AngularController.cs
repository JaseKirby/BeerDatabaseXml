using System.Collections.Generic;

namespace BootstrapGenerator.AngularGeneration.ControllerGeneration
{
    public class AngularController : AAngularController
    {
        public AngularController(AAngularView view, List<string> services = null) : base(view, services)
        {
        }

        protected override void CreateBody()
        {
        }

    }
}
