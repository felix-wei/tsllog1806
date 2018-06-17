using System;
using System.Collections.Generic;
using System.Data;

namespace C2
{
    public class CtmJobEventLog
    {
        private int id;
        private DateTime createDateTime;
        private string controller;
        private string jobNo;
        private string containerNo;
        private string trip;
        private string driver;
        private string towhead;
        private string trail;
        private string remark;
        private string note1;
        private string note2;
        private string note3;
        private string note4;
        private string lat;
        private string lng;
        private string platform;
        private string parentJobNo;
        private string parentJobType;
        private string jobType;
        private string note1Type;
        private string jobStatus;
        private decimal value1;
        private decimal value2;
        private decimal value3;
        private decimal value4;
        private decimal value5;
        private decimal value6;
        private decimal value7;
        private decimal value8;
        private int actionId;
        private string actionLevel;
        private string actionType;
        public int Id
        {
            get { return this.id; }
        }

        public DateTime CreateDateTime
        {
            get { return this.createDateTime; }
            set { this.createDateTime = value; }
        }

        public string Controller
        {
            get { return this.controller; }
            set { this.controller = value; }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string ContainerNo
        {
            get { return this.containerNo; }
            set { this.containerNo = value; }
        }

        public string Trip
        {
            get { return this.trip; }
            set { this.trip = value; }
        }

        public string Driver
        {
            get { return this.driver; }
            set { this.driver = value; }
        }

        public string Towhead
        {
            get { return this.towhead; }
            set { this.towhead = value; }
        }

        public string Trail
        {
            get { return this.trail; }
            set { this.trail = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string Note1
        {
            get { return this.note1; }
            set { this.note1 = value; }
        }

        public string Note2
        {
            get { return this.note2; }
            set { this.note2 = value; }
        }

        public string Note3
        {
            get { return this.note3; }
            set { this.note3 = value; }
        }

        public string Note4
        {
            get { return this.note4; }
            set { this.note4 = value; }
        }

        public string Lat
        {
            get { return this.lat; }
            set { this.lat = value; }
        }

        public string Lng
        {
            get { return this.lng; }
            set { this.lng = value; }
        }

        public string ParentJobNo
        {
            get { return this.parentJobNo; }
            set { this.parentJobNo = value; }
        }

        public string ParentJobType
        {
            get { return this.parentJobType; }
            set { this.parentJobType = value; }
        }

        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }

        public string Note1Type
        {
            get { return this.note1Type; }
            set { this.note1Type = value; }
        }

        public string JobStatus
        {
            get { return this.jobStatus; }
            set { this.jobStatus = value; }
        }

        public decimal Value1
        {
            get { return this.value1; }
            set { this.value1 = value; }
        }

        public decimal Value2
        {
            get { return this.value2; }
            set { this.value2 = value; }
        }

        public decimal Value3
        {
            get { return this.value3; }
            set { this.value3 = value; }
        }

        public decimal Value4
        {
            get { return this.value4; }
            set { this.value4 = value; }
        }

        public decimal Value5
        {
            get { return this.value5; }
            set { this.value5 = value; }
        }

        public decimal Value6
        {
            get { return this.value6; }
            set { this.value6 = value; }
        }

        public decimal Value7
        {
            get { return this.value7; }
            set { this.value7 = value; }
        }

        public decimal Value8
        {
            get { return this.value8; }
            set { this.value8 = value; }
        }


        public string Platform
        {
            get { return this.platform; }
            set { this.platform = value.ToUpper(); }
        }
        #region platform format
        public void Platform_isWeb()
        {
            this.Platform = "WEB";
        }
        public void Platform_isMobile()
        {
            this.Platform = "MOBILE";
        }
        public void Platform_isBackend()
        {
            this.platform = "BACKEND";
        }
        //public void Platform_isAndroid()
        //{
        //    this.Platform = "ANDROID";
        //}
        //public void Platform_isIOS()
        //{
        //    this.Platform = "IOS";
        //}
        #endregion

        public int ActionId
        {
            get { return this.actionId; }
            set
            {
                this.actionId = value;
                fixActionInfo_ByActionIdLV();
            }
        }

        public string ActionLevel
        {
            get { return this.actionLevel; }
            set
            {
                this.actionLevel = value.ToUpper();
                fixActionInfo_ByActionIdLV();
            }
        }
        public string ActionType
        {
            get { return this.actionType; }
            set
            {
                this.actionType = value;
            }
        }
        #region actionlevel format

        public void setActionLevel(int actId, CtmJobEventLogRemark.Level actionLevel, int remarkCommond)
        {
            setActionLevel(actId, actionLevel, remarkCommond, "");
        }
        public void setActionLevel(int actId, CtmJobEventLogRemark.Level actionLevel, int remarkCommond, string remarkPlus)
        {
            this.ActionLevel = actionLevel.ToString();
            this.ActionId = actId;
            setRemark(actionLevel, remarkCommond, remarkPlus);
        }
        public void ActionLevel_isJOB(int actId)
        {
            this.ActionId = actId;
            this.ActionLevel = "JOB";
        }
        public void ActionLevel_isQuoted(int actId)
        {
            this.ActionId = actId;
            this.ActionLevel = "QUOTATION";
        }
        public void ActionLevel_isCONT(int actId)
        {
            this.ActionId = actId;
            this.ActionLevel = "CONT";
        }
        public void ActionLevel_isTRIP(int actId)
        {
            this.ActionId = actId;
            this.ActionLevel = "TRIP";
        }
        public void ActionLevel_isCARGO(int actId)
        {
            this.ActionId = actId;
            this.ActionLevel = "CARGO";
        }
        public void ActionLevel_isINVOICE(int actId)
        {
            this.ActionId = actId;
            this.ActionLevel = "INVOICE";
        }

        public void ActionLevel_isAttachment(int actId)
        {
            this.ActionId = actId;
            this.ActionLevel = "ATTACHMENT";
        }
        #endregion

        #region fix information by action Id & level
        public void fixActionInfo_ByQuotedNo(string _QuoteNo)
        {
            string sql = string.Format(@"select Id from CTM_Job where QuoteNo=@QuoteNo");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@QuoteNo", _QuoteNo, SqlDbType.NVarChar, 100));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                //this.jobNo = dt.Rows[0]["JobNo"].ToString();
                //this.jobType = dt.Rows[0]["JobType"].ToString();
                ActionLevel_isJOB(SafeValue.SafeInt(dt.Rows[0]["Id"], 0));
            }
        }
        public void fixActionInfo_ByJobNo(string _JobNo)
        {
            string sql = string.Format(@"select Id from CTM_Job where JobNo=@JobNo");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@JobNo", _JobNo, SqlDbType.NVarChar, 100));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                //this.jobNo = dt.Rows[0]["JobNo"].ToString();
                //this.jobType = dt.Rows[0]["JobType"].ToString();
                ActionLevel_isJOB(SafeValue.SafeInt(dt.Rows[0]["Id"], 0));
            }
        }
        public void fixActionInfo_ByAttachmentId(int AttachId)
        {
            //string sql = string.Format(@"select RefNo from CTM_Attachment where Id=@Id");
            //List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            //list.Add(new ConnectSql_mb.cmdParameters("@Id", AttachId, SqlDbType.Int));
            //DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            //if (dt.Rows.Count > 0)
            //{
            //    this.jobNo = dt.Rows[0]["RefNo"].ToString();
            //    fixActionInfo_ByJobNo(this.jobNo);
            //}
            ActionLevel_isAttachment(AttachId);
        }
        private void fixActionInfo_ByActionIdLV()
        {
            if (this.actionId > 0 && this.actionLevel != null)
            {
                switch (this.actionLevel)
                {
                    case "QUOTATION":
                        fixActionInfo_levelQuoted();
                        break;
                    case "JOB":
                        fixActionInfo_levelJob();
                        break;
                    case "CONT":
                    case "CONTAINER":
                        fixActionInfo_levelCont();
                        break;
                    case "TRIP":
                        fixActionInfo_levelTrip();
                        break;
                    case "CARGO":
                        fixActionInfo_levelCargo();
                        break;
                    case "INVOICE":
                        fixActionInfo_levelInvoice();
                        break;
                    case "ATTACHMENT":
                        fixActionInfo_levelAttachment();
                        break;
                }
            }
        }
        private void fixActionInfo_levelQuoted()
        {
            string sql = string.Format(@"select QuoteNo,JobType from CTM_Job where Id=@ActionId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ActionId", this.actionId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                this.jobNo = dt.Rows[0]["QuoteNo"].ToString();
                this.jobType = dt.Rows[0]["JobType"].ToString();
            }
        }
        private void fixActionInfo_levelJob()
        {
            string sql = string.Format(@"select JobNo,JobType from CTM_Job where Id=@ActionId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ActionId", this.actionId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                this.jobNo = dt.Rows[0]["JobNo"].ToString();
                this.jobType = dt.Rows[0]["JobType"].ToString();
            }
        }
        private void fixActionInfo_levelCont()
        {
            string sql = string.Format(@"select det1.ContainerNo,det1.ContainerType,job.JobNo,job.JobType 
from CTM_JobDet1 as det1
left outer join CTM_Job as job on det1.JobNo=job.JobNo
where det1.Id=@ActionId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ActionId", this.actionId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                this.jobNo = dt.Rows[0]["JobNo"].ToString();
                this.jobType = dt.Rows[0]["JobType"].ToString();
                this.containerNo = dt.Rows[0]["ContainerNo"].ToString();
            }
        }
        private void fixActionInfo_levelTrip()
        {
            string sql = string.Format(@"select job.JobNo,job.JobType,det1.ContainerNo,det2.DriverCode,det2.TowheadCode,det2.ChessisCode,det2.TripIndex  
from CTM_JobDet2 as det2
left outer join CTM_JobDet1 as det1 on det2.Det1Id=det1.Id
left outer join CTM_Job as job on det2.JobNo=job.JobNo
where det2.Id=@ActionId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ActionId", this.actionId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                this.jobNo = dt.Rows[0]["JobNo"].ToString();
                this.jobType = dt.Rows[0]["JobType"].ToString();
                this.containerNo = dt.Rows[0]["ContainerNo"].ToString();
                this.driver = dt.Rows[0]["DriverCode"].ToString();
                this.towhead = dt.Rows[0]["TowheadCode"].ToString();
                this.trail = dt.Rows[0]["ChessisCode"].ToString();
                this.trip = dt.Rows[0]["TripIndex"].ToString();
            }
        }
        private void fixActionInfo_levelCargo()
        {
            string sql = string.Format(@"select job.JobNo,job.JobType,det1.ContainerNo,det2.TripIndex
from job_house as cargo
left outer join CTM_JobDet2 as det2 on cargo.TripId=det2.Id 
left outer join CTM_JobDet1 as det1 on cargo.ContId=det1.Id 
left outer join CTM_Job as job on cargo.JobNo=job.JobNo
where cargo.Id=@ActionId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ActionId", this.actionId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                this.jobNo = dt.Rows[0]["JobNo"].ToString();
                this.jobType = dt.Rows[0]["JobType"].ToString();
                this.containerNo = dt.Rows[0]["ContainerNo"].ToString();
                this.trip = dt.Rows[0]["TripIndex"].ToString();
            }
        }
        private void fixActionInfo_levelInvoice()
        {
            string sql = string.Format(@"select MastRefNo,JobType from XAArInvoice i inner join CTM_Job j on i.MastRefNo=j.JobNo  where SequenceId=@ActionId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ActionId", this.actionId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                this.jobNo = dt.Rows[0]["MastRefNo"].ToString();
                this.jobType = dt.Rows[0]["JobType"].ToString();
            }
        }

