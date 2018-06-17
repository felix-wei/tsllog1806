using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// RebuildImage 的摘要说明
/// </summary>
public class RebuildImage
{
    public RebuildImage()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    static string folder = "~/Photos/";
    static public void Rebuild(string path, int width)
    {
        if (isImage(path))
        {
            if (needRebuild(path))
            {
                Rebuild_byWidth(path, width);
            }
            else
            {
                Copy_file(path, width);
            }
        }
    }
    static public void Rebuild(string path)
    {
        if (isImage(path))
        {
            Rebuild_byWidth(path, 100);
            Rebuild_byWidth(path, 500);
        }
    }

    //static public void Rebuild_size200(string path, string filename)
    //{
    //    //string folder = "~/Upload/EventIcon";
    //    //string path = folder + "/";// HttpContext.Current.Server.MapPath(folder + "\\");
    //    //string filename = "1416480839837.jpg";


    //    //System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(path + filename));
    //    System.Drawing.Image image = System.Drawing.Image.FromFile(path + filename);
    //    int width = 200;
    //    int height = width * image.Height / image.Width;
    //    Image img = null;
    //    Image bmcpy = null;
    //    Graphics gh = null;
    //    img = image; //Image.FromFile(path + filename);
    //    bmcpy = new Bitmap(width, height);
    //    gh = Graphics.FromImage(bmcpy);
    //    gh.DrawImage(img, new Rectangle(0, 0, width, height));
    //    //bmcpy.Save(HttpContext.Current.Server.MapPath(path + "s200_" + filename), ImageFormat.Jpeg);
    //    bmcpy.Save(path + "s200_" + filename, ImageFormat.Jpeg);
    //    gh.Dispose();
    //    bmcpy.Dispose();
    //    img.Dispose();
    //}
    //static public void Rebuild_size200(string path)
    //{
    //    string folder = "~/Mobile/Upload/";
    //    //string filename = path.Substring(path.LastIndexOf('/')+1);
    //    //string pp = folder + path.Substring(0, path.LastIndexOf('/')) + "/";
    //    System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(folder + path));
    //    int width = 200;
    //    int height = width * image.Height / image.Width;
    //    Image img = null;
    //    Image bmcpy = null;
    //    Graphics gh = null;
    //    img = image;
    //    bmcpy = new Bitmap(width, height);
    //    gh = Graphics.FromImage(bmcpy);
    //    gh.DrawImage(img, new Rectangle(0, 0, width, height));
    //    bmcpy.Save(HttpContext.Current.Server.MapPath(folder + path.Substring(0, path.LastIndexOf('.')) + "_s200" + path.Substring(path.LastIndexOf('.'))), ImageFormat.Jpeg);
    //    gh.Dispose();
    //    bmcpy.Dispose();
    //    img.Dispose();
    //}


    static public void Rebuild_byWidth(string path, int _width)
    {
        //string folder = "~/Photos/";
        System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(folder + path));
        int width = _width;
        int height = width * image.Height / image.Width;
        Image img = null;
        Image bmcpy = null;
        Graphics gh = null;
        img = image;
        bmcpy = new Bitmap(width, height);
        gh = Graphics.FromImage(bmcpy);
        gh.DrawImage(img, new Rectangle(0, 0, width, height));
        string uploadPath = HttpContext.Current.Server.MapPath(folder + path.Substring(0, path.LastIndexOf('/')) + "/" + width.ToString() + "/");
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadPath);
        if (!dir.Exists)
        {
            dir.Create();
        }
        string newPath = folder + path.Substring(0, path.LastIndexOf('/')) + "/" + width.ToString() + path.Substring(path.LastIndexOf('/'));
        bmcpy.Save(HttpContext.Current.Server.MapPath(newPath), ImageFormat.Jpeg);
        gh.Dispose();
        bmcpy.Dispose();
        img.Dispose();
    }

    static public void Copy_file(string path, int width)
    {
        //string folder = "~/Photos/";
        string uploadPath = HttpContext.Current.Server.MapPath(folder + path.Substring(0, path.LastIndexOf('/')) + "/" + width.ToString() + "/");
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadPath);
        if (!dir.Exists)
        {
            dir.Create();
        }
        string newPath = HttpContext.Current.Server.MapPath(folder + path.Substring(0, path.LastIndexOf('/')) + "/" + width.ToString() + path.Substring(path.LastIndexOf('/')));
        string oldPath = HttpContext.Current.Server.MapPath(folder + path);
        File.Copy(HttpContext.Current.Server.MapPath(folder + path), newPath, true);
    }

    static public bool isImage(string path)
    {
        string[] extendFileName = { ".psd", ".jpg", ".gif", ".bmp", ".jpeg", ".png" };
        bool re = false;
        string temp = path.ToLower();
        for (int i = 0; i < extendFileName.Length; i++)
        {
            if (path.EndsWith(extendFileName[i]))
            {
                re = true;
                break;
            }
        }
        return re;
    }
    static public bool needRebuild(string path)
    {
        bool re = false;
        //======================= 100KB以上的才rebuild
        int l = 100 * 1024;
        //string folder = "~/Photos/";
        FileInfo fi = new FileInfo(HttpContext.Current.Server.MapPath(folder + path));
        if (fi.Length > l)
        {
            re = true;
        }
        return re;
    }


    static public bool Image_ExistOtherSize(string path, string type, int size)
    {
        bool res = false;
        if (type == "Image")
        {
            try
            {
                if (File.Exists(HttpContext.Current.Server.MapPath(folder + path.Substring(0, path.LastIndexOf('/')) + "/" + size.ToString() + path.Substring(path.LastIndexOf('/')))))
                {
                    res = true;
                }
            }
            catch { }
        }
        return res;
    }


    static public void File_ReName(string path, string newFileName)
    {
        string newPath = HttpContext.Current.Server.MapPath(folder + path.Substring(0, path.LastIndexOf('/')) + "/" + newFileName);
        string oldPath = HttpContext.Current.Server.MapPath(folder + path);
        File.Copy(HttpContext.Current.Server.MapPath(folder + path), newPath, true);
        //File.Delete(path);
    }
    static public bool File_Exist(string path)
    {
        string oldPath = HttpContext.Current.Server.MapPath(folder + path);
        return File.Exists(oldPath);
    }

    static public void Audio_ReName(string filename, string rename)
    {
        if (filename.EndsWith(".map3") && rename.Length > 0)
        {
            File_ReName(filename, rename);
        }
    }


    static public void Copy_file(string fp, string tp)
    {
        string uploadPath = HttpContext.Current.Server.MapPath(folder + tp.Substring(0, tp.LastIndexOf('/')) + "/");
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadPath);
        if (!dir.Exists)
        {
            dir.Create();
        }
        string newPath = HttpContext.Current.Server.MapPath(folder + tp);
        string oldPath = HttpContext.Current.Server.MapPath(folder + fp);
        File.Copy(HttpContext.Current.Server.MapPath(folder + fp), newPath, true);
    }

}