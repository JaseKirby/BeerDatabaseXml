using System;
using System.Collections.Generic;
using System.Xml;

namespace BootstrapGenerator.AngularGeneration
{
    public abstract class AAngularView
    {
        public string ViewName { get; set; }
        protected BootstrapGenerator generator { get; set; }
        public XmlDocument Doc { get; set; }
        public XmlNode StartNode { get; set; }
        public Object GenerationObj { get; set; }
        public string GenerationObjName { get; set; }
        public bool AddController { get; set; }
        public string OutputPath { get; set; }
        public bool IsTemplateView { get; set; }
        public string ScopeObjectString { get; set; }

        protected AAngularView(BootstrapGenerator generator)
        {
            this.generator = generator;
        }

        public void GenerateView(Object generationObj, string outputPath, bool useTemplate = false)
        {
            this.GenerationObj = generationObj;
            this.GenerationObjName = generationObj.GetType().Name;
            this.OutputPath = outputPath;
            Doc = new XmlDocument();

            if (useTemplate)
            {
                IsTemplateView = true;
                Doc.Load(generator.TemplateFilePath);
                CreateViewFromTemplate();
            }
            else
            {
                IsTemplateView = false;
                CreateViewPartial();
            }
        }

        void CreateViewPartial()
        {
            XmlNode div = Doc.CreateElement("div");
            Doc.AppendChild(div);
            StartNode = div;
            GenerateHtml();
            SaveView(true);
        }
        void CreateViewFromTemplate()
        {
            XmlNode divContainer = Doc.SelectSingleNode("//div[@id='start']");
            StartNode = divContainer;
            GenerateHtml();
            SaveView(true);
        }

        protected abstract void GenerateHtml();
        public abstract void GenerateController(List<string> services = null, bool isHttp = false);


        public void SaveView(bool printMessage)
        {
            string path = String.Format(@"{0}\{1}{2}.html", OutputPath, GenerationObjName, ViewName);
            Doc.Save(path);
            if (printMessage)
                Console.WriteLine("{0}{1} generated at: {2}", GenerationObjName, ViewName, path);
        }
    }
}
