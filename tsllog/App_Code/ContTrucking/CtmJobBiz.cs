using C2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CtmJobBiz
/// </summary>
public class CtmJobBiz
{
    public CtmJobBiz(int Id)
    {
        CtmJobBiz_instance(Id);
    }
    public CtmJobBiz(string JobNo)
    {
        //CtmJob par = Manager.ORManager.GetObject<CtmJob>("JobNo='"+JobNo+"'");
        string sql = string.Format(@"select Id from ctm_job where JobNo=@JobNo");
        List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
        list.Add(new ConnectSql_mb.cmdParameters("@JobNo", JobNo, System.Data.SqlDbType.NVarChar, 100));
        int Id = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
        CtmJobBiz_instance(Id);
    }
    CtmJob job;
    CtmJob job_old;
    private void CtmJobBiz_instance(int Id)
    {
        if (Id > 0)
        {
            CtmJob par = Manager.ORManager.GetObject<CtmJob>(Id);
            setData(par);
        }
        else
        {
            setData(null);
        }
    }
    public void setData(CtmJob par)
    {
        job = par;
        resetOldValues();
    }
    public CtmJob getData()
    {
        return job;
    }

    private void resetOldValues()
    {
        if (job != null)
        {
            job_old = job.Clone() as CtmJob;
        }
        else
        {
            job_old = null;
        }
    }


    public BizResult toggleVoidJob(string user)
    {
        BizResult res = new BizResult();
        if (this.job != null && this.job.Id > 0)
        {
            if (job.JobStatus.Equals("Voided"))
            {
                res = unVoidJob(user);
            }else
            {
                res = voidJob(user);
            }
        }
        else
        {
            res.context = "error: data empty";
        }
        return res;
    }
    public BizResult voidJob(string user)
    {
        BizResult res = new BizResult();
        if (this.job != null && this.job.Id > 0)
        {

            System.Collections.ObjectModel.Collection<C2.CtmJobDet1> det1 = C2.Manager.ORManager.GetCollection<C2.CtmJobDet1>("JobNo='" + this.job.JobNo+"'");
            C2.CtmJobDet1Biz det1Bz = new CtmJobDet1Biz(0);
            foreach (C2.CtmJobDet1 temp in det1)
            {
                det1Bz.setData(temp);
                det1Bz.delete(user);
            }


            this.job.StatusCode = "CNL";
            this.job.JobStatus = "Voided";

            Manager.ORManager.StartTracking(this.job, Wilson.ORMapper.InitialState.Updated);
            Manager.ORManager.PersistChanges(this.job);
            
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = user;
            elog.ActionLevel_isJOB(this.job.Id);
            elog.setActionLevel(this.job.Id, CtmJobEventLogRemark.Level.Job, 5);
            elog.log();

            res.status = true;
        }
        else
        {
            res.context = "error: data empty";
        }
        return res;
    }
    public BizResult unVoidJob(string user)
    {
        BizResult res = new BizResult();
        if (this.job != null && this.job.Id > 0)
        {
            this.job.StatusCode = "USE";
            this.job.JobStatus = "Confirmed";

            Manager.ORManager.StartTracking(this.job, Wilson.ORMapper.InitialState.Updated);
            Manager.ORManager.PersistChanges(this.job);
            C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
            elog.Platform_isWeb();
            elog.Controller = user;
            elog.ActionLevel_isJOB(this.job.Id);
            elog.setActionLevel(this.job.Id, CtmJobEventLogRemark.Level.Job, 8);
            elog.log();
            res.status = true;
        }
        else
        {
            res.context = "error: data empty";
        }
        return res;
    }

    public BizResult jobBilling(string user)
    {
        BizResult res = new BizResult();
        if (this.job != null && this.job.Id > 0)
        {
            string sql = string.Format(@"select * from CTM_JobDet2 as det2
left outer join ctm_job as job on det2.JobNo=job.JobNo
where job.Id=@jobId and det2.Statuscode<>'C'");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@jobId", this.job.Id, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                res.context= "Have pending trips need to delivery";
            }
            else
            {
                sql = "update CTM_Job set StatusCode='CLS',JobStatus='Completed' where Id=@jobId";
                if (ConnectSql_mb.ExecuteNonQuery(sql, list).status)
                {
                    res.status = true;
                    C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                    elog.Platform_isWeb();
                    elog.Controller = user;
                    elog.ActionLevel_isJOB(this.job.Id);
                    elog.setActionLevel(this.job.Id, CtmJobEventLogRemark.Level.Job, 7);
                    elog.log();
                }
            }
        }
        else
        {
            res.context = "error: data empty";
        }
        return res;
    }
}