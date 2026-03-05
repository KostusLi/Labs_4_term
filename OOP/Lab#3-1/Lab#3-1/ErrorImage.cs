using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_1
{
    public static class ErrorImage
    {
        public static void ShowError(string path)
        {
            try
            {
                AboutAndErrors form = new AboutAndErrors(path);
                form.Show();
            }
            catch
            {
                MessageBox.Show("Some problem with image, my brother");
            }
        }
    }
}
