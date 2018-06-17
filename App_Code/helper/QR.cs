using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using ThoughtWorks.QRCode.Codec;

/// <summary>
/// QR 的摘要说明
/// </summary>
public class QR
{
	public QR()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//

	}
    public Bitmap Create_QR(string text)
    {
        Bitmap bt;
        string enCodeString = text;
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        //设置编码模式  
        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
        //设置编码测量度  
        qrCodeEncoder.QRCodeScale = 5;
        //设置编码版本  
        qrCodeEncoder.QRCodeVersion = 0;
        //设置编码错误纠正  
        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M; 
        bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
        return bt;
    }
}