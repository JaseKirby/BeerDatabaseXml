using System;
using System.Collections.Generic;
using System.Xml;

namespace BootstrapGenerator.AngularGeneration
{
    public abstract class AAngularView : AView
    {
        public bool AddController { get; private set; }
        public string ScopeObjectString { get; set; }

        public AAngularView(BootstrapGenerator generator) : base(generator)
        {
        }

        public abstract void GenerateController(List<string> services = null, bool isHttp = false);
    }
}
