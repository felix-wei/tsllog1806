using Aspose.Cells;
using DevExpress.Web.ASPxUploadControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PSA_Import_PSA_billing : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			search_from.Date = DateTime.Today.AddDays(-1);
			search_to.Date = DateTime.Today;
		}
	}

	public void ImportJob(string batch, string file, out string error_text)
	{
		Aspose.Cells.License license = new Aspose.Cells.License();
		license.SetLicense(MapPath("~/Aspose.lic"));
		Workbook wb = new Workbook();
		if (file.ToLower().EndsWith(".csv"))
		{
			wb.Open(file, FileFormatType.CSV);
		}
		else
		{
			wb.Open(file);
		}

		int empty_i = 0;
		string re_text = "";
		Worksheet ws = wb.Worksheets[0];

		int existDo = 0;
		int successJob = 0;
		int successItem = 0;
		int errorDo = 0;

		//=================================== version 1
        bool beginImport = false;
		//int existDo = 0;
		//int successJob = 0;
		for (int i = 1; true; i++)
		{
			if (empty_i >= 10) { break; }
			string A = ws.Cells["A" + i].StringValue;
			string B = ws.Cells["B" + i].StringValue;
			if (B.Length <= 0)
			{
				empty_i++;
				continue;
			}

			empty_i = 0;

			if (beginImport)
			{
				string C = ws.Cells["C" + i].StringValue;
				string D = ws.Cells["D" + i].StringValue;
				string E = ws.Cells["E" + i].StringValue;
				string F = ws.Cells["F" + i].StringValue;
				string G = ws.Cells["G" + i].StringValue;
				string H = ws.Cells["H" + i].StringValue;
				string I = ws.Cells["I" + i].StringValue;
				I = I.Replace(" ", "");
				string J = ws.Cells["J" + i].StringValue;
				string K = ws.Cells["K" + i].StringValue;
				string L = ws.Cells["L" + i].StringValue;
				string M = ws.Cells["M" + i].StringValue;
				string N = ws.Cells["N" + i].StringValue;
				string O = ws.Cells["O" + i].StringValue;
				string P = ws.Cells["P" + i].StringValue;
				string Q = ws.Cells["Q" + i].StringValue;
				string R = ws.Cells["R" + i].StringValue;
				string S = ws.Cells["S" + i].StringValue;
				string T = ws.Cells["T" + i].StringValue;
				string U = ws.Cells["U" + i].StringValue;
				string V = ws.Cells["V" + i].StringValue;
				string W = ws.Cells["W" + i].StringValue;
				string X = ws.Cells["X" + i].StringValue;
				string Y = ws.Cells["Y" + i].StringValue;
				string Z = ws.Cells["Z" + i].StringValue;

				string AA = ws.Cells["AA" + i].StringValue;
				string AB = ws.Cells["AB" + i].StringValue;
				string AC = ws.Cells["AC" + i].StringValue;
				string AD = ws.Cells["AD" + i].StringValue;
				string AE = ws.Cells["AE" + i].StringValue;
				string AF = ws.Cells["AF" + i].StringValue;
				string AG = ws.Cells["AG" + i].StringValue;
				string AH = ws.Cells["AH" + i].StringValue;
				string AI = ws.Cells["AI" + i].StringValue;
				string AJ = ws.Cells["AJ" + i].StringValue;
				string AK = ws.Cells["AK" + i].StringValue;
				string AL = ws.Cells["AL" + i].StringValue;
				string AM = ws.Cells["AM" + i].StringValue;
				string AN = ws.Cells["AN" + i].StringValue;
				string AO = ws.Cells["AO" + i].StringValue;
				string AP = ws.Cells["AP" + i].StringValue;
				string AQ = ws.Cells["AQ" + i].StringValue;
				string AR = ws.Cells["AR" + i].StringValue;
				string AS = ws.Cells["AS" + i].StringValue;
				string AT = ws.Cells["AT" + i].StringValue;
				string AU = ws.Cells["AU" + i].StringValue;
				string AV = ws.Cells["AV" + i].StringValue;
				string AW = ws.Cells["AW" + i].StringValue;
				string AX = ws.Cells["AX" + i].StringValue;
				string AY = ws.Cells["AY" + i].StringValue;
				string AZ = ws.Cells["AZ" + i].StringValue;

				string BA = ws.Cells["BA" + i].StringValue;
				string BB = ws.Cells["BB" + i].StringValue;
				string BC = ws.Cells["BC" + i].StringValue;
				string BD = ws.Cells["BD" + i].StringValue;
				string BE = ws.Cells["BE" + i].StringValue;
				string BF = ws.Cells["BF" + i].StringValue;
				string BG = ws.Cells["BG" + i].StringValue;
				string BH = ws.Cells["BH" + i].StringValue;
				string BI = ws.Cells["BI" + i].StringValue;
				string BJ = ws.Cells["BJ" + i].StringValue;
				string BK = ws.Cells["BK" + i].StringValue;
				string BL = ws.Cells["BL" + i].StringValue;
				string BM = ws.Cells["BM" + i].StringValue;
				string BN = ws.Cells["BN" + i].StringValue;
				string BO = ws.Cells["BO" + i].StringValue;
				string BP = ws.Cells["BP" + i].StringValue;
				string BQ = ws.Cells["BQ" + i].StringValue;
				string BR = ws.Cells["BR" + i].StringValue;
				string BS = ws.Cells["BS" + i].StringValue;
				string BT = ws.Cells["BT" + i].StringValue;
				string BU = ws.Cells["BU" + i].StringValue;
				string BV = ws.Cells["BV" + i].StringValue;
				string BW = ws.Cells["BW" + i].StringValue;
				string BX = ws.Cells["BX" + i].StringValue;
				string BY = ws.Cells["BY" + i].StringValue;

				//=====================检测是否存在Bill Number
                string billNo = D;
				string billItem = E;
				string sql_check = string.Format(@"select [F1] from psa_bill where [BILL NUMBER]='{0}' and [BILL ITEM NUMBER]='{1}'", billNo, billItem);
				DataTable dt_check = ConnectSql.GetTab(sql_check);
				if (dt_check.Rows.Count > 0)
				{
					existDo++;
					continue;
				}

				try
				{
					List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
					string A_t = SafeValue.SafeDate(A, new DateTime(1900, 1, 1)).ToString("yyyyMMdd");
					if (A.Length > 10)
					{
						string[] ar_t = A.Split(' ');
						if (ar_t.Length == 2)
						{
							DateTime dt_temp = SafeValue.SafeDate(ar_t[0], new DateTime(1900, 1, 1));
							for (int A_i = 1; i < ar_t.Length; A_i++)
							{
								if (ar_t[A_i].Length > 0)
								{
									A_t = dt_temp.ToString("yyyyMMdd") + " " + ar_t[A_i];
									break;
								}
							}
						}
					}

					//ConnectSql_mb.cmdParameters cpar = new ConnectSql_mb.cmdParameters("@A", SafeValue.SafeDate(A, new DateTime(1900, 1, 1)), SqlDbType.DateTime);
					//ConnectSql_mb.cmdParameters cpar = new ConnectSql_mb.cmdParameters("@A", A_t, SqlDbType.NVarChar, 100);
					ConnectSql_mb.cmdParameters cpar = new ConnectSql_mb.cmdParameters("@A", reGetDateFormat(A, file), SqlDbType.NVarChar, 100);
					list.Add(cpar);
					#region list add
                    cpar = new ConnectSql_mb.cmdParameters("@B", B, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@C", C, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@D", D, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@E", SafeValue.SafeDecimal(E, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@F", SafeValue.SafeDecimal(F, 0), SqlDbType.Float);
					list.Add(cpar);
					//cpar = new ConnectSql_mb.cmdParameters("@G", SafeValue.SafeDate(G, new DateTime(1900, 1, 1)), SqlDbType.DateTime);
					cpar = new ConnectSql_mb.cmdParameters("@G", reGetDateFormat(G, file), SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@H", H, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@I", I, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@J", SafeValue.SafeDecimal(J, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@K", K, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@L", SafeValue.SafeDecimal(L, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@M", M, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@N", SafeValue.SafeDecimal(N, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@O", SafeValue.SafeDecimal(O, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@P", P, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@Q", Q, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@R", R, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@S", S, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@T", T, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@U", U, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@V", V, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@W", W, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@X", SafeValue.SafeDecimal(X, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@Y", Y, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@Z", Z, SqlDbType.NVarChar, 100);
					list.Add(cpar);

					//cpar = new ConnectSql_mb.cmdParameters("@AA", SafeValue.SafeDate(AA, new DateTime(1900, 1, 1)), SqlDbType.DateTime);
					string _AA = reGetDateFormat(AA, file);
					cpar = new ConnectSql_mb.cmdParameters("@AA", _AA, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					//cpar = new ConnectSql_mb.cmdParameters("@AB", SafeValue.SafeDate(AB, new DateTime(1900, 1, 1)), SqlDbType.DateTime);
					string _AB = reGetDateFormat(AB, file);
					DateTime _ETA = new DateTime(S.Int(BP.Substring(10, 4)), S.Int(BP.Substring(7, 2)).S.Int(BP.Substring(4, 2)));
					string ETA1 = _ETA.AddDays(-3).ToString("yyyy-MM-dd");
					string ETA2 = _ETA.AddDays(3).ToString("yyyy-MM-dd");
					string JOBTYPE = "IMP";
					if(AT == "O")
						JOBTYPE = "EXP";
					string JOB = D.Text("select top 1 c.jobno from ctm_job j, ctm_jobdet1 c where j.jobno=c.jobno and j.etadate>'"+ETA1+"' and j.etadate<'"+ETA2+"' and c.containerno='"+I+"' AND (j.JobType = '"+JOBTYPE+"') ");
					cpar = new ConnectSql_mb.cmdParameters("@AB", reGetDateFormat(_AB, file), SqlDbType.NVarChar, 100);
					list.Add(cpar);
					//cpar = new ConnectSql_mb.cmdParameters("@AC", SafeValue.SafeDate(AC, new DateTime(1900, 1, 1)), SqlDbType.DateTime);
					cpar = new ConnectSql_mb.cmdParameters("@AC", reGetDateFormat(AC, file), SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AD", AD, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AE", AE, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AF", AF, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AG", AG, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AH", AH, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AI", AI, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AJ", AJ, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AK", AK, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AL", AL, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AM", AM, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AN", reGetDateFormat(AN, file), SqlDbType.DateTime);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AO", reGetDateFormat(AO, file), SqlDbType.DateTime);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AP", AP, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AQ", AQ, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AR", AR, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AS", AS, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AT", AT, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AU", AU, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AV", AV, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AW", AW, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AX", SafeValue.SafeDecimal(AX, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AY", AY, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AZ", AZ, SqlDbType.NVarChar, 100);
					list.Add(cpar);

					cpar = new ConnectSql_mb.cmdParameters("@BA", BA, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BB", BB, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BC", BC, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BD", BD, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BE", BE, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BF", BF, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BG", SafeValue.SafeDecimal(BG, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BH", BH, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BI", SafeValue.SafeDecimal(BI, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BJ", BJ, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BK", BK, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BL", BL, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BM", BM, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BN", BN, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BO", BO, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BP", BP, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BQ", BQ, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BR", BR, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BS", SafeValue.SafeDecimal(BS, 0), SqlDbType.Float);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BT", BT, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BU", BU, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BV", BV, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BW", BW, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BX", BX, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@BY", BY, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@JOB", JOB, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					#endregion

                    string sql = string.Format(@"insert into psa_bill (JOB_NO,F1,[BILL TYPE],[BILLING COMPANY],[BILL NUMBER],[BILL ITEM NUMBER],[ACCOUNT NUMBER],[BILL DATE],[REF NUMBER],[CONTAINER NUMBER], 
[TARIFF CODE],[TARIFF DESCRIPTION],RATE,[UNIT DESCRIPTION],[BILLABLE UNIT],AMOUNT,[FULL VESSEL NAME],[FULL OUT VOY NUMBER],[FULL IN VOY NUMBER],
[ABBR VESSEL NAME],[ABBR OUT VOY NUMBER],[ABBR IN VOY NUMBER],[LINE CODE],[GROSS TONNAGE],LOA,[SERVICE ROUTE],[IN SERVICE ROUTE],[LAST BTR DATE],
[ATB DATE],[ATU DATE],[FIRST ACTIVITY DATE],[LAST ACTIVITY DATE],[CONNECTING FULL VSL NAME],[CONNECTING FULL OUT VOY NUMBER],[CONNECTING ABBR VSL NAME],
[CONNECTING ABBR VOY NUMBER],[CONNECTING SERVICE ROUTE],[CONNECTING IN SERVICE ROUTE],[CONNECTING VESSEL COD DATE],[CONNECTING VESSEL ATB DATE],
[SERVICE START DATE],[SERVICE END DATE],[LOCATION FROM],[LOCATION TO],[BERTH NUMBER],[SLOT OPERATOR],[LOAD/DISC INDICATOR],[FROM],[TO],[CNTR TYPE],[CNTR SIZE],
[ISO SIZE TYPE],[DG IMO CLASS],[TRANSHIP INDICATOR],[DEPOT INDICATOR],[REASON CODE],[LADEN STATUS],[CNTR OPERATOR],[GST INDICATOR],[GST PERCENTAGE],
[CURRENCY CODE],[EXCHANGE RATE],[ORG CODE],[CHARGE CATEGORY],[CHARGE TYPE],[CHARGE CLASSIFICATION 1],[CHARGE CLASSIFICATION 2],[CHARGE DESCRIPTION],
[DESCRIPTION LINE 1],[DESCRIPTION LINE 2],[DISCOUNT TARIFF CODE],[DISCOUNT PERCENT],[CUSTOMER REF 1],[CUSTOMER REF 2],[CUSTOMER REF 3],
[CUSTOMER REF 4],[CUSTOMER REF 5],[CUSTOMER REF 6]) values (@JOB,@A,@B,@C,@D,@E,@F,@G,@H,@I,@J,@K,@L,@M,@N,@O,@P,@Q,@R,@S,@T,@U,@V,@W,@X,@Y,@Z,
@AA,@AB,@AC,@AD,@AE,@AF,@AG,@AH,@AI,@AJ,@AK,@AL,@AM,@AN,@AO,@AP,@AQ,@AR,@AS,@AT,@AU,@AV,@AW,@AX,@AY,@AZ,
@BA,@BB,@BC,@BD,@BE,@BF,@BG,@BH,@BI,@BJ,@BK,@BL,@BM,@BN,@BO,@BP,@BQ,@BR,@BS,@BT,@BU,@BV,@BW,@BX,@BY)");

					//ConnectSql_mb.sqlResult re = ConnectSql_mb.ExecuteNonQuery(sql, list);

					SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["local"].ConnectionString);
					SqlCommand cmd = new SqlCommand(sql, sqlConn);
					sqlConn.Open();
					
					foreach (cmdParameters par in list)
                    {
                        if (par.size == 0)
                        {
                            com.Parameters.Add(par.name, par.type);
                        }
                        else
                        {
                            com.Parameters.Add(par.name, par.type, par.size);
                        }
                        com.Parameters[par.name].Value = par.value;
                    }
            object o = cmd.ExecuteNonQuery();
            sqlConn.Close();

					//if (re.status)
					//{
						successJob++;
					//}
					//else
					//{
					//	errorDo++;
						//throw new Exception(re.context);
					//}
				}
				catch (Exception ex)
				{
					errorDo++;
					//throw new Exception(ex.ToString());
				}
			}
			else
			{
				if (B.Trim().ToUpper().Equals("BILL TYPE"))
				{
					beginImport = true;
				}
			}
		}

		re_text = string.Format(@"uploaded {0} jobs", successJob);
		re_text += successItem > 0 ? string.Format(@" {0} items.", successItem) : ".";
		re_text += existDo > 0 ? string.Format(@" {0} existed", existDo) : "";
		re_text += errorDo > 0 ? string.Format(@" {0} error", errorDo) : "";
		error_text = re_text;
		string user = HttpContext.Current.User.Identity.Name;
		string sql1 = string.Format(@"insert into CTM_JobEventLog (CreateDatetime,Controller,JobType,Remark) values (getdate(),@Controller,@JobType,@Remark)");
		List<ConnectSql_mb.cmdParameters> list1 = new List<ConnectSql_mb.cmdParameters>();
		list1.Add(new ConnectSql_mb.cmdParameters("@Controller", user, SqlDbType.NVarChar, 100));
		list1.Add(new ConnectSql_mb.cmdParameters("@JobType", "PSA", SqlDbType.NVarChar, 100));
		list1.Add(new ConnectSql_mb.cmdParameters("@Remark", re_text, SqlDbType.NVarChar, 300));
		ConnectSql_mb.sqlResult re1 = ConnectSql_mb.ExecuteNonQuery(sql1, list1);
	}

	protected void btn_upload_Click(object sender, EventArgs e)
	{
		string _name = "";
		try
		{
			string _desc = "";
			UploadedFile[] files = this.file_upload1.UploadedFiles;
			_name = (files[0].FileName ?? "").ToLower().Trim();
			if (_name.Length == 0) return;
			string error_text = "";
			ProcessFile(_type, _id, _name, _desc, files[0].FileBytes, out error_text);
			this.lb_txt.Text = error_text.Length > 0 ? error_text : "Upload Error";
		}
		catch (Exception ex)
		{
			this.lb_txt.Text = "Upload File fail, pls try agin, error:" + ex.Message + ex.StackTrace;

			string user = HttpContext.Current.User.Identity.Name;
			string sql1 = string.Format(@"insert into CTM_JobEventLog (CreateDatetime,Controller,JobType,Remark) values (getdate(),@Controller,@JobType,@Remark)");
			List<ConnectSql_mb.cmdParameters> list1 = new List<ConnectSql_mb.cmdParameters>();
			list1.Add(new ConnectSql_mb.cmdParameters("@Controller", user, SqlDbType.NVarChar, 100));
			list1.Add(new ConnectSql_mb.cmdParameters("@JobType", "PSA", SqlDbType.NVarChar, 100));
			list1.Add(new ConnectSql_mb.cmdParameters("@Remark", this.lb_txt.Text, SqlDbType.NVarChar, 300));
			ConnectSql_mb.sqlResult re1 = ConnectSql_mb.ExecuteNonQuery(sql1, list1);
		}
	}

	public string _id = "0";
	public string _site = "ALL";
	public string _type = "ATTACH";
	public string _page = "AttachEdit";
	public string _where = "Where";
	public string _key = "id";
	public string _query = "RowType=''";
	public string _code = "code";
	public string _status = "LIST";
	public string _table = "X2";
	public string _cat = "";
	public void ProcessFile(string _type, string _code, string _name, string _desc, byte[] _buffer, out string error_text)
	{
		string path1 = string.Format("~/files/{0}_{1}/", _type, _code);
		string path2 = path1.Replace(' ', '_').Replace('\'', '_');
        string pathx = path2.Substring(1);
        string path3 = MapPath(path2);
        if (!Directory.Exists(path3))
            Directory.CreateDirectory(path3);
        string __name = _name.Replace(' ', '_').Replace('\'', '_');
		string path4 = path3 + __name;
		bool isOk = false;
		FileStream fs = new FileStream(path4, FileMode.Create);

		BinaryWriter bw = new BinaryWriter(fs);
		bw.Write(_buffer);
		bw.Close();
		fs.Close();
		throw new Exception("here");
        //AddFile(_type, _code, pathx, _desc, __name, _buffer.Length);
        string batch = DateTime.Now.ToString("yyyyMMddHHmmss");
        ImportJob(batch, path4, out error_text);
    }




    protected void btn_search_Click(object sender, EventArgs e)
    {
        string sql = string.Format(@"select * From psa_bill where 1=1");
        if (search_from.Date != null)
        {
            sql += " and F1>='" + search_from.Date.ToString("yyyyMMdd") + "'";
        }
        if (search_to.Date != null)
        {
            sql += " and F1<'" + search_to.Date.AddDays(1).ToString("yyyyMMdd") + "'";
        }
        DataTable dt = ConnectSql.GetTab(sql);
        gv.DataSource = dt;
        gv.DataBind();
    }

    private string reGetDateFormat(string par, string file)
    {
        string res = "19000101";

        if (file.ToLower().EndsWith(".csv"))
        {
            string[] ar = par.Split(' ');
            if (ar[0].Length > 0)
            {
                string[] ar_date = ar[0].Split('/');
                string ar_date_year = ar_date[2];
                if (ar_date_year.Length == 2)
                {
                    ar_date_year = DateTime.Now.ToString("yyyy").Substring(0, 2) + ar_date_year;
                }
                DateTime dt_t = new DateTime(SafeValue.SafeInt(ar_date_year, 1900), SafeValue.SafeInt(ar_date[1], 1), SafeValue.SafeInt(ar_date[0], 1));
                res = dt_t.ToString("yyyyMMdd");
                if (ar.Length > 1)
                {
                    res += " " + ar[1];
                }
            }
        }
        else
        {
            res = SafeValue.SafeDate(par, new DateTime(1900, 1, 1)).ToString("yyyyMMdd HH:mm:ss");
        }

        return res;
    }


}