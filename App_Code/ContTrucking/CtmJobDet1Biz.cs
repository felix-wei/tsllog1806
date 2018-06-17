using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace C2
{
    /// <summary>
    /// Summary description for CtmJobDet1Biz
    /// </summary>
    public class CtmJobDet1Biz
    {
        public CtmJobDet1Biz(int id)
        {
            if (id > 0)
            {
                CtmJobDet1 par = Manager.ORManager.GetObject<CtmJobDet1>(id);
                setData(par);
            }
        }
        CtmJobDet1 det1;
        public void setData(CtmJobDet1 par)
        {
            det1 = par;
            resetOldValues();
        }
        public CtmJobDet1 getData()
        {
            return det1;
        }
        private static string default_ServiceType = "Two-Way";
        string oldv_containerNo;
        string oldv_statusCode;
        string oldv_ServiceType;
        string oldv_Remark;

        private void resetOldValues()
        {
            if (det1 != null)
            {
                oldv_containerNo = det1.ContainerNo;
                oldv_statusCode = det1.StatusCode;
                oldv_ServiceType = det1.ServiceType;
                oldv_Remark = det1.Remark;
            }
            else
            {
                oldv_containerNo = "";
                oldv_statusCode = "";
                oldv_ServiceType = "";
                oldv_Remark = "";
            }
        }


        public BizResult insert(string user, CtmJobDet1 par)
        {
            setData(null);
            if (par == null)
            {
                throw new Exception("data is null");
            }
            BizResult res = new BizResult();
            if (inserting_verify(res, user, par))
            {
                par.ScheduleDate = SafeValue_mb.DateTime_ClearTime(par.ScheduleDate);
                par.ScheduleTime = SafeValue_mb.convertTimeFormat(par.ScheduleTime);
                par.ScheduleStartDate = SafeValue_mb.DateTime_ClearTime(par.ScheduleStartDate);
                par.ServiceType = (SafeValue.SafeString(par.ServiceType).Equals("") ? default_ServiceType : par.ServiceType);
                Manager.ORManager.StartTracking(par, Wilson.ORMapper.InitialState.Inserted);
                Manager.ORManager.PersistChanges(par);
                res.status = true;
                det1 = par;
                updated_Status_changed(user);

                resetOldValues();
                oldv_ServiceType = "";//==== update trips by service type.
                inserted_auto_CreateTrip(user);
                updated_to_trip();
                resetOldValues();


                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isBackend();
                elog.Controller = user;
                elog.setActionLevel(det1.Id, CtmJobEventLogRemark.Level.Container, 1);
                elog.log();
            }
            return res;
        }

        private bool inserting_verify(BizResult bzR, string user, CtmJobDet1 par)
        {
            bool res = false;
            string role = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select role from [user] where Name='{0}'", user)));
            CtmJob job = Manager.ORManager.GetObject(new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + par.JobNo + "'")) as CtmJob;
            if (job == null)
            {
                bzR.context = "Can not find this job";
            }
            else
            {
                if (role.ToLower() == "client")
                {
                    switch (job.JobStatus)
                    {
                        case "Booked":
                            res = true;
                            break;
                        case "Cancel":
                            bzR.context = "This job have been cancel";
                            break;
                        case "Reject":
                        case "Confirmed":
                        case "Completed":
                        case "Closed":
                        default:
                            bzR.context = "No permission";
                            break;
                    }

                }
                else
                {
                    switch (job.JobStatus)
                    {
                        case "Booked":
                        case "Cancel":
                            bzR.context = "Requair confirm this job";
                            break;
                        case "Completed":
                        case "Closed":
                            bzR.context = "This Job have completed";
                            break;
                        default:
                            res = true;
                            break;
                    }
                }
            }
            return res;
        }

        private void inserted_auto_CreateTrip(string user)
        {
            CtmJob job = Manager.ORManager.GetObject(new Wilson.ORMapper.OPathQuery<CtmJob>("JobNo='" + det1.JobNo + "'"));
            if (job != null)
            {
                CtmJobDet2Biz det2Bz = new CtmJobDet2Biz(0);
                CtmJobDet2 det2 = new CtmJobDet2();
                det2.JobNo = det1.JobNo;
                det2.Det1Id = det1.Id;
                det2.ContainerNo = det1.ContainerNo;
                det2.FromDate = det1.ScheduleDate;
                det2.FromTime = det1.ScheduleTime;
                det2.ToDate = det1.ScheduleDate;
                det2.ToTime = det1.ScheduleTime;
                det2.Statuscode = "P";
                det2.SubletFlag = "N";
                det2.Overtime = "Normal";
                det2.OverDistance = "N";
                if (job.JobType == "IMP")
                {
                    det2.TripCode = "IMP";
                    det2.FromCode = job.PickupFrom;
                    det2.ToCode = job.DeliveryTo;
                    det2Bz.insert(user, det2);

                    det2.TripCode = "RET";
                    det2.FromCode = job.DeliveryTo;
                    det2.ToCode = det1.YardAddress;
                    det2Bz.insert(user, det2);
                }
                if (job.JobType == "EXP")
                {
                    det2.TripCode = "COL";
                    det2.FromCode = det1.YardAddress;
                    det2.ToCode = job.PickupFrom;
                    det2Bz.insert(user, det2);

                    det2.TripCode = "EXP";
                    det2.FromCode = job.PickupFrom;
                    det2.ToCode = job.DeliveryTo;
                    det2Bz.insert(user, det2);
                }
                if (job.JobType == "LOC")
                {
                    det2.TripCode = "LOC";
                    det2.FromCode = job.PickupFrom;
                    det2.ToCode = job.DeliveryTo;
                    det2Bz.insert(user, det2);
                }
            }
        }

        public BizResult update(string user)
        {
            BizResult res = new BizResult();
            if (det1 == null)
            {
                throw new Exception("data is null");
            }

            if (updating_verify(res, user, det1))
            {
                det1.ScheduleDate = SafeValue_mb.DateTime_ClearTime(det1.ScheduleDate);
                det1.ScheduleTime = SafeValue_mb.convertTimeFormat(det1.ScheduleTime);
                det1.ServiceType = (SafeValue.SafeString(det1.ServiceType).Equals("") ? default_ServiceType : det1.ServiceType);
                Manager.ORManager.StartTracking(det1, Wilson.ORMapper.InitialState.Updated);
                Manager.ORManager.PersistChanges(det1);
                res.status = true;

                //========== log container no changed
                C2.CtmJobEventLog elog = new C2.CtmJobEventLog();
                elog.Platform_isBackend();
                elog.Controller = user;
                if (oldv_containerNo != null && det1.ContainerNo != null && !oldv_containerNo.Equals(det1.ContainerNo))
                {
                    elog.setActionLevel(det1.Id, CtmJobEventLogRemark.Level.Container, -1, "ContainerNo[" + oldv_containerNo + "]->[" + det1.ContainerNo + "]");
                    elog.log();
                }

                updated_Status_changed(user);
                updated_to_trip();
                updated_to_house(user);

                resetOldValues();


                elog.setActionLevel(det1.Id, CtmJobEventLogRemark.Level.Container, 3);
                elog.log();
            }
            return res;
        }

        private void updated_Status_changed(string user)
        {
            if (oldv_statusCode != det1.StatusCode)
            {
                if (det1.StatusCode == "Completed")
                {
                    CtmJobEventLog lg = new CtmJobEventLog();
                    lg.Controller = user;
                    lg.JobNo = det1.JobNo;
                    lg.ContainerNo = det1.ContainerNo;
                    lg.Remark = "ContComplete";
                    lg.log();
                }
                updated_Status_changed_email(user);
            }
        }

        private void updated_Status_changed_email(string user)
        {

            if (det1.StatusCode == "Completed")
            {
                //CtmJob job = Manager.ORManager.GetObject(new Wilson.ORMapper.OPathQuery<CtmJob>("JobNo='" + det1.JobNo + "'"));
                //EmailTemplateBiz emailBz = new EmailTemplateBiz();
                //EmailTemplate temp = emailBz.getTempalte(job.ClientId, "ContComplete", job.EmailAddress);
                //if (temp != null)
                //{
                //    if (emailBz.sendEmail(job.ClientRefNo, det1.ContainerNo, det1.SealNo, job.Vessel, job.Voyage, job.EtaDate, job.Pod,job.Pol, temp).status)
                //    {
                //        CtmJobEventLog l = new CtmJobEventLog();
                //        l.Controller = user;
                //        l.JobNo = det1.JobNo;
                //        l.Platform = "Web";
                //        l.Note1Type = "Email";
                //        l.Remark = "ContComplete email to:" + temp.EmailTo;
                //        l.log();
                //    }
                //}
            }
        }

        private void updated_to_trip()
        {
            IList<CtmJobDet2> list = Manager.ORManager.GetCollection<CtmJobDet2>("Det1Id=" + det1.Id);
            if (list != null)
            {
                foreach (CtmJobDet2 temp in list)
                {
                    temp.ContainerNo = det1.ContainerNo;
                    //=============== one-way
                    updated_to_trip_byServiceType(temp);

                    if (temp.Statuscode.Equals("P"))
                    {
                        if ((temp.DriverCode == null || temp.DriverCode.Equals("")) && (temp.DriverCode2 == null || temp.DriverCode2.Equals("")) && (temp.DriverCode3 == null || temp.DriverCode3.Equals("")) && (temp.DriverCode11 == null || temp.DriverCode11.Equals("")) && (temp.DriverCode12 == null || temp.DriverCode12.Equals("")))
                        {
                            if (temp.TripCode.Equals("IMP") || temp.TripCode.Equals("COL"))
                            {
                                temp.FromDate = det1.ScheduleDate;
                                temp.FromTime = det1.ScheduleTime;
                                
                                //======== container level remark （det1.Remark） copy to trip level Trip Instruction.
                                if (temp.Remark == null || temp.Remark.Length == 0 || temp.Remark.Equals(oldv_Remark))
                                {
                                    temp.Remark = det1.Remark;
                                }
                            }
                            if (temp.TripCode.Equals("RET"))
                            {
                                temp.ToCode = det1.YardAddress;
                            }
                            if (temp.TripCode.Equals("COL"))
                            {
                                temp.FromCode = det1.YardAddress;
                            }

                            ////======== container level remark （det1.Remark） copy to trip level Trip Instruction.
                            //if (temp.TripCode.Equals("COL") || temp.TripCode.Equals("IMP"))
                            //{
                            //    if (temp.Remark == null || temp.Remark.Length == 0 || temp.Remark.Equals(oldv_Remark))
                            //    {
                            //        temp.Remark = det1.Remark;
                            //    }
                            //}
                        }
                    }
                    Manager.ORManager.StartTracking(temp, Wilson.ORMapper.InitialState.Updated);
                    Manager.ORManager.PersistChanges(temp);
                }
            }
            //if (oldv_containerNo != det1.ContainerNo)
            //{
            //    IList<CtmJobDet2> list = Manager.ORManager.GetCollection<CtmJobDet2>("Det1Id=" + det1.Id);
            //    if (list != null)
            //    {
            //        foreach (CtmJobDet2 temp in list)
            //        {
            //            temp.ContainerNo = det1.ContainerNo;
            //            Manager.ORManager.StartTracking(temp, Wilson.ORMapper.InitialState.Updated);
            //            Manager.ORManager.PersistChanges(temp);
            //        }
            //    }
            //}
        }

        private void updated_to_trip_byServiceType(CtmJobDet2 trip)
        {
            if (!det1.ServiceType.Equals(oldv_ServiceType))
            {
                if (det1.ServiceType.Equals("") || det1.ServiceType.Equals(default_ServiceType))
                {
                    if (trip.Statuscode.Equals("X"))
                    {
                        trip.Statuscode = "P";
                    }
                }
                else
                {
                    if (trip.Statuscode == "P" || trip.Statuscode == "X")
                    {
                        string temp = "|IMP|RET|COL|EXP|";
                        //temp=temp.Replace(det1.ServiceType, "");
                        if (temp.IndexOf("|" + trip.TripCode + "|") >= 0)
                        {
                            if (det1.ServiceType.Equals(trip.TripCode))
                            {
                                trip.Statuscode = "P";
                            }
                            else
                            {
                                trip.Statuscode = "X";
                            }
                        }
                    }
                }
            }

        }
        private bool updated_to_house(string user)
        {
            bool status = false;

            string sql = string.Format(@"update job_house set ContNo=@ContainerNo where ContId=@Det1Id");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@Det1Id", this.det1.Id, SqlDbType.Int));
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", this.det1.ContainerNo, SqlDbType.NVarChar, 100));
            status = ConnectSql_mb.ExecuteNonQuery(sql, list).status;
            return status;
        }
        private bool updating_verify(BizResult bzR, string user, CtmJobDet1 par)
        {
            if (user.Equals("skip")) { return true; }
            bool res = false;
            string role = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select role from [user] where Name='{0}'", user)));
            CtmJob job = Manager.ORManager.GetObject(new Wilson.ORMapper.OPathQuery(typeof(C2.CtmJob), "JobNo='" + par.JobNo + "'")) as CtmJob;
            if (job == null)
            {
                bzR.context = "Can not find this job";
            }
            else
            {
                if (role.ToLower() == "client")
                {
                    switch (job.JobStatus)
                    {
                        case "Booked":
                            res = true;
                            break;
                        case "Cancel":
                            bzR.context = "This job have been cancel";
                            break;
                        case "Reject":
                        case "Confirmed":
                        case "Completed":
                        case "Closed":
                        default:
                            bzR.context = "No permission";
                            break;
                    }

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
                            res = true;
                            break;
                    }
                    //switch (job.JobStatus)
                    //{
                    //    case "Booked":
                    //    case "Cancel":
                    //        bzR.context = "Requair confirm this job";
                    //        break;
                    //    case "Completed":
                    //    case "Closed":
                    //        bzR.context = "This Job have completed";
                    //        break;
                    //    default:
                    //        res = true;
                    //        break;
                    //}
                }
            }
            return res;
        }


        public BizResult delete(string user)
        {
            BizResult res = new BizResult();
            if (det1 == null || det1.Id == 0)
            {
                res.context = "Data is empty";
            }
            else
            {
                if (updating_verify(res, user, det1))
                {
                    string sql = string.Format(@"delete from ctm_jobdet1 where Id=@Id");
                    List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                    list.Add(new ConnectSql_mb.cmdParameters("@Id", det1.Id, SqlDbType.Int));
                    ConnectSql_mb.sqlResult sqlRes = ConnectSql_mb.ExecuteNonQuery(sql, list);
                    if (sqlRes.status)
                    {
                        res.status = true;
                        CtmJobEventLog lg = new CtmJobEventLog();
                        lg.Platform_isBackend();
                        lg.Controller = user;
                        //lg.JobNo = det1.JobNo;
                        //lg.ContainerNo = det1.ContainerNo;
                        //lg.Remark = "ContDelete";
                        lg.setActionLevel(det1.Id, CtmJobEventLogRemark.Level.Container, 2);
                        lg.log();
                        System.Collections.ObjectModel.Collection<C2.CtmJobDet2> det2 = C2.Manager.ORManager.GetCollection<C2.CtmJobDet2>("det1Id=" + det1.Id);
                        C2.CtmJobDet2Biz det2Bz = new CtmJobDet2Biz(0);
                        foreach (C2.CtmJobDet2 temp in det2)
                        {
                            det2Bz.setData(temp);
                            det2Bz.delete(user);
                        }
                        //sql = string.Format(@"delete from ctm_jobdet2 where det1Id=@Id");
                        //ConnectSql_mb.ExecuteNonQuery(sql, list);
                    }
                }
            }
            return res;
        }
        public BizResult delete_RowDeleting(string user)
        {
            BizResult res = new BizResult();
            if (det1 == null || det1.Id == 0)
            {
                res.context = "Data is empty";
            }
            else
            {
                if (updating_verify(res, user, det1))
                {
                    res.status = true;


                    System.Collections.ObjectModel.Collection<C2.CtmJobDet2> det2 = C2.Manager.ORManager.GetCollection<C2.CtmJobDet2>("det1Id=" + det1.Id);
                    C2.CtmJobDet2Biz det2Bz = new CtmJobDet2Biz(0);
                    foreach (C2.CtmJobDet2 temp in det2)
                    {
                        det2Bz.setData(temp);
                        det2Bz.delete(user);
                    }
                    //string sql = string.Format(@"delete from ctm_jobdet2 where det1Id=@Id");
                    //List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
                    //list.Add(new ConnectSql_mb.cmdParameters("@Id", det1.Id, SqlDbType.Int));
                    //ConnectSql_mb.sqlResult sqlRes = ConnectSql_mb.ExecuteNonQuery(sql, list);
                }
            }
            return res;
        }

    }
}