using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BootstrapGenerator
{
    public abstract class AView
    {
        public string ViewName { get; set; }
        public BootstrapGenerator Generator { get; private set; }
        public XmlDocument Doc { get; private set; }
        public XmlNode StartNode { get; private set; }
        public Object GenerationObj { get; private set; }
        public string GenerationObjName { get; private set; }
        public bool IsTemplateView { get; private set; }

        protected AView(BootstrapGenerator generator)
        {
            this.Generator = generator;
        }

        public void GenerateView(Object generationObj, bool useTemplate = false)
        {
            this.GenerationObj = generationObj;
            this.GenerationObjName = generationObj.GetType().Name;
            Doc = new XmlDocument();

            if (useTemplate)
            {
                IsTemplateView = true;
                Doc.Load(Generator.TemplateFilePath);
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

        public void SaveView(bool printMessage)
        {
            string path = String.Format(@"{0}\{1}{2}.html", Generator.OutputPath, GenerationObjName, ViewName);
            Doc.Save(path);
            if (printMessage)
                Console.WriteLine("{0}{1} generated at: {2}", GenerationObjName, ViewName, path);
        }
    }
}
