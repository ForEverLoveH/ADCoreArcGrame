using ArcFaceSDK.Utils;
using ArcSoftFace.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ArcSoftFace.ADCoreSystem
{
    public class ImageData
    {
        /// <summary>
        /// 图片最大大小限制
        /// </summary>
        private long maxSize = 1024 * 1024 * 2;
        /// <summary>
        /// 最大宽度
        /// </summary>
        private int maxWidth = 3000;

        /// <summary>
        /// 最大高度
        /// </summary>
        private int maxHeight = 3000;
        /// <summary>
        /// 检查图片的宽高
        /// </summary>
        /// <param name="image"></param>
        public  void CheckImageWidthAndHeight(ref Image image)
        {
            if (image == null)
            {
                return;
            }
            try
            {
                if (image.Height > maxWidth || image.Height > maxHeight)
                {
                    image = ImageUtil.ScaleImage(image, maxWidth, maxHeight);
                }
            }
            catch (Exception ex)
            {
                Console.Write("检测异常，请重试");
                return;
            }
        }
        /// <summary>
        ///  检查图片
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool CheckImage(string imagePath)
        {
            try
            {
                if (imagePath == null)
                {
                    Console.Write("图片不存在，请确认后再导入");
                    return false;
                }
                try
                {
                    //判断图片是否正常，如将其他文件把后缀改为.jpg，这样就会报错
                    Image image = ImageUtil.ReadFromFile(imagePath);
                    if (image == null)
                    {
                        throw new ArgumentException(" image is null");
                    }
                    else
                    {
                        image.Dispose();
                    }
                }
                catch
                {
                    Console.Write(string.Format("{0} 图片格式有问题，请确认后再导入", imagePath));
                    return false;
                }
                FileInfo fileCheck = new FileInfo(imagePath);
                if (!fileCheck.Exists)
                {
                    Console.Write(string.Format("{0} 不存在", fileCheck.Name));
                    return false;
                }
                else if (fileCheck.Length > maxSize)
                {
                    Console.Write(string.Format("{0} 图片大小超过2M，请压缩后再导入", fileCheck.Name));
                    return false;
                }
                else if (fileCheck.Length < 2)
                {
                    Console.Write(string.Format("{0} 图像质量太小，请重新选择", fileCheck.Name));
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogInfo(GetType(), ex);
            }
            return true;
        }
        
        public  void  SaveImageFileToDestion(string destionDirectory , List<string> ImagePath)
        {
            if (!Directory.Exists(destionDirectory))
            {
                Directory.CreateDirectory(destionDirectory);
            }
            if (ImagePath.Count > 0)
            {
                foreach (var image in ImagePath)
                {
                    var m = GetImage(image);
                    SaveImageToFace(destionDirectory, m);
                }
            }
        }
        /// <summary>
        ///  将bitmap 文件存到本地文件中
        /// </summary>
        /// <param name="destionDirectory"></param>
        /// <param name="m"></param>
        private void SaveImageToFace(string destionDirectory, Bitmap m)
        {
             
        }

        // <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Bitmap GetImage(string image)
        {
            Bitmap b = null;
            if (!File.Exists(image))
            {
                MessageBox.Show("该路径下：" + image.ToString() + "!文件找不到");
                return b;
            }
            try
            {
                FileStream fs = new FileStream(image, FileMode.Open, FileAccess.Read);
                b = (Bitmap)System.Drawing.Bitmap.FromStream(fs);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return b;
        }
    }
}
