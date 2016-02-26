using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace Bm.Services.Common
{
    /// <summary>
    /// 含有QR码的描述类和包装编码和渲染
    /// </summary>
    public static class QrCodeService
    {
        /// <summary>  
        /// 获取二维码  
        /// </summary>  
        /// <param name="strContent">待编码的字符</param>  
        /// <param name="ms">输出流</param>  
        ///<returns>True if the encoding succeeded, false if the content is empty or too large to fit in a QR code</returns>  
        private static bool GetQrCode(string strContent, MemoryStream ms)
        {
            var Ecl = ErrorCorrectionLevel.M; //误差校正水平   
            var content = strContent;//待编码内容  
            var QuietZones = QuietZoneModules.Two;  //空白区域   
            var ModuleSize = 12;//大小  
            var encoder = new QrEncoder(Ecl);
            QrCode qr;
            if (encoder.TryEncode(content, out qr))//对内容进行编码，并保存生成的矩阵  
            {
                var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
                render.WriteToStream(qr.Matrix, ImageFormat.Png, ms);
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将字符串转换为QrCode结果
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static ActionResult QrCodeResult(string content)
        {
            // Render the QR code as an image  
            using (var ms = new MemoryStream())
            {
                if (GetQrCode(content, ms))
                {
                    return new FileContentResult(ms.GetBuffer(), "image/Png");
                }
                return new ContentResult { Content = "生成QrCode失败，请重试", ContentEncoding = Encoding.UTF8 };
            }
        }
    }
}
