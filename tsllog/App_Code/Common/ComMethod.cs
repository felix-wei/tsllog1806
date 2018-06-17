using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace C2
{
    /// <summary>
    /// ComMethod 的摘要说明
    /// </summary>
    public static class ComMethod
    {
        public static void CreateInv(string invN, int id, string docId, int n,string billType,string currency,decimal exrate)
        {
            string sql = string.Format(@"select * from job_cost where Id={0}", id);
            DataTable dt = ConnectSql.GetTab(sql);
            string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType,JobCostId,LineIndex,ChgDes4,GroupBy,ChgDes2,ChgDes3)
values");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sql = "";
                decimal qty = SafeValue.SafeDecimal(dt.Rows[i]["Qty"]);
                string chgCodeId = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
                string chgCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
                decimal price = SafeValue.SafeDecimal(dt.Rows[i]["Price"]);
                string cntNo = SafeValue.SafeString(dt.Rows[i]["ContNo"]);
                string cntType = SafeValue.SafeString(dt.Rows[i]["ContType"]);
                int lineIndex = SafeValue.SafeInt(dt.Rows[i]["LineIndex"], 0);
                string jobNo = SafeValue.SafeString(dt.Rows[i]["JobNo"]);
                string gstType = SafeValue.SafeString(dt.Rows[i]["GstType"]);
                string unit = SafeValue.SafeString(dt.Rows[i]["Unit"]);
                string remark = SafeValue.SafeString(dt.Rows[i]["Remark"]);
                string groupBy = SafeValue.SafeString(dt.Rows[i]["GroupBy"]);
                if (cntType.Length == 0)
                {
                    cntType = unit;
                }
                string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId,ArCode from XXChgCode where chgCodeId='{0}'", chgCodeId);
                DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
                decimal gst = 0;
                string chgTypeId = "";
                string arCode = "";
                if (dt_chgCode.Rows.Count > 0)
                {
                    gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                    gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                    chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
                    arCode = SafeValue.SafeString(dt_chgCode.Rows[0]["ArCode"]);
                }
                decimal amt = SafeValue.ChinaRound(qty * SafeValue.SafeDecimal(price, 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
                decimal docAmt = amt+gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * exrate, 2);
                if (chgCodeDes.ToUpper().Equals("TRUCKING"))
                {
                    chgCodeDes += " " + billType.ToUpper();
                }
                string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','{18}','CR','{3}','{4}','{14}',{16},{5},'{9}','{21}',{22},{10},{11},{12},{13},0,'{6}','{7}','{8}',{15},{17},'{19}','{20}','','') select @@IDENTITY ", docId, invN, n + 1, chgCodeId, chgCodeDes, price, jobNo, cntNo, chgTypeId, cntType, gst, gstAmt, docAmt, locAmt, gstType, id, qty, lineIndex, arCode, remark, groupBy,currency,exrate);
                sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
            }
            if (sql.Length > 0)
            {
                sql = sql_part1 + sql;
				//throw new Exception(sql);
                int re = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);

                //UpdateMaster(SafeValue.SafeInt(docId, 0));
                UpdateLineId(id, re);
            }
        }
        public static void CreateWhInv(string invN, int id, string docId)
        {
            string sql = string.Format(@"select c.*,det3.ContainerNo,det3.ContainerType from Wh_Costing c left join wh_dodet3 det3 on c.RefNo=det3.DoNo where c.Id={0}", id);
            DataTable dt = ConnectSql.GetRemoteTab(sql);
            string sql_part1 = string.Format(@"insert into XAArInvoiceDet (DocId,DocNo,DocType,DocLineNo,AcCode,AcSource,ChgCode,ChgDes1, ChgDes2, ChgDes3,
GstType,Qty,Price,Unit,Currency,ExRate,Gst,GstAmt,DocAmt,LocAmt,LineLocAmt,MastRefNo,JobRefNo,MastType)
values");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sql = "";
                string chgCodeId = SafeValue.SafeString(dt.Rows[i]["ChgCode"]);
                string cheCodeDes = SafeValue.SafeString(dt.Rows[i]["ChgCodeDes"]);
                decimal price = SafeValue.SafeDecimal(dt.Rows[i]["CostLocAmt"]);
                string cntNo = SafeValue.SafeString(dt.Rows[i]["ContainerNo"]);
                string cntType = SafeValue.SafeString(dt.Rows[i]["ContainerType"]);
                string jobNo = SafeValue.SafeString(dt.Rows[i]["RefNo"]);
                string gstType = SafeValue.SafeString(dt.Rows[i]["CostGstType"]);
                string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeDes='{0}'", chgCodeId);
                DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
                decimal gst = 0;
                string chgTypeId = "";
                if (dt_chgCode.Rows.Count > 0)
                {
                    gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
                    gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
                    chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
                }
                decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
                decimal docAmt = amt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
                string sql_part2 = string.Format(@"('{0}','{1}','IV','{2}','','CR','{3}','{4}','','','{14}',1,{5},'{9}','SGD',1,{10},{11},{12},{13},0,'{6}','{7}','{8}')", docId, invN, i + 1, chgCodeId, cheCodeDes, price, jobNo, cntNo, chgTypeId, cntType, gst, gstAmt, docAmt, locAmt, gstType);
                sql += sql.Length > 0 ? "," + sql_part2 : sql_part2;
            }
            if (sql.Length > 0)
            {
                sql = sql_part1 + sql;
                int re = ConnectSql.ExecuteSql(sql);
                C2.XAArInvoice.update_invoice_mast(SafeValue.SafeInt(docId, 0));

                //UpdateMaster(SafeValue.SafeInt(docId, 0));

            }
        }
        private static void UpdateLineId(int id, int lineId)
        {
            string sql = string.Format(@"update job_cost set LineId={1} where Id={0}", id, lineId);
            ConnectSql.ExecuteSql(sql);
        }
        public static bool IsCostCreated(object id)
        {
            bool res = false;
            string sql = string.Format(@"select count(*) from XAArInvoiceDet where JobCostId={0}", id);
            int n = SafeValue.SafeInt(ConnectSql_mb.ExecuteScalar(sql), 0);
            if (n == 0)
            {
                res = true;
            }
            return res;
        }

    }
}