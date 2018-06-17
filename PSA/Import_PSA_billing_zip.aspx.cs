using Aspose.Cells;
using DevExpress.Web.ASPxUploadControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using C2;

public partial class PSA_Import_PSA_billing_zip : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack) 
		{
			//InsertPl();
			search_from.Date = DateTime.Today.AddDays(-1);
			search_to.Date = DateTime.Today;
		}
	}

	public void ImportJob(string batch, string file, out string error_text)
	{
		//throw new Exception("importjob");
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

		//throw new Exception("here");
		//=================================== version 1
        bool beginImport = false;
		int _row = 0;
		//int existDo = 0;
		//int successJob = 0;
        string errorNo = "";
		string type = "";
		for (int i = 1; true; i++)
		{
			_row++;
			if (empty_i >= 10) { break; }
			DateTime date = DateTime.Today;

			string A = "";//ws.Cells["A" + i].StringValue;
			string B = ws.Cells["B" + i].StringValue;
			if (B.Trim().ToUpper().Equals("BILL TYPE"))
			{
				beginImport = true;
				empty_i++;
				continue;
			}

			//throw new Exception(ws.Cells["A" + i].Type.ToString());
			try
			{
				type = ws.Cells["A" + i].Type.ToString();
				if (type == "IsString")
				{
					string processDate = SafeValue.SafeString(ws.Cells["A" + i].StringValue);
					A = processDate;
				}
				else {
					date = ws.Cells["A" + i].DateTimeValue;
					A = date.ToString("yyyyddMM");
					// by kevin one time
					//	A = reGetDateFormat(ws.Cells["A" + i].StringValue, file);
				}
			}
			catch
			{
				empty_i++;
				continue;
			}

			if (B.Length <= 0)
			{
				empty_i++;
				continue;
			}

			empty_i = 0;

			
			DateTime dt_AA = new DateTime(1900,1,1);
			DateTime dt_AB = new DateTime(1900,1,1);
			DateTime dt_AC = new DateTime(1900,1,1);
			if (beginImport)
			{
				#region
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
				string S1 = ws.Cells["S" + i].StringValue;
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

				//					if(AA.IndexOf("/") < 0)
				//		dt_AA = ws.Cells["AA" + i].DateTimeValue;
				//	if(AB.IndexOf("/") < 0)
				//		dt_AB = ws.Cells["AB" + i].DateTimeValue;
				//	if(AC.IndexOf("/") < 0)
				//		dt_AC = ws.Cells["AC" + i].DateTimeValue;
				//throw new Exception(G + " / " + AA + " / " + AB + " / " + AC + 
				//" / " + ws.Cells["AB" + i].Value.ToString()
			//	" / " + dt_AA.ToString() +
			//	" / " + dt_AB.ToString() +
			//	" / " + dt_AC.ToString()
					
			//		);

				
			//	throw new Exception(
			//		"\r\n" + type + "/" + ws.Cells["A" + i].StringValue + "/" + A + "#\r\n" +
			//		G + "/" + reGetDateFormat(G, file) + "#\r\n" +
			//		AA + "/" + reGetDateFormat(AA, file) + "#\r\n" +
			//		AB + "/" + reGetDateFormat(AB, file) + "#\r\n" +
			//		AC + "/" + reGetDateFormat(AC, file) + "#\r\n" +
			//		AN + "/" + reGetDateFormat(AN, file) + "#\r\n" +
			//		AO + "/" + reGetDateFormat(AO, file) + "#\r\n"
			//		);

				#endregion
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
						string[] ar_t = A.ToString().Split(' ');
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
					ConnectSql_mb.cmdParameters cpar = null;
					if (type == "IsString")
					{
						cpar = new ConnectSql_mb.cmdParameters("@A", reGetDateFormat(A, file), SqlDbType.NVarChar, 100);
					}
					else
					{
						cpar = new ConnectSql_mb.cmdParameters("@A", A, SqlDbType.NVarChar, 200);
					}

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
					cpar = new ConnectSql_mb.cmdParameters("@I", I.Replace(" ", ""), SqlDbType.NVarChar, 100);
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
					cpar = new ConnectSql_mb.cmdParameters("@S", S1, SqlDbType.NVarChar, 100);
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

					string _AA = "";
					if(AA.IndexOf("/") < 0 && AA!="")
						_AA = ws.Cells["AA" + i].DateTimeValue.ToString("yyyyMMdd HH:mm");
					else
						_AA = reGetDateFormat(AA, file);


					cpar = new ConnectSql_mb.cmdParameters("@AA", _AA, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					//cpar = new ConnectSql_mb.cmdParameters("@AB", SafeValue.SafeDate(AB, new DateTime(1900, 1, 1)), SqlDbType.DateTime);
					string _AB = "";
					if(AB.IndexOf("/") < 0 && AB!="")
						_AB = ws.Cells["AB" + i].DateTimeValue.ToString("yyyyMMdd HH:mm");
					else
						_AB = reGetDateFormat(AB, file);
						DateTime _ETA = new DateTime(Helper.Safe.SafeInt(_AB.Substring(0, 4)), Helper.Safe.SafeInt(_AB.Substring(4, 2)), Helper.Safe.SafeInt(_AB.Substring(6, 2)));
					string ETA1 = _ETA.AddDays(-7).ToString("yyyy-MM-dd");
					string ETA2 = _ETA.AddDays(7).ToString("yyyy-MM-dd");

					string _G = "";
					if(G.IndexOf("/") < 0 && G!="")
						_G = ws.Cells["G" + i].DateTimeValue.ToString("yyyyMMdd HH:mm");
					else
						_G = reGetDateFormat(G, file);
						DateTime _BILLD = new DateTime(Helper.Safe.SafeInt(_G.Substring(0, 4)), Helper.Safe.SafeInt(_G.Substring(4, 2)), Helper.Safe.SafeInt(_G.Substring(6, 2)));
					string BILLD1 = _BILLD.AddDays(-16).ToString("yyyy-MM-dd");
					string BILLD2 = _BILLD.AddDays(-2).ToString("yyyy-MM-dd");
					string JOBTYPE = "";
					string VOY = "";
					if(AT == "O") {
						JOBTYPE = " AND (j.JobType = 'EXP') ";
						VOY = Q;
					}
					if(AT == "I") {
						JOBTYPE = " AND (j.JobType = 'IMP') ";
						VOY = R;
					}
					//string JOB = Helper.Sql.Text("select top 1 c.jobno from ctm_job j, ctm_jobdet1 c where j.jobno=c.jobno and j.etadate>'"+ETA1+"' and j.etadate<'"+ETA2+"' and c.containerno='"+I+"' " + JOBTYPE);
					// change to add in voyage and based on job date
					string JOB="";
					if(I != "") {
						string _job_sql = "select top 1 c.jobno from ctm_job j, ctm_jobdet1 c where j.jobno=c.jobno and (j.voyage like '%"+Q+"%' or j.voyage like '%"+R+"%') and j.etadate>'"+BILLD1+"' and j.etadate<'"+BILLD2+"' and c.containerno like '%"+I+"%' " + JOBTYPE;
						//throw new Exception(_job_sql);
						JOB = Helper.Sql.Text(_job_sql);
					}
					cpar = new ConnectSql_mb.cmdParameters("@AB", _AB, SqlDbType.NVarChar, 100);
					list.Add(cpar);
					//cpar = new ConnectSql_mb.cmdParameters("@AC", SafeValue.SafeDate(AC, new DateTime(1900, 1, 1)), SqlDbType.DateTime);

					string _AC = "";
					if(AC.IndexOf("/") < 0  && AC!="")
						_AC = ws.Cells["AC" + i].DateTimeValue.ToString("yyyyMMdd HH:mm");
					else
						_AC = reGetDateFormat(AC, file);
					cpar = new ConnectSql_mb.cmdParameters("@AC", _AC, SqlDbType.NVarChar, 100);
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
					cpar = new ConnectSql_mb.cmdParameters("@AN", reGetDateFormat(AN, file), SqlDbType.NVarChar, 100);
					list.Add(cpar);
					cpar = new ConnectSql_mb.cmdParameters("@AO", reGetDateFormat(AO, file), SqlDbType.NVarChar, 100);
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
					cpar = new ConnectSql_mb.cmdParameters("@JOB", JOB, SqlDbType.NVarChar, 20);
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

					foreach (ConnectSql_mb.cmdParameters par in list)
					{
						if (par.size == 0)
						{
							cmd.Parameters.Add(par.name, par.type);
						}
						else
						{
							cmd.Parameters.Add(par.name, par.type, par.size);
						}

						cmd.Parameters[par.name].Value = par.value;
					}

					object o = cmd.ExecuteNonQuery();
					sqlConn.Close();

					//if (re.status)
					//{
					successJob++;
					CreateJobCost(J,K,I,JOB,S.Dec(N),S.Dec(L),S.Dec(O),BF);
                    //Create_JobCost(J, K, I, Q, SafeValue.SafeDecimal(O, 0));
					//throw new Exception("Break Now");
					//feeUpdate(J, K, I, Q, SafeValue.SafeDecimal(O, 0));
                    
					//}
					//else
					//{
					//	errorDo++;
					//throw new Exception(re.context);
					//}

					//  if (re.status)
					//  {
					//      successJob++;
					//  }
					//  else
					//  {
					//      errorDo++;
					//      throw new Exception(re.context);
					//  }
				}
				catch (Exception ex)
				{
					errorDo++;
                    errorNo += billNo + " / " + i+",";
					throw new Exception(_row.ToString() + "##" + ex.Message+ex.StackTrace);
				}
			}
			else
			{
			}
		}

		re_text = string.Format(@"uploaded {0} records", successJob);
		re_text += successItem > 0 ? string.Format(@" {0} items.", successItem) : ".";
		re_text += existDo > 0 ? string.Format(@" {0} existed", existDo) : "";
		re_text += errorDo > 0 ? string.Format(@" {0} error", errorDo)+"Bill/Line No:"+errorNo : "";
		error_text = re_text;
		string user = HttpContext.Current.User.Identity.Name;
		string sql1 = string.Format(@"insert into CTM_JobEventLog (CreateDatetime,Controller,JobType,Remark) values (getdate(),@Controller,@JobType,@Remark)");
		List<ConnectSql_mb.cmdParameters> list1 = new List<ConnectSql_mb.cmdParameters>();
		list1.Add(new ConnectSql_mb.cmdParameters("@Controller", user, SqlDbType.NVarChar, 100));
		list1.Add(new ConnectSql_mb.cmdParameters("@JobType", "PSA", SqlDbType.NVarChar, 100));
		list1.Add(new ConnectSql_mb.cmdParameters("@Remark", re_text + " ("+ file + ")", SqlDbType.NVarChar, 300));
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

			//Auto Payable
			InsertPl();
			//Auto Payment
            //InsertPay();
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
			list1.Add(new ConnectSql_mb.cmdParameters("@Remark", this.lb_txt.Text + " @" + _name, SqlDbType.NVarChar, 300));
			ConnectSql_mb.sqlResult re1 = ConnectSql_mb.ExecuteNonQuery(sql1, list1);
		}
	}

	public string _id = "0";
	public string _site = "ALL";
	public string _type = "PSA_ZipFile";
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
        string __name = _name.Replace(' ', '_').Replace('\'', '_');
		if (!Directory.Exists(path3))
			Directory.CreateDirectory(path3);
		//string __name = _name.Replace(' ', '_').Replace('\'', '_');
		string path4 = path3 + __name;
		bool isOk = false;
		FileStream fs = new FileStream(path4, FileMode.Create);

		BinaryWriter bw = new BinaryWriter(fs);
		bw.Write(_buffer);
		bw.Close();
		fs.Close();
		//AddFile(_type, _code, pathx, _desc, __name, _buffer.Length);
		string batch = DateTime.Now.ToString("yyyyMMddHHmmss");

		if (path4.ToLower().EndsWith(".zip"))
		{
			string folder_dir = path4.Substring(0, path4.Length - 4);
			ZipFile.ExtractToDirectory(path4, folder_dir);
			DirectoryInfo dir = new System.IO.DirectoryInfo(folder_dir);
			string target_file = "";
			foreach (FileInfo f in dir.GetFiles())
			{
				string _fname = f.Name.ToLower();
                if (_fname.EndsWith(".csv") || _fname.EndsWith(".xls") || _fname.EndsWith(".xlsx"))
                {
                    target_file = f.FullName;
                    break;
                }
            }
            error_text = "";
            if (target_file != "")
            {
                ImportJob(batch, target_file, out error_text);
            }
            dir.Delete(true);
        }
        else
        {
            ImportJob(batch, path4, out error_text);
        }
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


    private string reGetDateFormat(string par2, string file)
    {
        string res = "19000101";
		string par = par2;
		//par = "7/5/2017 19:30";

        if (file.ToLower().EndsWith(".csv") || file.ToLower().EndsWith(".xls"))
        {
            string[] ar = par.Split(' ');
            if (ar[0].Length > 0)
            {

                string[] ar_date = ar[0].Split(new char[] {'/',' ',':'});
                if (ar_date.Length < 3)
                {
                    return res;
                }
                string ar_date_year = ar_date[2];

                if (ar_date_year.Length == 2)
                {
                    ar_date_year = DateTime.Now.ToString("yyyy").Substring(0, 2) + ar_date_year;
                }
                DateTime dt_t = new DateTime(SafeValue.SafeInt(ar_date_year, 1900), SafeValue.SafeInt(ar_date[1], 1), SafeValue.SafeInt(ar_date[0], 1));
               res = dt_t.ToString("yyyyMMdd");
                if (ar.Length > 1)
                {
                    //res += " " + ar[1];
                }
            }
        }
        else
        {
            res = SafeValue.SafeDate(par, new DateTime(1900, 1, 1)).ToString("yyyyMMdd HH:mm:ss");
        }
		//throw new Exception("[" + par + "###" + res + "]");
        return res;
    }

    private string reGetDateFormat2(string par2, string file)
    {
        string res = "19000101";
		string par = par2;
		//par = "7/5/2017 19:30";
		throw new Exception(par + "//" + file);
        if (file.ToLower().EndsWith(".csv") || file.ToLower().EndsWith(".xls"))
        {
            string[] ar = par.Split(' ');
            if (ar[0].Length > 0)
            {

                string[] ar_date = ar[0].Split(new char[] {'/',' ',':'});
                if (ar_date.Length < 3)
                {
                    return res;
                }
                string ar_date_year = ar_date[2];

                if (ar_date_year.Length == 2)
                {
                    ar_date_year = DateTime.Now.ToString("yyyy").Substring(0, 2) + ar_date_year;
                }
                DateTime dt_t = new DateTime(SafeValue.SafeInt(ar_date_year, 1900), SafeValue.SafeInt(ar_date[1], 1), SafeValue.SafeInt(ar_date[0], 1));
               res = dt_t.ToString("yyyyMMdd");
                if (ar.Length > 1)
                {
                    //res += " " + ar[1];
                }
            }
        }
        else
        {
            res = SafeValue.SafeDate(par, new DateTime(1900, 1, 1)).ToString("yyyyMMdd HH:mm:ss");
        }
		//throw new Exception("[" + par + "###" + res + "]");
        return res;
    }
    private string InsertPay()
    {
        string sql = string.Format(@"select max(F1) as F1,[BILL NUMBER] as BillNo,max([CURRENCY CODE]) Currency,max([CONTAINER NUMBER]) as ContNo,max([BILLING COMPANY]) as PartyTo,max([BILL TYPE]) as MastType,max([BILL DATE]) as BillDate from psa_bill group by [BILL NUMBER] ");
        string invN = "";
        int invId = 0;
        DataTable mast = ConnectSql.GetTab(sql);
        string type = "PL";
        for (int i = 0; i < mast.Rows.Count; i++)
        {

            string partyId = SafeValue.SafeString(mast.Rows[i]["PartyTo"]);
            string billType = SafeValue.SafeString(mast.Rows[i]["MastType"]);
            string billNo = SafeValue.SafeString(mast.Rows[i]["BillNo"]);
            string cur = SafeValue.SafeString(mast.Rows[i]["Currency"]);
            string cntNo = SafeValue.SafeString(mast.Rows[i]["ContNo"]);

            string counterType = "";

            DateTime docDate = SafeValue.SafeDate(mast.Rows[i]["BillDate"], DateTime.Today);
            sql = string.Format(@"select count(*) from XAApPayment where PartyTo='{0}' and DocDate='{1}' and MastRefNo='{2}'", partyId, docDate, billNo);
            int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (n == 0)
            {
                #region invoice mast
                counterType = "AP-PAYMENT-PSA";
                C2.XAApPayment inv = new C2.XAApPayment();

                inv.DocDate = docDate;
                invN = C2Setup.GetNextNo("", counterType, inv.DocDate);
                inv.PartyTo = "PSAC";
                inv.DocType = type;
                inv.DocType1 = "PSA";
                inv.DocNo = invN.ToString();
                string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

                inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
                inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);
                inv.ChqNo = "GIRO";
                //

                inv.Remark = "";
                inv.CurrencyId = cur;
                inv.ExRate = 1;
                inv.AcCode = EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
                inv.AcSource = "CR";
                inv.MastRefNo = billNo;
                inv.JobRefNo = "";

                inv.ExportInd = "N";
                inv.CreateBy = HttpContext.Current.User.Identity.Name;
                inv.CreateDateTime = DateTime.Now;
                inv.CancelDate = new DateTime(1900, 1, 1);
                inv.CancelInd = "N";
                try
                {
                    C2Setup.SetNextNo("", counterType, invN, inv.DocDate);
                    C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(inv);

                    invId = inv.SequenceId;
                }
                catch
                {
                }
                #endregion

                #region Inv det
                sql = string.Format(@"select * from psa_bill where [BILL NUMBER]='{0}' ", billNo);

                DataTable tab = ConnectSql.GetTab(sql);
                for (int j = 0; j < tab.Rows.Count; j++)
                {
                    decimal amount = Convert.ToDecimal(tab.Rows[j]["AMOUNT"]);
                    if (amount != 0)
                    {
                        string refNo = SafeValue.SafeString(tab.Rows[j]["REF NUMBER"]);
                        string code = SafeValue.SafeString(tab.Rows[j]["TARIFF CODE"]);
                        string des = SafeValue.SafeString(tab.Rows[j]["TARIFF DESCRIPTION"]);
                        DateTime date = SafeValue.SafeDate(tab.Rows[j]["BILL DATE"], DateTime.Today);
                        decimal qty = SafeValue.SafeDecimal(tab.Rows[j]["BILLABLE UNIT"], 0);
                        decimal price = SafeValue.SafeDecimal(tab.Rows[j]["AMOUNT"], 0);
                        decimal exRate = SafeValue.SafeDecimal(tab.Rows[j]["EXCHANGE RATE"], 0);
                        decimal gst = SafeValue.SafeDecimal(tab.Rows[j]["GST PERCENTAGE"], 0);
                        if (invId < 1)
                            return "";

                        if (des != "GST")
                        {
                            InsertPay_Det(invId, invN, type, j + 1, code, refNo, des, qty, price, cur, exRate, gst, billType, billNo, "", cntNo);
                        }
                        if(invId>0){
                            UpdateApPayment(invId);
                        }
                    }
                }
                #endregion
            }

        }
        return invN;
    }
    private void InsertPay_Det(int docId, string docNo, string docType, int index, string code, string refNo, string des, decimal qty, decimal price, string cur, decimal exRate, decimal gst, string billType, string billNo, string mastType, string cntNo)
    {
        try
        {
            C2.XAApPaymentDet det = new C2.XAApPaymentDet();
            det.PayNo = docId.ToString();
            det.PayId = docId;
            det.DocId = 0;
            det.PayLineNo = index;
            det.DocNo = docNo;
            det.DocType = docType;
            det.AcCode = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select ApCode from ref_product where Code='{0}'", code)), "");
            det.AcSource = "DB";
            det.MastRefNo = refNo;
            det.JobRefNo = "";


            det.Remark1 = code;
            det.Remark2 = des;
            det.Remark3 = cntNo;
            det.DocAmt = price;
            det.Currency = cur;
            det.ExRate = 1;

            if (det.ExRate == 0)
                det.ExRate = 1;
            decimal locAmt = SafeValue.ChinaRound(price * det.ExRate, 2);
            det.DocAmt = price;
            det.LocAmt = locAmt;
            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(det);
        }
        catch
        {
        }
    }
    private string InsertPl()
    {
        string sql = string.Format(@"select max(F1) as F1,[BILL NUMBER] as BillNo,sum([AMOUNT]) as LocAmt, max([CURRENCY CODE]) Currency,max([CONTAINER NUMBER]) 
		as ContNo,max([BILLING COMPANY]) as PartyTo,max([BILL TYPE]) as MastType,max([BILL DATE]) as BillDate 
		from psa_bill  where [BILL NUMBER] not in (select SupplierBillNo from XaApPayable where PartyTo='PSAC') group by [BILL NUMBER] ");
        string invN = "";
        int invId = 0;
        DataTable mast = ConnectSql.GetTab(sql);
        string type = "PL";
        for (int i = 0; i < mast.Rows.Count; i++)
        {

            string partyId = SafeValue.SafeString(mast.Rows[i]["PartyTo"]);
            string billType = SafeValue.SafeString(mast.Rows[i]["MastType"]);
            string billNo = SafeValue.SafeString(mast.Rows[i]["BillNo"]);
            string cur = SafeValue.SafeString(mast.Rows[i]["Currency"]);
            string cntNo = SafeValue.SafeString(mast.Rows[i]["ContNo"]);
            decimal locAmt = SafeValue.SafeDecimal(mast.Rows[i]["LocAmt"]);

            string counterType = "";

            DateTime docDate = SafeValue.SafeDate(mast.Rows[i]["BillDate"], DateTime.Today);
            sql = string.Format(@"select count(*) from XAApPayable where PartyTo='{0}' and DocDate='{1}' and SupplierBillNo='{2}'", partyId, docDate, billNo);
            int n = SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql), 0);
            if (n == 0)
            {
                #region invoice mast
                counterType = "AP-PAYABLE";
                C2.XAApPayable inv = new C2.XAApPayable();

                inv.DocDate = docDate;
                invN = C2Setup.GetNextNo("", counterType, inv.DocDate);
                inv.PartyTo = "PSAC";
                inv.DocType = type;
                inv.DocNo = invN.ToString();
                string[] currentPeriod = EzshipHelper.GetAccPeriod(inv.DocDate);

                inv.AcYear = SafeValue.SafeInt(currentPeriod[1], inv.DocDate.Year);
                inv.AcPeriod = SafeValue.SafeInt(currentPeriod[0], inv.DocDate.Month);
                inv.Term = "";
                inv.SupplierBillNo = billNo;
                inv.SupplierBillDate = docDate;
                //
                int dueDay = SafeValue.SafeInt(inv.Term.ToUpper().Replace("DAYS", "").Trim(), 0);
                inv.Description = "";
                inv.CurrencyId = "SGD"; //cur;
                inv.ExRate = 1;
                inv.AcCode = "20";//EzshipHelper.GetAccArCode(inv.PartyTo, inv.CurrencyId);
                inv.AcSource = "CR";
				
				inv.DocAmt = locAmt;
				inv.LocAmt = locAmt;
				inv.BalanceAmt = locAmt;

                inv.MastType = "";
                inv.MastRefNo = "";
                inv.JobRefNo = "";

                inv.ExportInd = "N";
                inv.UserId = HttpContext.Current.User.Identity.Name;
                inv.EntryDate = DateTime.Now;
                inv.CancelDate = new DateTime(1900, 1, 1);
                inv.CancelInd = "N";
                try
                {
                    C2Setup.SetNextNo("", counterType, invN, inv.DocDate);
                    C2.Manager.ORManager.StartTracking(inv, Wilson.ORMapper.InitialState.Inserted);
                    C2.Manager.ORManager.PersistChanges(inv);

                    invId = inv.SequenceId;
                }
                catch
                {
                }
                #endregion

                #region Inv det
                sql = string.Format(@"select * from psa_bill where [BILL NUMBER]='{0}' ", billNo);

                DataTable tab = ConnectSql.GetTab(sql);
                for (int j = 0; j < tab.Rows.Count; j++)
                {
                    decimal amount = Convert.ToDecimal(tab.Rows[j]["AMOUNT"]);
                    if (amount != 0)
                    {
                        string job = SafeValue.SafeString(tab.Rows[j]["JOB_NO"]);
                        string refNo = SafeValue.SafeString(tab.Rows[j]["REF NUMBER"]);
                        string code = SafeValue.SafeString(tab.Rows[j]["TARIFF CODE"]);
                        string des = SafeValue.SafeString(tab.Rows[j]["TARIFF DESCRIPTION"]);
                        DateTime date = SafeValue.SafeDate(tab.Rows[j]["BILL DATE"], DateTime.Today);
                        decimal qty = SafeValue.SafeDecimal(tab.Rows[j]["BILLABLE UNIT"], 0);
                        decimal price = SafeValue.SafeDecimal(tab.Rows[j]["RATE"], 0);
                        decimal locamt = SafeValue.SafeDecimal(tab.Rows[j]["AMOUNT"], 0);
                        decimal exRate = SafeValue.SafeDecimal(tab.Rows[j]["EXCHANGE RATE"], 0);
                        decimal gst = SafeValue.SafeDecimal(tab.Rows[j]["GST PERCENTAGE"], 0);
                        //sql = string.Format(@"select count(*) from  XAApPayableDet det inner join  XAApPayable mast on det.DocNo=mast.DocNo where PartyTo='{0}' and ChgCode='{1}' and DocDate='{2}' and mast.SupplierBillNo='{3}'", partyId, code, date.ToString("yyyy/MM/dd"), billNo);
                        //int res = SafeValue.SafeInt(Helper.Sql.One(sql), 0);
                        //if (res == 0)
                        //{
						//if(des=="GST")
						//		qty = locamt;
						if(locamt != 0 && (qty==0 || price==0))
						{
							qty = 1;
							price = locamt;
						}
                        if (invId < 1)
                            return "";

                        if (des != "XXX")
                        {
                            InsertPl_Det(invId, invN, type, j + 1, code, job , des, qty, price, cur, exRate, gst, billType, billNo, inv.MastType, cntNo);
                        }
                        //if (invId > 0)
                           // UpdateMaster1(invId);
                        //}
                    }
                }
                #endregion
            }

        }
        return invN;
    }
    private void InsertPl_Det(int docId, string docNo, string docType, int index, string code, string lotNo, string des, decimal qty, decimal price, string cur, decimal exRate, decimal gst, string billType, string billNo, string mastType, string cntNo)
    {
        try
        {
            C2.XAApPayableDet det = new C2.XAApPayableDet();
            det.DocId = docId;
            det.DocLineNo = index;
            det.DocNo = docNo;
            det.DocType = docType;
            det.AcCode = "41";// SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select ApCode from ref_product where Code='{0}'", code)), "");
            det.AcSource = "DB";
            det.MastRefNo = lotNo;
            det.JobRefNo = cntNo;
            det.MastType = mastType;
            det.SplitType = "";

           


            det.ChgCode = code;
            det.ChgDes1 = des;
            det.ChgDes2 = cntNo;
            det.ChgDes3 = "";
            det.Price = price;
            det.Qty = qty;
            det.Unit = "";
            det.Currency = cur;
            det.ExRate = 1;
			if(S.Text(det.ChgDes1) == "GST")
				det.AcCode="1203";

			det.GstType = "Z";
			det.Gst = 0;
			det.Dim1 = "PRIME MOVER";
			
            decimal amt = SafeValue.ChinaRound(det.Qty * det.Price, 2);
            decimal gstAmt = 0; //SafeValue.ChinaRound((amt * det.Gst), 2);
            decimal docAmt = amt + gstAmt;
            decimal locAmt = docAmt;
			
			det.GstAmt = gstAmt;
            det.DocAmt = docAmt;
            det.LocAmt = locAmt;
            C2.Manager.ORManager.StartTracking(det, Wilson.ORMapper.InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(det);
        }
        catch
        {
        }
    }
    private void UpdateMaster1(int docId)
    {
        string sql = string.Format("update XAApPayableDet set LineLocAmt=locAmt* (select ExRate from XAApPayable where SequenceId=XAApPayableDet.docid) where DocId={0}", docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
        decimal docAmt = 0;
        decimal locAmt = 0;
        sql = string.Format("select AcSource,LocAmt,LineLocAmt from XAApPayableDet where DocId={0}", docId);
        DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
        for (int i = 0; i < tab.Rows.Count; i++)
        {
            if (tab.Rows[i]["AcSource"].ToString() == "DB")
            {
                docAmt += SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt += SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }
            else
            {
                docAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LocAmt"], 0);
                locAmt -= SafeValue.SafeDecimal(tab.Rows[i]["LineLocAmt"], 0);
            }

        }
        decimal balAmt = SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM XAApPaymentDet AS det INNER JOIN  XAApPayment AS mast ON det.PayId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", docId)), 0);
        balAmt += SafeValue.SafeDecimal(Manager.ORManager.ExecuteScalar(string.Format(@"SELECT  sum(det.DocAmt)
FROM XAArReceiptDet AS det INNER JOIN  XAArInvoice AS mast ON det.RepId = mast.SequenceId
WHERE (det.DocId = '{0}') and (det.DocType='PL' or det.DocType='SC' or det.DocType='SD')", docId)), 0);

        sql = string.Format("Update XAApPayable set DocAmt='{0}',LocAmt='{1}',BalanceAmt='{2}' where SequenceId='{3}'", docAmt, locAmt, docAmt - balAmt, docId);
        C2.Manager.ORManager.ExecuteCommand(sql);
    }
    private void UpdateApPayment(int docId)
    {
        string sql = "select SUM(docAmt) from XAApPaymentDet where PayId ='" + docId + "' and DocType='PS'";
        decimal docAmt = SafeValue.SafeDecimal(C2.Manager.ORManager.ExecuteScalar(sql), 0);
        string sql_invoice = string.Format("update XAApPayment set DocAmt={1},LocAmt={2} where SequenceId={0}", docId, docAmt, docAmt);
        int x = Manager.ORManager.ExecuteCommand(sql_invoice);

    }
    private void feeUpdate(string tariffCode, string tariffDes, string Cont, string Voyage, Decimal amount)
    {
        string fee_code = "";
        switch (tariffCode)
        {
            case "5501":
                fee_code = "PSA LOLO";
                break;
            case "5502":
                fee_code = "PSA LOLO";
                break;
            case "8928":
                fee_code = "WRONG WEIGHT";
                break;
            case "4546":
                fee_code = "PSA STORAGE";
                break;
            case "5314":
                fee_code = "PSA FLEXIBOOK";
                break;
            case "5311":
                fee_code = "PSA FLEXIBOOK";
                break;
            case "4361":
                fee_code = "PSA NO SHOW";
                break;
            case "9502":
                fee_code = "CHASSIS DEMURRAGE";
                break;
        }
        string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeDes like '%{0}%'", fee_code);
        DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
        string chgCodeId = "";
        decimal gst = 0;
        string gstType = "";
        string chgTypeId = "";
        if (dt_chgCode.Rows.Count > 0)
        {
            chgCodeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgcodeId"]);
            gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
            gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
            chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
        }
        if (!fee_code.Equals(""))
        {
            Cont = Cont.Replace(" ", "");
            string sql = string.Format(@"select distinct job.JobNo,ContainerType,JobType from ctm_job as job
left outer join ctm_jobdet1 as det1 on job.jobno=det1.jobno
where det1.ContainerNo=@ContainerNo and job.Voyage=@Voyage");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", Cont, SqlDbType.NVarChar, 100));
            list.Add(new ConnectSql_mb.cmdParameters("@Voyage", Voyage, SqlDbType.NVarChar, 100));
            DataTable dt = ConnectSql_mb.GetDataTable(sql, list);
            if (dt.Rows.Count > 0)
            {
                string jobNo = dt.Rows[0]["JobNo"].ToString();
                string contType=dt.Rows[0]["ContainerType"].ToString();
                string jobType=dt.Rows[0]["JobType"].ToString();
                decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(amount, 0), 2);
                decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
                decimal docAmt = amt + gstAmt;
                decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
                sql = string.Format(@"insert into job_cost(ChgCode,ChgCodeDes,Qty,Price,JobNo,ContNo,ContType,JobType,CurrencyId,ExRate,LocAmt) values('{0}','{1}',1,{2},'{3}','{4}','{5}','{6}','SGD',1,{7})", chgCodeId, fee_code, amount, jobNo, Cont, contType, jobType, locAmt);
                if (ConnectSql_mb.ExecuteNonQuery(sql)>0)
                {
                    string user = HttpContext.Current.User.Identity.Name;
                    C_Job_Detail_EventLog c = new C_Job_Detail_EventLog();
                    c.Controller = user;
                    c.Remark = tariffDes + ": " + amount;
                    c.JobNo = jobNo;
                    c.ContainerNo = Cont;
                    c.Job_Detail_EventLog_Add();
                }
            }

        }
    }
    private class C_Job_Detail_EventLog
    {
        #region columns
        public int Id { get; set; }
        DateTime CreateDateTime { get; set; }
        public string Controller { get; set; }
        public string JobNo { get; set; }
        public string JobType { get; set; }
        public string ParentJobNo { get; set; }
        public string ParentJobType { get; set; }
        public string ContainerNo { get; set; }
        public string Trip { get; set; }
        public string Driver { get; set; }
        public string Towhead { get; set; }
        public string Trail { get; set; }
        public string Remark { get; set; }
        public string Note1 { get; set; }
        public string Note1Type { get; set; }
        public string Note2 { get; set; }
        public string Note3 { get; set; }
        public string Note4 { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Platform { get; set; }
        #endregion

        public void Job_Detail_EventLog_Add()
        {
            C_Job_Detail_EventLog l = this;
            //string sql = string.Format(@"insert into CTM_JobEventLog (CreateDateTime,Controller,JobNo,ContainerNo,Trip,Driver,Towhead,Trail,Remark,Note1,Note2,Note3,Note4,Lat,Lng,Platform,JobType,ParentJobNo,ParentJobType,Note1Type) values(getdate(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')", l.Controller, l.JobNo, l.ContainerNo, l.Trip, l.Driver, l.Towhead, l.Trail, l.Remark, l.Note1, l.Note2, l.Note3, l.Note4, l.Lat, l.Lng, l.Platform, l.JobType, l.ParentJobNo, l.ParentJobType, l.Note1Type);
            //ConnectSql_mb.ExecuteNonQuery(sql);

            string sql = string.Format(@"insert into CTM_JobEventLog (CreateDateTime,Controller,JobNo,ContainerNo,Trip,Driver,Towhead,Trail,Remark,Note1,Note2,Note3,Note4,Lat,Lng,Platform,JobType,ParentJobNo,ParentJobType,Note1Type) values(getdate(),@Controller,@JobNo,@ContainerNo,@Trip,@Driver,@Towhead,@Trail,@Remark,@Note1,@Note2,@Note3,@Note4,@Lat,@Lng,@Platform,@JobType,@ParentJobNo,@ParentJobType,@Note1Type)");
            List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            ConnectSql_mb.cmdParameters cpar = new ConnectSql_mb.cmdParameters("@Controller", l.Controller, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@JobNo", l.JobNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ContainerNo", l.ContainerNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Trip", l.Trip, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Driver", l.Driver, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Towhead", l.Towhead, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Trail", l.Trail, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Remark", l.Remark, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note1", l.Note1, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note2", l.Note2, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note3", l.Note3, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note4", l.Note4, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Lat", l.Lat, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Lng", l.Lng, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Platform", l.Platform, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@JobType", l.JobType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ParentJobNo", l.ParentJobNo, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@ParentJobType", l.ParentJobType, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            cpar = new ConnectSql_mb.cmdParameters("@Note1Type", l.Note1Type, SqlDbType.NVarChar, 100);
            list.Add(cpar);
            ConnectSql_mb.ExecuteNonQuery(sql, list);
        }
    }
    private void Create_JobCost(string tariffCode, string tariffDes, string Cont, string Voyage, Decimal amoun)
    {
        string sql_chgCode = string.Format(@"select top 1 ChgcodeId,ChgcodeDes,GstP,GstTypeId,ChgTypeId from XXChgCode where ChgcodeDes like '%TRUCKING%'");
        DataTable dt_chgCode = ConnectSql.GetTab(sql_chgCode);
        decimal gst = 0;
        string gstType = "";
        string chgTypeId = "";
			if (dt_chgCode.Rows.Count > 0)
			{
				gst = SafeValue.SafeDecimal(dt_chgCode.Rows[0]["GstP"]);
				gstType = SafeValue.SafeString(dt_chgCode.Rows[0]["GstTypeId"]);
				chgTypeId = SafeValue.SafeString(dt_chgCode.Rows[0]["ChgTypeId"]);
			}

            Cont = Cont.Replace(" ", "");
            string sql = string.Format(@"select distinct job.JobNo,ClientId,ContainerType from ctm_job as job
left outer join ctm_jobdet1 as det1 on job.jobno=det1.jobno
where det1.ContainerNo='{0}' and job.Voyage like '%{1}'", Cont, Voyage);
            //List<ConnectSql_mb.cmdParameters> list = new List<ConnectSql_mb.cmdParameters>();
            //list.Add(new ConnectSql_mb.cmdParameters("@ContainerNo", Cont, SqlDbType.NVarChar, 100));
            //list.Add(new ConnectSql_mb.cmdParameters("@Voyage", Voyage, SqlDbType.NVarChar, 100));
            DataTable dt = D.List(sql); //ConnectSql_mb.GetDataTable(sql, list);
			//throw new Exception(sql);
            if (dt.Rows.Count > 0)
            {
                string client = SafeValue.SafeString(dt.Rows[0]["ClientId"]);
                string jobNo=SafeValue.SafeString(dt.Rows[0]["JobNo"]);
                string cntType=SafeValue.SafeString(dt.Rows[0]["ContainerType"]);
                string billType = "";//SafeValue.SafeString(dt.Rows[0]["BillType"]);
                string str = cntType.Substring(0, 2);
                string sql_rate = "";
                if (cntType != "20HD" && str == "20")
                {
                    sql_rate = string.Format(@"select top 1  ChgCode,ChgCodeDes,Qty,Price,BillClass,BillScope,CurrencyId,ExRate from job_rate where ClientId='{0}' and ContType!='20HD' and ContSize='{1}' and BillType='{2}' and BillScope='CONT'", client, str, billType);
                }
                else if (cntType != "40HD" && str == "40")
                {
                    sql_rate = string.Format(@"select top 1 ChgCode,ChgCodeDes,Qty,Qty,Price,BillClass,BillScope,CurrencyId,ExRate from job_rate where ClientId='{0}' and ContType!='40HD' and ContSize='{1}' and BillType='{2}' and BillScope='CONT'", client, str, billType);
                }
                else
                {
                    sql_rate = string.Format(@"select top 1  ChgCode,ChgCodeDes,Price,BillClass,BillScope,CurrencyId,ExRate from job_rate where ClientId='{0}' and ContType='{1}' and ContSize='{2}' and BillType='{3}' and BillScope='CONT'", client, cntType, str, billType);
                }
                string sql_cost = string.Format(@"select count(*) from job_cost where JobNo='{0}' and ContNo='{1}' and ChgCodeDes like '%TRUCKING%'", jobNo, Cont);
                int n=SafeValue.SafeInt(ConnectSql.ExecuteScalar(sql_cost),0);
				n=0;
                if (n == 0)
                {
                    DataTable dt_rate = ConnectSql.GetTab(sql_rate);
                    for (int i = 0; i < dt_rate.Rows.Count; i++)
                    {
                        string chgCode = SafeValue.SafeString(dt_rate.Rows[i]["ChgCode"]);
                        string chgCodeDes = SafeValue.SafeString(dt_rate.Rows[i]["ChgCodeDes"]);
                        decimal price = SafeValue.SafeDecimal(dt_rate.Rows[i]["Price"]);
                        decimal qty = SafeValue.SafeDecimal(dt_rate.Rows[i]["Qty"]);


                        C2.Job_Cost cost = new C2.Job_Cost();
                        cost.JobNo = jobNo;
                        cost.ChgCode = chgCode;
                        cost.ChgCodeDe = chgCodeDes;
                        cost.ContNo = Cont;
                        cost.ContType = cntType;
                        cost.Price = price;
						cost.LineType="PSA";
                        cost.Qty = 1;
                        cost.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
                        cost.ExRate = new decimal(1.0);
                        decimal amt = SafeValue.ChinaRound(1 * SafeValue.SafeDecimal(price, 0), 2);
                        decimal gstAmt = SafeValue.ChinaRound((amt * SafeValue.SafeDecimal(gst, 0)), 2);
                        decimal docAmt = amt + gstAmt;
                        decimal locAmt = SafeValue.ChinaRound(docAmt * SafeValue.SafeDecimal(1, 1), 2);
                        cost.LocAmt = locAmt;
                        C2.Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
                        C2.Manager.ORManager.PersistChanges(cost);

						
						
                        string user = HttpContext.Current.User.Identity.Name;
                        C_Job_Detail_EventLog c = new C_Job_Detail_EventLog();
                        c.Controller = user;
                        c.Remark = tariffDes + ": " + price;
                        c.JobNo = jobNo;
                        c.ContainerNo = Cont;
                        c.Job_Detail_EventLog_Add();
                    }
                }
            }
        
    }
	
	 private void CreateJobCost(string psaCode, string psaDes, string cont2, string job, Decimal qty, Decimal price, Decimal amt, string gsttype)
    {
		if(psaCode == "7999")
			return;
		if(amt <= 0)
			return;

		string	cont = cont2.Replace(" ", "");

		DataTable dt = D.List(string.Format("select top 1 o.ClientId, c.BillType, c.Containertype from ctm_job o,ctm_jobdet1 c where o.jobno=c.jobno and c.jobno='{0}' and c.containerno='{1}'",job,cont));	
		if(dt.Rows.Count == 0)
			return;
			
		DataRow dr = dt.Rows[0];
		
		decimal gst = gsttype=="Z" ? 0: new decimal(0.07);
		string tariffType = psaCode.Length > 2 ? psaCode.Substring(0,2) : psaCode;
		
	
		string contType = S.Text(dr["ContainerType"]);
		string clientCode = S.Text(dr["ClientId"]);
		string billType = S.Text(dr["BillType"]);
		string user = HttpContext.Current.User.Identity.Name;
		
		string psaCode2 = "";
		string psaDes2 = "";
		decimal tamt = 0;
		decimal gstAmt = 0;
		decimal locAmt = 0;
		decimal docAmt = 0;
		if(tariffType == "55")
		{
			// add in trucking charges from bill rate
			DataTable dt2 = D.List(string.Format(@"select top 1 * from job_rate where ClientId='{0}' and BillType='{1}' and BillScope='CONT' and BillClass='{2}' and ContSize='{3}' and ChgCodeDes like '%{4}%'",
				clientCode, billType, "TRUCKING", contType.Substring(0,2), "TRUCKING"));
			DataTable dt3 = D.List(string.Format(@"select top 1 * from job_rate where ClientId='{0}' and BillType='{1}' and BillScope='CONT' and BillClass='{2}' and ContSize='{3}' and ChgCodeDes like '%{4}%'",
				clientCode, billType, "TRUCKING", contType.Substring(0,2), "PSA LOLO"));

			if(dt2.Rows.Count == 1)
			{
				psaCode2 = S.Text(dt2.Rows[0]["ChgCode"]);
				psaDes2 = S.Text(dt2.Rows[0]["ChgCodeDes"]);
				decimal _rate = S.Dec(dt2.Rows[0]["Price"]);
				C2.Job_Cost cost2 = new C2.Job_Cost();
				cost2.JobNo = job;
				cost2.ChgCode = psaCode2;
				cost2.ChgCodeDe = psaDes2;
				cost2.ContNo = cont;
				cost2.ContType = contType;
				cost2.Price = _rate;
				cost2.LineType="TRIP";
				cost2.Qty = qty;
				cost2.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
				cost2.ExRate = new decimal(1.0);
				tamt = SafeValue.ChinaRound(qty * _rate, 2);
				gstAmt = SafeValue.ChinaRound((tamt * SafeValue.SafeDecimal(gst, 0)), 2);
				docAmt = tamt + gstAmt;
				locAmt = docAmt;
				cost2.LocAmt = locAmt;
				C2.Manager.ORManager.StartTracking(cost2, Wilson.ORMapper.InitialState.Inserted);
				C2.Manager.ORManager.PersistChanges(cost2);

				C_Job_Detail_EventLog c2 = new C_Job_Detail_EventLog();
				c2.Controller = user;
				c2.Remark = "Add TRUCKING Charge [" + psaDes2 + "] " + price;
				c2.JobNo = job;
				c2.ContainerNo = cont;
				c2.Job_Detail_EventLog_Add();
				
			}
			
			if(dt3.Rows.Count == 1)
			{
				psaCode2 = S.Text(dt3.Rows[0]["ChgCode"]);
				psaDes2 = S.Text(dt3.Rows[0]["ChgCodeDes"]);

				C2.Job_Cost cost3 = new C2.Job_Cost();
				cost3.JobNo = job;
				cost3.ChgCode = psaCode2;
				cost3.ChgCodeDe = psaDes2;
				cost3.ContNo = cont;
				cost3.ContType = contType;
				cost3.Price = price;
				cost3.LineType="TRIP";
				cost3.Qty = qty;
				cost3.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
				cost3.ExRate = new decimal(1.0);
				tamt = SafeValue.ChinaRound(qty * price, 2);
				gstAmt = SafeValue.ChinaRound((tamt * SafeValue.SafeDecimal(gst, 0)), 2);
				docAmt = tamt + gstAmt;
				locAmt = docAmt;
				cost3.LocAmt = locAmt;
				C2.Manager.ORManager.StartTracking(cost3, Wilson.ORMapper.InitialState.Inserted);
				C2.Manager.ORManager.PersistChanges(cost3);

				 C_Job_Detail_EventLog c3 = new C_Job_Detail_EventLog();
				c3.Controller = user;
				c3.Remark = "Add PSA LOLO Charge [" + psaDes2 + "] " + price;
				c3.JobNo = job;
				c3.ContainerNo = cont;
				c3.Job_Detail_EventLog_Add();
				
			}
		}
		// normal psa cost	
		C2.Job_Cost cost = new C2.Job_Cost();
		cost.JobNo = job;
		cost.ChgCode = psaCode;
		cost.ChgCodeDe = psaDes;
		cost.ContNo = cont;
		cost.ContType = contType;
		cost.Price = price;
		cost.LineType="PSA";
		cost.Qty = qty;
		cost.CurrencyId = System.Configuration.ConfigurationManager.AppSettings["Currency"];
		cost.ExRate = new decimal(1.0);
		tamt = SafeValue.ChinaRound(qty * price, 2);
		gstAmt = SafeValue.ChinaRound((tamt * SafeValue.SafeDecimal(gst, 0)), 2);
		docAmt = tamt + gstAmt;
		locAmt = docAmt;
		cost.LocAmt = locAmt;
		C2.Manager.ORManager.StartTracking(cost, Wilson.ORMapper.InitialState.Inserted);
		C2.Manager.ORManager.PersistChanges(cost);

		C_Job_Detail_EventLog c = new C_Job_Detail_EventLog();
		c.Controller = user;
		c.Remark = "Add PSA Cost [" + psaDes + "] " + price;
		c.JobNo = job;
		c.ContainerNo = cont;
		c.Job_Detail_EventLog_Add();
        
    }
	
	
}