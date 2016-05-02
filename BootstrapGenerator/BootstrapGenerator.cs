using BootstrapGenerator.AngularGeneration.ViewGeneration;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BootstrapGenerator
{
    public class BootstrapGenerator
    {
        public GeneratorConfiguration configuration { get; set; }
        public string OutputPath { get; set; }
        public readonly string TemplateFileName = "AngularBootstrap.xml";
        public string TemplateFilePath { get; set; }

        public BootstrapGenerator()
        {
            configuration = new GeneratorConfiguration(this);
            TemplateFilePath = String.Format(@"{0}\Templates\{1}", Functions.GetProjectPathString(), TemplateFileName);
        }

        [STAThread]
        static void Main(string[] args)
        {
            BootstrapGenerator generator = new BootstrapGenerator();

            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.Description = "Choose an output path to save your generated file to...";
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                generator.OutputPath = fdb.SelectedPath;
                Person person = new Person();
                List<string> classAttributes = new List<string>(){ "table-striped", "table-hover", "table-bordered" };

                AngularTable angularTable = new AngularTable(generator, classAttributes);
                angularTable.GenerateView(person, true);
                angularTable.GenerateController();

                //AngularForm angularForm = new AngularForm(generator);
                //angularForm.GenerateView(person, fdb.SelectedPath, true);

                //AngularFormSmall angFormSmall = new AngularFormSmall(generator);
                //angFormSmall.GenerateView(person, fdb.SelectedPath, true);

                //AngularDetails angDetails = new AngularDetails(generator);
                //angDetails.GenerateView(person, fdb.SelectedPath, true);
            }

            Console.WriteLine("\nView generation operations successful, press enter key to exit...");
            Console.ReadLine();
        }
    }
}
