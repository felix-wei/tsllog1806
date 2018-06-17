using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace C2
{
    /// <summary>
    /// Summary description for CtmJobDet2Biz
    /// </summary>
    public class CtmJobDet2Biz
    {
        public static int getTripSeqId_tripDate(int tripId)
        {
            int res = 0;
            string sql = string.Format(@"select dbo.func_getTripSequenceId(@ii)");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ii", tripId, SqlDbType.Int));
            res = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list).context, 0);
            return res;
        }

        public static void reSizeIncentiveData(int tripId, Dictionary<string, decimal> d)
        {
            //============= auto set trip incentive
            //            string sql = string.Format(@"select det2.BillLock
            //from ctm_jobdet2 as det2
            //where det2.Id=@TripId");
            //            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            //            list.Add(new ConnectSql_mb.cmdParameters("@TripId", tripId, SqlDbType.Int));
            //            string BillLock = ConnectSql_mb.ExecuteScalar(sql, list).context;
            //            if (BillLock != "YES")
            //            {
            //                if (d.ContainsKey("Trip"))
            //                {
            //                    d.Remove("Trip");
            //                }
            //            }
        }

        public CtmJobDet2Biz(int id)
        {
            if (id > 0)
            {
                CtmJobDet2 par = Manager.ORManager.GetObject<CtmJobDet2>(id);
                setData(par);
            }
        }
        CtmJobDet2 trip;
        public void setData(CtmJobDet2 par)
        {
            trip = par;
            resetOldValues();
        }
        public CtmJobDet2 getData()
        {
            return trip;
        }

        string oldv_StatusCode;
        string oldv_containerNo;
        string oldv_driver;
        string oldv_driver2;
        string oldv_driver11;
        string oldv_driver12;
        private void resetOldValues()
        {
            if (trip != null)
            {
                oldv_StatusCode = trip.Statuscode;
                oldv_containerNo = trip.ContainerNo;
                oldv_driver = trip.DriverCode;
                oldv_driver2 = trip.DriverCode2;
                oldv_driver11 = trip.DriverCode11;
                oldv_driver12 = trip.DriverCode12;
            }
            else
            {
                oldv_StatusCode = "";
                oldv_containerNo = "";
                oldv_driver = "";
                oldv_driver2 = "";
                oldv_driver11 = "";
                oldv_driver12 = "";
            }
        }



        public BizResult insert(string user, CtmJobDet2 par)
        {
            setData(null);
            if (par == null)
            {
                throw new Exception("data is null");
            }
            BizResult res = new BizResult();
            if (inserting_verify(res, user, par))
            {
                Manager.ORManager.StartTracking(par, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(par);
                res.status = true;
                trip = par;

                resetOldValues();

                inserted_to_container(user);


                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isBackend();
                elog.Controller = user;
                elog.setActionLevel(trip.Id, CtmJobEventLogRemark.Level.Trip, 1);
                elog.log();
            }
            return res;
        }
        private bool inserting_verify(BizResult bzR, string user, CtmJobDet2 par)
        {
            bool res = updating_verify(bzR, user, par);
            return res;
        }

        private void inserted_to_container(string user)
        {
            C2.CtmJobDet1Biz bz = new CtmJobDet1Biz(this.trip.Det1Id);
            C2.CtmJobDet1 det1 = bz.getData();
            if (det1 != null)
            {
                string sql = string.Format(@"select count(*) from ctm_jobdet2 where (StatusCode='P' or StatusCode='S') and Det1Id=@Det1Id");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", det1.Id, SqlDbType.Int));
                int cc = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql, list), 0);
                if (cc > 0)
                {
                    det1.StatusCode = "InTransit";
                    bz.update(user);
                }
            }
        }

        public BizResult update(string user)
        {

            BizResult res = new BizResult();
            if (trip == null)
            {
                res.context = "Data is empty";
                return res;
            }

            if (updating_verify(res, user, trip))
            {
                updated_before(user);
                Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(trip);
                res.status = true;
                updated_Status_changed(user);
                updated_reSequence_autoFixIncentive();

                updated_to_container(user);
                updated_to_house(user);
                updated_to_cost(user);

                resetOldValues();

                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isBackend();
                elog.Controller = user;
                elog.setActionLevel(trip.Id, CtmJobEventLogRemark.Level.Trip, 3," Status["+trip.Statuscode+"]FromDate["+trip.FromDate.ToString("yyyyMMdd")+"]");
                elog.log();
            }

            return res;
        }

        private void updated_before(string user)
        {
            updated_before_clearDriver_cost();
            if (trip.DriverCode != null && trip.DriverCode.Length > 0)
            {
                trip.DriverCode3 = trip.DriverCode;
            }else
            {
                if (oldv_driver != null && oldv_driver.Length > 0)
                {
                    trip.DriverCode3 = "";
                }
            }
        }
        private void updated_before_clearDriver_cost()
        {
            if (oldv_driver!=null&&oldv_driver != "" && oldv_driver != trip.DriverCode)
            {
                string sql = string.Format(@"delete from job_cost where TripNo=@TripId and DriverCode=@DriverCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", trip.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", oldv_driver, SqlDbType.NVarChar, 100));
                ConnectSql_mb.ExecuteNonQuery(sql, list);
            }
            if (oldv_driver2 != null && oldv_driver2 != "" && oldv_driver2 != trip.DriverCode2)
            {
                string sql = string.Format(@"delete from job_cost where TripNo=@TripId and DriverCode=@DriverCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", trip.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", oldv_driver2, SqlDbType.NVarChar, 100));
                ConnectSql_mb.ExecuteNonQuery(sql, list);
            }
            if (oldv_driver11 != null && oldv_driver11 != "" && oldv_driver11 != trip.DriverCode11)
            {
                string sql = string.Format(@"delete from job_cost where TripNo=@TripId and DriverCode=@DriverCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", trip.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", oldv_driver11, SqlDbType.NVarChar, 100));
                ConnectSql_mb.ExecuteNonQuery(sql, list);
            }
            if (oldv_driver12 != null && oldv_driver12 != "" && oldv_driver12 != trip.DriverCode12)
            {
                string sql = string.Format(@"delete from job_cost where TripNo=@TripId and DriverCode=@DriverCode");
                List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                list.Add(new ConnectSql_mb.cmdParameters("@TripId", trip.Id, SqlDbType.Int));
                list.Add(new ConnectSql_mb.cmdParameters("@DriverCode", oldv_driver12, SqlDbType.NVarChar, 100));
                ConnectSql_mb.ExecuteNonQuery(sql, list);
            }
        }
        private void updated_Status_changed(string user)
        {
            if (oldv_StatusCode != trip.Statuscode)
            {
                updated_Status_changed_to_containerTrips(user);
                if (trip.Statuscode == "C")
                {
                    CtmJobEventLog lg = new CtmJobEventLog();
                    lg.Controller = user;
                    lg.JobNo = trip.JobNo;
                    lg.ContainerNo = trip.ContainerNo;
                    lg.Trip = SafeValue.SafeString(trip.Id);
                    lg.Remark = "TripComplete";
                    lg.log();
                }
                updated_Status_changed_email(user);
            }
        }
        private void updated_Status_changed_email(string user)
        {

            if (trip.Statuscode == "C")
            {
                CtmJob job = Manager.ORManager.GetObject(new Wilson.ORMapper.OPathQuery<CtmJob>("JobNo='" + trip.JobNo + "'"));
                CtmJobDet1 det1 = Manager.ORManager.GetObject<CtmJobDet1>(trip.Det1Id);
                if (job != null)
                {
                    //EmailTemplateBiz emailBz = new EmailTemplateBiz();
                    //EmailTemplate temp = null;
                    //temp = emailBz.getTempalte(job.ClientId, "Trip"+trip.TripCode, job.EmailAddress);
                    //if (temp != null)
                    //{
                    //    string contNo = "";
                    //    string sealNo = "";
                    //    if (det1 != null)
                    //    {
                    //        contNo = det1.ContainerNo;
                    //        sealNo = det1.SealNo;
                    //    }
                    //    if (emailBz.sendEmail(job.ClientRefNo, contNo, sealNo, job.Vessel, job.Voyage, job.EtaDate, job.Pod,job.Pol, temp).status)
                    //    {
                    //        CtmJobEventLog l = new CtmJobEventLog();
                    //        l.Controller = user;
                    //        l.JobNo = trip.JobNo;
                    //        l.Platform = "Web";
                    //        l.Note1Type = "Email";
                    //        l.Remark = "TripComplete email to:" + temp.EmailTo;
                    //        l.log();
                    //    }
                    //}
                }
            }
        }

        private bool updated_Status_changed_to_containerTrips(string user)
        {
            bool status = false;
            if (this.trip.TripCode == "IMP" || this.trip.TripCode == "COL" || ((this.trip.TripCode == "SHF" || this.trip.TripCode == "SMT" || this.trip.TripCode == "SLD") && this.trip.Det1Id > 0))
            {
                if (this.trip.Statuscode == "C")
                {
//                    string sql = string.Format(@"update ctm_jobdet2 set ChessisCode=@ChessisCode,FromCode=@FromCode,FromDate=@FromDate 
//where Det1Id=@Det1Id and Statuscode='P' and isnull(DriverCode,'')='' and isnull(DriverCode3,'')=''");
                    string sql = string.Format(@"update ctm_jobdet2 set ChessisCode=@ChessisCode,FromCode=@FromCode  
where Det1Id=@Det1Id and Statuscode='P'");
                    List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", this.trip.Det1Id, SqlDbType.Int));
                    list.Add(new ConnectSql_mb.cmdParameters("@ChessisCode", this.trip.ChessisCode, SqlDbType.NVarChar, 100));
                    list.Add(new ConnectSql_mb.cmdParameters("@FromCode", this.trip.ToCode, SqlDbType.NVarChar, 300));
                    list.Add(new ConnectSql_mb.cmdParameters("@FromDate", this.trip.ToDate.ToString("yyyyMMdd"), SqlDbType.NVarChar, 10));
                    status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
                }
            }

            return status;
        }

        private bool updating_verify(BizResult bzR, string user, CtmJobDet2 par)
        {
            bool res = false;
            string role = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select role from [user] where Name='{0}'", user)));

            if (role.ToLower() == "client")
            {
                bzR.context = "No permission";
                return res;
            }
            if (role.ToLower().Equals("driver"))
            {
                if (trip.DriverCode.Equals(user) || trip.DriverCode2.Equals(user) || trip.DriverCode11.Equals(user) || trip.DriverCode12.Equals(user))
                {

                }else
                {
                    bzR.context = "No permission";
                    return res;
                }
            }

            CtmJobDet1 det1 = Manager.ORManager.GetObject<CtmJobDet1>(par.Det1Id);
            CtmJob job = Manager.ORManager.GetObject(new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + par.JobNo + "'")) as CtmJob;
            if (job == null)
            {
                bzR.context = "Mast data is empty";
            }
            else
            {
                switch (job.StatusCode)
                {
                    case "CLS":
                        bzR.context = "This Job have been completed";
                        break;
                    case "CNL":
                        bzR.context = "This Job have been Canceled";
                        break;
                    default:
                        break;
                }
                //switch (job.JobStatus)
                //{
                //    case "Booked":
                //        bzR.context = "Requair confirm job";
                //        break;
                //    case "Cancel":
                //        bzR.context = "This job have been cancel";
                //        break;
                //    case "Reject":
                //        bzR.context = "This job have been Reject";
                //        break;
                //    case "Completed":
                //    case "Closed":
                //        bzR.context = "This job have been Completed";
                //        break;
                //    default:
                //        break;
                //}
                if (det1 != null)
                {
                    //if (det1.StatusCode == "Completed")
                    //{
                    //    bzR.context = "This container have been Completed";
                    //}
                }

                updating_verify_diffJobType_tripType(bzR, job, par);


                if (bzR.context.Length == 0)
                {
                    res = true;
                }
            }

            return res;
        }

        private void updating_verify_diffJobType_tripType(BizResult bzR, CtmJob job, CtmJobDet2 par)
        {
            if (job != null && trip != null)
            {
                if (par.TripCode.Equals("IMP") || par.TripCode.Equals("EXP"))
                {
                    if (job != null)
                    {
                        if (job.JobType.Equals("EXP") && par.TripCode.Equals("IMP"))
                        {
                            bzR.context = "Export Job can not save IMP trip";
                        }
                        if (job.JobType.Equals("IMP") && par.TripCode.Equals("EXP"))
                        {
                            bzR.context = "Import Job can not save EXP trip";
                        }
                    }
                }
            }
        }

        private void updated_to_container(string user)
        {

            CtmJobDet1Biz det1Bz = new CtmJobDet1Biz(trip.Det1Id);
            CtmJobDet1 det1 = det1Bz.getData();
            if (det1 != null)
            {
                bool canSave = false;
                canSave = canSave || updated_to_container_contNo(det1);
                canSave = canSave || updated_to_container_status(det1);
                if (canSave)
                {
                    det1Bz.update(user);
                }
            }
        }

        private bool updated_to_container_contNo(CtmJobDet1 det1)
        {
            bool res = false;
            if (det1 != null)
            {
                if (oldv_containerNo != trip.ContainerNo)
                {
                    det1.ContainerNo = trip.ContainerNo;
                    res = true;
                }
                if (trip.TripCode.Equals("COL") || trip.TripCode.Equals("IMP"))
                {
                    det1.ScheduleDate = trip.FromDate;
                    det1.ScheduleTime = trip.FromTime;
                    res = true;
                }
            }
            return res;
        }
        private bool updated_to_container_status(CtmJobDet1 det1)
        {
            bool res = false;
            if (oldv_StatusCode != trip.Statuscode)
            {
                if (trip.Statuscode == "C")
                {
                    if (trip.TripCode.Equals("RET") || trip.TripCode.Equals("EXP"))
                    {
                        det1.StatusCode = "Completed";
                        res = true;
                    }
                }
            }
            return res;
        }

        private bool updated_to_house(string user)
        {
            bool status = false;

            //string sql = string.Format(@"update job_house set ContNo=@ContainerNo where ContId=@Det1Id");
            //List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            //list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", this.trip.Det1Id, SqlDbType.Int));
            //list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", this.trip.ContainerNo, SqlDbType.NVarChar, 100));
            //status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
            return status;
        }
        private bool updated_to_cost(string user)
        {
            bool status = false;
            string sql = string.Format(@"update job_cost set ContNo=@ContainerNo where TripNo=@TripNo");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@TripNo", this.trip.Id, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", this.trip.ContainerNo, SqlDbType.NVarChar, 100));
            status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
            return status;
        }

        public BizResult delete(string user)
        {
            BizResult res = new BizResult();
            if (trip == null || trip.Id == 0)
            {
                res.context = "Data is empty";
            }
            else
            {
                if (updating_verify(res, user, trip))
                {
                    C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                    elog.Platform_isBackend();
                    elog.Controller = user;
                    elog.setActionLevel(trip.Id, CtmJobEventLogRemark.Level.Trip, 2);

                    //===========
                    deleting_reSequence_autoFixIncentive();
                    Manager.ORManager.ExecuteDelete<CtmJobDet2>("Id=" + trip.Id);
                    res.status = true;
                    deleted_removeCost();


                    elog.log();
                }
            }
            return res;
        }
        public BizResult delete_RowDeleting(string user)
        {
            BizResult res = new BizResult();
            if (trip == null || trip.Id == 0)
            {
                res.context = "Data is empty";
            }
            else
            {
                if (updating_verify(res, user, trip))
                {
                    deleting_reSequence_autoFixIncentive();
                    res.status = true;
                    deleted_removeCost();
                }
            }
            return res;
        }

        public bool deleted_removeCost()
        {
            string sql = string.Format(@"delete from job_cost where TripNo=@TripId");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@TripId", trip.Id, SqlDbType.Int));
            return ConnectSql_mb.ExecuteNonQuery(sql, list).status;
        }

        private void deleting_reSequence_autoFixIncentive()
        {
            trip.Statuscode = "X";
            Manager.ORManager.StartTracking(trip, Wilson.ORMapper.InitialState.Updated);
            Manager.ORManager.PersistChanges(trip);
            updated_reSequence_autoFixIncentive();
        }
        private void updated_reSequence_autoFixIncentive()
        {
            //if (trip.Statuscode == "S" || trip.Statuscode == "C" || oldv_StatusCode == "S" || oldv_StatusCode == "C")
            //{
            //    if (SafeValue.SafeString(oldv_driver) != SafeValue.SafeString(trip.DriverCode))
            //    {
            //        updated_reSequence_autoFixIncentive_part1(oldv_driver, trip.FromDate);
            //    }
            //    updated_reSequence_autoFixIncentive_part1(trip.DriverCode, trip.FromDate);
            //}

        }
        private void updated_reSequence_autoFixIncentive_part1(string driver, DateTime tripDateTime)
        {
            string sql = string.Format(@"with tb0 as (
select ROW_NUMBER()over(order by tb1.FromTime) as seq,case when tb1.TripCode='LOC' then 3 else 1 end as v,tb1.Id,tb1.DriverCode,tb1.TripCode,tb1.BillLock,tb1.FromTime,cont.BillType 
from CTM_JobDet2 as tb1 
left outer join ctm_jobdet1 as cont on tb1.Det1Id=cont.Id
where (tb1.Statuscode='S' or tb1.Statuscode='C') and TripCode<>'SHF' and tb1.DriverCode=@driver and datediff(day,tb1.FromDate,@tripDateTime)=0
)
select *,isnull((select sum(v) from tb0 where seq<t.seq),0)+1 as seqId,(select sum(v) from tb0) as total from tb0 as t");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@driver", driver, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@tripDateTime", tripDateTime.ToString("yyyyMMdd"), SqlDbType.NVarChar, 10));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string BillLock = SafeValue.SafeString(dr["BillLock"]);
                if (BillLock == "YES")
                {
                    continue;
                }
                int seqId = SafeValue.SafeInt(dr["seqId"], 0);
                int tripId = SafeValue.SafeInt(dr["Id"], 0);
                int total = SafeValue.SafeInt(dr["total"], 0);
                string TripCode = SafeValue.SafeString(dr["TripCode"]);
                string BillType = SafeValue.SafeString(dr["BillType"]);
                string FromTime = SafeValue.SafeString(dr["FromTime"]);

                //============= auto set trip incentive
                //decimal inc_trip = CtmDriverIncentiveBiz.getIncentiveTrip(seqId, total, TripCode, BillType, driver, FromTime);
                //C2.CtmJobDet2.Incentive_Trip_Save(tripId, inc_trip);
            }
        }


    }
}