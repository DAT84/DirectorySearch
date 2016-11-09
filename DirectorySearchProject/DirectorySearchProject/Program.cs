using DirectorySearchProject.Histogram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorySearchProject
{
    public class Program
    {
        [STAThreadAttribute]
        public static void Main(string[] args)
        {
            Search searchProgram = new Search();
            Histogram<string> histogram = searchProgram.StartSearch();

            if (histogram.Count > 0)
            {
                ChartForm form = new ChartForm();
                form.Draw(histogram);
                form.Focus();
                form.BringToFront();
                form.ShowDialog();
            }
        }
    }
}
