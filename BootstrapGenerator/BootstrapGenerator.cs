using BootstrapGenerator.AngularGeneration;
using System;
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
            fdb.Description = "Chose an output path to save your generated html view to";
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                Dog dog = new Dog();

                AngularTable tableGenerator = new AngularTable(generator);
                tableGenerator.GenerateView(dog, fdb.SelectedPath, true);
            }

            Console.WriteLine("View generation successful, press any key to exit...");
            Console.ReadLine();
        }
    }
}
