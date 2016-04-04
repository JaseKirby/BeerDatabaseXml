using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BootstrapGenerator
{
    class BootstrapGenerator
    {
        [STAThread]
        static void Main(string[] args)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            fdb.Description = "Chose an output path to save your generated html view to";
            if (fdb.ShowDialog() == DialogResult.OK)
            {
                Dog dog = new Dog() { Id = 1, FirstName = "Spot", LastName = "Wolferson", DateOfBirth = DateTime.Now };
                AngularTableGenerator tableGenerator = new AngularTableGenerator(dog, fdb.SelectedPath, true);
            }

            Console.WriteLine("View generation successful, press any key to exit...");
            Console.ReadLine();
        }
    }
}
