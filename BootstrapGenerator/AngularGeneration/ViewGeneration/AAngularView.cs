using System;
using System.Collections.Generic;
using System.Xml;

namespace BootstrapGenerator.AngularGeneration
{
    public abstract class AAngularView
    {
        public string ViewName { get; set; }
        protected BootstrapGenerator generator { get; set; }
        protected XmlDocument doc { get; set; }
        public Object GenerationObj { get; set; }
        public string GenerationObjName { get; set; }
        public bool AddController { get; set; }
        public string OutputPath { get; set; }
        public bool IsTemplateView { get; set; }

        protected AAngularView(BootstrapGenerator generator)
        {
            this.generator = generator;
        }

        public void GenerateView(Object generationObj, string outputPath, bool useTemplate = false)
        {
            this.GenerationObj = generationObj;
            this.GenerationObjName = generationObj.GetType().Name;
            this.OutputPath = outputPath;
            doc = new XmlDocument();

            if (useTemplate)
            {
                IsTemplateView = true;
                doc.Load(generator.TemplateFilePath);
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
            XmlNode div = doc.CreateElement("div");
            doc.AppendChild(div);
            GenerateHtml(div);
        }
        void CreateViewFromTemplate()
        {
            XmlNode divContainer = doc.SelectSingleNode("//div[@id='start']");
            GenerateHtml(divContainer);
        }

        protected abstract void GenerateHtml(XmlNode startNode);
        public abstract void GenerateController(List<string> services = null, bool isHttp = false);


        protected void SaveFile()
        {
            string path = String.Format(@"{0}\{1}{2}.html", OutputPath, GenerationObjName, ViewName);
            doc.Save(path);
        }
    }
}
