using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem
{
    public class LocalFile
    {
        public string[] openLocalFile(bool s)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择图片";
            openFileDialog.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = s;
            openFileDialog.FileName = string.Empty;
            if (openFileDialog.ShowDialog().Equals(DialogResult.OK))
            {
                string[] imagePath;
                imagePath = openFileDialog.FileNames;
                return imagePath;
            }
            else
            {
                return null;
            }
        }
    }
}
