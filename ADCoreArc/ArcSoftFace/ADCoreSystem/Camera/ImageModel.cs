using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.ADCoreSystem
{
    public class ImageModel
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public List<string> ImagePath { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public List<Image> images { get; set; }    
    }
}
