using ArcFaceSDK.Utils;
using ArcSoftFace.Utils;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.UDF;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

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
        public void CheckImageWidthAndHeight(ref Image image)
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
        /// <summary>
        ///  将图片文件存入本地
        /// </summary>
        /// <param name="destionDirectory"></param>
        /// <param name="ImagePath"></param>
        public void SaveImageFileToDestion(string destionDirectory, string[] ImagePath)
        {

            if (!Directory.Exists(destionDirectory))
            {
                Directory.CreateDirectory(destionDirectory);
            }
            if (ImagePath.Length > 0)
            {
                for (int i = 0; i < ImagePath.Length; i++)
                {
                    byte[] by = SetImageToByte(ImagePath[i]);
                    SaveImageDataToDestion(destionDirectory, by, i);
                }
            }
        }
        /// <summary>
        /// 保存图片到本地
        /// </summary>
        /// <param name="by"></param>

        private void SaveImageDataToDestion(string path, byte[] by, int index)
        {
            try
            {

                using (MemoryStream ms = new MemoryStream(by))
                {
                    Image image = Image.FromStream(ms);

                    image.Save(@path + "/" + index + ".png");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///  将图片转为字节的形式
        /// </summary>
        /// <param name="path"></param>

        private byte[] SetImageToByte(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            return data;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="destionDirectory"></param>
        /// <param name="bitmap"></param>
        private void SaveBitMapImage(string destionDirectory, Bitmap bitmap)
        {
            bitmap.Save(destionDirectory, System.Drawing.Imaging.ImageFormat.Png);
        }


        public Bitmap GetImageToBitMap(string image)
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
        /// <summary>
        ///  创建对应组的文件夹
        /// </summary>
        /// <param name="groupId"></param>
        public string CreateFaceGroupFile(string groupId, string FaceDirectory)
        {
            if (!System.IO.Directory.Exists(FaceDirectory))
            {
                Directory.CreateDirectory(FaceDirectory);
            }

            String path = FaceDirectory + "/" + groupId;
            if (System.IO.Directory.Exists(path))
            {
                var l = Directory.CreateDirectory(path);
                string groupDirect = l.FullName;
                return groupDirect; 
            }
            else
            {
                return path;
            }
            
            
             

        }
        /// <summary>
        ///  获取文件中的所有图片文件
        /// </summary>
        /// <param name="paths"></param>
        public ImageModel GetDirectoryImageFile(string paths)
        {
            if (!string.IsNullOrEmpty(paths))
            {
                List<Image> list = new List<Image>();
                string[] imageFilePath = Directory.GetFiles(paths);
                List<string> images = new List<string>();
                foreach (string filePath in imageFilePath)
                {
                    if (filePath.EndsWith(".bmp") || filePath.EndsWith(".png") || filePath.EndsWith(".jpg") | filePath.EndsWith(".jpeg"))
                    {
                        images.Add(filePath);
                    }
                }
                if (images.Count > 0)
                {
                    for (int i = 0; i < images.Count; i++)
                    {
                        byte[] by = SetImageToByte(images[i]);
                        list.Add(SetByteToImage(by));

                    }
                }
                ImageModel imageModel = new ImageModel()
                {
                    ImagePath = images,
                    images = list
                };
                return imageModel;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Imagebyte"></param>
        /// <returns></returns>
        private Image SetByteToImage(byte[] Imagebyte)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(Imagebyte))
                {
                    Image image = Image.FromStream(ms);
                    return image;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public  void DelectDirectoryImageFile(string path, int index)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                FileInfo[] files = directoryInfo.GetFiles(); // 获取这个文见夹下所有文件
                List<string> pathlist = new List<string>();
                foreach (FileInfo file in files)
                {
                    string filePath = file.FullName;// 拿到路径
                    if (filePath.EndsWith(".bmp") || filePath.EndsWith(".png") || filePath.EndsWith(".jpg") | filePath.EndsWith(".jpeg"))
                    {
                        pathlist.Add(filePath);  // 判断文件是不是图片文件，是则拿到路径
                    }
                }
                string spth = pathlist[index];
                if (!string.IsNullOrEmpty(spth))
                {
                    File.Delete(spth);
                    Console.WriteLine("删除成功！！");
                }
                else
                {
                    Console.WriteLine("文件不存在！");
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}
