using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseObject
/// </summary>
public class BaseObject
{
    private string createBy;
    private DateTime createDateTime;
    private string updateBy;
    private DateTime updateDateTime;
    private string statusCode;
    public string StatusCode
    {
        get { return this.statusCode; }
        set { this.statusCode = value; }
    }
    public string CreateBy
    {
        get { return this.createBy; }
        set { this.createBy = value; }
    }

    public DateTime CreateDateTime
    {
        get { return this.createDateTime; }
        set { this.createDateTime = value; }
    }

    public string UpdateBy
    {
        get { return this.updateBy; }
        set { this.updateBy = value; }
    }

    public DateTime UpdateDateTime
    {
        get { return this.updateDateTime; }
        set { this.updateDateTime = value; }
    }
    private string postBy;
    private DateTime postDateTime;
    public string PostBy
    {
        get { return this.postBy; }
        set { this.postBy = value; }
    }

    public DateTime PostDateTime
    {
        get { return this.postDateTime; }
        set { this.postDateTime = value; }
    }

}