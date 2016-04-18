using BootstrapGenerator.AngularGeneration;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BootstrapGenerator
{
    public class BootstrapGenerator
    {
        public GeneratorConfiguration configuration { get; set; }
        public readonly string TemplateFileName = "BootstrapTemplate.xml";
        public string TemplateFilePath { get; set; }

        public BootstrapGenerator()
        {
            configuration = new GeneratorConfiguration(this);
            TemplateFilePath = String.Format(@"{0}\Templates\{1}", GlobalMethods.GetProjectPathString(), TemplateFileName);
        }

        [STAThread]
        static void Main(string[] args)
        {
            BootstrapGenerator generator = new BootstrapGenerator();

            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.Description = "Chose an output path to save your generated file to...";
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                Dog dog = new Dog();
                List<string> classAttributes = new List<string>(){ "table-striped", "table-hover", "table-bordered" };

                AngularTable angularTable = new AngularTable(generator, classAttributes);
                angularTable.GenerateView(dog, fdb.SelectedPath, true);
                //angularTable.GenerateController();  Funcitionality not yet implemented
            }

            Console.WriteLine("View generation successful, press any key to exit...");
            Console.ReadLine();
        }
    }
}