        private void fixActionInfo_levelAttachment()
        {
            string sql = string.Format(@"select att.Id,att.RefNo,job.JobType,cont.ContainerNo,trip.DriverCode,trip.TowheadCode,trip.ChessisCode
from CTM_Attachment as att
left outer join CTM_JobDet2 as trip on trip.Id=att.TripId
left outer join CTM_JobDet1 as cont on cont.ContainerNo=att.ContainerNo and cont.JobNo=att.RefNo
left outer join CTM_Job as job on job.JobNo=att.RefNo
where att.Id=@ActionId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ActionId", this.actionId, SqlDbType.Int));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                this.jobNo = dt.Rows[0]["RefNo"].ToString();
                this.jobType = dt.Rows[0]["JobType"].ToString();
                this.containerNo = dt.Rows[0]["ContainerNo"].ToString();
                this.driver = dt.Rows[0]["DriverCode"].ToString();
                this.towhead = dt.Rows[0]["TowheadCode"].ToString();
                this.trail = dt.Rows[0]["ChessisCode"].ToString();
            }

        }
        #endregion

        private void setRemark(CtmJobEventLogRemark.Level l, CtmJobEventLogRemark.Command c)
        {
            this.Remark = CtmJobEventLogRemark.getDes(l, c);
        }
        private void setRemark(CtmJobEventLogRemark.Level l, CtmJobEventLogRemark.Command c, string plus)
        {
            this.Remark = CtmJobEventLogRemark.getDes(l, c);
            this.Remark += plus;
        }
        public void setRemark(CtmJobEventLogRemark.Level l, int c)
        {
            this.Remark = CtmJobEventLogRemark.getDes(l, c);
        }
        public void setRemark(CtmJobEventLogRemark.Level l, int c, string plus)
        {
            this.Remark = CtmJobEventLogRemark.getDes(l, c);
            this.Remark += plus;
        }
        private void setRemark(int l, int c, string plus)
        {
            this.Remark = CtmJobEventLogRemark.getDes(l, c);
            this.Remark += plus;
        }
        private void setRemark(string code)
        {
            this.Remark = CtmJobEventLogRemark.getDes(code);
        }
        private void setRemark(string code, string plus)
        {
            this.Remark = CtmJobEventLogRemark.getDes(code);
            this.Remark += plus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public void isAlert(string client)
        {
            this.note1Type = "AlertClient";
            this.note1 = client;
        }

        public void log()
        {
            if (ActionType == null)
            {
                ActionType = "UPDATE";
            }
            string sql = string.Format(@"insert into CTM_JobEventLog (CreateDateTime,Controller,JobNo,ContainerNo,Trip,Driver,Towhead,Trail,Remark,Note1,Note2,Note3,Note4,Lat,Lng,Platform,JobType,ParentJobNo,ParentJobType,Note1Type,ActionId,ActionLevel,JobStatus,ActionType) values(getdate(),@Controller,@JobNo,@ContainerNo,@Trip,@Driver,@Towhead,@Trail,@Remark,@Note1,@Note2,@Note3,@Note4,@Lat,@Lng,@Platform,@JobType,@ParentJobNo,@ParentJobType,@Note1Type,@ActionId,@ActionLevel,@JobStatus,@ActionType)");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = new ConnectSql_mb.cmdParameters("@Controller", this.Controller, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@JobNo", this.JobNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", this.ContainerNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Trip", this.Trip, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Driver", this.Driver, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Towhead", this.Towhead, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Trail", this.Trail, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Remark", this.Remark, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note1", this.Note1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note2", this.Note2, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note3", this.Note3, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note4", this.Note4, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Lat", this.Lat, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Lng", this.Lng, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Platform", this.Platform, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@JobType", this.JobType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ParentJobNo", this.ParentJobNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ParentJobType", this.ParentJobType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note1Type", this.Note1Type, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            list.Add(new ConnectSql_mb.cmdParameters("@ActionId", this.actionId, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@ActionLevel", this.actionLevel, SqlDbType.NVarChar, 50));
            list.Add(new ConnectSql_mb.cmdParameters("@JobStatus", this.JobStatus, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@ActionType", this.actionType, SqlDbType.NVarChar, 50));
            ConnectSql_mb.ExecuteNonQuery(sql, list);


        }

        public void log_edi()
        {
            if (ActionType == null)
            {
                ActionType = "UPDATE";
            }
            string sql = string.Format(@"insert into CTM_JobEventLog (CreateDateTime,Controller,JobNo,ContainerNo,Trip,Driver,Towhead,Trail,Remark,Note1,Note2,Note3,Note4,Lat,Lng,Platform,JobType,ParentJobNo,ParentJobType,Note1Type,ActionId,ActionLevel,JobStatus,ActionType) values(getdate(),@Controller,@JobNo,@ContainerNo,@Trip,@Driver,@Towhead,@Trail,@Remark,@Note1,@Note2,@Note3,@Note4,@Lat,@Lng,@Platform,@JobType,@ParentJobNo,@ParentJobType,@Note1Type,@ActionId,@ActionLevel,@JobStatus,@ActionType)");
            List<ConnectEdi.cmdParameters> list = new List<ConnectEdi.cmdParameters>();
            ConnectEdi.cmdParameters cpar = new ConnectEdi.cmdParameters("@Controller", this.Controller, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@JobNo", this.JobNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@ContainerNo", this.ContainerNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Trip", this.Trip, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Driver", this.Driver, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Towhead", this.Towhead, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Trail", this.Trail, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Remark", this.Remark, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Note1", this.Note1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Note2", this.Note2, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Note3", this.Note3, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Note4", this.Note4, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Lat", this.Lat, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Lng", this.Lng, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Platform", this.Platform, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@JobType", this.JobType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@ParentJobNo", this.ParentJobNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@ParentJobType", this.ParentJobType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectEdi.cmdParameters("@Note1Type", this.Note1Type, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            list.Add(new ConnectEdi.cmdParameters("@ActionId", this.actionId, SqlDbType.Int));
            list.Add(new ConnectEdi.cmdParameters("@ActionLevel", this.actionLevel, SqlDbType.NVarChar, 50));
            list.Add(new ConnectEdi.cmdParameters("@JobStatus", this.JobStatus, SqlDbType.NVarChar, 100));
            list.Add(new ConnectEdi.cmdParameters("@ActionType", this.actionType, SqlDbType.NVarChar, 50));
            ConnectEdi.ExecuteNonQuery(sql, list);


        }
    }
}
