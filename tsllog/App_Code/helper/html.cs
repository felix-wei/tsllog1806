using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// html 的摘要说明
/// </summary>
public class html
{
    public html()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 生成Html
    /// </summary>
    /// <param name="template">模版文件</param>
    /// <param name="path">生成的文件目录</param>
    /// <param name="htmlname">生成的文件名</param>
    /// <param name="dic">字典</param>
    /// <param name="message">异常消息</param>
    /// <returns></returns>
    public static bool CreateHtml(string template, string path, string htmlname, Dictionary<string, string> dic, string message)
    {
        bool result = false;
        string templatepath = System.Web.HttpContext.Current.Server.MapPath(template);
        string htmlpath = System.Web.HttpContext.Current.Server.MapPath(path);
        string htmlnamepath = Path.Combine(htmlpath, htmlname);
        Encoding encode = Encoding.UTF8;
        StringBuilder html = new StringBuilder();
        if (File.Exists(htmlpath))
        {
            File.Delete(htmlpath);
        }
        try
        {
            //读取模版
            html.Append(File.ReadAllText(templatepath, encode));
        }
        catch (FileNotFoundException ex)
        {
            message = ex.Message;
            return false;
        }

        foreach (KeyValuePair<string, string> d in dic)
        {
            //替换数据
            html.Replace(
            string.Format("${0}$", d.Key),
            d.Value);
        }

        try
        {
            //写入html文件
            if (!Directory.Exists(htmlpath))
                Directory.CreateDirectory(htmlpath);
            File.WriteAllText(htmlnamepath, html.ToString(), encode);
            result = true;
        }
        catch (IOException ex)
        {
            message = ex.Message;
            return false;
        }

        return result;
    }
}
