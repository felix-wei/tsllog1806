using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

public static class SeaFreightDocPrint
{
    #region import Ref
    public static DataTable dsImpLetter(string refNo)
    {
        string strsql = string.Format(@"exec proc_dsImpLetter '{0}','{1}','{2}','{3}','{4}'", refNo, "", "", "", "");
        DataSet ds_temp = ConnectSql.GetDataSet(strsql);
        DataTable dt = new DataTable();
        if (ds_temp.Tables.Count > 0)
        {
            dt = ds_temp.Tables[0].Copy();
        }
        return dt;
//        DataTable tab = new DataTable();
//        tab.Columns.Add("RefNo");
//        tab.Columns.Add("RefDate");
//        tab.Columns.Add("To");
//        tab.Columns.Add("Attn");
//        tab.Columns.Add("Ves");
//        tab.Columns.Add("Eta");
//        tab.Columns.Add("Pol");
//        tab.Columns.Add("Agent");
//        tab.Columns.Add("Obl");
//        tab.Columns.Add("Qty");
//        tab.Columns.Add("ContNo");
//        tab.Columns.Add("SpecRmk");
//        tab.Columns.Add("UserId");

//        DataRow rptRow = tab.NewRow();
//        string sql = string.Format(@"SELECT  RefNo, WarehouseId, Vessel + ' ' + Voyage AS Vessel, Eta, Pol, CrAgentId, OblNo AS obl, CONVERT(varchar(20),Qty) + ' ' + PackageType AS Pkgs, UserId, Remark
//FROM SeaImportRef WHERE (RefNo = '{0}')", refNo);

//        DataTable mast = ConnectSql.GetTab(sql);
//        if (mast.Rows.Count > 0)
//        {
//            DataRow row = mast.Rows[0];
//            rptRow["RefNo"] = SafeValue.SafeString(row["RefNo"], "");
//            rptRow["RefDate"] = DateTime.Today.ToString("dd/MM/yyyy");

//            string whId = SafeValue.SafeString(row["WarehouseId"], "");
//            string sql_vendor = string.Format("Select Name,AddRess,Fax1,Contact1 from XXParty where PartyId='{0}'", whId);
//            DataTable tab_vendor = ConnectSql.GetTab(sql_vendor);
//            if (tab_vendor.Rows.Count > 0)
//            {
//                rptRow["To"] = SafeValue.SafeString(tab_vendor.Rows[0]["Name"], "");
//                rptRow["To"] += "\n" + SafeValue.SafeString(tab_vendor.Rows[0]["Address"], "");
//                rptRow["Attn"] = SafeValue.SafeString(tab_vendor.Rows[0]["Contact1"], "");
//            }

//            rptRow["Ves"] = SafeValue.SafeString(row["Vessel"], "");
//            rptRow["Eta"] = SafeValue.SafeDateStr(row["ETA"]);
//            rptRow["Pol"] = EzshipHelper.GetPortName(row["Pol"]);
//            whId = SafeValue.SafeString(row["CrAgentId"], "");
//            rptRow["Agent"] = EzshipHelper.GetPartyName(whId);
//            rptRow["Obl"] = SafeValue.SafeString(row["Obl"], "");
//            rptRow["Qty"] = SafeValue.SafeString(row["Pkgs"], "");

//            rptRow["SpecRmk"] = SafeValue.SafeString(row["Remark"], "");
//            rptRow["UserId"] = SafeValue.SafeString(row["UserId"], "");
//        }
//        string sql_cont = string.Format(@"select ContainerNo + '/' + SealNo + '/' + ContainerType as container from SeaImportMkg where RefNo='{0}' and MkgType='Cont'", refNo);

//        DataTable cont_tab = ConnectSql.GetTab(sql_cont);
//        for (int i = 0; i < cont_tab.Rows.Count; i++)
//        {
//            if (i == 0)
//                rptRow["ContNo"] = SafeValue.SafeString(cont_tab.Rows[i][0], "");
//            else
//                rptRow["ContNo"] += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0], "");
//        }
//        tab.Rows.Add(rptRow);
//        return tab;

    }
    public static DataSet dsImpManifest(string refNo)
    {
        DataSet set = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_dsImpManifest '{0}','{1}','{2}','{3}','{4}'", refNo, "", "", "", "");
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set.Tables.Add(mast);
            set.Tables.Add(det);
            set.Relations.Add("Rela", mast.Columns["RefNo"], det.Columns["RefNo"]);
        }
        catch (Exception ex) { }
        return set;


//        DataTable mast = new DataTable("Mast");
//        mast.Columns.Add("RefNo");
//        mast.Columns.Add("Pol");
//        mast.Columns.Add("Ves");
//        mast.Columns.Add("Eta");
//        mast.Columns.Add("Obl");
//        mast.Columns.Add("Carrier");
//        mast.Columns.Add("ExRate");
//        mast.Columns.Add("ContNo");
//        mast.Columns.Add("Agt");
//        mast.Columns.Add("NvoAgt");

//        mast.Columns.Add("TotWt");
//        mast.Columns.Add("TotQty");
//        mast.Columns.Add("TotM3");
//        DataTable det = new DataTable("Detail");
//        det.Columns.Add("RefNo");
//        det.Columns.Add("HblN");
//        det.Columns.Add("ImpNo");
//        det.Columns.Add("CustName");
//        det.Columns.Add("Pod");
//        det.Columns.Add("ContNo");
//        det.Columns.Add("Qty");
//        det.Columns.Add("Pkg");

//        det.Columns.Add("Mkg");
//        det.Columns.Add("Des");
//        det.Columns.Add("Wt");
//        det.Columns.Add("M3");
//        det.Columns.Add("Cng");

//        det.Columns.Add("Fc");
//        det.Columns.Add("CltAmt");
//        det.Columns.Add("TVes");
//        det.Columns.Add("ExpBkgN");
//        det.Columns.Add("Agt");
//        det.Columns.Add("TEtd");
//        det.Columns.Add("TEta");
//        det.Columns.Add("NomInd");


//        string sql_mast = string.Format(@"SELECT  JobType,RefNo,AgentId,NvoccAgentId, WarehouseId, Vessel + ' ' + Voyage AS Vessel, Eta, Pol, CrAgentId, OblNo AS Obl, CONVERT(varchar(20),Qty) + ' ' + PackageType AS Pkgs, UserId, Remark,ExRate
//FROM SeaImportRef WHERE (RefNo = '{0}')", refNo);

//        DataTable tabMast = ConnectSql.GetTab(sql_mast);
//        if (tabMast.Rows.Count > 0)
//        {
//            string jobType = tabMast.Rows[0]["JobType"].ToString();
//            int totQty = 0;
//            decimal totM3 = 0;
//            decimal totWt = 0;

//            string sql_det = string.Format(@"SELECT imp.JobNo AS ImpNo, imp.HblNo,Imp.CustomerId, imp.Consignee, imp.TsPod AS Pod, mkg.Qty AS Pkgs, mkg.PackageType AS PkgType, mkg.Weight AS Wt, mkg.Volume AS M3, 
//                     mkg.ContainerNo, mkg.Marking, mkg.Description, imp.TsInd , imp.FrCollectInd AS FC, imp.TsAgentId AS TAGT, imp.TsEta AS TETA, imp.TsEtd AS TETD, imp.TsVessel AS TVES, 
//                      imp.TsVoyage AS TVOY, imp.CollectAmount AS cltamt, imp.TsBkgNo AS EXPBKGN,imp.flagNomination
//FROM         SeaImportMkg AS mkg INNER JOIN SeaImport AS imp ON mkg.JobNo = imp.JobNo
//WHERE (imp.RefNo = '{0}') ", refNo);
//            if (jobType == "FCL")
//                sql_det += " and MkgType='Cont' ORDER BY mkg.JobNo";
//            else
//                sql_det += " and MkgType='Bl' ORDER BY mkg.JobNo";

//            DataTable tabDet = ConnectSql.GetTab(sql_det);
//            for (int t = 0; t < tabDet.Rows.Count; t++)
//            {
//                DataRow rowDet = tabDet.Rows[t];
//                DataRow rptRowDet = det.NewRow();
//                rptRowDet["RefNo"] = refNo;
//                rptRowDet["HblN"] = SafeValue.SafeString(rowDet["HblNo"], "");
//                rptRowDet["ImpNo"] = rowDet["ImpNo"].ToString();
//                //pageInt++;
//                rptRowDet["CustName"] = EzshipHelper.GetPartyName(rowDet["CustomerId"]);
//                rptRowDet["Pod"] = EzshipHelper.GetPortName(rowDet["Pod"]);
//                rptRowDet["ContNo"] = SafeValue.SafeString(rowDet["ContainerNo"], "");

//                rptRowDet["Qty"] = SafeValue.SafeString(rowDet["Pkgs"], "");
//                totQty += SafeValue.SafeInt(rowDet["Pkgs"], 0);
//                rptRowDet["Pkg"] = SafeValue.SafeString(rowDet["Pkgtype"], "");

//                rptRowDet["Wt"] = SafeValue.SafeString(rowDet["Wt"], "").Trim();
//                totWt += SafeValue.SafeDecimal(rowDet["Wt"], 0);
//                rptRowDet["M3"] = SafeValue.SafeString(rowDet["M3"], "").Trim();
//                totM3 += SafeValue.SafeDecimal(rowDet["M3"], 0);
//                rptRowDet["Cng"] = SafeValue.SafeString(rowDet["CONSIGNEE"], "");

//                rptRowDet["Mkg"] = SafeValue.SafeString(rowDet["Marking"], "");
//                rptRowDet["Des"] = SafeValue.SafeString(rowDet["Description"], "");

//                rptRowDet["Nomind"] = rowDet["flagNomination"];
//                string tInd = SafeValue.SafeString(rowDet["TsInd"], "N");
//                rptRowDet["Fc"] = SafeValue.SafeString(rowDet["FC"], "");
//                if (tInd == "Y")
//                {
//                    rptRowDet["TEtd"] = SafeValue.SafeDateStr(rowDet["TETD"]);
//                    rptRowDet["TEta"] = SafeValue.SafeDateStr(rowDet["TETA"]);
//                }
//                else
//                {
//                    rptRowDet["TEtd"] = "----";
//                    rptRowDet["TEta"] = "----";
//                }
//                rptRowDet["TVes"] = SafeValue.SafeString(rowDet["TVES"], "") + "/" + SafeValue.SafeString(rowDet["TVOY"], "");
//                rptRowDet["CltAmt"] = SafeValue.SafeDecimal(rowDet["CLTAMT"], 0).ToString("0.00");
//                rptRowDet["ExpBkgN"] = SafeValue.SafeString(rowDet["EXPBKGN"], "");
//                string tAgtId = SafeValue.SafeString(rowDet["TAGT"], "");
//                if (tAgtId == "NA")
//                    rptRowDet["Agt"] = tAgtId;
//                else
//                {
//                    DataTable tab_TAgt = ConnectSql.GetTab("select name from XXParty where PartyId='" + tAgtId + "'");
//                    if (tab_TAgt.Rows.Count > 0)
//                    {
//                        rptRowDet["Agt"] = tab_TAgt.Rows[0][0];
//                    }
//                }

//                det.Rows.Add(rptRowDet);

//            }



//            DataRow row = tabMast.Rows[0];
//            DataRow rptRowMast = mast.NewRow();

//            rptRowMast["RefNo"] = refNo;
//            rptRowMast["Ves"] = SafeValue.SafeString(row["Vessel"], "");
//            rptRowMast["Carrier"] = EzshipHelper.GetPartyName(row["CrAgentId"]);
//            rptRowMast["Obl"] = SafeValue.SafeString(row["Obl"], "");


//            rptRowMast["Pol"] = EzshipHelper.GetPortName(row["Pol"]);
//            rptRowMast["Eta"] = SafeValue.SafeString(row["ETA"], "");
//            rptRowMast["ExRate"] = SafeValue.SafeString(row["ExRate"], "");
//            string sql_cont = string.Format(@"select ContainerNo + '/' + SealNo + '/' + ContainerType as container from SeaImportMkg where RefNo='{0}' and MkgType='Cont'", refNo);
//            DataTable cont_tab = ConnectSql.GetTab(sql_cont);
//            for (int i = 0; i < cont_tab.Rows.Count; i++)
//            {
//                if (i == 0)
//                    rptRowMast["ContNo"] = SafeValue.SafeString(cont_tab.Rows[i][0], "");
//                else
//                    rptRowMast["ContNo"] += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0], "");
//            }


//            string agtId = SafeValue.SafeString(row["AgentId"], "");
//            if (agtId == "NA")
//                rptRowMast["Agt"] = agtId;
//            else
//            {
//                rptRowMast["Agt"] = EzshipHelper.GetPartyName(agtId);
//            }

//            string nvoAgtId = SafeValue.SafeString(row["NvoccAgentId"], "");
//            rptRowMast["NvoAgt"] = EzshipHelper.GetPartyName(nvoAgtId);

//            rptRowMast["TotWt"] = totWt.ToString("#,##0.000");
//            rptRowMast["TotM3"] = totM3.ToString("#,##0.000");
//            rptRowMast["TotQty"] = totQty;


//            mast.Rows.Add(rptRowMast);
//        }
//        DataSet set = new DataSet();
//        set.Tables.Add(mast);
//        set.Tables.Add(det);
//        set.Relations.Add("Rela", mast.Columns["RefNo"], det.Columns["RefNo"]);
//        return set;
    }
    public static DataTable PrintImpPermit(string refN)
    {
        DataTable dt = new DataTable();
        try
        {
            string strsql = string.Format(@"exec proc_PrintImpPermit '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            dt = ds_temp.Tables[0].Copy();
            dt.TableName = "Permit";
        }
        catch (Exception ex) { }
        return dt;


//        string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaImportMkg WHERE RefNo='{0}' and MkgType='Cont'", refN);
//        string contN = "";
//        DataTable cont_tab = ConnectSql.GetTab(sql_cont);
//        for (int i = 0; i < cont_tab.Rows.Count; i++)
//        {
//            if (i == 0)
//                contN += SafeValue.SafeString(cont_tab.Rows[i][0]);
//            else
//                contN += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0]);
//        }

//        string sql = @"SELECT RefNo AS RefN, OblNo AS Bl,JobType, Vessel + '/' + Voyage AS Ves, POL, Pod, Eta AS ET, CrAgentID AS Vend1, HaulierName AS Ms, 
//                      HaulierCrNo AS Cr, CONVERT(varchar(20), Weight) + 'Kgs' + '/' + CONVERT(varchar(20), Volume) + 'M3' AS Wt, CONVERT(varchar(20), Qty) + ' ' + PackageType AS Pack, 
//                      UserID AS Us FROM  SeaImportRef WHERE (RefNo = '" + refN + "')";

//        DataTable tab1 = ConnectSql.GetTab(sql);
//        DataTable Mast = new DataTable("Permit");

//        Mast.Columns.Add("NowD");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("Ves");
//        Mast.Columns.Add("Bl");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("Vend1");
//        Mast.Columns.Add("Wt");
//        Mast.Columns.Add("Pack");
//        Mast.Columns.Add("Us");
//        Mast.Columns.Add("Ms");
//        Mast.Columns.Add("Cr");
//        Mast.Columns.Add("CONTN");
//        Mast.Columns.Add("CompanyName");
//        Mast.Columns.Add("NoDo");
//        try
//        {
//            DataRow rptRow = Mast.NewRow();
//            for (int i = 0; i < Mast.Columns.Count; i++)
//            {
//                string colName = Mast.Columns[i].ToString();
//                if (colName == "Vend1")
//                {
//                    string crAgtId = SafeValue.SafeString(tab1.Rows[0]["Vend1"], "0");
//                    string sql_vendor = "select PartyId, Code, Name,  Address, Tel1,Fax1 from XXParty where PartyId='" + crAgtId + "'";
//                    DataTable tab_vendor = C2.Manager.ORManager.GetDataSet(sql_vendor).Tables[0];
//                    if (tab_vendor.Rows.Count > 0)
//                        rptRow["Vend1"] = tab_vendor.Rows[0]["Name"] + "\n" + tab_vendor.Rows[0]["Address"];
//                    else
//                        rptRow["Vend1"] = "";
//                }
//                else if (colName == "NowD")
//                {
//                    rptRow["NowD"] = DateTime.Today.ToString("dd/MM/yyyy");
//                }
//                else if (colName == "POL")
//                {
//                    rptRow[colName] = EzshipHelper.GetPortName(tab1.Rows[0]["Pol"]);
//                    rptRow[colName] += " " +EzshipHelper.GetPortName(tab1.Rows[0]["Pod"]);
//                }
//                else if (colName == "CONTN")
//                    rptRow[colName] = contN;
//                else if (colName == "NoDo")
//                {
//            string sql_bl = string.Format("select count(sequenceId) from SeaImport where RefNo='{0}'",refN);
//                    rptRow[colName] = C2.Manager.ORManager.ExecuteScalar(sql_bl); ;
//                }
//                else if (colName == "CompanyName")
//                    rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                else
//                    rptRow[colName] = tab1.Rows[0][colName];
//            }
//            //if (SafeValue.SafeString(tab1.Rows[0]["JobType"]) == "FCL")
//            //    sql_bl += " and MkgType='Cont'";
//            //else
//            //    sql_bl += " and MkgType='BL'";
//            //rptRow["NoDo"] = C2.Manager.ORManager.ExecuteScalar(sql_bl);
//            Mast.Rows.Add(rptRow);

//        }
//        catch (Exception ex)
//        {
//        }

//        return Mast;
    }
    //
    public static DataTable PrintImpPermitNvocc(string refN)
    {
        DataTable dt = new DataTable();
        try
        {
            string strsql = string.Format(@"exec proc_PrintImpPermitNvocc '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            dt = ds_temp.Tables[0].Copy();
            dt.TableName = "Permit";
        }
        catch (Exception ex) { }
        return dt;

//        string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaImportMkg WHERE RefNo='{0}' and MkgType='Cont'", refN);
//        string contN = "";
//        DataTable cont_tab = ConnectSql.GetTab(sql_cont);
//        for (int i = 0; i < cont_tab.Rows.Count; i++)
//        {
//            if (i == 0)
//                contN += SafeValue.SafeString(cont_tab.Rows[i][0]);
//            else
//                contN += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0]);
//        }

//        string sql = @"SELECT RefNo AS RefN, OblNo AS Bl,JobType, Vessel + '/' + Voyage AS Ves, POL, Pod, Eta AS ET, NvoccAgentID AS Vend1, HaulierName AS Ms, 
//                      HaulierCrNo AS Cr, CONVERT(varchar(20), Weight) + 'Kgs' + '/' + CONVERT(varchar(20), Volume) + 'M3' AS Wt, CONVERT(varchar(20), Qty) + ' ' + PackageType AS Pack, 
//                      UserID AS Us FROM  SeaImportRef WHERE (RefNo = '" + refN + "')";

//        DataTable tab1 = ConnectSql.GetTab(sql);
//        DataTable Mast = new DataTable("Permit");

//        Mast.Columns.Add("NowD");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("Ves");
//        Mast.Columns.Add("Bl");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("Vend1");
//        Mast.Columns.Add("Wt");
//        Mast.Columns.Add("Pack");
//        Mast.Columns.Add("Us");
//        Mast.Columns.Add("Ms");
//        Mast.Columns.Add("Cr");
//        Mast.Columns.Add("CONTN");
//        Mast.Columns.Add("CompanyName");
//        Mast.Columns.Add("NoDo");
//        try
//        {
//            DataRow rptRow = Mast.NewRow();
//            for (int i = 0; i < Mast.Columns.Count; i++)
//            {
//                string colName = Mast.Columns[i].ToString();
//                if (colName == "Vend1")
//                {
//                    string crAgtId = SafeValue.SafeString(tab1.Rows[0]["Vend1"], "0");
//                    string sql_vendor = "select PartyId, Code, Name,  Address, Tel1,Fax1 from XXParty where PartyId='" + crAgtId + "'";
//                    DataTable tab_vendor = C2.Manager.ORManager.GetDataSet(sql_vendor).Tables[0];
//                    if (tab_vendor.Rows.Count > 0)
//                        rptRow["Vend1"] = tab_vendor.Rows[0]["Name"] + "\n" + tab_vendor.Rows[0]["Address"];
//                    else
//                        rptRow["Vend1"] = "";
//                }
//                else if (colName == "NowD")
//                {
//                    rptRow["NowD"] = DateTime.Today.ToString("dd/MM/yyyy");
//                }
//                else if (colName == "POL")
//                {
//                    rptRow[colName] = EzshipHelper.GetPortName(tab1.Rows[0]["Pol"]);
//                    rptRow[colName] += " " + EzshipHelper.GetPortName(tab1.Rows[0]["Pod"]);
//                }
//                else if (colName == "CONTN")
//                    rptRow[colName] = contN;
//                else if (colName == "NoDo")
//                {
//                    string sql_bl = string.Format("select count(sequenceId) from SeaImport where RefNo='{0}'", refN);
//                    rptRow[colName] = C2.Manager.ORManager.ExecuteScalar(sql_bl); ;
//                }
//                else if (colName == "CompanyName")
//                    rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                else
//                    rptRow[colName] = tab1.Rows[0][colName];
//            }
//            //if (SafeValue.SafeString(tab1.Rows[0]["JobType"]) == "FCL")
//            //    sql_bl += " and MkgType='Cont'";
//            //else
//            //    sql_bl += " and MkgType='BL'";
//            //rptRow["NoDo"] = C2.Manager.ORManager.ExecuteScalar(sql_bl);
//            Mast.Rows.Add(rptRow);

//        }
//        catch (Exception ex)
//        {
//        }

//        return Mast;
    }
    public static DataTable PrintAuth(string refN)
    {
        DataTable dt = new DataTable();
        try
        {
            string strsql = string.Format(@"exec proc_PrintAuth '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            dt = ds_temp.Tables[0].Copy();
            dt.TableName = "Permit";
        }
        catch (Exception ex) { }
        return dt;

//        string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaImportMkg WHERE RefNo='{0}' and MkgType='Cont'", refN);
//        string contN = "";
//        DataTable cont_tab =ConnectSql.GetTab(sql_cont);
//        for (int i = 0; i < cont_tab.Rows.Count; i++)
//        {
//            if (i == 0)
//                contN += SafeValue.SafeString(cont_tab.Rows[i][0]);
//            else
//                contN += "\n\r" + SafeValue.SafeString(cont_tab.Rows[i][0]);
//        }

//        string sql = @"  SELECT  RefNo AS RefN,OblNo AS Bl, Vessel + '/' + Voyage AS Ves, POL, Eta AS ET, HaulierName AS Ms, HaulierCrNo AS Cr, '' AS nodo, 
//                      CONVERT(varchar(20), Weight) + 'Kgs' + '/' + CONVERT(varchar(20), Volume) + 'M3' AS Wt, CONVERT(varchar(20), Qty) + ' ' + PackageType AS Pack, UserID AS Us, 
//                      CrAgentID AS Vend1,'" + contN + @"' as contn  
//    FROM SeaImportRef WHERE (RefNo = '" + refN + "')";

//        DataTable tab1 = ConnectSql.GetTab(sql);
//        DataTable Mast =  new DataTable("Permit");
//        Mast.Columns.Add("NowD");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("Ves");
//        Mast.Columns.Add("Bl");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("Vend1");
//        Mast.Columns.Add("Wt");
//        Mast.Columns.Add("Pack");
//        Mast.Columns.Add("Us");
//        Mast.Columns.Add("Ms");
//        Mast.Columns.Add("Cr");
//        Mast.Columns.Add("NoDo");
//        Mast.Columns.Add("CONTN");
//        Mast.Columns.Add("CompanyName");

//        try
//        {
//            DataRow rptRow = Mast.NewRow();
//            for (int i = 0; i < Mast.Columns.Count; i++)
//            {
//                string colName = Mast.Columns[i].ToString();
//                if (colName == "Vend1")
//                {
//                    string crAgtId = SafeValue.SafeString(tab1.Rows[0]["Vend1"], "0");
//                    string sql_vendor = "select PartyId, Code, Name,  Address, Tel1,Fax1 from XXParty where PartyId='" + crAgtId + "'";
//                    DataTable tab_vendor = C2.Manager.ORManager.GetDataSet(sql_vendor).Tables[0];
//                    if (tab_vendor.Rows.Count > 0)
//                        rptRow["Vend1"] = tab_vendor.Rows[0]["Name"] + "\n" + tab_vendor.Rows[0]["Address"];
//                    else
//                        rptRow["Vend1"] = "";
//                }
//                else if (colName == "NowD")
//                {
//                    rptRow["NowD"] = DateTime.Today.ToString("dd/MM/yyyy");
//                }
//                else if (colName == "POL")
//                {
//                    rptRow["Pol"]= ConnectSql.ExecuteScalar("select Name from xxport where code='"+tab1.Rows[0]["Pol"]+"'");
//                }
//                else if (colName == "ET")
//                {
//                    rptRow["ET"] = string.Format("{0:dd/MM/yyyy}", tab1.Rows[0][colName]);
//                }
//                else if (colName == "CompanyName")
//                {
//                    rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                }
//                else
//                    rptRow[colName] = tab1.Rows[0][colName];
//            }
//            Mast.Rows.Add(rptRow);
//        }
//        catch
//        {

//        }

//        return Mast;
    }
    public static DataTable PrintAuthNvocc(string refN)
    {
        DataTable dt = new DataTable();
        try
        {
            string strsql = string.Format(@"exec proc_PrintAuthNvocc '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            dt = ds_temp.Tables[0].Copy();
            dt.TableName = "Permit";
        }
        catch (Exception ex) { }
        return dt;


//        string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaImportMkg WHERE RefNo='{0}' and MkgType='Cont'", refN);
//        string contN = "";
//        DataTable cont_tab =ConnectSql.GetTab(sql_cont);
//        for (int i = 0; i < cont_tab.Rows.Count; i++)
//        {
//            if (i == 0)
//                contN += SafeValue.SafeString(cont_tab.Rows[i][0]);
//            else
//                contN += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0]);
//        }

//        string sql = @"  SELECT  RefNo AS RefN, NvoccBl AS Bl, Vessel + '/' + Voyage AS Ves, POL, Eta AS ET, HaulierName AS Ms, HaulierCrNo AS Cr, '' AS nodo, 
//                      CONVERT(varchar(20), Weight) + 'Kgs' + '/' + CONVERT(varchar(20), Volume) + 'M3' AS Wt, CONVERT(varchar(20), Qty) + ' ' + PackageType AS Pack, UserID AS Us, 
//                      NvoccAgentID AS Vend1,'" + contN + @"' as contn  
//    FROM SeaImportRef WHERE (RefNo = '" + refN + "')";

//        DataTable tab1 = ConnectSql.GetTab(sql);
//        DataTable Mast = new DataTable("Permit");
//        Mast.Columns.Add("NowD");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("Ves");
//        Mast.Columns.Add("Bl");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("Vend1");
//        Mast.Columns.Add("Wt");
//        Mast.Columns.Add("Pack");
//        Mast.Columns.Add("Us");
//        Mast.Columns.Add("Ms");
//        Mast.Columns.Add("Cr");
//        Mast.Columns.Add("NoDo");
//        Mast.Columns.Add("CONTN");
//        Mast.Columns.Add("CompanyName");

//        try
//        {
//            DataRow rptRow = Mast.NewRow();
//            for (int i = 0; i < Mast.Columns.Count; i++)
//            {
//                string colName = Mast.Columns[i].ToString();
//                if (colName == "Vend1")
//                {
//                    string crAgtId = SafeValue.SafeString(tab1.Rows[0]["Vend1"], "0");
//                    string sql_vendor = "select PartyId, Code, Name,  Address, Tel1,Fax1 from XXParty where PartyId='" + crAgtId + "'";
//                    DataTable tab_vendor = C2.Manager.ORManager.GetDataSet(sql_vendor).Tables[0];
//                    if (tab_vendor.Rows.Count > 0)
//                        rptRow["Vend1"] = tab_vendor.Rows[0]["Name"] + "\n" + tab_vendor.Rows[0]["Address"];
//                    else
//                        rptRow["Vend1"] = "";
//                }
//                else if (colName == "NowD")
//                {
//                    rptRow["NowD"] = DateTime.Today.ToString("dd/MM/yyyy");
//                }
//                else if (colName == "POL")
//                {
//                    rptRow["Pol"] = ConnectSql.ExecuteScalar("select Name from xxport where code='" + tab1.Rows[0]["Pol"] + "'");
//                }
//                else if (colName == "ET")
//                {
//                    rptRow["ET"] = string.Format("{0:dd/MM/yyyy}", tab1.Rows[0][colName]);
//                }
//                else if (colName == "CompanyName")
//                {
//                    rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                }
//                else
//                    rptRow[colName] = tab1.Rows[0][colName];
//            }
//            Mast.Rows.Add(rptRow);
//        }
//        catch
//        {

//        }

//        return Mast;
    }
    public static DataSet PrintPermitList(string refN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintPermitList '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;

//        string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaImportMkg WHERE RefNo='{0}' and MkgType='Cont'", refN);
//        string contN = "";
//        DataTable cont_tab = ConnectSql.GetTab(sql_cont);
//        for (int i = 0; i < cont_tab.Rows.Count; i++)
//        {
//            if (i == 0)
//                contN += SafeValue.SafeString(cont_tab.Rows[i][0]);
//            else
//                contN += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0]);
//        }


//        string sql1 = @"SELECT RefNo AS RefN, OblNo AS Bl, Vessel + '/' + Voyage AS Ves, POL, Pod, Eta AS ET, UserId AS Us, CrAgentID as Vend1
//FROM  SeaImportRef WHERE (RefNo= '" + refN + "')";

//        string sql2 = @"SELECT JobNo AS ImportN, RefNo AS RefN, HblNo AS Hbl, Volume AS M3, Weight AS Wt, CONVERT(varchar(20), Qty) + '/' + PackageType AS Pack, 
//                      PermitRmk AS PermitN FROM SeaImport WHERE (RefNo= '" + refN + "')";

//        DataTable tab1 = ConnectSql.GetTab(sql1);
//        DataTable tab2 = ConnectSql.GetTab(sql2);
//        DataTable Mast = new DataTable("Mast");
//        Mast.Columns.Add("NowD");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("Ves");
//        Mast.Columns.Add("Bl");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("Us");
//        Mast.Columns.Add("Vend1");
//        Mast.Columns.Add("CONTN");
//        Mast.Columns.Add("CompanyName");
//        DataTable Detail = new DataTable("Detail");
//        Detail.Columns.Add("ImportN");
//        Detail.Columns.Add("RefN");
//        Detail.Columns.Add("Hbl");
//        Detail.Columns.Add("M3");
//        Detail.Columns.Add("Wt");
//        Detail.Columns.Add("Pack");
//        //Detail.Columns.Add("gd1");
//        //Detail.Columns.Add("gd2");
//        Detail.Columns.Add("PermitN");
//        try
//        {
//            DataRow rptRow = Mast.NewRow();
//            for (int i = 0; i < Mast.Columns.Count; i++)
//            {
//                string colName = Mast.Columns[i].ToString();
//                if (colName == "CONTN")
//                    rptRow[colName] = contN;
//                else if (colName == "NowD")
//                    rptRow[colName] = DateTime.Today.ToString("dd/MM/yyyy");
//                else if (colName == "POL")
//                {
//                    rptRow[colName] = SafeValue.SafeString(tab1.Rows[0]["Pol"], "") + SafeValue.SafeString(tab1.Rows[0]["Pod"], "");
//                }
//                else if (colName == "Vend1")
//                {
//                    string crAgtId = SafeValue.SafeString(tab1.Rows[0]["Vend1"], "0");
//                    string sql_vendor = "select PartyId, Code, Name,  Address, Tel1,Fax1 from XXparty where PartyId='" + crAgtId + "'";
//                    DataTable tab_vendor = C2.Manager.ORManager.GetDataSet(sql_vendor).Tables[0];
//                    if (tab_vendor.Rows.Count > 0)
//                        rptRow["Vend1"] = tab_vendor.Rows[0]["Name"] + "\n" + tab_vendor.Rows[0]["Address"];
//                    else
//                        rptRow["Vend1"] = "";
//                }
//                else if (colName == "ET")
//                    rptRow[colName] = SafeValue.SafeDate(tab1.Rows[0]["ET"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
//                else if (colName == "CompanyName")
//                    rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                else
//                    rptRow[colName] = tab1.Rows[0][colName];
//            }
//            Mast.Rows.Add(rptRow);

//            for (int j = 0; j < tab2.Rows.Count; j++)
//            {
//                DataRow detailRow = Detail.NewRow();
//                for (int i = 0; i < Detail.Columns.Count; i++)
//                {
//                    detailRow[Detail.Columns[i].ToString()] = tab2.Rows[j][Detail.Columns[i].ToString()];
//                }
//                Detail.Rows.Add(detailRow);
//            }
//        }
//        catch (Exception ex)
//        {
//        }
//        DataSet ds = new DataSet();
//        ds.Tables.Add(Mast);
//        ds.Tables.Add(Detail);
//        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
//        ds.Relations.Add(r);

//        return ds;
    }
    public static DataSet PrintPermitListNvocc(string refN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintPermitListNvocc '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;


//        string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaImportMkg WHERE RefNo='{0}' and MkgType='Cont'", refN);
//        string contN = "";
//        DataTable cont_tab = ConnectSql.GetTab(sql_cont);
//        for (int i = 0; i < cont_tab.Rows.Count; i++)
//        {
//            if (i == 0)
//                contN += SafeValue.SafeString(cont_tab.Rows[i][0]);
//            else
//                contN += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0]);
//        }


//        string sql1 = @"SELECT RefNo AS RefN,  NvoccBl AS Bl, Vessel + '/' + Voyage AS Ves, POL, Pod, Eta AS ET, UserId AS Us, NvoccAgentID as Vend1
//FROM  SeaImportRef WHERE (RefNo= '" + refN + "')";

//        string sql2 = @"SELECT JobNo AS ImportN, RefNo AS RefN, HblNo AS Hbl, Volume AS M3, Weight AS Wt, CONVERT(varchar(20), Qty) + '/' + PackageType AS Pack , 
//                      PermitRmk AS PermitN FROM SeaImport WHERE (RefNo= '" + refN + "')";

//        DataTable tab1 = ConnectSql.GetTab(sql1);
//        DataTable tab2 = ConnectSql.GetTab(sql2);
//        DataTable Mast = new DataTable("Mast");
//        Mast.Columns.Add("NowD");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("Ves");
//        Mast.Columns.Add("Bl");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("Us");
//        Mast.Columns.Add("Vend1");
//        Mast.Columns.Add("CONTN");
//        Mast.Columns.Add("CompanyName");
//        DataTable Detail = new DataTable("Detail");
//        Detail.Columns.Add("ImportN");
//        Detail.Columns.Add("RefN");
//        Detail.Columns.Add("Hbl");
//        Detail.Columns.Add("M3");
//        Detail.Columns.Add("Wt");
//        Detail.Columns.Add("Pack");
//        //Detail.Columns.Add("gd1");
//        //Detail.Columns.Add("gd2");
//        Detail.Columns.Add("PermitN");
//        try
//        {
//            DataRow rptRow = Mast.NewRow();
//            for (int i = 0; i < Mast.Columns.Count; i++)
//            {
//                string colName = Mast.Columns[i].ToString();
//                if (colName == "CONTN")
//                    rptRow[colName] = contN;
//                else if (colName == "NowD")
//                    rptRow[colName] = DateTime.Today.ToString("dd/MM/yyyy");
//                else if (colName == "POL")
//                {
//                    rptRow[colName] = SafeValue.SafeString(tab1.Rows[0]["Pol"], "") + SafeValue.SafeString(tab1.Rows[0]["Pod"], "");
//                }
//                else if (colName == "Vend1")
//                {
//                    string crAgtId = SafeValue.SafeString(tab1.Rows[0]["Vend1"], "0");
//                    string sql_vendor = "select PartyId, Code, Name,  Address, Tel1,Fax1 from XXParty where PartyId='" + crAgtId + "'";
//                    DataTable tab_vendor = C2.Manager.ORManager.GetDataSet(sql_vendor).Tables[0];
//                    if (tab_vendor.Rows.Count > 0)
//                        rptRow["Vend1"] = tab_vendor.Rows[0]["Name"] + "\n" + tab_vendor.Rows[0]["Address"];
//                    else
//                        rptRow["Vend1"] = "";
//                }
//                else if (colName == "ET")
//                    rptRow[colName] = SafeValue.SafeDate(tab1.Rows[0]["ET"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
//                else if (colName == "CompanyName")
//                    rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                else
//                    rptRow[colName] = tab1.Rows[0][colName];
//            }
//            Mast.Rows.Add(rptRow);

//            for (int j = 0; j < tab2.Rows.Count; j++)
//            {
//                DataRow detailRow = Detail.NewRow();
//                for (int i = 0; i < Detail.Columns.Count; i++)
//                {
//                    detailRow[Detail.Columns[i].ToString()] = tab2.Rows[j][Detail.Columns[i].ToString()];
//                }
//                Detail.Rows.Add(detailRow);
//            }
//        }
//        catch (Exception ex)
//        {
//        }
//        DataSet ds = new DataSet();
//        ds.Tables.Add(Mast);
//        ds.Tables.Add(Detail);
//        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
//        ds.Relations.Add(r);

//        return ds;
    }

    public static DataSet PrintTrans(string refN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintTrans '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", "");
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;


//        string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaImportMkg WHERE RefNo='{0}' and MkgType='Cont'", refN);
//        string contN = "";
//        DataTable cont_tab = ConnectSql.GetTab(sql_cont);
//        for (int i = 0; i < cont_tab.Rows.Count; i++)
//        {
//            if (i == 0)
//                contN += SafeValue.SafeString(cont_tab.Rows[i][0]);
//            else
//                contN += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0]);
//        }

//        string sql1 = @"select 'I' + '/' + RefNo AS RefN, Vessel + '/' + Voyage AS Ves, AgentID AS Agent, Eta AS ET, POL, OblNo AS OceanBL, 
//                      WarehouseID AS Warehouse FROM SeaImportRef WHERE (RefNo= '" + refN + "')";

//        string sql2 = @"SELECT  'I' + '/' + RefNo AS RefN,HblNo AS Hbl, TsEta AS Eta, TsEtd AS Etd, TsPortFinName, TsAgentId AS Agent, TsPod AS Pod, FrCollectInd AS frInd, '' AS framt, TsVessel + '-' + TsVoyage AS Ves, 
//                      Qty AS Pack, convert(nvarchar(50),Qty) + PackageType AS Pkgs, Weight AS Kgs, Volume AS Cbm, JobNo AS HouseNo, CollectAmount, CollectCurrency,TsRemark as Cargo1
//FROM         SeaImport";
//        sql2 += " WHERE (RefNo= '" + refN + "') AND (TsInd = 'Y') ORDER BY JobNo";

//        DataTable tab1 = ConnectSql.GetTab(sql1);
//        DataTable tab2 = ConnectSql.GetTab(sql2);

//        DataTable Mast = new DataTable();
//        Mast.TableName = "Mast";
//        Mast.Columns.Add("NowD");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("Ves");
//        Mast.Columns.Add("Agent");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("OceanBL");
//        Mast.Columns.Add("Warehouse");
//        Mast.Columns.Add("CONTN");     

//        DataTable Detail = new DataTable();
//        Detail.TableName = "Detail";
//        Detail.Columns.Add("RefN");
//        Detail.Columns.Add("Hbl");
//        Detail.Columns.Add("Pkgs");
//        Detail.Columns.Add("Pack");
//        Detail.Columns.Add("Cargo1");
//        Detail.Columns.Add("Kgs");
//        Detail.Columns.Add("Cbm");
//        Detail.Columns.Add("Pod");
//        Detail.Columns.Add("Ves");
//        Detail.Columns.Add("EtaEtd");
//        Detail.Columns.Add("FrInd");
//        Detail.Columns.Add("FrAmt");
//        Detail.Columns.Add("Agent");

//        try
//        {
//            DataRow rptRow = Mast.NewRow();
//            for (int i = 0; i < Mast.Columns.Count; i++)
//            {
//                string colName = Mast.Columns[i].ToString();
//                if (colName == "NowD")
//                    rptRow["NowD"] = DateTime.Today.ToString("dd/MM/yyyy");
//                else if (colName == "ET")
//                {
//                    rptRow["ET"] = SafeValue.SafeDate(tab1.Rows[0]["Et"], DateTime.Today).ToString("dd/MM/yyyy");
//                }
//                else if (colName == "Agent")
//                {
//                    string sql_vendor = "select Name from XXParty where PartyId='" + tab1.Rows[0]["Agent"] + "'";
//                    rptRow["Agent"] = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_vendor), "");
//                }
//                else if (colName == "Warehouse")
//                {
//                    string sql_vendor = "select Name from XXParty where PartyId='" + tab1.Rows[0]["Warehouse"] + "'";
//                    rptRow["Warehouse"] = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_vendor), "");
//                }
//                else if (colName == "CONTN")
//                {
//                    rptRow["CONTN"] = contN;
//                }
//                else
//                    rptRow[colName] = tab1.Rows[0][colName];
//            }

//            Mast.Rows.Add(rptRow);

//            for (int j = 0; j < tab2.Rows.Count; j++)
//            {
//                DataRow detailRow = Detail.NewRow();
//                for (int i = 0; i < Detail.Columns.Count; i++)
//                {
//                    string colName = Detail.Columns[i].ToString();
//                    try
//                    {
//                        if (colName == "EtaEtd")
//                        {
//                            string eta = SafeValue.SafeDateStr(tab2.Rows[j]["Eta"]);
//                            string etd = SafeValue.SafeDateStr(tab2.Rows[j]["Etd"]);
//                            detailRow["EtaEtd"] = eta + "/" + etd;
//                        }
//                        else if (colName == "FrInd")
//                        {
//                            string frInd = SafeValue.SafeString(tab2.Rows[j]["FrInd"], "");
//                            detailRow["FrInd"] = frInd;
//                            if (frInd == "Y")
//                            {
//                                detailRow["FrAmt"] = SafeValue.SafeString(tab2.Rows[j]["CollectCurrency"], "") + SafeValue.SafeDecimal(tab2.Rows[j]["CollectAmount"], 0).ToString("0.00");
//                            }
//                        }
//                        else if (colName == "Agent")
//                        {
//                            detailRow["Agent"] = EzshipHelper.GetPartyName(tab2.Rows[j]["Agent"]);
//                        }
//                        else if (colName == "Pod")
//                        {
//                            detailRow["Pod"] = EzshipHelper.GetPortName(tab2.Rows[j]["Pod"]);
//                        }
//                        else
//                            detailRow[Detail.Columns[i].ToString()] = tab2.Rows[j][Detail.Columns[i].ToString()];
//                    }
//                    catch
//                    {
//                    }
//                }
//                Detail.Rows.Add(detailRow);
//            }
//        }
//        catch
//        {
//        }
//        DataSet ds = new DataSet();
//        ds.Tables.Add(Mast);
//        ds.Tables.Add(Detail);
//        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
//        ds.Relations.Add(r);

//        return ds;
    }
    public static DataTable PrintHaulier_Import(string refN, string jobNo, string jobType)
    {
        DataTable dt = new DataTable();
        try
        {
            string strsql = string.Format(@"exec proc_PrintHaulier_Import '{0}','{1}','{2}','{3}','{4}'", refN, jobNo, jobType, "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            dt = ds_temp.Tables[0].Copy();
        }
        catch (Exception ex) { }
        return dt;


//        string sql1 = string.Format(@"  SELECT HaulierName as Haulier,HaulierRemark AS R1, HaulierCollectDate AS ColD,HaulierTruck 
//                     , HaulierCollect, HaulierAttention AS Attn, HaulierCrNo AS CrNote,  
//                      RefNo AS RefN, Eta AS ET, Etd AS ED, Pod , Pol, Vessel + '/' + Voyage AS Ves, CrBkgNo AS Bkg, OblNo AS Bl, UserID AS Us, 
//                      CrAgentID AS Agent FROM SeaImportRef WHERE (RefNo= '{0}')", refN);
//        DataTable tab1 = ConnectSql.GetTab(sql1);
//        DataTable tab = new DataTable();
//        tab.Columns.Add("R1");
//        tab.Columns.Add("ColD");
//        tab.Columns.Add("JobType");
//        tab.Columns.Add("ContN");
//        tab.Columns.Add("Wt");
//        tab.Columns.Add("M3");
//        tab.Columns.Add("Pack");
//        tab.Columns.Add("HaulierTruck");
//        tab.Columns.Add("HaulierCollect"); ;
//        tab.Columns.Add("Attn");
//        tab.Columns.Add("CrNote");
//        tab.Columns.Add("Haulier");
//        tab.Columns.Add("RefN");
//        tab.Columns.Add("ET");
//        tab.Columns.Add("ED");
//        tab.Columns.Add("POD");
//        tab.Columns.Add("POL");
//        tab.Columns.Add("Ves");
//        tab.Columns.Add("Bkg");
//        tab.Columns.Add("Bl");
//        tab.Columns.Add("Agent");
//        tab.Columns.Add("Us");
//        tab.Columns.Add("CompanyName");
//        try
//        {
//            DataRow rptRow = tab.NewRow();
//            rptRow["R1"] = tab1.Rows[0]["R1"];

//            DateTime d = SafeValue.SafeDate(tab1.Rows[0]["ColD"], DateTime.Today);
//            if (d > new DateTime(2000, 1, 1))
//                rptRow["ColD"] = SafeValue.SafeDateStr(d);

//                rptRow["JobType"] = "EXPORT";

//            string sql_bkg = string.Format("select sum(Weight) Weight, sum(Volume) Volume, sum(Qty) Qty, max(PackageType) PkgType from SeaImportMkg where RefNo='{0}'", refN);

//            DataTable tab_bkg = C2.Manager.ORManager.GetDataSet(sql_bkg).Tables[0];
//            if (tab_bkg.Rows.Count == 1)
//            {
//                rptRow["Wt"] = SafeValue.SafeDecimal(tab_bkg.Rows[0]["Weight"]).ToString("0.000");
//                rptRow["M3"] = SafeValue.SafeDecimal(tab_bkg.Rows[0]["Volume"]).ToString("0.000");
//                rptRow["Pack"] = SafeValue.SafeString(tab_bkg.Rows[0]["Qty"]) + "x" + SafeValue.SafeString(tab_bkg.Rows[0]["PkgType"]);
//            }
//            rptRow["HaulierTruck"] = tab1.Rows[0]["HaulierTruck"];
//            rptRow["HaulierCollect"] = tab1.Rows[0]["HaulierCollect"];
//            rptRow["Attn"] = tab1.Rows[0]["Attn"];
//            rptRow["CrNote"] = tab1.Rows[0]["CrNote"];
//            rptRow["Haulier"] = tab1.Rows[0]["Haulier"];
//            rptRow["RefN"] = tab1.Rows[0]["RefN"];
//            rptRow["ET"] = SafeValue.SafeDateStr(tab1.Rows[0]["ET"]);
//            rptRow["ED"] = SafeValue.SafeDateStr(tab1.Rows[0]["ED"]);
//            string pod = SafeValue.SafeString(tab1.Rows[0]["Pod"], "");
//            rptRow["Pod"] = EzshipHelper.GetPortName(pod);
//            string pol = SafeValue.SafeString(tab1.Rows[0]["Pol"], "SGSIN");
//            rptRow["POL"] = EzshipHelper.GetPortName(pol);
//            rptRow["Ves"] = tab1.Rows[0]["Ves"];
//            rptRow["Bkg"] = tab1.Rows[0]["Bkg"];
//            rptRow["Bl"] = tab1.Rows[0]["Bl"];
//            rptRow["Agent"] = EzshipHelper.GetPartyName(tab1.Rows[0]["Agent"]);

//            rptRow["Us"] = tab1.Rows[0]["Us"];
//            rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];


//            tab.Rows.Add(rptRow);
//        }
//        catch (Exception ex)
//        {
//        }
//        return tab;
    }
    #endregion
    #region import REF P&L
    public static DataSet PrintPl_SeaRef(string refN, string refType, string userId)
    {
        DataSet set = new DataSet();
        DataTable tab_mast = Pl_Mast(refN, refType, userId);
        DataTable tab_Inv = Pl_Inv(refN, refType);
        DataTable tab_Dn = Pl_Dn(refN, refType);
        DataTable tab_Ts = Pl_Ts(refN, refType);
        DataTable tab_Cn = Pl_Cn(refN, refType);
        DataTable tab_Pl = Pl_Pl(refN, refType);
        DataTable tab_Vo = Pl_Vo(refN, refType);
        DataTable tab_Cost = Pl_Cost(refN, refType);
        tab_mast.TableName = "Mast";
        tab_Inv.TableName = "IV";
        tab_Ts.TableName = "TS";
        tab_Cn.TableName = "CN";
        tab_Dn.TableName = "DN";
        tab_Pl.TableName = "PL";
        tab_Vo.TableName = "VO";
        tab_Cost.TableName = "COST";

        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_Inv);
        set.Tables.Add(tab_Dn);
        set.Tables.Add(tab_Ts);
        set.Tables.Add(tab_Cn);
        set.Tables.Add(tab_Pl);
        set.Tables.Add(tab_Vo);
        set.Tables.Add(tab_Cost);
        return set;
    }
    private static DataTable Pl_Mast(string refN, string refType, string userId)
    {
        DataTable tab = new DataTable("PlMast");
        tab.Columns.Add("RefN");
        tab.Columns.Add("NowD");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Currency");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("JobType");
        tab.Columns.Add("TsM3");
        tab.Columns.Add("LocalM3");

        tab.Columns.Add("Agent");
        tab.Columns.Add("Company");
        tab.Columns.Add("Obl");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Eta");
        tab.Columns.Add("Qty");
        tab.Columns.Add("Pack");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("Pol");
        tab.Columns.Add("Pod");
        tab.Columns.Add("ContN");

        tab.Columns.Add("Rev1");
        tab.Columns.Add("Rev2");
        tab.Columns.Add("Rev3");
        tab.Columns.Add("Rev4");
        tab.Columns.Add("Rev");

        tab.Columns.Add("Cost1");
        tab.Columns.Add("Cost2");
        tab.Columns.Add("Cost3");
        tab.Columns.Add("Cost");
        tab.Columns.Add("Profit");
        string sql3 = "SELECT distinct ContainerNo + '/' + SealNo + '/' + ContainerType FROM SeaImportMkg WHERE RefNo = '" + refN + "' and MkgType='Cont'";

        string sql = string.Format(@"SELECT mast.RefNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,mast.CreateBy as UserId,Mast.CurrencyId as Currency,mast.ExRate,mast.JobType
,dbo.fun_GetPartyName(mast.AgentId) as Agent,'' as Company,mast.OblNo as Obl,mast.Vessel+'/'+mast.Voyage as Ves,convert(nvarchar(10),mast.Eta,103) as Eta,dbo.fun_GetPortName(mast.Pol) as Pol,dbo.fun_GetPortName(mast.Pod) as Pod,'' ContN
,mast.Qty,mast.PackageType as Pack,mast.Weight as Wt,mast.Volume as M3
,isnull((select sum(volume) from SeaImport where refNo=mast.RefNo and TsInd='Y'),0) as TsM3
,isnull((select sum(volume) from SeaImport where refNo=mast.RefNo and TsInd='N'),0) as LocalM3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='IV') Rev1
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsAgtRate),2)),0) FROM SeaImport  WHERE (RefNo = mast.RefNo) AND (TsAgtRate > 0))*mast.ExRate as Rev2
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='DN') Rev3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='CN') Rev4
,(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = 'SI' and (DocType='PL' or DocType='SD' or DocType='VO'))-(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = 'SI' and DocType='SC')  as Cost1
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsImpRate),2)),0) FROM SeaImport WHERE (RefNo = mast.RefNo) AND (TsImpRate > 0))*mast.ExRate as Cost2
,( SELECT sum(CostLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobType ='SI') Cost3
FROM SeaImportRef mast
where mast.RefNo='{0}'", refN);
        if (refType == "SE")
        {
            sql = string.Format(@"SELECT mast.RefNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,mast.CreateBy as UserId,Mast.CurrencyId as Currency,mast.ExRate,mast.JobType
,dbo.fun_GetPartyName(mast.AgentId) as Agent,'' as Company,mast.OblNo as Obl,mast.Vessel+'/'+mast.Voyage as Ves,convert(nvarchar(10),mast.Eta,103) as Eta,dbo.fun_GetPortName(mast.Pol) as Pol,dbo.fun_GetPortName(mast.Pod) as Pod,'' ContN
,mast.Qty,mast.PackageType as Pack,mast.Weight as Wt,mast.Volume as M3
,isnull((select sum(volume) from SeaExport where refNo=mast.RefNo and TsInd='Y'),0) as TsM3
,isnull((select sum(volume) from SeaExport where refNo=mast.RefNo and TsInd='N'),0) as LocalM3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SE' and DocType='IV') Rev1
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * ImpCharge),2)),0) FROM SeaExport  WHERE (RefNo = mast.RefNo) AND (ImpCharge > 0))*mast.ExRate as Rev2
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SE' and DocType='DN') Rev3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SE' and DocType='CN') Rev4
,(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = 'SE' and (DocType='PL' or DocType='SD' or DocType='VO'))-(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo  and MastType = 'SE' and DocType='SC')  as Cost1
,0 as Cost2
,( SELECT sum(CostLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobType ='SE') Cost3
FROM SeaExportRef mast
where mast.RefNo='{0}'", refN);

            sql3 = "SELECT distinct ContainerNo + '/' + SealNo + '/' + ContainerType FROM SeaExportMkg WHERE RefNo = '" + refN + "' and MkgType='Cont'";
        }

        DataTable dt = ConnectSql.GetTab(sql);
        DataRow row1 = tab.NewRow();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            string colName = dt.Columns[i].ColumnName;
            row1[colName] = dt.Rows[0][i];
        }
        tab.Rows.Add(row1);
        if (tab.Rows.Count > 0)
        {
            DataRow row = tab.Rows[0];
            decimal allM3 = SafeValue.SafeDecimal(tab.Rows[0]["M3"], 0);
            decimal transM3 = SafeValue.SafeDecimal(tab.Rows[0]["TsM3"], 0);

            if (allM3 == 0)
            {
                row["TsM3"] = "";
                row["LocalM3"] = "";
            }
            else
            {
                row["TsM3"] = transM3 + " - " + (transM3 * 100 / allM3).ToString("0.00") + "%";
                row["LocalM3"] = (allM3 - transM3).ToString("0.000") + " - " + ((allM3 - transM3) * 100 / allM3).ToString("0.00") + "%";
            }

            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            string contN = "";
            DataTable dt3 = ConnectSql.GetTab(sql3);
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                contN += dt3.Rows[i][0].ToString();
                if (i != dt3.Rows.Count - 1)
                    contN += "\n";
            }
            row["ContN"] = contN;


            decimal rev1 = SafeValue.SafeDecimal(tab.Rows[0]["Rev1"], 0); //iv
            decimal rev2 = SafeValue.SafeDecimal(tab.Rows[0]["Rev2"], 0); //ts
            decimal rev3 = SafeValue.SafeDecimal(tab.Rows[0]["Rev3"], 0); //dn
            decimal rev4 = SafeValue.SafeDecimal(tab.Rows[0]["Rev4"], 0); //cn
            row["Rev1"] = rev1.ToString("###,##0.00");
            row["Rev2"] = rev2.ToString("###,##0.00");
            row["Rev3"] = rev3.ToString("###,##0.00");
            row["Rev4"] = "(" + rev4.ToString("###,##0.00") + ")";
            decimal sumRev = rev1 + rev2 + rev3 - rev4;
            row["Rev"] = sumRev.ToString("###,##0.00");


            decimal cost1 = SafeValue.SafeDecimal(tab.Rows[0]["Cost1"], 0); //pl/sc/sc
            decimal cost2 = SafeValue.SafeDecimal(tab.Rows[0]["Cost2"], 0); //vo
            decimal cost3 = SafeValue.SafeDecimal(tab.Rows[0]["Cost3"], 0); //costing

            row["Cost1"] = cost1.ToString("###,##0.00");
            row["Cost2"] = cost2.ToString("###,##0.00");
            row["Cost3"] = cost3.ToString("###,##0.00");
            decimal cost = cost1 + cost2 + cost3;
            row["Cost"] = cost.ToString("###,##0.00");
            row["Profit"] = (sumRev - cost).ToString("###,##0.00");
        }
        return tab;
    }

    private static DataTable Pl_Inv(string refN, string refType)
    {
        DataTable tab = new DataTable("Invoice");
        tab.Columns.Add("JobN");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("Cust");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("InvN");
        tab.Columns.Add("Amount");
        tab.Columns.Add("Receipt");

        tab.Columns.Add("Frt");
        tab.Columns.Add("Agy");
        tab.Columns.Add("Thc");
        tab.Columns.Add("Lcl");
        tab.Columns.Add("DoFee");
        tab.Columns.Add("Other");

        string sql = string.Format(@"SELECT import.JobNo, import.HblNo, import.Weight, import.Volume,  inv.PartyTo as CustomerId, inv.DocNo, inv.SequenceId
FROM     XAArInvoice AS inv   left JOIN  SeaImport AS import  ON import.JobNo = inv.JobRefNo AND import.RefNo = inv.MastRefNo
WHERE     (inv.DocType = 'IV') AND (inv.MastType = '{1}') and  inv.MastRefNo='{0}' order by inv.JobRefNo,inv.DocNo", refN,refType);
        DataTable tab_Inv = ConnectSql.GetTab(sql);
        decimal gstA = 0;
        for (int i = 0; i < tab_Inv.Rows.Count; i++)
        {
            string jobNo = SafeValue.SafeString(tab_Inv.Rows[i]["JobNo"]);
            string hbl = SafeValue.SafeString(tab_Inv.Rows[i]["HblNo"]);
            string cust = EzshipHelper.GetPartyName(tab_Inv.Rows[i]["CustomerId"]);
            decimal wt = SafeValue.SafeDecimal(tab_Inv.Rows[i]["Weight"], 0);
            decimal m3 = SafeValue.SafeDecimal(tab_Inv.Rows[i]["Volume"], 0);
            string billNo = tab_Inv.Rows[i]["DocNo"].ToString();
            string billId = tab_Inv.Rows[i]["SequenceId"].ToString();

            decimal frt = 0;
            decimal agy = 0;
            decimal thc = 0;
            decimal lcl = 0;
            decimal doFee = 0;
            decimal other = 0;
            decimal amt = 0;
            string sqlBillDet = string.Format("SELECT DocNo, DocType, ChgCode, Currency, ExRate, Gst, GstAmt,DocAmt, LocAmt,LineLocAmt FROM XAArInvoiceDet where DocId='{0}'", billId);
            DataTable tab_InvDet = ConnectSql.GetTab(sqlBillDet);
            for (int j = 0; j < tab_InvDet.Rows.Count; j++)
            {
                string chgCode = tab_InvDet.Rows[j]["ChgCode"].ToString();
                if (chgCode.ToUpper() == "FRTOC")
                {
                    frt += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "AGY")
                {
                    agy += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "THC")
                {
                    thc += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "LCL")
                {
                    lcl += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "DOFEE")
                {
                    doFee += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else//other
                {
                    other += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                amt += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
            }

            DataRow row = tab.NewRow();
            row["JobN"] = jobNo;
            row["Hbl"] = hbl;
            row["Cust"] = cust;
            row["Wt"] = wt.ToString("0.000");
            row["M3"] = m3.ToString("0.000");
            row["InvN"] = billNo;
            row["Amount"] = amt.ToString("0.00");
            row["Frt"] = frt.ToString("0.00");
            row["Agy"] = agy.ToString("0.00");
            row["Thc"] = thc.ToString("0.00");
            row["Lcl"] = lcl.ToString("0.00");
            row["DoFee"] = doFee.ToString("0.00");
            row["Other"] = other.ToString("0.00");

            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable Pl_Dn(string refN, string refType)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, dbo.fun_GetPartyName(MAX(mast.PartyTo)) AS CustName, SUM(det.LineLocAmt) AS Amount
FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.MastType = '{1}') and mast.DocType='DN' GROUP BY mast.DocNo", refN,refType);

        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        tab.TableName = "DN";
        return tab;
    }
    private static DataTable Pl_Ts(string refN, string refType)
    {
        DataTable tab = new DataTable("Ts");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Pod");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("AgtRate");
        tab.Columns.Add("Amount");
        tab.Columns.Add("Currency");
        if (refType == "SI")
        {
            string sql = string.Format(@"SELECT HBLNo AS Hbl, TsVessel+'/'+TsVoyage as Ves,dbo.fun_GetPortName(TsPod) as Pod,Volume AS M3, Weight AS WT, TsAgtRate as AgtRate
,cast((Case when Weight/1000>Volume then Weight/1000 else Volume end)*TsAgtRate as numeric(10,2)) as Amount
FROM SeaImport WHERE (RefNo = '{0}') AND (TsAgtRate > 0)  and TsInd='Y'", refN, refType);
            DataTable dt = ConnectSql.GetTab(sql);
            tab = dt.Copy();
        }
        return tab;
    }

    private static DataTable Pl_Cn(string refN, string refType)
    {
        DataTable tab = new DataTable("Cn");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, dbo.fun_GetPartyName(MAX(mast.PartyTo)) AS CustName, SUM(det.LineLocAmt) AS Amount
FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.MastType = '{1}') and mast.DocType='CN' GROUP BY mast.DocNo", refN,refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Pl(string refN, string refType)
    {
        string sql = string.Format(@"SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, 
case when Mast.DocType='SC' then -det.LineLocAmt else det.LineLocAmt end AS Amount
FROM XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.DocType = 'PL' or mast.DocType = 'SC' or mast.DocType = 'SD') AND (mast.MastType = '{1}')", refN,refType);
        DataTable dt = ConnectSql.GetTab(sql);
        DataTable tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Vo(string refN, string refType)
    {
        string sql = string.Format(@" SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, 
det.LineLocAmt AS Amount
FROM XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
WHERE     (mast.MastRefNo = '{0}') AND (mast.DocType = 'VO') AND (mast.MastType = '{1}')", refN,refType);
        DataTable dt = ConnectSql.GetTab(sql);
        DataTable tab = dt.Copy();
        tab.TableName = "VO";
        return tab;
    }
    private static DataTable Pl_Cost(string refN, string refType)
    {
        string sql = string.Format(@"SELECT ChgCodeDes+Remark as Des, CostLocAmt as Amount FROM SeaCosting Where RefNo='{0}' and JobType='{1}'", refN,refType);
        DataTable dt = ConnectSql.GetTab(sql);
        DataTable tab = dt.Copy();
        return tab;
    }


    #endregion


    #region import
    public static DataTable dsBatchDo(string refNo)
    {
        string sql_det = string.Format(@"select import_n from import where IMPORT_REF_N='{0}' order by import_n", refNo);
        DataTable tabDet = ConnectSql.GetTab(sql_det);
        return tabDet;
    }
    public static DataSet dsDo(string importN)
    {
        DataSet set1 = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_Print_dsDo '{0}','{1}','{2}','{3}','{4}'", "", importN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set1.Tables.Add(mast);
            set1.Tables.Add(det);
            set1.Relations.Add("Rela", mast.Columns["ImpNo"], det.Columns["ImpNo"]);
        }
        catch (Exception ex) { }
        return set1;

//        DataTable mast = new DataTable("Mast");
//        mast.Columns.Add("RefNo");
//        mast.Columns.Add("ImpNo");
//        mast.Columns.Add("Pol");
//        mast.Columns.Add("Pod");
//        mast.Columns.Add("Eta");
//        mast.Columns.Add("ContNo");

//        mast.Columns.Add("Cng");
//        mast.Columns.Add("Ves");
//        mast.Columns.Add("Obl");
//        mast.Columns.Add("Hbl");

//        mast.Columns.Add("Fl");
//        mast.Columns.Add("Dn");
//        mast.Columns.Add("Tracing");
//        mast.Columns.Add("Wh");
//        mast.Columns.Add("Admin");
//        mast.Columns.Add("Normal");


//        mast.Columns.Add("Ms");
//        mast.Columns.Add("TShipper");
//        mast.Columns.Add("TCng");
//        mast.Columns.Add("TVes");
//        mast.Columns.Add("TEtd");
//        mast.Columns.Add("TExpBkgN");

//        mast.Columns.Add("CltFrm");
//        mast.Columns.Add("IssuedBy");
//        mast.Columns.Add("CompanyName");

//        DataTable det = new DataTable("Detail");
//        det.Columns.Add("ImpNO");
//        det.Columns.Add("Mkg");
//        det.Columns.Add("Qty");
//        det.Columns.Add("Des");
//        det.Columns.Add("Wt");
//        det.Columns.Add("M3");
//        try
//        {
//            string sql = string.Format(@"SELECT RefNo, HblNo, UserId, TsInd, TsVessel, TsVoyage, TsEtd, TsBkgNo, TsPod, CustomerId, rateForklift, rateProcess,rateTracing, rateWarehouse, rateAdmin, flagNomination
//FROM SeaImport WHERE (JobNo = '{0}')", importN);
//            string sqlMkgs = string.Format("SELECT ContainerNo, SealNo, Marking, Description, Weight, Volume, Qty, PackageType, Remark FROM SeaIMPORTmkg WHERE JobNo='{0}'", importN);

//            string sqlRef = string.Format(@"SELECT master.JobType,master.AgentId, master.Eta, master.Vessel + ' ' + master.Voyage AS Vessel, master.UserId AS userID, master.OblNo, master.Pol , master.Pod, 
//                      master.WarehouseId
//FROM SeaImportRef AS master INNER JOIN  SeaImport AS import ON master.RefNo = import.RefNo
//WHERE (import.JobNo = '{0}')", importN);

//            DataTable tab_ref = ConnectSql.GetTab(sqlRef);
//            DataTable tab_job = ConnectSql.GetTab(sql);
//            DataTable tab_mkg = ConnectSql.GetTab(sqlMkgs);
//            if (tab_ref.Rows.Count < 1) return new DataSet();
//            string jobType = tab_ref.Rows[0]["JobType"].ToString();
//            if (jobType == "FCL")
//                sqlMkgs += " and MkgType='Cont'";
//            else
//                sqlMkgs += " and MkgType='Bl'";


//            string contNo = "";
//            for (int t = 0; t < tab_mkg.Rows.Count; t++)
//            {
//                DataRow rowMkg = tab_mkg.Rows[t];
//                DataRow rowDet = det.NewRow();
//                string marks = SafeValue.SafeString(rowMkg["Marking"], "").Trim();
//                string dets = SafeValue.SafeString(rowMkg["Description"], "").Trim();
//                contNo = SafeValue.SafeString(rowMkg["ContainerNo"], "").Trim();
//                rowDet["ImpNo"] = importN;
//                rowDet["Mkg"] = marks;
//                rowDet["Qty"] = SafeValue.SafeString(rowMkg["Qty"], "") + SafeValue.SafeString(rowMkg["PackageType"], "");
//                rowDet["Des"] = "SAID TO CONTAIN : \n " + dets;
//                rowDet["Wt"] = SafeValue.SafeDouble(rowMkg["Weight"], 0).ToString("0.000");
//                rowDet["M3"] = SafeValue.SafeDouble(rowMkg["Volume"], 0).ToString("0.000");
//                det.Rows.Add(rowDet);
//            }

//            DataRow importRef = tab_ref.Rows[0];
//            DataRow rowMast = mast.NewRow();
//            string refN = "";

//            if (tab_job.Rows.Count > 0)
//            {
//                DataRow import = tab_job.Rows[0];
//                //barcode

//                refN = SafeValue.SafeString(import["RefNo"], "");
//                rowMast["RefNo"] = refN;
//                rowMast["ImpNo"] = importN;



//                rowMast["ContNo"] = contNo;
//                string custId = import["CustomerId"].ToString();
//                string sql_cust = string.Format("Select Name,address from XXParty where PartyId='{0}'", custId);
//                DataTable tab_cust = ConnectSql.GetTab(sql_cust);
//                if (tab_cust.Rows.Count > 0)
//                {
//                    rowMast["Cng"] = SafeValue.SafeString(tab_cust.Rows[0]["Name"], "");
//                    rowMast["Ms"] = SafeValue.SafeString(tab_cust.Rows[0]["Name"], "");

//                    rowMast["Cng"] += "\n" + SafeValue.SafeString(tab_cust.Rows[0]["Address"], "");
//                }

//                rowMast["Hbl"] = SafeValue.SafeString(import["HblNo"], "");

//                rowMast["IssuedBy"] = SafeValue.SafeString(import["UserId"], "");

//                //t/s 
//                string tPod = "";
//                if (SafeValue.SafeString(import["TsInd"], "N") == "Y")
//                {
//                    //
//                    custId = importRef["AgentId"].ToString();
//                    rowMast["TShipper"] = EzshipHelper.GetPartyName(custId);
//                    rowMast["TCng"] = rowMast["Cng"];
//                    rowMast["TVes"] = SafeValue.SafeString(import["TsVessel"], "") + "/" + SafeValue.SafeString(import["TsVoyage"], "");
//                    rowMast["TEtd"] = SafeValue.SafeDateStr(import["TsEtd"]);
//                    rowMast["TExpBkgN"] = SafeValue.SafeString(import["TsBkgNo"], "");
//                    tPod = SafeValue.SafeString(import["TsPod"], "NA");
//                    if (tPod == "NA" || tPod.Length < 3)
//                    {
//                        tPod = "";
//                    }
//                    else
//                    {
//                        tPod = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + tPod + "'"),"");
//                        tPod += " Via ";
//                    }
//                }

//                rowMast["Ves"] = SafeValue.SafeString(importRef["Vessel"], "");
//                rowMast["Obl"] = SafeValue.SafeString(importRef["OblNo"], "");
//                rowMast["Pol"] = SafeValue.SafeString(importRef["Pol"], "");
//                rowMast["Pod"] = tPod + SafeValue.SafeString(importRef["Pod"], "");
//                rowMast["Eta"] = SafeValue.SafeDateStr(importRef["ETA"]);


//                rowMast["Fl"] = SafeValue.SafeDecimal(import["RateForklift"], 0).ToString("0.00");
//                rowMast["Dn"] = SafeValue.SafeDecimal(import["RateProcess"], 0).ToString("0.00");
//                rowMast["Tracing"] = SafeValue.SafeDecimal(import["RateTracing"], 0).ToString("0.00");
//                rowMast["Wh"] = SafeValue.SafeDecimal(import["RateWarehouse"], 0).ToString("0.00");
//                rowMast["Admin"] = SafeValue.SafeDecimal(import["RateAdmin"], 0).ToString("0.00");
//                string nomination = SafeValue.SafeString(import["FlagNomination"], "N").ToUpper();
//                if (nomination == "Y")
//                    nomination = "Nomination";
//                else
//                    nomination = "FreeHand";
//                rowMast["Normal"] = nomination;

//                //footer
//                string vendorId = importRef["WarehouseId"].ToString();
//                string sql_vendor = string.Format("Select Name,AddRess,Fax1,Tel1 from XXParty where PartyId='{0}'", vendorId);
//                DataTable tab_vendor = ConnectSql.GetTab(sql_vendor);
//                if (tab_vendor.Rows.Count > 0)
//                {
//                    rowMast["CltFrm"] = SafeValue.SafeString(tab_vendor.Rows[0]["Name"], "");
//                    rowMast["CltFrm"] += "\n" + SafeValue.SafeString(tab_vendor.Rows[0]["Address"], "");
//                    rowMast["CltFrm"] += "\nTel:" + SafeValue.SafeString(tab_vendor.Rows[0]["Tel1"], "");
//                    rowMast["CltFrm"] += "  Fax:" + SafeValue.SafeString(tab_vendor.Rows[0]["Fax1"], "");
//                }
//                rowMast["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//            }
//            mast.Rows.Add(rowMast);
//        }
//        catch (Exception ex)
//        { }
//        DataSet set1 = new DataSet();

//        set1.Tables.Add(mast);
//        set1.Tables.Add(det);
//        set1.Relations.Add("Rela", mast.Columns["ImpNo"], det.Columns["ImpNo"]);
//        return set1;
    }
    public static DataSet dsImpConverPage(string refNo)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_dsImpConverPage '{0}','{1}','{2}','{3}','{4}'", refNo, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("Rela", Mast.Columns["RefNo"], Detail.Columns["RefNo"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;

//        DataTable mast = new DataTable("Mast");
//        mast.Columns.Add("RefNo");
//        mast.Columns.Add("Ves");
//        mast.Columns.Add("Eta");
//        mast.Columns.Add("Obl");
//        mast.Columns.Add("Carrier");
//        mast.Columns.Add("ContNo");
//        mast.Columns.Add("Agt");
//        mast.Columns.Add("Unstuffing");
//        mast.Columns.Add("TotWt");
//        mast.Columns.Add("TotQty");
//        mast.Columns.Add("TotM3");
//        mast.Columns.Add("CompanyName");
//        DataTable det = new DataTable("Detail");
//        det.Columns.Add("RefNo");
//        det.Columns.Add("HblN");
//        det.Columns.Add("CustName");
//        det.Columns.Add("Pkg");
//        det.Columns.Add("Qty");

//        det.Columns.Add("Wt");
//        det.Columns.Add("M3");


//        string sql_mast = string.Format(@"SELECT JobType, RefNo,AgentId,Vessel + '/ ' + Voyage AS Vessel, Eta,CrAgentId, OblNo AS Obl, CONVERT(varchar(20),Qty) AS Pkgs
//FROM SeaImportRef WHERE (RefNo = '{0}')", refNo);

//        DataTable tabMast = ConnectSql.GetTab(sql_mast);
//        if (tabMast.Rows.Count > 0)
//        {
//            int totQty = 0;
//            decimal totM3 = 0;
//            decimal totWt = 0;

//            string sql_det = string.Format(@"SELECT JobNo AS ImpNo, HblNo,CustomerId,CustomerName, Qty AS Pkgs, PackageType AS PkgType, Weight AS Wt, Volume AS M3
//FROM         SeaImport
//WHERE (RefNo = '{0}') ", refNo);

//            DataTable tabDet = ConnectSql.GetTab(sql_det);
//            for (int t = 0; t < tabDet.Rows.Count; t++)
//            {
//                DataRow rowDet = tabDet.Rows[t];
//                DataRow rptRowDet = det.NewRow();
//                rptRowDet["RefNo"] = refNo;
//                rptRowDet["HblN"] = SafeValue.SafeString(rowDet["HblNo"], "");
//                //pageInt++;
//                rptRowDet["CustName"] = rowDet["CustomerName"];

//                rptRowDet["Qty"] = SafeValue.SafeString(rowDet["Pkgs"], "");
//                totQty += SafeValue.SafeInt(rowDet["Pkgs"], 0);
//                rptRowDet["Pkg"] = SafeValue.SafeString(rowDet["Pkgtype"], "");

//                rptRowDet["Wt"] = SafeValue.SafeString(rowDet["Wt"], "").Trim();
//                totWt += SafeValue.SafeDecimal(rowDet["Wt"], 0);
//                rptRowDet["M3"] = SafeValue.SafeString(rowDet["M3"], "").Trim();
//                totM3 += SafeValue.SafeDecimal(rowDet["M3"], 0);
//                det.Rows.Add(rptRowDet);

//            }



//            DataRow row = tabMast.Rows[0];
//            DataRow rptRowMast = mast.NewRow();

//            rptRowMast["RefNo"] = refNo;
//            rptRowMast["Ves"] = SafeValue.SafeString(row["Vessel"], "");
//            rptRowMast["Carrier"] = EzshipHelper.GetPartyName(row["CrAgentId"]);
//            rptRowMast["Obl"] = SafeValue.SafeString(row["Obl"], "");


//            rptRowMast["Eta"] = SafeValue.SafeDateStr(row["ETA"]);
//            string sql_cont = string.Format(@"select ContainerNo + '/' + SealNo + '/' + ContainerType as container from SeaImportMkg where RefNo='{0}' and MkgType='Cont'", refNo);
//            DataTable cont_tab = ConnectSql.GetTab(sql_cont);
//            for (int i = 0; i < cont_tab.Rows.Count; i++)
//            {
//                if (i == 0)
//                    rptRowMast["ContNo"] = SafeValue.SafeString(cont_tab.Rows[i][0], "");
//                else
//                    rptRowMast["ContNo"] += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0], "");
//            }


//            string agtId = SafeValue.SafeString(row["AgentId"], "");
//            if (agtId == "NA")
//                rptRowMast["Agt"] = agtId;
//            else
//            {
//                rptRowMast["Agt"] = EzshipHelper.GetPartyName(agtId);
//            }


//            rptRowMast["TotWt"] = totWt.ToString("#,##0.000");
//            rptRowMast["TotM3"] = totM3.ToString("#,##0.000");
//            rptRowMast["TotQty"] = totQty;
//            rptRowMast["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];


//            mast.Rows.Add(rptRowMast);
//        }
//        DataSet set = new DataSet();
//        set.Tables.Add(mast);
//        set.Tables.Add(det);
//        set.Relations.Add("Rela", mast.Columns["RefNo"], det.Columns["RefNo"]);
//        return set;
    }
    private static decimal GetCharge(string port, string agent, string code, string date)
    {
        string where = string.Format("AGENT_ID='{0}' and Port_ID='{1}' and CHARGE_CODE='{2}' and BEGIN_DATE<=to_date('{3}','yyyy-mm-dd')", agent, port, code, date);
        string sql = string.Format("select CHARGE_PRICE from WAREHOUSE_CHARGE where {0} order by BEGIN_DATE DESC", where);
        decimal rate = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql), -1);


        if (rate == -1)
        {
            where = string.Format("AGENT_ID='{0}' and Port_ID='{1}' and CHARGE_CODE='{2}' and BEGIN_DATE<=to_date('{3}','yyyy-mm-dd')", agent, "NA", code, date);
            sql = string.Format("select CHARGE_PRICE from WAREHOUSE_CHARGE where {0} order by BEGIN_DATE DESC", where);
            rate = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql), -1);
        }

        if (rate == -1)
        {
            where = string.Format("AGENT_ID='{0}' and Port_ID='{1}' and CHARGE_CODE='{2}' and BEGIN_DATE<=to_date('{3}','yyyy-mm-dd')", "NA", port, code, date);
            sql = string.Format("select CHARGE_PRICE from WAREHOUSE_CHARGE where {0} order by BEGIN_DATE DESC", where);
            rate = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql), -1);
        }
        if (rate == -1)
        {
            where = string.Format("AGENT_ID='{0}' and Port_ID='{1}' and CHARGE_CODE='{2}' and BEGIN_DATE<=to_date('{3}','yyyy-mm-dd')", "NA", "NA", code, date);
            sql = string.Format("select CHARGE_PRICE from WAREHOUSE_CHARGE where {0} order by BEGIN_DATE DESC", where);
            rate = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql), 0);
        }

        return rate;
    }


    public static DataTable PrintPreAdvise(string refN,string jobN)
    {

        string sqlstr = string.Format(@"exec proc_PrintPreAdvise '{0}','{1}','{2}','{3}','{4}'", refN, jobN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
        DataTable Mast = ConnectSql.GetTab(sqlstr);
        Mast.TableName = "Mast";
        return Mast;


//        string sql = string.Format(@"SELECT det.JobNo as JobN,
//         det.hblno as Hbl,
//         det.customerId,
//         mast.vessel+'/'+mast.voyage as Ves,
//         mast.Pol,
//         mast.Eta as ET,
//         mast.warehouseid,
//        mast.oblno as Bl,
//         det.Weight as Wt,
//         det.volume as M3,
//         det.qty as Qty,
//         det.packagetype as PackType,  
//         det.SShipperRemark as Shipper,   
//        det.SConsigneeRemark as Consignee,   
//         det.userid as Us,
//         det.deliverydate as Delivery
//    FROM SeaImport det,SeaImportRef mast  
//   WHERE  det.RefNo= mast.RefNo and  
//         det.RefNo = '{0}' and det.JobNo='{1}'", refN, jobN);

//        DataTable tab1 =ConnectSql.GetTab(sql);
//        DataTable Mast = new DataTable("PreAdvise");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("JobN");
//        Mast.Columns.Add("Ves");
//        Mast.Columns.Add("Bl");
//        Mast.Columns.Add("Hbl");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("Cust");
//        Mast.Columns.Add("Wt");
//        Mast.Columns.Add("Contact");
//        Mast.Columns.Add("Us");
//        Mast.Columns.Add("Delivery");
//        Mast.Columns.Add("Shipper");
//        Mast.Columns.Add("CompanyName");
//        try
//        {
//            DataRow rptRow = Mast.NewRow();
//            for (int i = 0; i < Mast.Columns.Count; i++)
//            {
//                string colName = Mast.Columns[i].ToString();
//                if (colName == "RefN")
//                    rptRow[colName] = refN;
//                else if (colName == "ET")
//                    rptRow[colName] = SafeValue.SafeDateStr(tab1.Rows[0][colName]);
//                else if (colName == "Delivery")
//                    rptRow[colName] = SafeValue.SafeDateStr(tab1.Rows[0][colName]);
//                else if (colName == "Wt")
//                {
//                    rptRow[colName] = SafeValue.SafeDecimal(tab1.Rows[0]["Wt"], 0).ToString("0.00") + "KGS ";
//                    rptRow[colName] += SafeValue.SafeDecimal(tab1.Rows[0]["M3"], 0).ToString("0.00") + "M3 ";
//                    rptRow[colName] += SafeValue.SafeDecimal(tab1.Rows[0]["Qty"], 0).ToString("0") + " ";
//                    rptRow[colName] += SafeValue.SafeString(tab1.Rows[0]["PackType"], "");
//                }
//                else if (colName == "Contact")
//                {
//                }
//                else if (colName == "CompanyName")
//                {
//                    rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                }
//                else if (colName == "Cust")
//                {
//                    string custId = SafeValue.SafeString(tab1.Rows[0]["CustomerId"], "0");
//                    string sql_vendor = "select Name,  Address, Tel1,Fax1,Contact1 from XXParty where PartyId='" + custId + "'";
//                    DataTable tab_vendor = C2.Manager.ORManager.GetDataSet(sql_vendor).Tables[0];
//                    if (tab_vendor.Rows.Count > 0)
//                    {
//                        rptRow["Cust"] = tab_vendor.Rows[0]["Name"];
//                        if (SafeValue.SafeString(tab_vendor.Rows[0]["Contact1"], "").Length > 0)
//                            rptRow["Contact"] += "\n" + tab_vendor.Rows[0]["Contact1"];
//                    }
//                }
//                else
//                    rptRow[Mast.Columns[i].ToString()] = tab1.Rows[0][Mast.Columns[i].ToString()];
//            }
//            Mast.Rows.Add(rptRow);
//        }
//        catch (Exception ex)
//        {
//        }

//        return Mast;
    }
    public static DataTable PrintDN(string refN, string jobN,string dnId)
    {
        DataTable ds = new DataTable();
        try
        {
            string sqlstr = string.Format(@"exec proc_PrintDN '{0}','{1}','{2}','{3}','{4}'", refN, jobN, "", "", dnId);
            ds = ConnectSql.GetTab(sqlstr);
           // ds.TableName = "Mast";
        }
        catch (Exception ex)
        {
        }
        return ds;
    }

    public static DataSet PrintArrivalNotice(string refN, string jobN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintArrivalNotice '{0}','{1}','{2}','{3}','{4}'", refN, jobN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }

        return ds;


//        string sql_cont = string.Format("select ContainerNo, ContainerNo+'/'+SealNo+'/'+ContainerType from SeaImportMkg where refNo='{0}' and JobNo='{1}'", refN,jobN);
//        DataTable tab2 = ConnectSql.GetTab(sql_cont);
//        string contN = "";
//        string lastContNo = "";
//        for (int m = 0; m < tab2.Rows.Count; m++)
//        {
//            string contNo = tab2.Rows[m][0].ToString();
//            if (lastContNo == contNo)
//            {
//            }
//            else
//            {
//                if (contN.Length == 0)
//                {
//                    contN = tab2.Rows[m][1].ToString();
//                }
//                else
//                {
//                    contN += "\n" + tab2.Rows[m][1].ToString();
//                }
//                lastContNo = contNo;
//            }
//        }

//        DataTable Mast = new DataTable("Mast");
//        Mast.Columns.Add("Con");
//        Mast.Columns.Add("CustName");
//        Mast.Columns.Add("CustAddress");
//        Mast.Columns.Add("InvN");
//        Mast.Columns.Add("JobN");
//        Mast.Columns.Add("VendName");
//        Mast.Columns.Add("VendAddress");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("Import");
//        Mast.Columns.Add("Ves");
//        Mast.Columns.Add("DoReady");
//        Mast.Columns.Add("Hbl");
//        Mast.Columns.Add("BL");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("NowD");
//        Mast.Columns.Add("Carrier");
//        Mast.Columns.Add("PermitN");
//        Mast.Columns.Add("Release");
//        Mast.Columns.Add("Shipper");
//        Mast.Columns.Add("Gd1");
//        Mast.Columns.Add("Wt");
//        Mast.Columns.Add("Pack");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("CrRate");
//        Mast.Columns.Add("Us");
//        Mast.Columns.Add("ContN");
//        Mast.Columns.Add("Amount");
//        Mast.Columns.Add("CompanyName");
//        DataTable Detail = new DataTable("Detail");
//        Detail.Columns.Add("RefN");
//        Detail.Columns.Add("ChgDes");
//        Detail.Columns.Add("Currency");
//        Detail.Columns.Add("Rate");
//        Detail.Columns.Add("ChgA");
//        Detail.Columns.Add("ChgUnit");
//        Detail.Columns.Add("LineRate");
//        Detail.Columns.Add("Amount");
//        decimal amt = 0;
//        string invN = "";
//        string sql_Inv = string.Format(@"SELECT det.DocNo , det.ChgCode, det.ChgDes1, det.GstType, det.Qty, det.Price, det.Unit, det.Currency, det.ExRate, det.Gst, det.GstAmt, det.DocAmt, det.LocAmt
//FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.DocNo = det.DocNo
//WHERE (mast.MastType = 'SI') AND (mast.MastRefNo = '{0}') AND (mast.JobRefNo = '{1}')", refN, jobN);
//        DataTable tab_det = ConnectSql.GetTab(sql_Inv);
//        try
//        {
//            for (int i = 0; i < tab_det.Rows.Count; i++)
//            {
//                DataRow rptRowDet = Detail.NewRow();
//                invN = tab_det.Rows[i]["DocNo"].ToString();
//                rptRowDet["RefN"] = jobN + "/" + refN;
//                rptRowDet["ChgDes"] = tab_det.Rows[i]["ChgDes1"];
//                rptRowDet["Currency"] = tab_det.Rows[i]["Currency"];
//                rptRowDet["Rate"] = SafeValue.SafeDecimal(tab_det.Rows[i]["Qty"], 0).ToString("0.000");
//                rptRowDet["ChgA"] = SafeValue.SafeDecimal(tab_det.Rows[i]["Price"], 0).ToString("0.00");
//                rptRowDet["ChgUnit"] = tab_det.Rows[i]["Unit"];
//                rptRowDet["LineRate"] = SafeValue.SafeDecimal(tab_det.Rows[i]["ExRate"], 0).ToString("0.000");
//                rptRowDet["Amount"] = SafeValue.SafeDecimal(tab_det.Rows[i]["LocAmt"], 0).ToString("0.00");
//                amt += SafeValue.SafeDecimal(tab_det.Rows[i]["LocAmt"], 0);
//                Detail.Rows.Add(rptRowDet);
//            }
//            string sql_mast = string.Format(@"SELECT mast.JobType,det.doReadyInd,det.CustomerID AS Con, mast.Vessel + '/' + mast.Voyage AS Ves, mast.Pol, mast.Pod, mast.WarehouseId AS VendId,  
//                      det.HblNo AS Hbl, mast.Eta AS ET, mast.ExRate, mast.CrAgentID AS Carrier, mast.OblNo AS BL, det.ExpressBl AS Release, det.Shipper, det.Consignee, 
//                      det.Qty, det.PackageType AS Packtype, det.Weight AS Wt, det.Volume AS M3, det.UserID AS us
//FROM SeaImport AS det INNER JOIN SeaImportRef AS mast ON det.RefNo = mast.RefNo
//WHERE (det.RefNo= '{0}') AND (det.JobNo = '{1}')", refN, jobN);
//            DataTable tab_mast = ConnectSql.GetTab(sql_mast);
//            if (tab_mast.Rows.Count == 1)
//            {
//                DataRow rptRow = Mast.NewRow();
//                string custId = tab_mast.Rows[0]["Con"].ToString();
//                rptRow["Con"] = custId;
//                string sql_cust = "select Name,  Address, Tel1,Fax1,Contact1 from XXParty where PartyId='" + custId + "'";
//                DataTable tab_cust = ConnectSql.GetTab(sql_cust);
//                if (tab_cust.Rows.Count > 0)
//                {
//                    rptRow["CustName"] = tab_cust.Rows[0]["Name"];
//                    rptRow["CustAddress"] = tab_cust.Rows[0]["Address"];
//                    rptRow["CustAddress"] += "\n Tel:" + tab_cust.Rows[0]["Tel1"].ToString() + "  Fax:" + tab_cust.Rows[0]["Fax1"].ToString();
//                }
//                rptRow["InvN"] = invN;
//                rptRow["JobN"] = jobN;
//                string vendorId = tab_mast.Rows[0]["VendId"].ToString();
//                string sql_venodr = "select Name,  Address, Tel1,Fax1,Contact1 from XXParty where PartyId='" + vendorId + "'";
//                DataTable tab_vendor = ConnectSql.GetTab(sql_venodr);
//                if (tab_vendor.Rows.Count > 0)
//                {
//                    rptRow["VendName"] = tab_vendor.Rows[0]["Name"];
//                    rptRow["VendAddress"] = tab_vendor.Rows[0]["Address"];
//                    rptRow["VendAddress"] += "\n Tel:" + tab_vendor.Rows[0]["Tel1"].ToString() + "  Fax:" + tab_vendor.Rows[0]["Fax1"].ToString();
//                }
//                string pol = tab_mast.Rows[0]["Pol"].ToString();
//                if (pol.Length > 0)
//                    pol = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + pol + "'"),pol);
//                string pod = tab_mast.Rows[0]["Pod"].ToString();
//                if (pod.Length > 0)
//                    pod = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + pod + "'"),pod);
//                rptRow["Import"] = pol+"/"+pod;
//                rptRow["Ves"] = tab_mast.Rows[0]["Ves"];
//                rptRow["Hbl"] = tab_mast.Rows[0]["Hbl"];
//                rptRow["BL"] = tab_mast.Rows[0]["BL"];
//                rptRow["ET"] = SafeValue.SafeDateStr(tab_mast.Rows[0]["ET"]);
//                rptRow["NowD"] = DateTime.Today.ToString("dd/MM/yyyy");

//                rptRow["Carrier"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select Name from XXParty where PartyId='" + tab_mast.Rows[0]["Carrier"].ToString() + "'"), "");
//                //rptRow["PermitN"] = tab_mast.Rows[0]["PermitN"];
//                if (tab_mast.Rows[0]["Release"].ToString() == "Y")
//                {
//                    rptRow["Release"] ="TELEX RELEASE";
//                }
//                else
//                {
//                    rptRow["Release"] = "NO TELEX RELEASE";
//                }
//                if (tab_mast.Rows[0]["DoReadyInd"].ToString() == "Y")
//                {
//                    rptRow["DoReady"] ="D/O READY";
//                }
//                else
//                {
//                    rptRow["DoReady"] = "D/O NOT READY";
//                }
//                rptRow["Shipper"] = tab_mast.Rows[0]["Shipper"];
//                decimal wt = SafeValue.SafeDecimal(tab_mast.Rows[0]["Wt"], 0);
//                decimal m3 = SafeValue.SafeDecimal(tab_mast.Rows[0]["M3"], 0);
//                int qty = SafeValue.SafeInt(tab_mast.Rows[0]["Qty"], 0);
//                string packType = SafeValue.SafeString(tab_mast.Rows[0]["Packtype"], "");
//                rptRow["Wt"] = wt.ToString("0.000") + "KG " + m3.ToString("0.000") + "CBM";
//                rptRow["Pack"] = qty + " " + packType;
//                rptRow["RefN"] = jobN + "/" + refN;
//                rptRow["CrRate"] = SafeValue.SafeDecimal(tab_mast.Rows[0]["ExRate"],0).ToString("0.000");
//                rptRow["Us"] = tab_mast.Rows[0]["Us"];
//                rptRow["ContN"] =contN;
//                rptRow["Amount"] = amt.ToString("0.00");

//                rptRow["Gd1"] = SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("SELECT Description FROM SeaImportMkg WHERE (RefNo = '{0}') AND (JobNo = '{1}')",refN,jobN)), "");

//                rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                Mast.Rows.Add(rptRow);
//            }

//        }
//        catch (Exception ex)
//        {
//        }

//        DataSet ds = new DataSet();
//        ds.Tables.Add(Mast);
//        ds.Tables.Add(Detail);
//        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
//        ds.Relations.Add(r);


//        return ds;
    }
    public static DataSet PrintCertificate(string refN, string jobN, string impExp,string userId)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintCertificate '{0}','{1}'", refN, userId);
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["JobNo"], Detail.Columns["JobNo"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }

        return ds;

    }
    public static DataSet PrintSeaImport(string refN, string jobN,string refType, string userId)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintSeaImport '{0}','{1}','{2}','{3}','{4}'", refN, jobN, refType,userId, System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["JobNo"], Detail.Columns["JobNo"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }

        return ds;

    }
    #endregion
    #region import/Export HOUSE P&L
    public static DataSet PrintPl_house(string refN, string jobNo,string refType, string userId)
    {
        int jobCnt = 1;
        decimal m3Percent = 0;
        if (refType == "SI")
        {
            jobCnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(string.Format("select count(jobNo) from SeaImport where RefNo='{0}'", refN)), 1);
            m3Percent = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(string.Format(@"select (case when job.Weight/1000>job.volume then job.Weight/1000 else job.volume end)/ case when mast.Weight>0 then (case when mast.Weight/1000>mast.volume then mast.Weight/1000 else mast.volume end)  when mast.volume>0 then (case when mast.Weight/1000>mast.volume then mast.Weight/1000 else mast.volume end) else 1 end
 from SeaImport as job inner join SeaImportRef mast on mast.RefNo=job.RefNo where job.RefNo='{0}' and Job.JobNo='{1}'", refN, jobNo)), 1);
        }
        else if (refType == "SE")
        {
            jobCnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(string.Format("select count(jobNo) from SeaExport where RefNo='{0}'", refN)), 1);
            m3Percent = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(string.Format(@"select (case when job.Weight/1000>job.volume then job.Weight/1000 else job.volume end)/ case when mast.Weight>0 then (case when mast.Weight/1000>mast.volume then mast.Weight/1000 else mast.volume end)  when mast.volume>0 then (case when mast.Weight/1000>mast.volume then mast.Weight/1000 else mast.volume end) else 1 end
 from SeaExport as job inner join SeaExportRef mast on mast.RefNo=job.RefNo where job.RefNo='{0}' and Job.JobNo='{1}'", refN, jobNo)), 1);
        }
        if (jobCnt == 0)
            jobCnt = 1;
        DataSet set = new DataSet();
        DataTable tab_mast = Pl_Mast_house(refN, jobNo,refType, jobCnt, m3Percent, userId);
        DataTable tab_Inv = Pl_Inv_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Dn = Pl_Dn_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Ts = Pl_Ts_house(refN, jobNo, refType);
        DataTable tab_Cn = Pl_Cn_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Pl = Pl_Pl_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Vo = Pl_Vo_house(refN, jobNo, refType, jobCnt, m3Percent);
        DataTable tab_Cost = Pl_Cost_house(refN, jobNo, refType, jobCnt, m3Percent);
        tab_mast.TableName = "Mast";
        tab_Inv.TableName = "IV";
        tab_Ts.TableName = "TS";
        tab_Cn.TableName = "CN";
        tab_Dn.TableName = "DN";
        tab_Pl.TableName = "PL";
        tab_Vo.TableName = "VO";
        tab_Cost.TableName = "COST";
        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_Inv);
        set.Tables.Add(tab_Dn);
        set.Tables.Add(tab_Ts);
        set.Tables.Add(tab_Cn);
        set.Tables.Add(tab_Pl);
        set.Tables.Add(tab_Vo);
        set.Tables.Add(tab_Cost);
        return set;
    }
    private static DataTable Pl_Mast_house(string refN, string jobNo,string refType, int jobCnt, decimal m3Percent, string userId)
    {
        DataTable tab = new DataTable("PlMast");
        tab.Columns.Add("RefN");
        tab.Columns.Add("NowD");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Currency");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("JobType");
        tab.Columns.Add("TsM3");
        tab.Columns.Add("LocalM3");

        tab.Columns.Add("Agent");
        tab.Columns.Add("Company");
        tab.Columns.Add("Obl");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Eta");
        tab.Columns.Add("Qty");
        tab.Columns.Add("Pack");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("Pol");
        tab.Columns.Add("Pod");
        tab.Columns.Add("ContN");

        tab.Columns.Add("Rev1");
        tab.Columns.Add("Rev2");
        tab.Columns.Add("Rev3");
        tab.Columns.Add("Rev4");
        tab.Columns.Add("Rev");

        tab.Columns.Add("Cost1");
        tab.Columns.Add("Cost2");
        tab.Columns.Add("Cost3");
        tab.Columns.Add("Cost");
        tab.Columns.Add("Profit");
        string sql = string.Format(@"SELECT job.JobNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,mast.CreateBy as UserId,Mast.CurrencyId as Currency,mast.ExRate,mast.JobType
,dbo.fun_GetPartyName(job.customerid) as Agent,'' as Company,mast.OblNo as Obl,mast.Vessel+'/'+mast.Voyage as Ves,convert(nvarchar(10),mast.Eta,103) as Eta,dbo.fun_GetPortName(mast.Pol) as Pol,dbo.fun_GetPortName(mast.Pod) as Pod,'' ContN
,job.Qty,job.PackageType as Pack,job.Weight as Wt,job.Volume as M3
,(Case when Job.TsInd='Y' then job.Volume else 0 end) as TsM3
,(Case when Job.TsInd='N' then job.Volume else 0 end) as LocalM3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SI' and DocType='IV') Rev1
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsAgtRate),2)),0) FROM SeaImport  WHERE (RefNo = mast.RefNo) AND (TsAgtRate > 0))*mast.ExRate as Rev2
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SI' and DocType='DN') Rev3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SI' and DocType='CN') Rev4
,(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SI' and (DocType='PL' or DocType='SD' or DocType='VO'))-(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo  and MastType = 'SI' and DocType='SC')  as Cost1
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsImpRate),2)),0) FROM SeaImport WHERE (RefNo = mast.RefNo) AND (TsImpRate > 0))*mast.ExRate as Cost2
,( SELECT sum(CostLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobNo=job.JobNo and JobType ='SI') Cost3
FROM SeaImport job inner join SeaImportRef mast on job.RefNo=mast.RefNo
where job.RefNo='{0}' and job.JobNo='{1}'", refN, jobNo);

        string sql3 = string.Format("SELECT distinct ContainerNo + '/' + SealNo+'/'+ContainerType FROM SeaImportMkg WHERE RefNo = '{0}' and JobNo='{1}'", refN, jobNo); ;
        if (refType == "SE")
        {
            sql = string.Format(@"SELECT job.JobNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,mast.CreateBy as UserId,Mast.CurrencyId as Currency,mast.ExRate,mast.JobType
,dbo.fun_GetPartyName(job.customerid) as Agent,'' as Company,mast.OblNo as Obl,mast.Vessel+'/'+mast.Voyage as Ves,convert(nvarchar(10),mast.Eta,103) as Eta,dbo.fun_GetPortName(mast.Pol) as Pol,dbo.fun_GetPortName(mast.Pod) as Pod,'' ContN
,job.Qty,job.PackageType as Pack,job.Weight as Wt,job.Volume as M3
,(Case when Job.TsInd='Y' then job.Volume else 0 end) as TsM3
,(Case when Job.TsInd='N' then job.Volume else 0 end) as LocalM3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SE' and DocType='IV') Rev1
,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * ImpCharge),2)),0) FROM SeaExport  WHERE (RefNo = mast.RefNo) AND JobNo=job.JobNo AND (ImpCharge > 0))*mast.ExRate as Rev2
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SE' and DocType='DN') Rev3
,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SE' and DocType='CN') Rev4
,(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SE' and (DocType='PL' or DocType='SD' or DocType='VO'))-(SELECT isnull(sum(LineLocAmt),0) FROM XAApPayableDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo  and MastType = 'SE' and DocType='SC')  as Cost1
,0 as Cost2
,( SELECT sum(CostLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobNo=job.JobNo and JobType ='SE') Cost3
FROM SeaExport job inner join SeaExportRef mast on job.RefNo=mast.RefNo
where job.RefNo='{0}' and job.JobNo='{1}'", refN, jobNo);

            sql3 = string.Format("SELECT distinct ContainerNo + '/' + SealNo+'/'+ContainerType FROM SeaExportMkg WHERE RefNo = '{0}' and JobNo='{1}' and MkgType!='Bkg'", refN, jobNo); ;
        }
        DataTable dt = ConnectSql.GetTab(sql);
        DataRow row1 = tab.NewRow();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            string colName = dt.Columns[i].ColumnName;
            row1[colName] = dt.Rows[0][i];
        }
        tab.Rows.Add(row1);
        if (tab.Rows.Count > 0)
        {
            DataRow row = tab.Rows[0];
            decimal allM3 = SafeValue.SafeDecimal(tab.Rows[0]["M3"], 0);
            decimal transM3 = SafeValue.SafeDecimal(tab.Rows[0]["TsM3"], 0);

            if (allM3 == 0)
            {
                row["TsM3"] = "";
                row["LocalM3"] = "";
            }
            else
            {
                row["TsM3"] = transM3 + " - " + (transM3 * 100 / allM3).ToString("0.00") + "%";
                row["LocalM3"] = (allM3 - transM3).ToString("0.000") + " - " + ((allM3 - transM3) * 100 / allM3).ToString("0.00") + "%";
            }

            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            string contN = "";
            DataTable dt3 = ConnectSql.GetTab(sql3);
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                contN += dt3.Rows[i][0].ToString();
                if (i != dt3.Rows.Count - 1)
                    contN += "\n";
            }
            row["ContN"] = contN;


            decimal rev1 = SafeValue.SafeDecimal(tab.Rows[0]["Rev1"], 0); //invocie
            decimal rev2 = SafeValue.SafeDecimal(tab.Rows[0]["Rev2"], 0); //ts rate
            decimal rev3 = SafeValue.SafeDecimal(tab.Rows[0]["Rev3"], 0); //dn
            decimal rev4 = SafeValue.SafeDecimal(tab.Rows[0]["Rev4"], 0); //cn
            string sql_rev = string.Format(@"SELECT DocType
,isnull(sum(Case when SplitType='Set' then LineLocAmt/{2} else LineLocAmt*{3} end ),0) as Amt
 FROM XaArInvoiceDet  WHERE MastRefNo = '{0}' and (JobRefNo='0' or JobRefNo='') and MastType = '{4}' group by DocType", refN,jobNo,jobCnt,m3Percent,refType);
            DataTable tab_rev = ConnectSql.GetTab(sql_rev);
            for (int j = 0; j < tab_rev.Rows.Count; j++)
            {
                string docType = SafeValue.SafeString(tab_rev.Rows[j]["DocType"]).ToUpper() ;
                decimal amt=SafeValue.SafeDecimal(tab_rev.Rows[j]["Amt"],0);

                if (docType == "IV")
                    rev1 += amt;
                else if (docType == "DN")
                    rev3 += amt;
                else if (docType == "CN")
                    rev4 += amt;
            }
            row["Rev1"] = rev1.ToString("###,##0.00");
            row["Rev2"] = rev2.ToString("###,##0.00");
            row["Rev3"] = rev3.ToString("###,##0.00");
            row["Rev4"] = "(" + rev4.ToString("###,##0.00") + ")";
            decimal sumRev = rev1 + rev2 + rev3 - rev4;
            row["Rev"] = sumRev.ToString("###,##0.00");


            decimal cost1 = SafeValue.SafeDecimal(tab.Rows[0]["Cost1"], 0); //ap
            decimal cost2 = SafeValue.SafeDecimal(tab.Rows[0]["Cost2"], 0); //
            decimal cost3 = SafeValue.SafeDecimal(tab.Rows[0]["Cost3"], 0); //costing
            string sql_ap = string.Format(@"SELECT DocType
,isnull(sum(Case when SplitType='Set' then LineLocAmt/{2} else LineLocAmt*{3} end ),0) as Amt
 FROM XaApPayableDet  WHERE MastRefNo = '{0}' and (JobRefNo='0' or JobRefNo='') and MastType = '{4}' group by DocType", refN, jobNo, jobCnt, m3Percent,refType);
            DataTable tab_ap = ConnectSql.GetTab(sql_ap);
            for (int j = 0; j < tab_ap.Rows.Count; j++)
            {
                string docType = SafeValue.SafeString(tab_ap.Rows[j]["DocType"]).ToUpper();
                decimal amt = SafeValue.SafeDecimal(tab_ap.Rows[j]["Amt"], 0);
                if (docType == "SC")
                    cost1 -= amt;
                else
                    cost1 += amt;
            }
            string sql_cost = string.Format(@"SELECT sum(Cast(case when SplitType='Set' then CostLocAmt/{2}
	       else CostLocAmt*{3}  end as numeric(38,2))) as Amount FROM SeaCosting  where RefNo='{0}' and (JobNo='0' or JobNo='') and JobType='{4}'", refN,jobNo,jobCnt,m3Percent,refType);
            cost3 += SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql_cost),0);
            row["Cost1"] = cost1.ToString("###,##0.00");
            row["Cost2"] = cost2.ToString("###,##0.00");
            row["Cost3"] = cost3.ToString("###,##0.00");
            decimal cost = cost1 + cost2 + cost3;
            row["Cost"] = cost.ToString("###,##0.00");
            row["Profit"] = (sumRev - cost).ToString("###,##0.00");
        }
        return tab;
    }

    private static DataTable Pl_Inv_house(string refN, string jobNo,string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, det.ChgCode ,det.ChgDes1 as ChgDes
,Cast(case when det.JobRefNo='{1}' then det.LineLocAmt 
	  when det.SplitType='Set' then det.LineLocAmt/{2}
	  else det.LineLocAmt*{3} end as numeric(38,2)) as Amount
FROM XAArInvoiceDet AS det INNER JOIN XAArInvoice AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0') AND (mast.MastType = '{4}') and mast.DocType='IV' order by mast.DocNo "
            , refN, jobNo, jobCnt, m3Percent,refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Dn_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, det.ChgCode ,det.ChgDes1 as ChgDes
,Cast(case when det.JobRefNo='{1}' then det.LineLocAmt 
	  when det.SplitType='Set' then det.LineLocAmt/{2}
	  else det.LineLocAmt*{3} end as numeric(38,2)) as Amount
FROM XAArInvoiceDet AS det INNER JOIN XAArInvoice AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0') AND (mast.MastType = '{4}') and mast.DocType='DN'  order by mast.DocNo"
            , refN, jobNo, jobCnt, m3Percent, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Ts_house(string refN, string jobNo, string refType)
    {
        DataTable tab = new DataTable("Ts");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Pod");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("AgtRate");
        tab.Columns.Add("Amount");
        tab.Columns.Add("Currency");

        string sql = "";
        if (refType == "SI")
        {
            sql = string.Format(@"SELECT HBLNo AS Hbl, TsVessel+'/'+TsVoyage as Ves,dbo.fun_GetPortName(TsPod) as Pod,Volume AS M3, Weight AS WT,TsAgtRate AS AgtRate
,cast((Case when Weight/1000>Volume then Weight/1000 else Volume end)*TsAgtRate as numeric(38,2)) as Amount
,(Select CurrencyId from SeaImportRef where RefNo=SeaImport.RefNo) as Currency 
FROM SeaImport  WHERE (RefNo = '{0}') and JobNo='{1}' AND (TsAgtRate > 0) and TsInd='Y' ", refN, jobNo);
            DataTable dt = ConnectSql.GetTab(sql);
            tab = dt.Copy();
        }
        return tab;
    }

    private static DataTable Pl_Cn_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, det.ChgCode ,det.ChgDes1 as ChgDes
,Cast(case when det.JobRefNo='{1}' then det.LineLocAmt 
	  when det.SplitType='Set' then det.LineLocAmt/{2}
	  else det.LineLocAmt*{3} end as numeric(38,2)) as Amount
FROM XAArInvoiceDet AS det INNER JOIN XAArInvoice AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0') AND (mast.MastType = '{4}') and mast.DocType='CN' order by mast.DocNo"
            , refN, jobNo, jobCnt, m3Percent, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Pl_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, det.ChgCode ,det.ChgDes1 as ChgDes
,Cast(case when mast.DocType='SC' and det.JobRefNo='{1}' then -det.LineLocAmt
	       when mast.DocType='SC' and det.SplitType='Set' and (det.JobRefNo='' or det.JobRefNo='0') then -det.LineLocAmt/{2}
	       when mast.DocType='SC' and det.SplitType!='Set' and (det.JobRefNo='' or det.JobRefNo='0') then -det.LineLocAmt*{3}
	       
	       when (mast.DocType='PL' or Mast.DocType='SD') and det.JobRefNo='{1}' then det.LineLocAmt
	       when (mast.DocType='PL' or Mast.DocType='SD') and det.SplitType='Set' and (det.JobRefNo='' or det.JobRefNo='0') then det.LineLocAmt/{2}
	       when (mast.DocType='PL' or Mast.DocType='SD') and det.SplitType!='Set' and (det.JobRefNo='' or det.JobRefNo='0') then det.LineLocAmt*{3}
		   end as numeric(38,2)) as Amount
FROM XAApPayableDet AS det INNER JOIN XAApPayable AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0')and (mast.DocType='PL' or mast.DocType='SC' OR mast.DocType='SD') AND (mast.MastType = '{4}') 
 order by mast.DocNo", refN, jobNo, jobCnt, m3Percent, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Vo_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("ChgCode");
        tab.Columns.Add("ChgDes");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT mast.DocNo AS BillNo, det.ChgCode ,det.ChgDes1 as ChgDes
,Cast(case when mast.DocType='VO' and det.JobRefNo='{1}' then det.LineLocAmt
	       when mast.DocType='VO' and det.SplitType='Set' and (det.JobRefNo='' or det.JobRefNo='0') then det.LineLocAmt/{2}
	       when mast.DocType='VO' and det.SplitType!='Set' and (det.JobRefNo='' or det.JobRefNo='0') then det.LineLocAmt*{3}
		   end as numeric(38,2)) as Amount
FROM XAApPayableDet AS det INNER JOIN XAApPayable AS mast on mast.SequenceId = det.DocId
WHERE (det.MastRefNo = '{0}') and (det.JobRefNo='{1}' or det.JobRefNo='' or det.JobRefNo='0')and (mast.DocType='VO') AND (mast.MastType = '{4}') 
 order by mast.DocNo", refN, jobNo, jobCnt, m3Percent, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    private static DataTable Pl_Cost_house(string refN, string jobNo, string refType, int jobCnt, decimal m3Percent)
    {
        DataTable tab = new DataTable("Costing");
        tab.Columns.Add("Des");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT  ChgCodeDes+Remark as Des
,Cast(case when JobNo='{1}' then CostLocAmt
	       when SplitType='Set' and (JobNo='' or JobNo='0') then CostLocAmt/{2}
	       when SplitType!='Set' and (JobNo='' or JobNo='0') then CostLocAmt*{3}
		   end as numeric(38,2)) as Amount
FROM SeaCosting  Where RefNo='{0}' and (JobNo='{1}' OR JobNo='' or JobNo='0') and JobType='{4}' and CostLocAmt>0", refN, jobNo, jobCnt, m3Percent, refType);
        DataTable dt = ConnectSql.GetTab(sql);
        tab = dt.Copy();
        return tab;
    }
    #endregion

    #region export sch
    public static DataSet dsLoadPlan(string refNo)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_dsLoadPlan '{0}','{1}','{2}','{3}','{4}'", refNo, "", "", "", "");
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("Rela", Mast.Columns["TrxNo"], Detail.Columns["TrxNo"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;


//        DataTable mast = new DataTable("Mast");
//        mast.Columns.Add("TrxNo");

//        mast.Columns.Add("JobNo");
//        mast.Columns.Add("CargoType");
//        mast.Columns.Add("Etd");
//        mast.Columns.Add("Eta");
//        mast.Columns.Add("NoCont");
//        mast.Columns.Add("Stuff");
//        mast.Columns.Add("PrintDate");
//        mast.Columns.Add("Pod");
//        mast.Columns.Add("CrBkgRef");
//        mast.Columns.Add("Carrier");
//        mast.Columns.Add("Ves");
//        mast.Columns.Add("Voy");
//        mast.Columns.Add("Note");
//        mast.Columns.Add("UserId");

//        mast.Columns.Add("TotWt");
//        mast.Columns.Add("TotQty");
//        mast.Columns.Add("TotM3");
//        DataTable det = new DataTable("Detail");
//        det.Columns.Add("TrxNo");
//        det.Columns.Add("LineN", typeof(int));
//        det.Columns.Add("BkgNo");

//        det.Columns.Add("Shipper");
//        det.Columns.Add("Contact");
//        det.Columns.Add("Tel");
//        det.Columns.Add("Fax");

//        det.Columns.Add("Qty");
//        det.Columns.Add("Wt");
//        det.Columns.Add("M3");

//        det.Columns.Add("Rmk");
//        det.Columns.Add("Rail");


//        string sql = string.Format(@"SELECT JobType,Vessel, Voyage, RefNo, Eta, CrBkgNo, Remark, Pod, CrAgentId, NvoccAgentId, PodAgentId, FinDest, Etd, EtaDest, StuffDate, UserId, EntryDate
//FROM  SeaExportRef WHERE RefNo = '{0}'", refNo);
//        string sql1 = string.Format(@"SELECT BkgRefNo, JobNo,ShipperId,ShipperName, ShipperContact, ShipperTel,ShipperFax, Qty, PackageType, Volume, Weight, Remark, FinDest
//FROM SeaExport where StatusCode='USE' and (RefNo = '{0}') ", refNo);
//        DataTable tab = ConnectSql.GetTab(sql);

//        if (tab.Rows.Count > 0)
//        {
//            int qty = 0;
//            decimal wt = 0;
//            decimal m3 = 0;
//            DataTable tab1 = ConnectSql.GetTab(sql1);
//            for (int i = 0; i < tab1.Rows.Count; i++)
//            {
//                DataRow row1 = tab1.Rows[i];
//                DataRow rowDet = det.NewRow();
//                rowDet["TrxNo"] = refNo;
//                rowDet["LineN"] = i + 1;
//                rowDet["BkgNo"] = SafeValue.SafeString(row1["BkgRefNo"], "");
//                rowDet["Shipper"] = row1["ShipperName"];
//                rowDet["Contact"] = SafeValue.SafeString(row1["ShipperContact"], "");
//                rowDet["Tel"] = SafeValue.SafeString(row1["ShipperTel"], "");
//                rowDet["Fax"] = SafeValue.SafeString(row1["ShipperFax"], "");
//                string jobNo = SafeValue.SafeString(row1["JobNo"]);
//                string sql_cont = string.Format("select sum(qty) Qty,sum(Weight) Weight,sum(Volume) Volume,max(PackageType) PackageType from seaexportmkg where MkgType='bkg' and RefNo='{0}' and JobNo='{1}'", refNo, jobNo);
//                DataTable tab_cont = C2.Manager.ORManager.GetDataSet(sql_cont).Tables[0];
//                if (tab_cont.Rows.Count == 1)
//                {
//                    rowDet["Qty"] = SafeValue.SafeString(tab_cont.Rows[0]["Qty"]) + "x" + SafeValue.SafeString(tab_cont.Rows[0]["PackageType"], "");
//                    rowDet["Wt"] = SafeValue.SafeDecimal(tab_cont.Rows[0]["Weight"], 0).ToString("0.000");
//                    rowDet["M3"] = SafeValue.SafeDecimal(tab_cont.Rows[0]["Volume"], 0).ToString("0.000");
//                    qty += SafeValue.SafeInt(tab_cont.Rows[0]["Qty"], 0);
//                    m3 += SafeValue.SafeDecimal(tab_cont.Rows[0]["Volume"], 0);
//                    wt += SafeValue.SafeDecimal(tab_cont.Rows[0]["Weight"], 0);
//                }


//                string rmk = SafeValue.SafeString(row1["Remark"], "");
//                rowDet["Rmk"] = rmk;

//                det.Rows.Add(rowDet);
//            }
//            DataRow row = tab.Rows[0];
//            DataRow rowMast = mast.NewRow();



//            rowMast["TrxNo"] = refNo;
//            rowMast["JobNo"] = row["RefNo"];
//            rowMast["Etd"] = SafeValue.SafeDateStr(row["Etd"]);
//            rowMast["Eta"] = SafeValue.SafeDateStr(row["Eta"]);
//            string jobType=row["JobType"].ToString();
//            rowMast["CargoType"] = jobType;
//            if (jobType == "FCL")
//            {
//                string sql_cont = string.Format("select sum(qty) Qty,PackageType from seaexportmkg where MkgType='bkg' and RefNo='{0}' Group By PackageType", refNo);
//                DataTable tab_cont = C2.Manager.ORManager.GetDataSet(sql_cont).Tables[0];
//                for (int c = 0; c < tab_cont.Rows.Count; c++)
//                {
//                    if (c == 0)
//                        rowMast["NoCont"] = SafeValue.SafeString(tab_cont.Rows[c]["Qty"]) + SafeValue.SafeString(tab_cont.Rows[c]["PackageType"]);
//                    else
//                        rowMast["NoCont"] += "," + SafeValue.SafeString(tab_cont.Rows[c]["Qty"]) + SafeValue.SafeString(tab_cont.Rows[c]["PackageType"]);

//                }
//            }
//            else
//            {
//                //rowMast["NoCont"] = SafeValue.SafeString(row["NoOfCont"], "");
//            }
//            rowMast["Stuff"] = SafeValue.SafeDateStr(row["StuffDate"]);
//            rowMast["PrintDate"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

//            rowMast["Pod"] = EzshipHelper.GetPortName(row["Pod"]);
//            rowMast["CrBkgRef"] = row["CrBkgNo"];
//            string crAgtId = SafeValue.SafeString(row["CrAgentId"], "");
//            rowMast["Carrier"] = EzshipHelper.GetPartyName(crAgtId);

//            rowMast["Ves"] = row["Vessel"];
//            rowMast["Voy"] = row["Voyage"];
//            rowMast["Note"] = SafeValue.SafeString(row["Remark"], "");
//            rowMast["UserId"] = row["UserId"];
//            rowMast["TotWt"] = wt.ToString("0.000");
//            rowMast["TotQty"] = qty;
//            rowMast["TotM3"] = m3.ToString("0.000");


//            mast.Rows.Add(rowMast);

//        }
//        DataSet set1 = new DataSet();
//        set1.Tables.Add(mast);
//        set1.Tables.Add(det);
//        set1.Relations.Add("Rela", mast.Columns["TrxNo"], det.Columns["TrxNo"]);
//        return set1;

    }
    public static DataTable dsSo(string refNo, string jobNo)
    {
        DataTable dt = new DataTable();
        try
        {
            string strsql = string.Format(@"exec proc_PrintExpShippingOrder '{0}','{1}','{2}','{3}','{4}'", refNo, jobNo, "SE", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            dt = ds_temp.Tables[0].Copy();
            dt.TableName = "Mast";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["CustAdd"] = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
            }
        }
        catch (Exception ex) { }
        return dt;



//        DataTable mast = new DataTable("Mast");
//        mast.Columns.Add("Shipper");
//        mast.Columns.Add("BkgRefNo");
//        mast.Columns.Add("LineN");
//        mast.Columns.Add("Cust");
//        mast.Columns.Add("CustAdd");
//        mast.Columns.Add("Cng");
//        mast.Columns.Add("CltFrm");
//        mast.Columns.Add("Notify");
//        mast.Columns.Add("DeliverTo");
//        mast.Columns.Add("Ves");
//        mast.Columns.Add("Pol");
//        mast.Columns.Add("Pod");
//        mast.Columns.Add("FinDest");
//        mast.Columns.Add("Mkg");
//        mast.Columns.Add("Des");
//        mast.Columns.Add("Wt");
//        mast.Columns.Add("M3");
//        mast.Columns.Add("Eta");
//        mast.Columns.Add("Etd");
//        mast.Columns.Add("EtaDest");
//        mast.Columns.Add("CompanyName");
//        string sql_mkg = string.Format("select weight,volume,qty,packagetype,marking,description,Remark from seaexportmkg where MkgType='Bkg' and RefNo='{0}' and JobNo='{1}'",refNo,jobNo);
//        string  sql = string.Format(@"SELECT Vessel, Voyage, FinDest,Pol,Pod,WarehouseId,Eta,Etd,EtaDest FROM  SeaExportRef WHERE RefNo = '{0}'", refNo);
//        string sql1 = string.Format(@"SELECT SequenceId,BkgRefNo, ShipperId, ShipperName,ShipperContact, ShipperTel,ShipperContact,AsAgent,Remark, UpdateDateTime, FinDest,remark
//,CollectFrom,Marking
//FROM SeaExport where JobNo='{0}'", jobNo);
//        DataTable tab_bkg = ConnectSql.GetTab(sql_mkg);
//        for (int b = 0; b < tab_bkg.Rows.Count; b++)
//        {
//            DataTable tab = ConnectSql.GetTab(sql);
//            DataRow rowMast = mast.NewRow();
//            if (tab.Rows.Count > 0)
//            {
//                DataRow master = tab.Rows[0];

//                rowMast["Cust"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                rowMast["CustAdd"] = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
//                rowMast["Cng"] = "SAME AS ABOVE";
//                rowMast["Notify"] = "SAME AS ABOVE";
//                //string whId = SafeValue.SafeString(master["WarehouseId"], "");
//                //string sql_Vendor = string.Format("select name ,address from XXParty where PartyId='{0}'",whId);
//                //DataTable tab_Vendor = ConnectSql.GetTab(sql_Vendor);
//                //if (tab_Vendor.Rows.Count == 1)
//                //{
//                //    rowMast["DeliverTo"] = SafeValue.SafeString(tab_Vendor.Rows[0]["Name"], "");
//                //    rowMast["DeliverTo"] += "\n" + SafeValue.SafeString(tab_Vendor.Rows[0]["Address"], "");
//                //}
//                rowMast["Ves"] = SafeValue.SafeString(master["Vessel"], "") + "/" + SafeValue.SafeString(master["Voyage"], "");
//                rowMast["Pol"] = EzshipHelper.GetPortName(master["Pol"]);
//                rowMast["Pod"] = EzshipHelper.GetPortName(master["Pod"]);
//                //rowMast["FinDest"] = EzshipHelper.GetPortName(master["FinDest"], master["FinDest"]);
//                rowMast["Eta"] = SafeValue.SafeDateStr(master["Eta"]);
//                rowMast["Etd"] = SafeValue.SafeDateStr(master["Etd"]);
//                rowMast["EtaDest"] = SafeValue.SafeDateStr(master["EtaDest"]);

//                rowMast["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//            }
//            // DETAIL
//            DataTable tab1 = ConnectSql.GetTab(sql1);
//            if (tab1.Rows.Count > 0)
//            {
//                DataRow det = tab1.Rows[0];
//                rowMast["CltFrm"] = det["CollectFrom"];
//                rowMast["DeliverTo"] = det["Marking"];
//                rowMast["FinDest"] = EzshipHelper.GetPortName(det["FinDest"], det["FinDest"]);
//                if (SafeValue.SafeString(det["AsAgent"], "") == "N")
//                {
//                    rowMast["Shipper"] = EzshipHelper.GetPartyName(det["ShipperId"]);
//                }
//                else
//                {
//                    rowMast["Shipper"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"] + "\n   AS AGENTS ONLY";
//                }
//                rowMast["LineN"] = det["SequenceId"].ToString();
//                rowMast["BkgRefNo"] = SafeValue.SafeString(det["BkgRefNo"], "");
//                rowMast["Mkg"] = "";
//                //rowMast["Des"] = "SAID TO CONTAIN : \n " + SafeValue.SafeString(det["Qty"], "") + SafeValue.SafeString(det["PackageType"], "") + " \n " + SafeValue.SafeString(det["Remark"], "");
//                //rowMast["Wt"] = SafeValue.SafeDouble(det["Weight"], 0).ToString("0.000");
//                //rowMast["M3"] = SafeValue.SafeDouble(det["Volume"], 0).ToString("0.000");
//            }
//            rowMast["Des"] = "SAID TO CONTAIN : \n " + SafeValue.SafeString(tab_bkg.Rows[b]["Qty"], "") +"x"+ SafeValue.SafeString(tab_bkg.Rows[b]["PackageType"], "") + " \n " + SafeValue.SafeString(tab_bkg.Rows[b]["Remark"], "");
//            rowMast["Wt"] = SafeValue.SafeDecimal(tab_bkg.Rows[b]["Weight"], 0).ToString("0.000");
//            rowMast["M3"] = SafeValue.SafeDecimal(tab_bkg.Rows[b]["Volume"], 0).ToString("0.000");
//           mast.Rows.Add(rowMast);
//        }
//        return mast;
    }
    public static DataSet PrintBookingConfirm_FCL(string refN, string jobN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintBookingConfirm '{0}','{1}','{2}','{3}','{4}'", refN, jobN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("rela", Mast.Columns["RefNo"], Detail.Columns["RefNo"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;
    }
    public static DataSet PrintBookingConfirm(string refN, string jobN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintBookingConfirm '{0}','{1}','{2}','{3}','{4}'", refN, jobN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("rela", Mast.Columns["RefNo"], Detail.Columns["RefNo"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;




//        DataTable master = new DataTable("Mast");
//        master.Columns.Add("RefNo");
//        master.Columns.Add("NowD");
//        master.Columns.Add("Shipper");
//        master.Columns.Add("Contact");
//        master.Columns.Add("Fax");
//        master.Columns.Add("Tel");

//        master.Columns.Add("BkgN");
//        master.Columns.Add("CrBkgNo");

//        master.Columns.Add("Ves");
//        master.Columns.Add("PortLoad");
//        master.Columns.Add("PortDis");
//        master.Columns.Add("Fin");
//        master.Columns.Add("Eta");
//        master.Columns.Add("Etd");
//        master.Columns.Add("EtaD");

//        master.Columns.Add("Carrier");
//        master.Columns.Add("CrBlNo");
//        master.Columns.Add("PortnetNo");

//        master.Columns.Add("CltFrm");
//        master.Columns.Add("DeliveryTo");
//        master.Columns.Add("Warehouse");
//        master.Columns.Add("Rmk");
//        master.Columns.Add("Us");
//        master.Columns.Add("CompanyName");
//        DataTable det = new DataTable("Detail");
//        det.Columns.Add("RefNo");
//        det.Columns.Add("Qty");
//        det.Columns.Add("PkgType");
//        det.Columns.Add("Wt");
//        det.Columns.Add("M3");
//        det.Columns.Add("Des");
//        det.Columns.Add("GrossWt");
//        det.Columns.Add("NetWt");
//        string sql = string.Format(@"SELECT mast.RefNo,job.BkgRefNo, mast.Vessel + '/' + mast.Voyage AS Ves, mast.Pol AS Pol, mast.Pod AS Pod, job.FinDest AS Fin, mast.Eta, mast.Etd, mast.EtaDest AS EtaD, 
//                      mast.WarehouseId AS Warehouse,mast.CrAgentId AS Consignee,mast.PortnetNo, job.ShipperName, job.ShipperContact AS Contact, job.ShipperFax AS Fax, job.ShipperTel AS Tel, mast.CrBkgNo AS CrbkgNo,
//                        mkg.GrossWt,mkg.NetWt,mkg.Weight AS Wt, mkg.Volume AS M3, mkg.Qty, mkg.PackageType AS Packtype, job.CreateBy as UserId,mkg.Marking as Des
//,job.Marking as DeliveryTo,Job.CollectFrom,job.Marking as Rmk
//FROM SeaExportMkg AS mkg INNER JOIN
//                      SeaExport AS job ON mkg.RefNo = job.refno and mkg.Jobno=job.JobNo INNER JOIN
//                      SeaExportRef as Mast on mkg.RefNo = mast.refno 
//WHERE mkg.RefNo = '{0}' and mkg.Jobno='{1}' and Mkg.MkgType='Bkg'", refN, jobN);
//        DataTable tab = ConnectSql.GetTab(sql);
//        for (int i = 0; i < tab.Rows.Count; i++)
//        {
//            DataRow exportRef = tab.Rows[i];
//            if (i == 0)
//            {
//                DataRow row = master.NewRow();
//                row["RefNo"] = exportRef["RefNo"];
//                row["NowD"] = DateTime.Today.ToString("dd/MM/yyyy");
//                row["Shipper"] = exportRef["ShipperName"];
//                row["Contact"] = exportRef["Contact"];
//                row["Fax"] = exportRef["Fax"];
//                row["Tel"] = exportRef["Tel"];
//                row["BkgN"] = exportRef["BkgRefNo"];
//                row["CrBkgNo"] = exportRef["CrBkgNo"];

//                row["Ves"] = exportRef["Ves"];
//                row["PortLoad"] = EzshipHelper.GetPortName(exportRef["Pol"], System.Configuration.ConfigurationManager.AppSettings["PortName"]);
//                row["PortDis"] = EzshipHelper.GetPortName(exportRef["Pod"], exportRef["Pod"].ToString());
//                row["Fin"] = EzshipHelper.GetPortName(exportRef["Fin"], exportRef["Fin"].ToString());
//                row["Eta"] = SafeValue.SafeDateStr(exportRef["Eta"]);
//                row["Etd"] = SafeValue.SafeDateStr(exportRef["Etd"]);
//                row["EtaD"] = SafeValue.SafeDate(exportRef["EtaD"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");

//                string carrierId = exportRef["Consignee"].ToString();
//                row["Carrier"] = EzshipHelper.GetPartyName(carrierId);

//                row["CrBlNo"] = exportRef["CrBkgNo"];
//                row["PortnetNo"] = exportRef["PortnetNo"];
//                row["CltFrm"] = exportRef["CollectFrom"];
//                row["DeliveryTo"] = exportRef["DeliveryTo"];
//                row["Rmk"] = exportRef["Rmk"];
//                row["Us"] = exportRef["UserId"];
//                row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//                master.Rows.Add(row);
//            }


//            DataRow rowDet = det.NewRow();
//            rowDet["RefNo"] = exportRef["RefNo"];
//            rowDet["Qty"] = exportRef["Qty"].ToString();
//            rowDet["PkgType"] = exportRef["Packtype"].ToString();
//            rowDet["M3"] = SafeValue.SafeDecimal(exportRef["M3"], 0).ToString("0.000");
//            rowDet["Wt"] = SafeValue.SafeDecimal(exportRef["Wt"],0).ToString("0.000");
//            decimal grossWt = SafeValue.SafeDecimal(exportRef["GrossWt"], 0);
//            decimal netWt=SafeValue.SafeDecimal(exportRef["NetWt"], 0);
//            if(grossWt>0)
//                rowDet["Wt"] += "\nGross Weight:" + grossWt.ToString("0.000");
//            if (netWt > 0)
//                rowDet["Wt"] += "\nNet Weight:" + netWt.ToString("0.000");
//            rowDet["Des"] = exportRef["Des"];
//            det.Rows.Add(rowDet);

//        }
//        DataSet set = new DataSet();
//        set.Tables.Add(master);
//        set.Tables.Add(det);
//        set.Relations.Add("rela", master.Columns["RefNo"], det.Columns["RefNo"]);
//        return set;
    }

    public static DataTable dsBatchSo(string schNo)
    {
        string sql_det = string.Format(@"select BKG_LINE_N, T_IMPORT_NO from export_bkg_det where EXPORT_BKG_N='{0}'", schNo);
        DataTable tabDet = ConnectSql.GetTab(sql_det);
        return tabDet;
    }
    #endregion

    #region export ref
    public static DataSet PrintOceanBl(string refN,bool isCarrier)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintSea_OceanBl_Carrier '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", "");
            if (!isCarrier)
            {
                strsql = string.Format(@"exec proc_PrintSea_OceanBl_Nvocc '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", "");
            }
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;
//        string sql = string.Format(@"SELECT JobType,CrAgentId,NvoccAgentId, CrBkgNo, UserId, Pol, Pod, SShipperRemark AS SHIPPER1, SConsigneeRemark AS CON1, SNotifyPartyRemark AS NP1, Vessel + ' ' + Voyage AS VES, 
//                      OblTerm FROM SeaExportRef WHERE (RefNo = '{0}')", refN);
//        DataTable tab =ConnectSql.GetTab(sql);
//        DataTable Mast = new DataTable("Mast");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("CrAgent");
//        Mast.Columns.Add("Attn");
//        Mast.Columns.Add("CrBkgRefNo");
//        Mast.Columns.Add("Date");
//        Mast.Columns.Add("UserId");
//        Mast.Columns.Add("Shipper");
//        Mast.Columns.Add("Consignee");
//        Mast.Columns.Add("NotifyParty");
//        Mast.Columns.Add("VesVoy");
//        Mast.Columns.Add("Pol");
//        Mast.Columns.Add("Pod");
//        Mast.Columns.Add("FrtTerm");
//        Mast.Columns.Add("Qty");
//        DataTable Detail = new DataTable("Detail");
//        Detail.Columns.Add("RefN");
//        Detail.Columns.Add("ContNo");
//        Detail.Columns.Add("Wt");
//        Detail.Columns.Add("M3");
//        Detail.Columns.Add("Qty");
//        Detail.Columns.Add("CONTN");
//        try
//        {
//            if (tab.Rows.Count == 1)
//            {
//                string jobType = tab.Rows[0]["JobType"].ToString();
//                string sql1 = string.Format(@"SELECT ContainerNo AS ContN, SUM(Volume) AS M3, SUM(Weight) AS WT, SUM(Qty) AS qty
//FROM SeaExportMkg WHERE (RefNo = '{0}')  and (MkgType='Cont' or MkgType='BL') GROUP BY ContainerNo", refN);

//                DataTable tab1 = ConnectSql.GetTab(sql1);
//                int qty = 0;
//                for (int i = 0; i < tab1.Rows.Count; i++)
//                {
//                    DataRow rptRow = Detail.NewRow();
//                    rptRow["RefN"] = refN;
//                    // rptRow["TERMID"] = tab1.Rows[i]["TERMID"];
//                    rptRow["M3"] = SafeValue.SafeDecimal(tab1.Rows[i]["M3"], 0).ToString("0.000");
//                       rptRow["Wt"] = SafeValue.SafeDecimal(tab1.Rows[i]["WT"], 0).ToString("0.000") ;
//                    rptRow["Qty"]=tab1.Rows[i]["Qty"].ToString() ;
//                    qty += SafeValue.SafeInt(tab1.Rows[i]["Qty"], 0);
//                    string contId = tab1.Rows[i]["ContN"].ToString();
//                    string contNo = SafeValue.SafeString(tab1.Rows[i]["ContN"], "");

//                    rptRow["ContNo"] = ConnectSql.ExecuteScalar(string.Format("select ContainerNo+' / '+Sealno+' / '+ContainerType from SeaExportMkg where RefNo='{0}' and ContainerNo='{1}'", refN, contNo));

//                    Detail.Rows.Add(rptRow);
//                }
//                DataRow rptRowMast = Mast.NewRow();
//                rptRowMast["RefN"] = refN;
//                string crAgentId = SafeValue.SafeString(tab.Rows[0]["CrAgentId"], "");
//                string nvoccAgtId = SafeValue.SafeString(tab.Rows[0]["NvoccAgentId"], "");
//                string sql_CrAgent = string.Format("select Name ,Contact1 from XXParty where PartyId='{0}'",crAgentId);
//                if (!isCarrier)
//                {
//                    sql_CrAgent = string.Format("select Name ,Contact1 from XXParty where PartyId='{0}'", nvoccAgtId);
//                }
//                DataTable tab_CrAgent = ConnectSql.GetTab(sql_CrAgent);
//                if (tab_CrAgent.Rows.Count == 1)
//                {
//                    rptRowMast["CrAgent"] = tab_CrAgent.Rows[0]["Name"];
//                    rptRowMast["Attn"] = tab_CrAgent.Rows[0]["Contact1"];
//                }
//                rptRowMast["CrBkgRefNo"] = tab.Rows[0]["CrBkgNo"];
//                rptRowMast["Date"] = DateTime.Today.ToString("dd/MM/yyyy");
//                rptRowMast["UserId"] = tab.Rows[0]["UserId"];
//                rptRowMast["Shipper"] = tab.Rows[0]["SHIPPER1"];
//                rptRowMast["Consignee"] = tab.Rows[0]["CON1"];
//                rptRowMast["NotifyParty"] = tab.Rows[0]["NP1"];

//                rptRowMast["VesVoy"] = tab.Rows[0]["VES"]; 
//                rptRowMast["Pol"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from xxport where Code='" + tab.Rows[0]["Pol"] + "'"), tab.Rows[0]["POL"].ToString());
//                rptRowMast["Pod"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from xxport where Code='" + tab.Rows[0]["Pod"] + "'"), tab.Rows[0]["POD"].ToString());

//                string frtTerm = SafeValue.SafeString(tab.Rows[0]["OblTerm"],"FP").ToUpper();
//                if (frtTerm == "FP")
//                    frtTerm = "FREIGHT PREPAID";
//                else
//                    frtTerm = "FREIGHT COLLECT";
//                rptRowMast["FrtTerm"] = frtTerm;
//                rptRowMast["Qty"] = qty;

//                Mast.Rows.Add(rptRowMast);
//            }

//        }
//        catch (Exception ex)
//        { }
//        DataSet ds = new DataSet();
//        ds.Tables.Add(Mast);
//        ds.Tables.Add(Detail);
//        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);

//        return ds;
    }
    public static DataSet PrintOceanBlAttach(string refN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintOceanBlAttach '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "","");
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;

//        string sql = string.Format(@"SELECT JobType,CrAgentId, CrBkgNo, UserId, Pol, Pod, SShipperRemark AS SHIPPER1, SConsigneeRemark AS CON1, SNotifyPartyRemark AS NP1, Vessel + ' ' + Voyage AS VES, 
//                      OblTerm FROM SeaExportRef WHERE (RefNo = '{0}')", refN);
//        DataTable tab = ConnectSql.GetTab(sql);
//        DataTable Mast = new DataTable("Mast");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("CrBkgRefNo");
//        Mast.Columns.Add("Qty");
//        Mast.Columns.Add("Wt");
//        Mast.Columns.Add("M3");
//        DataTable Detail = new DataTable("Detail");
//        Detail.Columns.Add("RefN");
//        Detail.Columns.Add("ContNo");
//        Detail.Columns.Add("Wt");
//        Detail.Columns.Add("M3");
//        Detail.Columns.Add("Qty");
//        Detail.Columns.Add("PkgType");
//        Detail.Columns.Add("Mkg");
//        Detail.Columns.Add("Des");
//        try
//        {
//            if (tab.Rows.Count == 1)
//            {
//                string jobType = tab.Rows[0]["JobType"].ToString();
//        string sql1 = string.Format(@"SELECT ContainerNo+'/'+SealNo as ContN, Marking, Description, Weight, Volume, Qty, PackageType, Remark
//FROM SeaExportMkg where RefNo='{0}'", refN);
//        if (jobType == "FCL")
//        {
//            sql1 += " and MkgType='Cont'";
//        }
//        else
//        {
//            sql1 += " and MkgType='BL'";
//        }
//        DataTable tab1 = ConnectSql.GetTab(sql1);
//                int qty = 0;
//                decimal wt = 0;
//                decimal m3 = 0;
//                for (int i = 0; i < tab1.Rows.Count; i++)
//                {
//                    DataRow rptRow = Detail.NewRow();
//                    rptRow["RefN"] = refN;
//                    // rptRow["TERMID"] = tab1.Rows[i]["TERMID"];
//                    rptRow["M3"] = SafeValue.SafeDecimal(tab1.Rows[i]["Volume"], 0).ToString("0.000");
//                    rptRow["Wt"] = SafeValue.SafeDecimal(tab1.Rows[i]["Weight"], 0).ToString("0.000");
//                    rptRow["Qty"] = tab1.Rows[i]["Qty"];
//                    rptRow["PkgType"] = tab1.Rows[i]["PackageType"];
//                    qty += SafeValue.SafeInt(tab1.Rows[i]["Qty"], 0);
//                    wt += SafeValue.SafeDecimal(tab1.Rows[i]["Weight"], 0);
//                    m3 += SafeValue.SafeDecimal(tab1.Rows[i]["Volume"], 0);
//                    rptRow["ContNo"] = tab1.Rows[i]["ContN"];
//                    rptRow["Mkg"] = tab1.Rows[i]["Marking"];
//                    rptRow["Des"] = tab1.Rows[i]["Description"];

//                    Detail.Rows.Add(rptRow);
//                }
//                DataRow rptRowMast = Mast.NewRow();
//                rptRowMast["RefN"] = refN;
//                rptRowMast["CrBkgRefNo"] = tab.Rows[0]["CrBkgNo"];
//                rptRowMast["Qty"] = qty;
//                rptRowMast["Wt"] = wt.ToString("0.000");
//                rptRowMast["M3"] = m3.ToString("0.000");

//                Mast.Rows.Add(rptRowMast);
//            }

//        }
//        catch (Exception ex)
//        { }
//        DataSet ds = new DataSet();
//        ds.Tables.Add(Mast);
//        ds.Tables.Add(Detail);
//        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);

//        return ds;
    }
    public static DataSet PrintOceanBl_multiple(string refN, bool isCarrier,string where)
    {
        string sql = string.Format(@"SELECT JobType,CrAgentId,NvoccAgentId, CrBkgNo, CreateBy, Pol, Pod, SShipperRemark AS SHIPPER1, SConsigneeRemark AS CON1, SNotifyPartyRemark AS NP1, Vessel + ' ' + Voyage AS VES, 
                      OblTerm FROM SeaExportRef WHERE (RefNo = '{0}')", refN);
        DataTable tab = ConnectSql.GetTab(sql);
        DataTable Mast = new DataTable("Mast");
        Mast.Columns.Add("RefN");
        Mast.Columns.Add("CrAgent");
        Mast.Columns.Add("Attn");
        Mast.Columns.Add("CrBkgRefNo");
        Mast.Columns.Add("Date");
        Mast.Columns.Add("UserId");
        Mast.Columns.Add("Shipper");
        Mast.Columns.Add("Consignee");
        Mast.Columns.Add("NotifyParty");
        Mast.Columns.Add("VesVoy");
        Mast.Columns.Add("Pol");
        Mast.Columns.Add("Pod");
        Mast.Columns.Add("FrtTerm");
        Mast.Columns.Add("Qty");
        DataTable Detail = new DataTable("Detail");
        Detail.Columns.Add("RefN");
        Detail.Columns.Add("ContNo");
        Detail.Columns.Add("Wt");
        Detail.Columns.Add("M3");
        Detail.Columns.Add("Qty");
        Detail.Columns.Add("CONTN");
        try
        {
            if (tab.Rows.Count == 1)
            {
                string jobType = tab.Rows[0]["JobType"].ToString();
                string sql1 = string.Format(@"SELECT RefNo,ContainerNo AS ContN, SUM(Volume) AS M3, SUM(Weight) AS WT, SUM(Qty) AS qty
FROM SeaExportMkg WHERE {0}  and (MkgType='Cont' or MkgType='BL') and StatusCode='USE' GROUP BY RefNo,ContainerNo", where);

                DataTable tab1 = ConnectSql.GetTab(sql1);
                int qty = 0;
                for (int i = 0; i < tab1.Rows.Count; i++)
                {
                    DataRow rptRow = Detail.NewRow();
                    rptRow["RefN"] = refN;
                    // rptRow["TERMID"] = tab1.Rows[i]["TERMID"];
                    rptRow["M3"] = SafeValue.SafeDecimal(tab1.Rows[i]["M3"], 0).ToString("0.000");
                    rptRow["Wt"] = SafeValue.SafeDecimal(tab1.Rows[i]["WT"], 0).ToString("0.000");
                    rptRow["Qty"] = tab1.Rows[i]["Qty"].ToString();
                    qty += SafeValue.SafeInt(tab1.Rows[i]["Qty"], 0);
                    string contId = tab1.Rows[i]["ContN"].ToString();
                    string contNo = SafeValue.SafeString(tab1.Rows[i]["ContN"], "");

                    rptRow["ContNo"] = ConnectSql.ExecuteScalar(string.Format("select ContainerNo+' / '+Sealno+' / '+ContainerType from SeaExportMkg where RefNo='{0}' and ContainerNo='{1}'", tab1.Rows[i]["RefNo"], contNo));

                    Detail.Rows.Add(rptRow);
                }
                DataRow rptRowMast = Mast.NewRow();
                rptRowMast["RefN"] = refN;
                string crAgentId = SafeValue.SafeString(tab.Rows[0]["CrAgentId"], "");
                string nvoccAgtId = SafeValue.SafeString(tab.Rows[0]["NvoccAgentId"], "");
                string sql_CrAgent = string.Format("select Name ,Contact1 from XXParty where PartyId='{0}'", crAgentId);
                if (!isCarrier)
                {
                    sql_CrAgent = string.Format("select Name ,Contact1 from XXParty where PartyId='{0}'", nvoccAgtId);
                }
                DataTable tab_CrAgent = ConnectSql.GetTab(sql_CrAgent);
                if (tab_CrAgent.Rows.Count == 1)
                {
                    rptRowMast["CrAgent"] = tab_CrAgent.Rows[0]["Name"];
                    rptRowMast["Attn"] = tab_CrAgent.Rows[0]["Contact1"];
                }
                rptRowMast["CrBkgRefNo"] = tab.Rows[0]["CrBkgNo"];
                rptRowMast["Date"] = DateTime.Today.ToString("dd/MM/yyyy");
                rptRowMast["UserId"] = tab.Rows[0]["CreateBy"];
                rptRowMast["Shipper"] = tab.Rows[0]["SHIPPER1"];
                rptRowMast["Consignee"] = tab.Rows[0]["CON1"];
                rptRowMast["NotifyParty"] = tab.Rows[0]["NP1"];

                rptRowMast["VesVoy"] = tab.Rows[0]["VES"];
                rptRowMast["Pol"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from xxport where Code='" + tab.Rows[0]["Pol"] + "'"), tab.Rows[0]["POL"].ToString());
                rptRowMast["Pod"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from xxport where Code='" + tab.Rows[0]["Pod"] + "'"), tab.Rows[0]["POD"].ToString());

                string frtTerm = SafeValue.SafeString(tab.Rows[0]["OblTerm"], "FP").ToUpper();
                if (frtTerm == "FP")
                    frtTerm = "FREIGHT PREPAID";
                else
                    frtTerm = "FREIGHT COLLECT";
                rptRowMast["FrtTerm"] = frtTerm;
                rptRowMast["Qty"] = qty;

                Mast.Rows.Add(rptRowMast);
            }

        }
        catch (Exception ex)
        { }
        DataSet ds = new DataSet();
        ds.Tables.Add(Mast);
        ds.Tables.Add(Detail);
        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);

        return ds;
    }
    public static DataSet PrintOceanBlAttach_multiple(string refN,string where)
    {
        string sql = string.Format(@"SELECT JobType,CrAgentId, CrBkgNo, CreateBy, Pol, Pod, SShipperRemark AS SHIPPER1, SConsigneeRemark AS CON1, SNotifyPartyRemark AS NP1, Vessel + ' ' + Voyage AS VES, 
                      OblTerm FROM SeaExportRef WHERE (RefNo = '{0}')", refN);
        DataTable tab = ConnectSql.GetTab(sql);
        DataTable Mast = new DataTable("Mast");
        Mast.Columns.Add("RefN");
        Mast.Columns.Add("CrBkgRefNo");
        Mast.Columns.Add("Qty");
        Mast.Columns.Add("Wt");
        Mast.Columns.Add("M3");
        DataTable Detail = new DataTable("Detail");
        Detail.Columns.Add("RefN");
        Detail.Columns.Add("ContNo");
        Detail.Columns.Add("Wt");
        Detail.Columns.Add("M3");
        Detail.Columns.Add("Qty");
        Detail.Columns.Add("PkgType");
        Detail.Columns.Add("Mkg");
        Detail.Columns.Add("Des");
        try
        {
            if (tab.Rows.Count == 1)
            {
                string jobType = tab.Rows[0]["JobType"].ToString();
                string sql1 = string.Format(@"SELECT ContainerNo+'/'+SealNo as ContN, Marking, Description, Weight, Volume, Qty, PackageType, Remark
FROM SeaExportMkg where {0} ", where);
                if (jobType == "FCL")
                {
                    sql1 += " and MkgType='Cont' and StatusCode='USE'";
                }
                else
                {
                    sql1 += " and MkgType='BL' and StatusCode='USE'";
                }
                sql1 +=  " order by  RefNo,SequenceId";
                DataTable tab1 = ConnectSql.GetTab(sql1);
                int qty = 0;
                decimal wt = 0;
                decimal m3 = 0;
                for (int i = 0; i < tab1.Rows.Count; i++)
                {
                    DataRow rptRow = Detail.NewRow();
                    rptRow["RefN"] = refN;
                    // rptRow["TERMID"] = tab1.Rows[i]["TERMID"];
                    rptRow["M3"] = SafeValue.SafeDecimal(tab1.Rows[i]["Volume"], 0).ToString("0.000");
                    rptRow["Wt"] = SafeValue.SafeDecimal(tab1.Rows[i]["Weight"], 0).ToString("0.000");
                    rptRow["Qty"] = tab1.Rows[i]["Qty"];
                    rptRow["PkgType"] = tab1.Rows[i]["PackageType"];
                    qty += SafeValue.SafeInt(tab1.Rows[i]["Qty"], 0);
                    wt += SafeValue.SafeDecimal(tab1.Rows[i]["Weight"], 0);
                    m3 += SafeValue.SafeDecimal(tab1.Rows[i]["Volume"], 0);
                    rptRow["ContNo"] = tab1.Rows[i]["ContN"];
                    rptRow["Mkg"] = tab1.Rows[i]["Marking"];
                    rptRow["Des"] = tab1.Rows[i]["Description"];

                    Detail.Rows.Add(rptRow);
                }
                DataRow rptRowMast = Mast.NewRow();
                rptRowMast["RefN"] = refN;
                rptRowMast["CrBkgRefNo"] = tab.Rows[0]["CrBkgNo"];
                rptRowMast["Qty"] = qty;
                rptRowMast["Wt"] = wt.ToString("0.000");
                rptRowMast["M3"] = m3.ToString("0.000");

                Mast.Rows.Add(rptRowMast);
            }

        }
        catch (Exception ex)
        { }
        DataSet ds = new DataSet();
        ds.Tables.Add(Mast);
        ds.Tables.Add(Detail);
        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);

        return ds;
    }
    public static DataSet PrintExpPermitList(string refN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintExpPermitList '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;

        //string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaExportMkg WHERE RefNo='{0}' and MkgType='Cont'", refN);
        //string contN = "";
        //DataTable cont_tab = ConnectSql.GetTab(sql_cont);
        //for (int i = 0; i < cont_tab.Rows.Count; i++)
        //{
        //    if (i == 0)
        //        contN += SafeValue.SafeString(cont_tab.Rows[i][0],"");
        //    else
        //        contN += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0],"");
        //}


        //string sql1 = @"SELECT OblNo AS Bl, Vessel + '/' + Voyage AS Ves, Pol, Pod , Eta AS ET, UserId AS Us, CrAgentId FROM  SeaExportRef WHERE  (RefNo = '" + refN + @"')";

        //string sql2 = @"SELECT  JobNo AS ImportN, HblNo AS Hbl, Volume AS M3, Weight AS Wt, CONVERT(varchar(20), Qty) + '/' + PackageType AS Pack, PermitRmk AS PermitN FROM SeaExport WHERE (RefNo = '" + refN + "')";

        //DataTable tab1 = ConnectSql.GetTab(sql1);

        //DataTable tab2 = ConnectSql.GetTab(sql2);
        //DataTable Mast = new DataTable("Mast");
        //Mast.Columns.Add("NowD");
        //Mast.Columns.Add("RefN");
        //Mast.Columns.Add("Ves");
        //Mast.Columns.Add("Bl");
        //Mast.Columns.Add("ET");
        //Mast.Columns.Add("POL");
        //Mast.Columns.Add("Us");
        //Mast.Columns.Add("Vend1");
        //Mast.Columns.Add("CONTN");
        //Mast.Columns.Add("CompanyName");
        //DataTable Detail = new DataTable("Detail");
        //Detail.Columns.Add("ImportN");
        //Detail.Columns.Add("RefN");
        //Detail.Columns.Add("Hbl");
        //Detail.Columns.Add("M3");
        //Detail.Columns.Add("Wt");
        //Detail.Columns.Add("Pack");
        ////Detail.Columns.Add("gd1");
        ////Detail.Columns.Add("gd2");
        //Detail.Columns.Add("PermitN");
        //try
        //{
        //    DataRow rptRow = Mast.NewRow();
        //    for (int i = 0; i < Mast.Columns.Count; i++)
        //    {
        //        string colName = Mast.Columns[i].ToString();
        //        if (colName == "CONTN")
        //            rptRow[colName] = contN;
        //        else if (colName == "NowD")
        //            rptRow[colName] = DateTime.Today.ToString("dd/MM/yyyy");
        //        else if (colName == "RefN")
        //            rptRow[colName] = refN;
        //        else if (colName == "POL")
        //        {
        //            string pol = SafeValue.SafeString(tab1.Rows[0]["Pol"], "SGSIN");
        //            pol = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from xxport where Code='" + pol + "'"), pol);
        //            string pod = SafeValue.SafeString(tab1.Rows[0]["Pod"], "");
        //            pod = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from xxport where Code='" + pod + "'"), pod);
        //            rptRow[colName] = pol + "/" + pod;
        //        }
        //        else if (colName == "Vend1")
        //        {
        //            string crAgtId = SafeValue.SafeString(tab1.Rows[0]["CrAgentId"], "0");
        //            string sql_vendor = "select PartyId, Code, Name,  Address, Tel1,Fax1 from XXParty where PartyId='" + crAgtId + "'";
        //            DataTable tab_vendor = C2.Manager.ORManager.GetDataSet(sql_vendor).Tables[0];
        //            if (tab_vendor.Rows.Count > 0)
        //                rptRow["Vend1"] = tab_vendor.Rows[0]["Name"] + "\n" + tab_vendor.Rows[0]["Address"];
        //            else
        //                rptRow["Vend1"] = "";
        //        }
        //        else if (colName == "ET")
        //            rptRow[colName] = SafeValue.SafeDateStr(tab1.Rows[0]["ET"]);
        //        else if (colName == "CompanyName")
        //            rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
        //        else
        //            rptRow[colName] = tab1.Rows[0][colName];
        //    }
        //    Mast.Rows.Add(rptRow);

        //    for (int j = 0; j < tab2.Rows.Count; j++)
        //    {
        //        DataRow detailRow = Detail.NewRow();
        //        for (int i = 0; i < Detail.Columns.Count; i++)
        //        {
        //            string colName = Detail.Columns[i].ToString();
        //            if (colName == "RefN")
        //                rptRow[colName] = refN;
        //            else
        //                detailRow[Detail.Columns[i].ToString()] = tab2.Rows[j][Detail.Columns[i].ToString()];
        //        }
        //        Detail.Rows.Add(detailRow);
        //    }
        //}
        //catch (Exception ex)
        //{
        //}
        //DataSet ds = new DataSet();
        //ds.Tables.Add(Mast);
        //ds.Tables.Add(Detail);
        //DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
        //ds.Relations.Add(r);
        //return ds;
    }
    public static DataSet PrintExpPermitListNvocc(string refN)
    {
        string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaExportMkg WHERE RefNo='{0}' and MkgType='BL'", refN);
        string contN = "";
        DataTable cont_tab = ConnectSql.GetTab(sql_cont);
        for (int i = 0; i < cont_tab.Rows.Count; i++)
        {
            if (i == 0)
                contN += SafeValue.SafeString(cont_tab.Rows[i][0]);
            else
                contN += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0]);
        }
        string sql1 = @"SELECT OblNo AS Bl, Vessel + '/' + Voyage AS Ves, Pol, Pod , Eta AS ET, UserId AS Us, NvoccAgentID as Vend1 FROM  SeaExportRef WHERE  (RefNo = '" + refN + @"')";

        string sql2 = @"SELECT  JobNo AS ImportN, HblNo AS Hbl, Volume AS M3, Weight AS Wt, CONVERT(varchar(20), Qty) + '/' + PackageType AS Pack, PermitRmk AS PermitN FROM SeaExport WHERE (RefNo = '" + refN + "')";

        DataTable tab1 = ConnectSql.GetTab(sql1);
        DataTable tab2 = ConnectSql.GetTab(sql2);
        DataTable Mast = new DataTable("Mast");
        Mast.Columns.Add("NowD");
        Mast.Columns.Add("RefN");
        Mast.Columns.Add("Ves");
        Mast.Columns.Add("Bl");
        Mast.Columns.Add("ET");
        Mast.Columns.Add("POL");
        Mast.Columns.Add("Us");
        Mast.Columns.Add("Vend1");
        Mast.Columns.Add("CONTN");
        Mast.Columns.Add("CompanyName");
        DataTable Detail = new DataTable("Detail");
        Detail.Columns.Add("ImportN");
        Detail.Columns.Add("RefN");
        Detail.Columns.Add("Hbl");
        Detail.Columns.Add("M3");
        Detail.Columns.Add("Wt");
        Detail.Columns.Add("Pack");
        //Detail.Columns.Add("gd1");
        //Detail.Columns.Add("gd2");
        Detail.Columns.Add("PermitN");
        try
        {
            DataRow rptRow = Mast.NewRow();
            for (int i = 0; i < Mast.Columns.Count; i++)
            {
                string colName = Mast.Columns[i].ToString();
                if (colName == "CONTN")
                    rptRow[colName] = contN;
                else if (colName == "NowD")
                    rptRow[colName] = DateTime.Today.ToString("dd/MM/yyyy");
                else if (colName == "RefN")
                    rptRow[colName] = refN;
                else if (colName == "POL")
                {
                    string pol = SafeValue.SafeString(tab1.Rows[0]["Pol"], "SGSIN");
                    pol = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from xxport where Code='" + pol + "'"), pol);
                    string pod = SafeValue.SafeString(tab1.Rows[0]["Pod"], "");
                    pod = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from xxport where Code='" + pod + "'"), pod);
                    rptRow[colName] = pol + "/" + pod;
                }
                else if (colName == "Vend1")
                {
                    string crAgtId = SafeValue.SafeString(tab1.Rows[0]["Vend1"], "0");
                    string sql_vendor = "select PartyId, Code, Name,  Address, Tel1,Fax1 from XXParty where PartyId='" + crAgtId + "'";
                    DataTable tab_vendor = C2.Manager.ORManager.GetDataSet(sql_vendor).Tables[0];
                    if (tab_vendor.Rows.Count > 0)
                        rptRow["Vend1"] = tab_vendor.Rows[0]["Name"] + "\n" + tab_vendor.Rows[0]["Address"];
                    else
                        rptRow["Vend1"] = "";
                }
                else if (colName == "ET")
                    rptRow[colName] = SafeValue.SafeDate(tab1.Rows[0]["ET"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
                else if (colName == "CompanyName")
                    rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
                else
                    rptRow[colName] = tab1.Rows[0][colName];
            }
            Mast.Rows.Add(rptRow);

            for (int j = 0; j < tab2.Rows.Count; j++)
            {
                DataRow detailRow = Detail.NewRow();
                for (int i = 0; i < Detail.Columns.Count; i++)
                {
                    string colName = Detail.Columns[i].ToString();
                    if (colName == "RefN")
                        rptRow[colName] = refN;
                    else
                        detailRow[Detail.Columns[i].ToString()] = tab2.Rows[j][Detail.Columns[i].ToString()];
                }
                Detail.Rows.Add(detailRow);
            }
        }
        catch
        {
        }
        DataSet ds = new DataSet();
        ds.Tables.Add(Mast);
        ds.Tables.Add(Detail);
        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
        ds.Relations.Add(r);

        return ds;
    }
    public static DataSet PrintExpPreAdvise(string refN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintExpPreAdvise '{0}','{1}','{2}','{3}','{4}'", refN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;


//        string sql_cont = string.Format(@"SELECT ContainerNo + '/' + SealNo + '/' + ContainerType AS container FROM SeaExportMkg WHERE RefNo='{0}' and MkgType='Cont'", refN);
//        string contN = "";
//        //}
//        DataTable cont_tab =ConnectSql.GetTab(sql_cont);
//        for (int i = 0; i < cont_tab.Rows.Count; i++)
//        {
//            if (i == 0)
//                contN += SafeValue.SafeString(cont_tab.Rows[i][0]);
//            else
//                contN += "\n" + SafeValue.SafeString(cont_tab.Rows[i][0]);
//        }

//        string sql = string.Format(@"SELECT AgentID AS Agent, Eta AS ET, CrBkgNo, OblNo AS bl, Vessel + '/' + Voyage AS Ves, Etd AS ED, UserID AS Us, ExpressBL, OblTerm AS TERMID, 
//                      JobType AS JOBTYPE FROM SeaExportRef WHERE (RefNo= '{0}')", refN);

//        DataTable tab = ConnectSql.GetTab(sql);
//        DataTable Mast = new DataTable("Mast");
//        Mast.Columns.Add("NOWD");
//        Mast.Columns.Add("REFN");
//        Mast.Columns.Add("VES");
//        Mast.Columns.Add("AGENT");
//        Mast.Columns.Add("ET");
//        Mast.Columns.Add("ED");
//        Mast.Columns.Add("BL");
//        Mast.Columns.Add("EXPRESS");//(case when export_ref.express_bl='Y' then 'YES' ELSE 'NO' END)  as EXPRESS,
//        Mast.Columns.Add("TERMID");
//        Mast.Columns.Add("JobType");
//        Mast.Columns.Add("Us");
//        Mast.Columns.Add("CONTN");

//        Mast.Columns.Add("CrNote");
//        Mast.Columns.Add("DrNote");
//        Mast.Columns.Add("CompanyName");

//        try
//        {
//            DataRow rptRow = Mast.NewRow();
//            rptRow["NOWD"] = DateTime.Today.ToString("dd/MM/yyyy");
//            rptRow["REFN"] = refN;
//            rptRow["VES"] = tab.Rows[0]["Ves"];
//            string agtId = tab.Rows[0]["Agent"].ToString();
//            rptRow["AGENT"] = EzshipHelper.GetPartyName(agtId);
//            rptRow["ET"] = SafeValue.SafeDate(tab.Rows[0]["ET"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
//            rptRow["ED"] = SafeValue.SafeDate(tab.Rows[0]["ED"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
//            rptRow["BL"] = tab.Rows[0]["BL"];
//            string expressBl = SafeValue.SafeString(tab.Rows[0]["ExpressBL"], "N");
//            rptRow["EXPRESS"] = "NO";
//            if (expressBl == "Y")
//                rptRow["EXPRESS"] = "YES";
//            rptRow["TERMID"] = tab.Rows[0]["TERMID"];
//            rptRow["JobType"] = tab.Rows[0]["JobType"];
//            rptRow["Us"] = tab.Rows[0]["Us"];
//            rptRow["CONTN"] = contN;

//            //sql_cont = string.Format(@"SELECT DocNo, DocType FROM XAArInvoice WHERE (MastType = 'SE') AND (MastRefNo = '{0}') AND (DocType = 'CN' OR DocType = 'DN')", refN);
//            //DataTable tab_bill = ConnectSql.GetTab(sql_cont);
//            //for (int i = 0; i < tab_bill.Rows.Count; i++)
//            //{
//            //    string docType = tab_bill.Rows[i]["DocType"].ToString();
//            //    if (docType == "CN")
//            //    {
//            //        rptRow["CrNote"] = tab_bill.Rows[i]["DocNo"];
//            //    }
//            //    else if (docType == "DN")
//            //    {
//            //        rptRow["DrNote"] = tab_bill.Rows[i]["DocNo"];
//            //    }

//            //}
//            rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//            Mast.Rows.Add(rptRow);
//        }
//        catch (Exception ex)
//        {
//        }

//        string sql_det = string.Format(@"SELECT JobNo,HblNo AS hbl, ExpressBl AS Express,FrtTerm
//FROM   SeaExport WHERE (RefNo = '{0}') ORDER BY JobNo", refN);
//        DataTable Detail = new DataTable("Detail");
//        Detail.Columns.Add("RefN");
//        Detail.Columns.Add("Hbl");
//        Detail.Columns.Add("Express");
//        Detail.Columns.Add("TermID");
//        Detail.Columns.Add("Currency");
//        Detail.Columns.Add("Amount");

//        DataTable tab2 = ConnectSql.GetTab(sql_det);

//        try
//        {
//            for (int i = 0; i < tab2.Rows.Count; i++)
//            {
//                DataRow rptRow1 = Detail.NewRow();
//                rptRow1["RefN"] = refN;
//                rptRow1["Hbl"] = tab2.Rows[i]["Hbl"];
//                rptRow1["Express"] = tab2.Rows[i]["Express"];
//                rptRow1["TermID"] = tab2.Rows[i]["FrtTerm"];
//                rptRow1["Amount"] = "0.00";

//                string jobNo = tab2.Rows[i]["JobNo"].ToString();
//                string sql_expDet = string.Format("SELECT SUM(Amount) AS Expr1, MAX(Currency) as Expr2 FROM SeaExportDetail where JobNo='{0}'",jobNo);
//                DataTable tab_expDet = ConnectSql.GetTab(sql_expDet);
//                if (tab_expDet.Rows.Count == 1)
//                {
//                    rptRow1["Currency"] = tab_expDet.Rows[0]["Expr2"];
//                    rptRow1["Amount"] = tab_expDet.Rows[0]["Expr1"];
//                }
//                Detail.Rows.Add(rptRow1);
//            }
//        }
//        catch (Exception ex)
//        {
//        }

//        DataSet ds = new DataSet();
//        ds.Tables.Add(Mast);
//        ds.Tables.Add(Detail);
//        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
//        ds.Relations.Add(r);

//        return ds;
    }
    public static DataTable PrintHaulier_Export(string refN, string jobNo, string jobType)
    {
        DataTable dt = new DataTable();
        try
        {
            string strsql = string.Format(@"exec proc_PrintHaulier_Export '{0}','{1}','{2}','{3}','{4}'", refN, jobNo, jobType, "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            dt = ds_temp.Tables[0].Copy();
        }
        catch (Exception ex) { }
        return dt;


//        string sql1 = string.Format(@"  SELECT HaulierName as Haulier,HaulierRemark AS R1, HaulierCollectDate AS ColD,HaulierTruck 
//                     , HaulierCollect, HaulierAttention AS Attn, HaulierCrNo AS CrNote,  
//                      RefNo AS RefN, Eta AS ET, Etd AS ED, Pod , Pol, Vessel + '/' + Voyage AS Ves, CrBkgNo AS Bkg, OblNo AS Bl, UserID AS Us, 
//                      CrAgentID AS Agent,Remark FROM SeaExportRef WHERE (RefNo= '{0}')", refN);
//        if (jobNo != "0")
//        {
//            sql1 = string.Format(@"  SELECT det.HaulierName as Haulier,det.HaulierRemark AS R1, det.HaulierCollectDate AS ColD, det.HaulierTruck 
//                     , det.HaulierCollect, det.HaulierAttention AS Attn, det.HaulierCrNo AS CrNote,  
//                      mast.RefNo AS RefN, mast.Eta AS ET, mast.Etd AS ED, mast.Pod , mast.Pol, mast.Vessel + '/' + mast.Voyage AS Ves, mast.CrBkgNo AS Bkg, mast.OblNo AS Bl, det.CreateBy AS Us, 
//                      mast.CrAgentID AS Agent FROM SeaExport det,Seaexportref mast WHERE det.RefNo=mast.RefNo and (det.JobNo= '{0}')", jobNo);
//        }
//        DataTable tab1 = ConnectSql.GetTab(sql1);
//        DataTable tab = new DataTable();
//        tab.Columns.Add("R1");
//        tab.Columns.Add("ColD");
//        tab.Columns.Add("JobType");
//        tab.Columns.Add("ContN");
//        tab.Columns.Add("Wt");
//        tab.Columns.Add("M3");
//        tab.Columns.Add("Pack");
//        tab.Columns.Add("HaulierTruck");
//        tab.Columns.Add("HaulierCollect"); ;
//        tab.Columns.Add("Attn");
//        tab.Columns.Add("CrNote");
//        tab.Columns.Add("Haulier");
//        tab.Columns.Add("RefN");
//        tab.Columns.Add("ET");
//        tab.Columns.Add("ED");
//        tab.Columns.Add("POD");
//        tab.Columns.Add("POL");
//        tab.Columns.Add("Ves");
//        tab.Columns.Add("Bkg");
//        tab.Columns.Add("Bl");
//        tab.Columns.Add("Agent");
//        tab.Columns.Add("Us");
//        tab.Columns.Add("CompanyName");
//        try
//        {
//            DataRow rptRow = tab.NewRow();
//            rptRow["R1"] = tab1.Rows[0]["R1"];

//            DateTime d = SafeValue.SafeDate(tab1.Rows[0]["ColD"], DateTime.Today);
//            if (d > new DateTime(2000, 1, 1))
//                rptRow["ColD"] = SafeValue.SafeDateStr(d);

//            if (jobType == "I")
//                rptRow["JobType"] = "IMPORT";
//            else
//                rptRow["JobType"] = "EXPORT";

//            string sql_bkg = string.Format("select sum(Weight) Weight, sum(Volume) Volume, sum(Qty) Qty, max(PackageType) PkgType from SeaExportMkg where MkgType='Bkg' and RefNo='{0}' and JobNo='{1}'", refN, jobNo);
//            if (jobNo == "0")
//            {
//                sql_bkg = string.Format("select sum(Weight) Weight, sum(Volume) Volume, sum(Qty) Qty, max(PackageType) PkgType from SeaExportMkg where MkgType='Bkg' and RefNo='{0}'", refN);
//            }

//            DataTable tab_bkg = C2.Manager.ORManager.GetDataSet(sql_bkg).Tables[0];
//            if (tab_bkg.Rows.Count == 1)
//            {
//                rptRow["Wt"] = SafeValue.SafeDecimal(tab_bkg.Rows[0]["Weight"]).ToString("0.000");
//                rptRow["M3"] = SafeValue.SafeDecimal(tab_bkg.Rows[0]["Volume"]).ToString("0.000");
//                rptRow["Pack"] = SafeValue.SafeString(tab_bkg.Rows[0]["Qty"]) + "x" + SafeValue.SafeString(tab_bkg.Rows[0]["PkgType"]);
//            }
//            rptRow["HaulierTruck"] = tab1.Rows[0]["HaulierTruck"];
//            rptRow["HaulierCollect"] = tab1.Rows[0]["HaulierCollect"];
//            rptRow["Attn"] = tab1.Rows[0]["Attn"];
//            rptRow["CrNote"] = tab1.Rows[0]["CrNote"];
//            rptRow["Haulier"] = tab1.Rows[0]["Haulier"];
//            rptRow["RefN"] = tab1.Rows[0]["RefN"];
//            rptRow["ET"] = SafeValue.SafeDateStr(tab1.Rows[0]["ET"]);
//            rptRow["ED"] = SafeValue.SafeDateStr(tab1.Rows[0]["ED"]);
//            string pod = SafeValue.SafeString(tab1.Rows[0]["Pod"], "");
//            rptRow["Pod"] = EzshipHelper.GetPortName(pod);
//            string pol = SafeValue.SafeString(tab1.Rows[0]["Pol"], "SGSIN");
//            rptRow["POL"] = EzshipHelper.GetPortName(pol);
//            rptRow["Ves"] = tab1.Rows[0]["Ves"];
//            rptRow["Bkg"] = tab1.Rows[0]["Bkg"];
//            rptRow["Bl"] = tab1.Rows[0]["Bl"];
//            rptRow["Agent"] = EzshipHelper.GetPartyName(tab1.Rows[0]["Agent"]);

//            rptRow["Us"] = tab1.Rows[0]["Us"];
//            rptRow["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];


//            tab.Rows.Add(rptRow);
//        }
//        catch (Exception ex)
//        {
//        }
//        return tab;
    }

    public static DataSet PrintCargoManifest(string refno)
    {

        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintCargoManifest '{0}','{1}','{2}','{3}','{4}'", refno, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;
    }



    #endregion
      
    #region export
    public static DataSet PrintBL(string refN, string jobN)
    {
        return PrintBLDraft(refN, jobN);
    }
    public static DataSet PrintBLDraft(string refN, string jobN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintBLDraft '{0}','{1}','{2}','{3}','{4}'", refN, jobN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;

//        string sql = string.Format(@"SELECT det.HblNo AS HBLN, det.placeLoadingName AS POL, det.placeDischargeName AS POD, det.PlaceDeliveryname AS PlaceDel, det.PlaceReceiveName AS PlaceRec, 
//                      det.PreCarriage, det.SShipperRemark AS SHIPPER1, det.SConsigneeRemark AS CON1, det.ExpressBL,
//                      det.SNotifyPartyRemark AS NP1, det.SAgentRemark AS AGENT1, mast.Vessel + ' ' + mast.Voyage AS VES, mast.FinDest,det.FrtTerm,det.SurrenderBl
//,det.PlaceReceiveTerm,det.PlaceDeliveryTerm,mast.JobType,det.ShipOnBoardDate
//FROM SeaExport AS det INNER JOIN SeaExportRef AS mast ON det.RefNo = mast.RefNo
//WHERE     (det.RefNo = '{0}') and det.JobNo='{1}'", refN, jobN);
//        DataTable tab = ConnectSql.GetTab(sql);
//        DataTable Mast = new DataTable("Mast");
//        Mast.Columns.Add("RefN");
//        Mast.Columns.Add("HBLN");
//        Mast.Columns.Add("POL");
//        Mast.Columns.Add("POD");
//        Mast.Columns.Add("FIN");
//        Mast.Columns.Add("SHIPPER1");
//        Mast.Columns.Add("CON1");
//        Mast.Columns.Add("NP1");
//        Mast.Columns.Add("AGENT1");
//        Mast.Columns.Add("VES");
//        Mast.Columns.Add("ShipD");
//        Mast.Columns.Add("CompanyName");

//        Mast.Columns.Add("PreCarriage");
//        Mast.Columns.Add("PlaceRec");
//        Mast.Columns.Add("PlaceDel");
//        Mast.Columns.Add("FrtPayableAt");
//        Mast.Columns.Add("BlCnt");
//        Mast.Columns.Add("ClauseFreight");
//        Mast.Columns.Add("ClauseBl");
//        Mast.Columns.Add("ClauseCargo");
//        Mast.Columns.Add("ClauseContainer");
//        Mast.Columns.Add("OblTerm");
//        Mast.Columns.Add("ClauseSurrender");

//        Mast.Columns.Add("FrtChg");
//        Mast.Columns.Add("Qty");
//        Mast.Columns.Add("PriceUnit");
//        Mast.Columns.Add("Prepaid");
//        Mast.Columns.Add("CltAmt");


//        DataTable Detail = new DataTable("Detail");
//        Detail.Columns.Add("RefN");
//        Detail.Columns.Add("TERMID");
//        Detail.Columns.Add("Weight");
//        Detail.Columns.Add("VOLUME");
//        Detail.Columns.Add("MKGS");
//        Detail.Columns.Add("DES");
//        Detail.Columns.Add("CONTN");
//        DateTime shipD = new DateTime(1900, 1, 1);
//        try
//        {
//            if (tab.Rows.Count == 1)
//            {
//                string jobType = tab.Rows[0]["JobType"].ToString();
//                if (jobType == "FCL")
//                {
//                    string sql1 = string.Format(@"SELECT ContainerNo+'/'+SealNo+'/'+ContainerType AS ContN, Volume AS M3, Weight AS WT, Qty AS qty, PackageType AS Packtype, Marking, Description 
//FROM SeaExportMkg WHERE (RefNo = '{0}') AND (JobNo = '{1}') and MkgType='Cont' ", refN, jobN);
//                    DataTable tab1 = ConnectSql.GetTab(sql1);
//                    decimal wt = 0;
//                    decimal m3 = 0;
//                    string contNStr = "";
//                    string des = "";
//                    for (int i = 0; i < tab1.Rows.Count; i++)
//                    {
//                        wt += SafeValue.SafeDecimal(tab1.Rows[i]["Wt"], 0);
//                        m3 += SafeValue.SafeDecimal(tab1.Rows[i]["M3"], 0);
//                        if (i == 0)
//                        {
//                            des = " S.T.C." + "\r\n\r\n" + tab1.Rows[i]["Description"];
//                            contNStr += SafeValue.SafeString(tab1.Rows[i]["ContN"]);
//                        }
//                        else
//                        {
//                            des += "\r\n" + tab1.Rows[i]["Description"];
//                            contNStr += "\n" + SafeValue.SafeString(tab1.Rows[i]["ContN"]);
//                        }



//                    }
//                    DataRow rptRow = Detail.NewRow();
//                    rptRow["RefN"] = jobN;
//                    if (wt > 0)
//                        rptRow["Weight"] = wt.ToString("0.000");
//                    if (m3 > 0)
//                        rptRow["VOLUME"] = m3.ToString("0.000");
//                    rptRow["MKGS"] = contNStr;
//                    rptRow["DES"] = des;
//                    Detail.Rows.Add(rptRow);
//                }
//                else
//                {
//                    string sql1 = string.Format(@"SELECT ContainerNo+'/'+SealNo+'/'+ContainerType AS ContN, Volume AS M3, Weight AS WT, Qty AS qty, PackageType AS Packtype, Marking, Description 
//FROM SeaExportMkg WHERE (RefNo = '{0}') AND (JobNo = '{1}') and MkgType='Bl' ", refN, jobN);
//                    DataTable tab1 = ConnectSql.GetTab(sql1);

//                    for (int i = 0; i < tab1.Rows.Count; i++)
//                    {
//                        DataRow rptRow = Detail.NewRow();
//                        rptRow["RefN"] = jobN;
//                        decimal wt = SafeValue.SafeDecimal(tab1.Rows[i]["Wt"], 0);
//                        decimal m3 = SafeValue.SafeDecimal(tab1.Rows[i]["M3"], 0);
//                        if (wt > 0)
//                            rptRow["Weight"] = wt.ToString("0.000");
//                        if (m3 > 0)
//                            rptRow["VOLUME"] = m3.ToString("0.000");

//                        rptRow["CONTN"] = SafeValue.SafeString(tab1.Rows[i]["ContN"]);

//                        rptRow["MKGS"] = tab1.Rows[i]["Marking"];

//                        rptRow["DES"] = tab1.Rows[i]["Qty"].ToString() + " " + tab1.Rows[i]["Packtype"].ToString() + " S.T.C." + "\r\n\r\n" + tab1.Rows[i]["Description"];

//                        Detail.Rows.Add(rptRow);
//                    }
//                }

//                DataRow rptRowMast = Mast.NewRow();
//                rptRowMast["RefN"] = jobN;
//                rptRowMast["HBLN"] = tab.Rows[0]["HBLN"];
//                rptRowMast["POL"] = tab.Rows[0]["POL"];
//                rptRowMast["POD"] = tab.Rows[0]["POD"];
//                rptRowMast["SHIPPER1"] = tab.Rows[0]["SHIPPER1"];
//                rptRowMast["CON1"] = tab.Rows[0]["CON1"];
//                rptRowMast["NP1"] = tab.Rows[0]["NP1"];
//                rptRowMast["AGENT1"] = tab.Rows[0]["AGENT1"];
//                rptRowMast["VES"] = tab.Rows[0]["VES"];
//                //rptRowMast["ClauseFreight"] = tab.Rows[0]["ClauseFreightTerm"];
//                //rptRowMast["ClauseBl"] = tab.Rows[0]["ClauseBl"];
//                //rptRowMast["ClauseCargo"] = tab.Rows[0]["ClauseCargo"];
//                //rptRowMast["ClauseContainer"] = "";//tab.Rows[0]["ClauseCargo"];

//                shipD = SafeValue.SafeDate(tab.Rows[0]["ShipOnBoardDate"], shipD);
//                if (shipD > new DateTime(2000, 1, 1))
//                    rptRowMast["ShipD"] = shipD.ToString("dd/MM/yyyy");

//                rptRowMast["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];


//                rptRowMast["Fin"] = tab.Rows[0]["PlaceDel"].ToString() + "  " + tab.Rows[0]["PlaceDeliveryTerm"].ToString();// SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format("select Name from XXPort where Code='{0}'", tab.Rows[0]["FinDest"])), tab.Rows[0]["FinDest"].ToString());
//                rptRowMast["PreCarriage"] = tab.Rows[0]["PreCarriage"];
//                rptRowMast["PlaceRec"] = tab.Rows[0]["PlaceRec"].ToString() + "  " + tab.Rows[0]["PlaceReceiveTerm"].ToString();
//                rptRowMast["PlaceDel"] = tab.Rows[0]["PlaceDel"].ToString() + "  " + tab.Rows[0]["PlaceDeliveryTerm"].ToString();
//                string frtTerm = SafeValue.SafeString(tab.Rows[0]["FrtTerm"], "").ToLower();
//                if (frtTerm == "prepaid")
//                    rptRowMast["FrtPayableAt"] = "SINGAPORE";
//                else
//                    rptRowMast["FrtPayableAt"] = tab.Rows[0]["POD"];
//                string surrentBl = SafeValue.SafeString(tab.Rows[0]["SurrenderBl"], "N").ToLower();
//                if (surrentBl == "y")
//                {
//                    rptRowMast["ClauseSurrender"] = "Original Bl Surrendered In Singapore";
//                }
//                if (frtTerm == "prepaid")
//                    rptRowMast["OblTerm"] = "FREIGHT PREPAID";
//                else
//                    rptRowMast["OblTerm"] = "FREIGHT COLLECT";

//                rptRowMast["BlCnt"] = string.Format("{0}", tab.Rows[0]["ExpressBL"]) == "Y" ? "(0) ZERO" : "(3) THREE";// tab.Rows[0]["HBLN"];


//                string sql_det = string.Format(@"SELECT ChgCode, Description, Qty, Price, Amount, Currency, PrintInd, WtEntryInd, PrintTerm
//FROM SeaExportDetail where RefNo='{0}' and JobNo='{1}' and PrintInd='True'", refN, jobN);
//                DataTable tab_det = ConnectSql.GetTab(sql_det);
//                string chg = "";
//                string qty = "";
//                string price = "";
//                string prepaid = "";
//                string cltAmt = "";
//                for (int d = 0; d < tab_det.Rows.Count; d++)
//                {
//                    DataRow row2 = tab_det.Rows[d];
//                    chg += row2["Description"].ToString() + "\n";
//                    qty += row2["Qty"].ToString() + "\n";
//                    price += SafeValue.SafeDecimal(row2["Price"], 0).ToString("0.00") + "\n";
//                    prepaid += SafeValue.SafeString(row2["PrintTerm"], "") + "\n";
//                    cltAmt += SafeValue.SafeDecimal(row2["Amount"], 0).ToString("0.00") + "\n";

//                }
//                rptRowMast["FrtChg"] = chg;
//                rptRowMast["Qty"] = qty;
//                rptRowMast["PriceUnit"] = price;
//                rptRowMast["Prepaid"] = prepaid;
//                rptRowMast["CltAmt"] = cltAmt;
//                Mast.Rows.Add(rptRowMast);

//            }

//        }
//        catch (Exception ex)
//        { }
//        DataSet ds = new DataSet();
//        ds.Tables.Add(Mast);
//        ds.Tables.Add(Detail);
//        DataRelation r = new DataRelation("", Mast.Columns["RefN"], Detail.Columns["RefN"]);
//        return ds;
    }
    public static DataSet PrintBlAttachList(string refN, string jobN)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintBlAttachList '{0}','{1}','{2}','{3}','{4}'", refN, jobN, "", "", "");
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail = ds_temp.Tables[1].Copy();
            Detail.TableName = "Detail";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail);
            DataRelation r = new DataRelation("", Mast.Columns["EXPORTN"], Detail.Columns["EXPORTN"]);
            ds.Relations.Add(r);
        }
        catch (Exception ex) { }
        return ds;


//        string sql = string.Format(@"SELECT mkg.Weight AS Wt, mkg.Volume AS M3, mkg.Qty AS Pkgs, mkg.PackageType AS Packtype, mkg.ContainerNo+'/'+mkg.SealNo AS CONTN, mkg.Marking, mkg.Description, 
//                      det.HblNo AS HBL, mast.Vessel + '/' + mast.Voyage AS VES, det.ShipOnBoardDate AS ET, mast.OblNo AS obl
//FROM         SeaExportMkg AS mkg INNER JOIN
//                      SeaExport AS det ON mkg.JobNo= det.JobNo INNER JOIN
//                      SeaExportRef AS mast ON det.RefNo= mast.RefNo AND mkg.RefNo= mast.RefNo
//WHERE (det.JobNo = '{1}') AND (det.RefNo= '{0}')", refN, jobN);

//        DataTable Mast = new DataTable("Mast");
//        Mast.Columns.Add("EXPORTN");
//        Mast.Columns.Add("Obl");
//        Mast.Columns.Add("PACK");
//        Mast.Columns.Add("WT");
//        Mast.Columns.Add("M3");
//        DataTable Detail = new DataTable("Detail");
//        Detail.Columns.Add("EXPORTN");
//        Detail.Columns.Add("CONTN");
//        Detail.Columns.Add("MKGS");
//        Detail.Columns.Add("DES");
//        Detail.Columns.Add("PACK");
//        Detail.Columns.Add("WT");
//        Detail.Columns.Add("M3");
//        DataTable dt = ConnectSql.GetTab(sql);

//        decimal sumWt = 0;
//        decimal sumM3 = 0;
//        int sumPack = 0;

//        for (int i = 0; i < dt.Rows.Count; i++)
//        {
//            DataRow rptRow = Detail.NewRow();
//            rptRow["EXPORTN"] = jobN;
//            rptRow["CONTN"] =dt.Rows[i]["CONTN"];

//            rptRow["MKGS"] = dt.Rows[i]["Marking"];
//            rptRow["DES"] = dt.Rows[i]["Description"];
//            rptRow["PACK"] = dt.Rows[i]["Pkgs"];
//            rptRow["WT"] = SafeValue.SafeDecimal(dt.Rows[i]["WT"], 0).ToString("0.000");
//            rptRow["M3"] = SafeValue.SafeDecimal(dt.Rows[i]["M3"], 0).ToString("0.000");

//            sumWt += SafeValue.SafeDecimal(dt.Rows[i]["Wt"], 0);
//            sumM3 += SafeValue.SafeDecimal(dt.Rows[i]["M3"], 0);
//            sumPack += SafeValue.SafeInt(dt.Rows[i]["Pkgs"], 0);


//            Detail.Rows.Add(rptRow);
//        }

//        DataRow rptRow1 = Mast.NewRow();
//        rptRow1["EXPORTN"] = jobN;
//        rptRow1["Obl"] = dt.Rows[0]["Hbl"];
//        rptRow1["WT"] = sumWt.ToString("0.000");
//        rptRow1["M3"] = sumM3.ToString("0.000");
//        rptRow1["PACK"] = sumPack.ToString();
//        Mast.Rows.Add(rptRow1);

//        DataSet ds = new DataSet();
//        ds.Tables.Add(Mast);
//        ds.Tables.Add(Detail);
//        DataRelation r = new DataRelation("", Mast.Columns["EXPORTN"], Detail.Columns["EXPORTN"]);
//        ds.Relations.Add(r);

//        return ds;
    }

    public static DataTable PrintExpPermit(string refN,string jobN)
    {
        DataTable dt = new DataTable();
        try
        {
            string strsql = string.Format(@"exec proc_PrintExpPermit '{0}','{1}','{2}','{3}','{4}'", refN, jobN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            dt = ds_temp.Tables[0].Copy();
            //dt.TableName = "Permit";
        }
        catch (Exception ex) { }
        return dt;


//        string sql = string.Format(@"SELECT  det.CustomerID AS CUST, mast.Vessel + '/' + mast.Voyage AS VES, mast.Eta AS ET, mast.CrAgentID AS CARRIER, det.HblNo AS HBL, mast.UserID AS US, 
//                      mast.Etd AS ED, mast.OblNo AS OBL
//FROM         SeaExport AS det INNER JOIN
//                      SeaExportRef AS mast ON det.RefNo= mast.RefNo
//WHERE     det.RefNo='{0}' and (det.JobNo= '{1}')", refN,jobN);

//        DataTable tab1 = ConnectSql.GetTab(sql);
//        DataTable tab = new DataTable();
//        tab.Columns.Add("Cust");
//        tab.Columns.Add("Et");
//        tab.Columns.Add("Carrier");
//        tab.Columns.Add("Hbl");
//        tab.Columns.Add("Us");
//        tab.Columns.Add("Etd");
//        tab.Columns.Add("Obl");
//        tab.Columns.Add("Ves");
//        tab.Columns.Add("CompanyName");
//        for (int i = 0; i < tab1.Rows.Count; i++)
//        {
//            DataRow row1 = tab1.Rows[i];
//            DataRow row = tab.NewRow();
//            string custId = row1["Cust"].ToString();
//            row["Cust"] = EzshipHelper.GetPartyName(custId);
//            if (row["Cust"].ToString() == "-1")
//                row["Cust"] = "";
//            row["Et"] = SafeValue.SafeDate(row1["Et"], DateTime.Today).ToString("dd/MM/yyyy");
//            string vendorId = row1["Carrier"].ToString();
//            row["Carrier"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select Name from XXParty where PartyId='" + vendorId + "'"), "");
//            if (row["Carrier"].ToString() == "-1")
//                row["Carrier"] = "";
//            row["Hbl"] = row1["Hbl"];
//            row["Us"] = row1["Us"];
//            row["Ves"] = row1["Ves"];
//            row["Etd"] = SafeValue.SafeDateStr(row1["Ed"]);
//            row["Obl"] = row1["Obl"];
//            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];


//            tab.Rows.Add(row);
//        }
//        return tab;
    }

    #endregion

    #region quotation
    public static DataSet dsQuotation(string invN, string userName)
    {
        DataSet set = new DataSet();
        string cust_id = "2001";// 
        string address = "";


        DataTable tab_mast = new DataTable("Mast");
        DataTable tab_det = new DataTable("Detail");
        #region init column
        tab_mast.Columns.Add("BarCode");
        tab_mast.Columns.Add("InvoiceN");
        tab_mast.Columns.Add("InvDate");
        tab_mast.Columns.Add("FromDate");
        tab_mast.Columns.Add("ToDate");
        tab_mast.Columns.Add("Name");
        tab_mast.Columns.Add("Address");
        tab_mast.Columns.Add("CustId");
        tab_mast.Columns.Add("Terms");
        tab_mast.Columns.Add("Pol");
        tab_mast.Columns.Add("Pod");
        tab_mast.Columns.Add("Currency");
        tab_mast.Columns.Add("ExRate");
        tab_mast.Columns.Add("Des");
        tab_mast.Columns.Add("UserId");
        tab_mast.Columns.Add("CompanyName");
        tab_mast.Columns.Add("CompanyInvHeader");

        tab_det.Columns.Add("InvoiceN");
        tab_det.Columns.Add("LineN");
        tab_det.Columns.Add("Des");
        tab_det.Columns.Add("Currency");
        tab_det.Columns.Add("Price");
        tab_det.Columns.Add("Qty");
        tab_det.Columns.Add("Unit");
        tab_det.Columns.Add("Rate");
        tab_det.Columns.Add("Rmk");
        #endregion
        string sql = string.Format(@"SELECT  DocNo, DocDate,DocFromDate,DocToDate, PartyTo, CurrencyId, ExRate,Pol,Pod, Term, UserId, EntryDate, Description
 FROM  SeaQuotation WHERE (DocNo = '{0}') ", invN);
        string sql1 = string.Format(@"SELECT DocLineNo,ChgCode, ChgDes1, Remark, GstType, Qty, Price, Unit, Currency, ExRate, Gst FROM SeaQuotationDet WHERE (DocNo = '{0}') order by DocLineNo", invN);

        DataTable master = ConnectSql.GetTab(sql);
        DataTable det = ConnectSql.GetTab(sql1);
        for (int i = 0; i < det.Rows.Count; i++)
        {
            DataRow oldRow = det.Rows[i];
            DataRow newRow = tab_det.NewRow();

            newRow["InvoiceN"] = invN;
            newRow["LineN"] = oldRow["DocLineNo"];
            newRow["Des"] = oldRow["ChgDes1"];
            newRow["Rmk"] = oldRow["Remark"];
            newRow["Currency"] = oldRow["Currency"];
            newRow["Qty"] = SafeValue.SafeDecimal(oldRow["QTY"], 0).ToString("0.000");
            newRow["Price"] = SafeValue.SafeDecimal(oldRow["PRICE"], 0).ToString("0.000");
            newRow["Unit"] = oldRow["UNIT"];
            newRow["Rate"] = SafeValue.SafeDecimal(oldRow["ExRate"], 0).ToString("0.000000");
            tab_det.Rows.Add(newRow);
        }
        for (int i = 0; i < 10 - det.Rows.Count; i++)
        {
            DataRow newRow = tab_det.NewRow();
            tab_det.Rows.Add(newRow);

        }
        if (master.Rows.Count == 1)
        {
            DataRow row_Source = master.Rows[0];
            DataRow row = tab_mast.NewRow();

            row["InvoiceN"] = invN;
            row["FromDate"] = SafeValue.SafeDate(row_Source["DocFromDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
            row["ToDate"] = SafeValue.SafeDate(row_Source["DocToDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
            row["InvDate"] = SafeValue.SafeDate(row_Source["DocDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");

            cust_id = SafeValue.SafeString(row_Source["PartyTo"], "");
            string sql_cust = "select Name,Address,Tel1,Fax1 from XXParty where PartyId = '" + cust_id + "'";
            DataTable tab_cust = ConnectSql.GetTab(sql_cust);
            if (tab_cust.Rows.Count > 0)
            {
                // cust_id = tab_cust.Rows[0][0].ToString();
                address = tab_cust.Rows[0]["Address"].ToString().Trim();
                string tel = tab_cust.Rows[0]["Tel1"].ToString().Trim();
                string fax = tab_cust.Rows[0]["Fax1"].ToString().Trim();

                if (tel.Length > 0)
                {
                    address += "\nTel:" + tel;
                }
                if (fax.Length > 0)
                {
                    address += " Fax:" + fax;
                }
                row["Name"] = tab_cust.Rows[0]["Name"].ToString() + "[" + cust_id + "]";
                row["Address"] = address;
            }

            row["CustId"] = cust_id;
            row["Terms"] = row_Source["Term"];
            row["Des"] = row_Source["Description"];

            row["Pol"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pol"] + "'");
            row["Pod"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pod"] + "'");
            row["Currency"] = row_Source["CurrencyId"];
            row["ExRate"] = SafeValue.SafeDecimal(row_Source["ExRate"], 1).ToString("0.000000");

            row["UserId"] = row_Source["UserId"];
            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["CompanyInvHeader"] = System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader2"];
            tab_mast.Rows.Add(row);
        }

        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_det);
        DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
        set.Relations.Add(r);

        return set;
    }
    public static DataSet dsAccountQuote(string invN, string userName)
    {
        DataSet set = new DataSet();
        string cust_id = "2001";// 
        string address = "";


        DataTable tab_mast = new DataTable("Mast");
        DataTable tab_det = new DataTable("Detail");
        #region init column
        tab_mast.Columns.Add("InvoiceN");
        tab_mast.Columns.Add("InvDate");
        tab_mast.Columns.Add("FromDate");
        tab_mast.Columns.Add("ToDate");
        tab_mast.Columns.Add("Name");
        tab_mast.Columns.Add("Address");
        tab_mast.Columns.Add("Attention");
        tab_mast.Columns.Add("Saleman");
        tab_mast.Columns.Add("SalemanTel");
        tab_mast.Columns.Add("Terms");
        tab_mast.Columns.Add("Pol");
        tab_mast.Columns.Add("Pod");
        tab_mast.Columns.Add("Currency");
        tab_mast.Columns.Add("Des");
        tab_mast.Columns.Add("UserId");
        tab_mast.Columns.Add("Gp20");
        tab_mast.Columns.Add("Gp40");
        tab_mast.Columns.Add("Hc40");
        tab_mast.Columns.Add("TsDay");
        tab_mast.Columns.Add("CompanyName");
        tab_mast.Columns.Add("CompanyInvHeader");

        tab_det.Columns.Add("InvoiceN");
        tab_det.Columns.Add("LineN");
        tab_det.Columns.Add("ChgCode");
        tab_det.Columns.Add("Currency");
        tab_det.Columns.Add("Price20");
        tab_det.Columns.Add("Price40");
        tab_det.Columns.Add("Unit");
        #endregion
        string sql = string.Format(@"SELECT SequenceId, JobType,  DocNo, DocDate, DocFromDate, DocToDate, PartyTo, Attention, CurrencyId, Term, Pol, Pod, Gp20, Gp40, Hc40, TransmitDay, 
                      Description, UserId, EntryDate FROM SeaQuote WHERE (DocNo = '{0}') ", invN);


        DataTable master = ConnectSql.GetTab(sql);
        if (master.Rows.Count == 1)
        {
            DataRow row_Source = master.Rows[0];
            DataRow row = tab_mast.NewRow();

            row["InvoiceN"] = invN;
            row["FromDate"] = SafeValue.SafeDate(row_Source["DocFromDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
            row["ToDate"] = SafeValue.SafeDate(row_Source["DocToDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
            row["InvDate"] = SafeValue.SafeDate(row_Source["DocDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");

            cust_id = SafeValue.SafeString(row_Source["PartyTo"], "");
            string sql_cust = "select Name,Address,Tel1,Fax1,SalesmanId from XXParty where PartyId = '" + cust_id + "'";
            DataTable tab_cust = ConnectSql.GetTab(sql_cust);
            if (tab_cust.Rows.Count > 0)
            {
                // cust_id = tab_cust.Rows[0][0].ToString();
                address = tab_cust.Rows[0]["Address"].ToString().Trim();
                string tel = tab_cust.Rows[0]["Tel1"].ToString().Trim();
                string fax = tab_cust.Rows[0]["Fax1"].ToString().Trim();

                if (tel.Length > 0)
                {
                    address += "\nTel:" + tel;
                }
                if (fax.Length > 0)
                {
                    address += " Fax:" + fax;
                }
                row["Name"] = tab_cust.Rows[0]["Name"];
                row["Address"] = address;
                row["Saleman"] = tab_cust.Rows[0]["SalesmanId"];
                row["SalemanTel"] = C2.Manager.ORManager.ExecuteScalar(string.Format("select Tel from [XXSalesman] where Code='{0}'", tab_cust.Rows[0]["SalesmanId"]));
            }

            row["Terms"] = row_Source["Term"];
            row["Des"] = row_Source["Description"];

            row["Pol"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pol"] + "'");
            row["Pod"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pod"] + "'");
            row["Currency"] = row_Source["CurrencyId"];
            row["Gp20"] = SafeValue.SafeDecimal(row_Source["Gp20"], 0).ToString("0.00");
            row["Gp40"] = SafeValue.SafeDecimal(row_Source["Gp40"], 0).ToString("0.00");
            row["Hc40"] = SafeValue.SafeDecimal(row_Source["Hc40"], 0).ToString("0.00");
            row["TsDay"] = row_Source["TransmitDay"];
            row["Attention"] = row_Source["Attention"];

            row["UserId"] = row_Source["UserId"];
            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["CompanyInvHeader"] = System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader2"];
            tab_mast.Rows.Add(row);

            string docId = row_Source["SequenceId"].ToString();
            string sql1 = string.Format("SELECT DocLineNo, ChgCode, ChgDes1, Remark, Price20, Price40, Unit, Currency, Gst, GstType FROM SeaQuoteDet WHERE (DocId = '{0}') order by DocLineNo", docId);
            DataTable det = ConnectSql.GetTab(sql1);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                DataRow oldRow = det.Rows[i];
                DataRow newRow = tab_det.NewRow();

                newRow["InvoiceN"] = invN;
                newRow["LineN"] = oldRow["DocLineNo"];
                newRow["ChgCode"] = oldRow["ChgDes1"];
                newRow["Currency"] = oldRow["Currency"];
                newRow["Price20"] = SafeValue.SafeDecimal(oldRow["Price20"], 0).ToString("0.00");
                newRow["Price40"] = SafeValue.SafeDecimal(oldRow["Price40"], 0).ToString("0.00");
                newRow["Unit"] = oldRow["Unit"];
                tab_det.Rows.Add(newRow);
            }
            //for (int i = 0; i < 10 - det.Rows.Count; i++)
            //{
            //    DataRow newRow = tab_det.NewRow();
            //    tab_det.Rows.Add(newRow);

            //}

        }
        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_det);
        DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
        set.Relations.Add(r);

        return set;
    }
    public static DataSet dsQuote_Fcl_old(string invN, string userName)
    {
        DataSet set = new DataSet();
        string cust_id = "2001";// 
        string address = "";


        DataTable tab_mast = new DataTable("Mast");
        DataTable tab_det = new DataTable("Detail");
        #region init column
        tab_mast.Columns.Add("InvoiceN");
        tab_mast.Columns.Add("ImpExpInd");
        tab_mast.Columns.Add("Title");
        tab_mast.Columns.Add("InvDate");
        tab_mast.Columns.Add("ToDate");
        tab_mast.Columns.Add("Name");
        tab_mast.Columns.Add("Address");
        tab_mast.Columns.Add("Attention");
        tab_mast.Columns.Add("Saleman");
        tab_mast.Columns.Add("SalemanTel");
        tab_mast.Columns.Add("Pol");
        tab_mast.Columns.Add("Pod");
        tab_mast.Columns.Add("ViaPort");
        tab_mast.Columns.Add("Currency");
        tab_mast.Columns.Add("Note");
        tab_mast.Columns.Add("Rmk");
        tab_mast.Columns.Add("UserId");
        tab_mast.Columns.Add("Gp20");
        tab_mast.Columns.Add("Gp40");
        tab_mast.Columns.Add("Hc40");
        tab_mast.Columns.Add("Frequency");
        tab_mast.Columns.Add("TsDay");
        tab_mast.Columns.Add("CompanyName");
        tab_mast.Columns.Add("CompanyInvHeader");
        tab_mast.Columns.Add("ContType");
        tab_mast.Columns.Add("ContPrice");
        tab_mast.Columns.Add("ExRate");
        tab_mast.Columns.Add("TotAmt");

        tab_det.Columns.Add("InvoiceN");
        tab_det.Columns.Add("LineN");
        tab_det.Columns.Add("GroupTitle");
        tab_det.Columns.Add("ChgCode");
        tab_det.Columns.Add("Currency");
        tab_det.Columns.Add("Price");
        tab_det.Columns.Add("Unit");
        #endregion
        string sql = string.Format(@"SELECT SequenceId, Title,PartyTo, Pol,Pod,ViaPort, ImpExpInd, FclLclInd, Frequency, LclChg, Subject, QuoteNo, Status, CreateUser, CreateDate, UpdateUser, UpdateDate, 
                      QuoteDate, ExpireDate,Note, Rmk, Attention, TransmitDay, Gp20, Gp40, Hc40, CurrencyId, SalesmanId,ContType,ContPrice,ExRate
FROM SeaQuote1 WHERE (QuoteNo = '{0}') ", invN);


        DataTable master = ConnectSql.GetTab(sql);
        if (master.Rows.Count == 1)
        {
            DataRow row_Source = master.Rows[0];
            decimal totAmt = 0;
            string docId = row_Source["SequenceId"].ToString();
            string sql1 = string.Format("SELECT  QuoteLineNo,GroupTitle,ChgCode,ChgDes, Currency, Price, Unit, MinAmt, Rmk FROM SeaQuoteDet1 WHERE (QuoteId = '{0}') order by QuoteLineNo", docId);
            DataTable det = ConnectSql.GetTab(sql1);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                DataRow oldRow = det.Rows[i];
                DataRow newRow = tab_det.NewRow();

                newRow["InvoiceN"] = invN;
                newRow["LineN"] = oldRow["QuoteLineNo"];
                newRow["GroupTitle"] = SafeValue.SafeString(oldRow["GroupTitle"], "").Trim();
                //string chgCode = SafeValue.SafeString(oldRow["ChgCode"], "");
                //string sql_chgcode = string.Format("select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", chgCode);
                newRow["ChgCode"] = oldRow["ChgDes"];// SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_chgcode), chgCode);
                newRow["Currency"] = oldRow["Currency"];
                newRow["Price"] = SafeValue.SafeDecimal(oldRow["Price"], 0).ToString("0.00");
                newRow["Unit"] = oldRow["Unit"];
                totAmt += SafeValue.SafeDecimal(oldRow["Price"], 0);
                tab_det.Rows.Add(newRow);
            }

            //mast tab
            DataRow row = tab_mast.NewRow();

            row["InvoiceN"] = invN;
            row["ToDate"] = SafeValue.SafeDate(row_Source["ExpireDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
            row["InvDate"] = SafeValue.SafeDate(row_Source["QuoteDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");

            cust_id = SafeValue.SafeString(row_Source["PartyTo"], "");
            string sql_cust = "select Name,Address,Tel1,Fax1,SalesmanId from XXParty where PartyId = '" + cust_id + "'";
            DataTable tab_cust = ConnectSql.GetTab(sql_cust);
            if (tab_cust.Rows.Count > 0)
            {
                // cust_id = tab_cust.Rows[0][0].ToString();
                address = tab_cust.Rows[0]["Address"].ToString().Trim();
                string tel = tab_cust.Rows[0]["Tel1"].ToString().Trim();
                string fax = tab_cust.Rows[0]["Fax1"].ToString().Trim();

                if (tel.Length > 0)
                {
                    address += "\nTel:" + tel;
                }
                if (fax.Length > 0)
                {
                    address += " Fax:" + fax;
                }
                row["Name"] = tab_cust.Rows[0]["Name"];
                row["Address"] = address;
                row["Saleman"] = tab_cust.Rows[0]["SalesmanId"];
                row["SalemanTel"] = C2.Manager.ORManager.ExecuteScalar(string.Format("select Tel from [XXSalesman] where Code='{0}'", tab_cust.Rows[0]["SalesmanId"]));
            }

            row["Title"] = row_Source["Title"];
            row["Rmk"] = row_Source["Rmk"];
            row["Note"] = row_Source["Note"];
            string impExpInd = row_Source["ImpExpInd"].ToString();
            row["Pod"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pod"] + "'");
            row["Pol"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pol"] + "'");
            if (row["Pol"] == System.Configuration.ConfigurationManager.AppSettings["PortName"].ToString())
            {
                row["ImpExpInd"] = "Import";
            }
            else
            {
                row["ImpExpInd"] = "Export";
            }
            row["ViaPort"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["ViaPort"] + "'"), "DIRECT");
            row["Currency"] = row_Source["CurrencyId"];
            row["Gp20"] = SafeValue.SafeDecimal(row_Source["Gp20"], 0).ToString("0.00");
            row["Gp40"] = SafeValue.SafeDecimal(row_Source["Gp40"], 0).ToString("0.00");
            row["Hc40"] = SafeValue.SafeDecimal(row_Source["Hc40"], 0).ToString("0.00");
            row["TsDay"] = row_Source["TransmitDay"];
            row["Frequency"] = row_Source["Frequency"];
            row["Attention"] = row_Source["Attention"];

            row["UserId"] = row_Source["UpdateUser"];
            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["CompanyInvHeader"] = System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader2"];
            row["ContType"] = row_Source["ContType"];
            decimal price = SafeValue.SafeDecimal(row_Source["ContPrice"], 0);
            decimal exRate = SafeValue.SafeDecimal(row_Source["ExRate"], 1);
            row["ContPrice"] = price.ToString("0.00");
            row["ExRate"] = exRate.ToString("0.000000");
            row["TotAmt"] = (totAmt + SafeValue.ChinaRound(price * exRate, 2)).ToString("0.00");
            tab_mast.Rows.Add(row);
        }
        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_det);
        DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
        set.Relations.Add(r);

        return set;
    }
    //public static DataSet dsQuote_Fcl(string invN, string userName)
    //{
    //    DataSet set = new DataSet();
    //    try
    //    {
    //        string strsql = string.Format(@"exec proc_dsQuote '{0}','{1}','{2}','{3}','{4}'", invN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
    //        DataSet ds_temp = ConnectSql.GetDataSet(strsql);
    //        DataTable mast = ds_temp.Tables[0].Copy();
    //        mast.TableName = "Mast";
    //        DataTable det = ds_temp.Tables[1].Copy();
    //        det.TableName = "Detail";
    //        set.Tables.Add(mast);
    //        set.Tables.Add(det);
    //        set.Relations.Add("Rela", mast.Columns["RefNo"], det.Columns["RefNo"]);
    //    }
    //    catch (Exception ex) { }
    //    return set;


        //        DataSet set = new DataSet();
        //        string cust_id = "2001";// 
        //        string address = "";

        //        DataTable tab_mast = new DataTable("Mast");
        //        DataTable tab_det = new DataTable("Detail");
        //        #region init column
        //        tab_mast.Columns.Add("InvoiceN");
        //        tab_mast.Columns.Add("ImpExpInd");
        //        tab_mast.Columns.Add("Title");
        //        tab_mast.Columns.Add("InvDate");
        //        tab_mast.Columns.Add("ToDate");
        //        tab_mast.Columns.Add("Name");
        //        tab_mast.Columns.Add("Address");
        //        tab_mast.Columns.Add("Attention");
        //        tab_mast.Columns.Add("Saleman");
        //        tab_mast.Columns.Add("SalemanTel");
        //        tab_mast.Columns.Add("Pol");
        //        tab_mast.Columns.Add("Pod");
        //        tab_mast.Columns.Add("ViaPort");
        //        tab_mast.Columns.Add("Currency");
        //        tab_mast.Columns.Add("Note");
        //        tab_mast.Columns.Add("Rmk");
        //        tab_mast.Columns.Add("UserId");
        //        tab_mast.Columns.Add("Gp20");
        //        tab_mast.Columns.Add("Gp40");
        //        tab_mast.Columns.Add("Hc40");
        //        tab_mast.Columns.Add("Frequency");
        //        tab_mast.Columns.Add("TsDay");
        //        tab_mast.Columns.Add("CompanyName");
        //        tab_mast.Columns.Add("CompanyInvHeader");
        //        tab_mast.Columns.Add("ContType");
        //        tab_mast.Columns.Add("ContPrice");
        //        tab_mast.Columns.Add("ExRate");
        //        tab_mast.Columns.Add("TotAmt");

        //        tab_det.Columns.Add("InvoiceN");
        //        tab_det.Columns.Add("LineN");
        //        tab_det.Columns.Add("GroupTitle");
        //        tab_det.Columns.Add("ChgCode");
        //        tab_det.Columns.Add("Currency");
        //        tab_det.Columns.Add("ExRate");
        //        tab_det.Columns.Add("Price");
        //        tab_det.Columns.Add("Unit");
        //        #endregion
        //        string sql = string.Format(@"SELECT SequenceId, Title,PartyTo, Pol,Pod,ViaPort, ImpExpInd, FclLclInd, Frequency, LclChg, Subject, QuoteNo, Status, CreateUser, CreateDate, UpdateUser, UpdateDate, 
        //                              QuoteDate, ExpireDate,Note, Rmk, Attention, TransmitDay, Gp20, Gp40, Hc40, CurrencyId, SalesmanId,ContType,ContPrice,ExRate
        //        FROM SeaQuote1 WHERE (QuoteNo = '{0}') ", invN);


        //        DataTable master = ConnectSql.GetTab(sql);
        //        if (master.Rows.Count == 1)
        //        {
        //            DataRow row_Source = master.Rows[0];
        //            decimal totAmt = 0;
        //            string docId = row_Source["SequenceId"].ToString();
        //            string sql1 = string.Format("SELECT  QuoteLineNo,GroupTitle,ChgCode,ChgDes, Currency,ExRate, Price, Unit, MinAmt, Rmk FROM SeaQuoteDet1 WHERE (QuoteId = '{0}') order by QuoteLineNo", docId);
        //            DataTable det = ConnectSql.GetTab(sql1);
        //            for (int i = 0; i < det.Rows.Count; i++)
        //            {
        //                DataRow oldRow = det.Rows[i];
        //                DataRow newRow = tab_det.NewRow();

        //                newRow["InvoiceN"] = invN;
        //                newRow["LineN"] = oldRow["QuoteLineNo"];
        //                newRow["GroupTitle"] = SafeValue.SafeString(oldRow["GroupTitle"], "").Trim();
        //                //string chgCode = SafeValue.SafeString(oldRow["ChgCode"], "");
        //                //string sql_chgcode = string.Format("select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", chgCode);
        //                newRow["ChgCode"] = oldRow["ChgDes"];// SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_chgcode), chgCode);
        //                newRow["Currency"] = oldRow["Currency"];
        //                decimal exRateDet = SafeValue.SafeDecimal(oldRow["ExRate"], 1);
        //                newRow["ExRate"] = exRateDet.ToString("0.000000");
        //                newRow["Price"] = SafeValue.SafeDecimal(oldRow["Price"], 0).ToString("0.00");
        //                newRow["Unit"] = oldRow["Unit"];
        //                totAmt += SafeValue.ChinaRound(SafeValue.SafeDecimal(oldRow["Price"], 0) * exRateDet, 2);
        //                tab_det.Rows.Add(newRow);
        //            }

        //            //mast tab
        //            DataRow row = tab_mast.NewRow();

        //            row["InvoiceN"] = invN;
        //            row["ToDate"] = SafeValue.SafeDate(row_Source["ExpireDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
        //            row["InvDate"] = SafeValue.SafeDate(row_Source["QuoteDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");

        //            cust_id = SafeValue.SafeString(row_Source["PartyTo"], "");
        //            string sql_cust = "select Name,Address,Tel1,Fax1,SalesmanId from XXParty where PartyId = '" + cust_id + "'";
        //            DataTable tab_cust = ConnectSql.GetTab(sql_cust);
        //            if (tab_cust.Rows.Count > 0)
        //            {
        //                // cust_id = tab_cust.Rows[0][0].ToString();
        //                address = tab_cust.Rows[0]["Address"].ToString().Trim();
        //                string tel = tab_cust.Rows[0]["Tel1"].ToString().Trim();
        //                string fax = tab_cust.Rows[0]["Fax1"].ToString().Trim();

        //                if (tel.Length > 0)
        //                {
        //                    address += "\nTel:" + tel;
        //                }
        //                if (fax.Length > 0)
        //                {
        //                    address += " Fax:" + fax;
        //                }
        //                row["Name"] = tab_cust.Rows[0]["Name"];
        //                row["Address"] = address;
        //                row["Saleman"] = tab_cust.Rows[0]["SalesmanId"];
        //                row["SalemanTel"] = C2.Manager.ORManager.ExecuteScalar(string.Format("select Tel from [XXSalesman] where Code='{0}'", tab_cust.Rows[0]["SalesmanId"]));
        //            }

        //            row["Title"] = row_Source["Title"];
        //            row["Rmk"] = row_Source["Rmk"];
        //            row["Note"] = row_Source["Note"];
        //            string impExpInd = row_Source["ImpExpInd"].ToString();
        //            row["Pol"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pol"] + "'");
        //            if (row["Pol"] == System.Configuration.ConfigurationManager.AppSettings["PortName"].ToString())
        //            {
        //                row["ImpExpInd"] = "Import";
        //            }
        //            else
        //            {
        //                row["ImpExpInd"] = "Export";
        //            }
        //            string viaPort = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["ViaPort"] + "'"), row_Source["ViaPort"].ToString());
        //            row["Pod"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pod"] + "'");
        //            if (viaPort.Length > 0)
        //                row["ViaPort"] = "(VIA:" + viaPort + ")";

        //            row["Currency"] = row_Source["CurrencyId"];
        //            row["Gp20"] = SafeValue.SafeDecimal(row_Source["Gp20"], 0).ToString("0.00");
        //            row["Gp40"] = SafeValue.SafeDecimal(row_Source["Gp40"], 0).ToString("0.00");
        //            row["Hc40"] = SafeValue.SafeDecimal(row_Source["Hc40"], 0).ToString("0.00");
        //            row["TsDay"] = row_Source["TransmitDay"];
        //            row["Frequency"] = row_Source["Frequency"];
        //            row["Attention"] = row_Source["Attention"];

        //            row["UserId"] = row_Source["UpdateUser"];
        //            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
        //            row["CompanyInvHeader"] = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
        //            row["ContType"] = row_Source["ContType"];
        //            decimal price = SafeValue.SafeDecimal(row_Source["ContPrice"], 0);
        //            decimal exRate = SafeValue.SafeDecimal(row_Source["ExRate"], 1);
        //            row["ContPrice"] = price.ToString("0.00");
        //            if (exRate > 0 && exRate != 1)
        //                row["ExRate"] = "*" + exRate.ToString("0.000000");
        //            row["TotAmt"] = (totAmt + SafeValue.ChinaRound(price * exRate, 2)).ToString("0.00");
        //            tab_mast.Rows.Add(row);
        //        }
        //        set.Tables.Add(tab_mast);
        //        set.Tables.Add(tab_det);
        //        DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
        //        set.Relations.Add(r);

        //        return set;
    //}

    //public static DataSet dsQuote_Lcl(string invN, string userName)
    //{
    //    DataSet set = new DataSet();
    //    try
    //    {
    //        string strsql = string.Format(@"exec proc_dsQuote '{0}','{1}','{2}','{3}','{4}'", invN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
    //        DataSet ds_temp = ConnectSql.GetDataSet(strsql);
    //        DataTable mast = ds_temp.Tables[0].Copy();
    //        mast.TableName = "Mast";
    //        DataTable det = ds_temp.Tables[1].Copy();
    //        det.TableName = "Detail";
    //        set.Tables.Add(mast);
    //        set.Tables.Add(det);
    //        set.Relations.Add("Rela", mast.Columns["RefNo"], det.Columns["RefNo"]);
    //    }
    //    catch (Exception ex) { }
    //    return set;

//        DataSet set = new DataSet();
//        string cust_id = "2001";// 
//        string address = "";


//        DataTable tab_mast = new DataTable("Mast");
//        DataTable tab_det = new DataTable("Detail");
//        #region init column
//        tab_mast.Columns.Add("InvoiceN");
//        tab_mast.Columns.Add("ImpExpInd");
//        tab_mast.Columns.Add("Title");
//        tab_mast.Columns.Add("InvDate");
//        tab_mast.Columns.Add("ToDate");
//        tab_mast.Columns.Add("Name");
//        tab_mast.Columns.Add("Address");
//        tab_mast.Columns.Add("Attention");
//        tab_mast.Columns.Add("Saleman");
//        tab_mast.Columns.Add("SalemanTel");
//        tab_mast.Columns.Add("Pol");
//        tab_mast.Columns.Add("Pod");
//        tab_mast.Columns.Add("ViaPort");
//        tab_mast.Columns.Add("Currency");
//        tab_mast.Columns.Add("ExRate");
//        tab_mast.Columns.Add("Note");
//        tab_mast.Columns.Add("Rmk");
//        tab_mast.Columns.Add("UserId");
//        tab_mast.Columns.Add("LclChg");
//        tab_mast.Columns.Add("Frequency");
//        tab_mast.Columns.Add("TsDay");
//        tab_mast.Columns.Add("CompanyName");
//        tab_mast.Columns.Add("CompanyInvHeader");
//        tab_mast.Columns.Add("TotAmt");

//        tab_det.Columns.Add("InvoiceN");
//        tab_det.Columns.Add("LineN");
//        tab_det.Columns.Add("GroupTitle");
//        tab_det.Columns.Add("ChgCode");
//        tab_det.Columns.Add("Currency");
//        tab_det.Columns.Add("ExRate");
//        tab_det.Columns.Add("Qty");
//        tab_det.Columns.Add("Price");
//        tab_det.Columns.Add("Unit");
//        tab_det.Columns.Add("Amt");
//        tab_det.Columns.Add("MinAmt");
//        #endregion
//        string sql = string.Format(@"SELECT SequenceId, Title,PartyTo, Pol,Pod,ViaPort, ImpExpInd, FclLclInd, Frequency, LclChg, Subject, QuoteNo, Status, CreateUser, CreateDate, UpdateUser, UpdateDate, 
//                      QuoteDate, ExpireDate,Note, Rmk, Attention, TransmitDay, Gp20, Gp40, Hc40, CurrencyId,ExRate, SalesmanId
//FROM SeaQuote1 WHERE (QuoteNo = '{0}') ", invN);


//        DataTable master = ConnectSql.GetTab(sql);
//        if (master.Rows.Count == 1)
//        {
//            DataRow row_Source = master.Rows[0];

//            decimal amt = 0;
//            string docId = row_Source["SequenceId"].ToString();
//            string sql1 = string.Format("SELECT  QuoteLineNo,GroupTitle,ChgCode,ChgDes, Currency,ExRate, Qty,Price, Unit,Amt, MinAmt,Rmk FROM SeaQuoteDet1 WHERE QuoteId = '{0}' order by QuoteLineNo", docId);
//            DataTable det = ConnectSql.GetTab(sql1);
//            for (int i = 0; i < det.Rows.Count; i++)
//            {
//                DataRow oldRow = det.Rows[i];
//                DataRow newRow = tab_det.NewRow();

//                newRow["InvoiceN"] = invN;
//                newRow["LineN"] = oldRow["QuoteLineNo"];
//                newRow["GroupTitle"] = SafeValue.SafeString(oldRow["GroupTitle"], "").Trim();
//                //string chgCode = SafeValue.SafeString(oldRow["ChgCode"], "");
//                //string sql_chgcode = string.Format("select ChgcodeDes from XXChgCode where ChgcodeId='{0}'",chgCode);
//                newRow["ChgCode"] = oldRow["ChgDes"];//SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_chgcode), chgCode);
//                newRow["Currency"] = oldRow["Currency"];
//                decimal exRateDet = SafeValue.SafeDecimal(oldRow["ExRate"], 1);
//                newRow["ExRate"] = exRateDet.ToString("0.000000");
//                newRow["Qty"] = SafeValue.SafeDecimal(oldRow["Qty"], 0).ToString("0.000");
//                newRow["Price"] = SafeValue.SafeDecimal(oldRow["Price"], 0).ToString("0.00");
//                newRow["Unit"] = oldRow["Unit"];
//                newRow["Amt"] = SafeValue.SafeDecimal(oldRow["Amt"], 0).ToString("0.00");
//                decimal minAmt = SafeValue.SafeDecimal(oldRow["MinAmt"], 0);
//                newRow["MinAmt"] = minAmt.ToString("0.00");
//                if (minAmt == 0)
//                    newRow["MinAmt"] = "";
//                amt += SafeValue.ChinaRound(SafeValue.SafeDecimal(oldRow["Amt"], 0) * exRateDet, 2);
//                tab_det.Rows.Add(newRow);
//            }





//            DataRow row = tab_mast.NewRow();

//            row["InvoiceN"] = invN;
//            row["ToDate"] = SafeValue.SafeDate(row_Source["ExpireDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
//            row["InvDate"] = SafeValue.SafeDate(row_Source["QuoteDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");

//            cust_id = SafeValue.SafeString(row_Source["PartyTo"], "");
//            string sql_cust = "select Name,Address,Tel1,Fax1,SalesmanId from XXParty where PartyId = '" + cust_id + "'";
//            DataTable tab_cust = ConnectSql.GetTab(sql_cust);
//            if (tab_cust.Rows.Count > 0)
//            {
//                // cust_id = tab_cust.Rows[0][0].ToString();
//                address = tab_cust.Rows[0]["Address"].ToString().Trim();
//                string tel = tab_cust.Rows[0]["Tel1"].ToString().Trim();
//                string fax = tab_cust.Rows[0]["Fax1"].ToString().Trim();

//                if (tel.Length > 0)
//                {
//                    address += "\nTel:" + tel;
//                }
//                if (fax.Length > 0)
//                {
//                    address += " Fax:" + fax;
//                }
//                row["Name"] = tab_cust.Rows[0]["Name"];
//                row["Address"] = address;
//                row["Saleman"] = tab_cust.Rows[0]["SalesmanId"];
//                row["SalemanTel"] = C2.Manager.ORManager.ExecuteScalar(string.Format("select Tel from [XXSalesman] where Code='{0}'", tab_cust.Rows[0]["SalesmanId"]));
//            }

//            row["Title"] = row_Source["Title"];
//            row["Rmk"] = row_Source["Rmk"];
//            row["Note"] = row_Source["Note"];
//            string impExpInd = row_Source["ImpExpInd"].ToString();
//            row["Pod"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pod"] + "'");
//            row["Pol"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pol"] + "'");
//            if (row["Pol"].ToString() == System.Configuration.ConfigurationManager.AppSettings["PortName"].ToString())
//            {
//                row["ImpExpInd"] = "Import";
//            }
//            else
//            {
//                row["ImpExpInd"] = "Export";
//            }

//            string viaPort = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["ViaPort"] + "'"), "");
//            if (viaPort.Length > 0)
//                row["ViaPort"] = "(VIA:" + viaPort + ")";
//            row["Currency"] = row_Source["CurrencyId"];
//            decimal exRate = SafeValue.SafeDecimal(row_Source["ExRate"], 1);
//            row["ExRate"] = exRate.ToString("0.000000");
//            row["LclChg"] = SafeValue.SafeDecimal(row_Source["LclChg"], 0).ToString("0.00");
//            row["TsDay"] = row_Source["TransmitDay"];
//            row["Frequency"] = row_Source["Frequency"];
//            row["Attention"] = row_Source["Attention"];

//            row["UserId"] = row_Source["UpdateUser"];
//            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
//            row["CompanyInvHeader"] = System.Configuration.ConfigurationManager.AppSettings["CompanyAddress1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress2"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyAddress3"];
//            row["TotAmt"] = (amt + SafeValue.ChinaRound(SafeValue.SafeDecimal(row_Source["LclChg"], 0) * exRate, 2)).ToString("0.00");
//            tab_mast.Rows.Add(row);

//        }
//        set.Tables.Add(tab_mast);
//        set.Tables.Add(tab_det);
//        DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
//        set.Relations.Add(r);

//        return set;
    //}

    public static DataSet dsQuote(string invN, string userName)
    {
        DataSet set = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_dsQuote '{0}','{1}','{2}','{3}','{4}'", invN, "", "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set.Tables.Add(mast);
            set.Tables.Add(det);
            set.Relations.Add("Rela", mast.Columns["RefNo"], det.Columns["RefNo"]);
        }
        catch (Exception ex) { }
        return set;
    }

    public static DataSet dsQuote_AirFcl(string invN, string userName)
    {
        DataSet set = new DataSet();
        string cust_id = "2001";// 
        string address = "";


        DataTable tab_mast = new DataTable("Mast");
        DataTable tab_det = new DataTable("Detail");
        #region init column
        tab_mast.Columns.Add("InvoiceN");
        tab_mast.Columns.Add("ImpExpInd");
        tab_mast.Columns.Add("Title");
        tab_mast.Columns.Add("InvDate");
        tab_mast.Columns.Add("ToDate");
        tab_mast.Columns.Add("Name");
        tab_mast.Columns.Add("Address");
        tab_mast.Columns.Add("Attention");
        tab_mast.Columns.Add("Saleman");
        tab_mast.Columns.Add("SalemanTel");
        tab_mast.Columns.Add("Pol");
        tab_mast.Columns.Add("Pod");
        tab_mast.Columns.Add("ViaPort");
        tab_mast.Columns.Add("Currency");
        tab_mast.Columns.Add("Note");
        tab_mast.Columns.Add("Rmk");
        tab_mast.Columns.Add("UserId");
        tab_mast.Columns.Add("Gp20");
        tab_mast.Columns.Add("Gp40");
        tab_mast.Columns.Add("Hc40");
        tab_mast.Columns.Add("TsDay");
        tab_mast.Columns.Add("Frequency");
        tab_mast.Columns.Add("CompanyName");
        tab_mast.Columns.Add("CompanyInvHeader");
        tab_mast.Columns.Add("ContType");
        tab_mast.Columns.Add("ContPrice");
        tab_mast.Columns.Add("ExRate");
        tab_mast.Columns.Add("TotAmt");

        tab_det.Columns.Add("InvoiceN");
        tab_det.Columns.Add("LineN");
        tab_det.Columns.Add("GroupTitle");
        tab_det.Columns.Add("ChgCode");
        tab_det.Columns.Add("Currency");
        tab_det.Columns.Add("Price");
        tab_det.Columns.Add("Unit");
        #endregion
        string sql = string.Format(@"SELECT SequenceId, Title,PartyTo, Pol,Pod,ViaPort, ImpExpInd, FclLclInd, Frequency, LclChg, Subject, QuoteNo, Status, CreateUser, CreateDate, UpdateUser, UpdateDate, 
                      QuoteDate, ExpireDate,Note, Rmk, Attention, TransmitDay, Gp20, Gp40, Hc40, CurrencyId, SalesmanId,ContType,ContPrice,ExRate
FROM AirQuote1 WHERE (QuoteNo = '{0}') ", invN);


        DataTable master = ConnectSql.GetTab(sql);
        if (master.Rows.Count == 1)
        {
            decimal totAmt = 0;
            DataRow row_Source = master.Rows[0];
            string docId = row_Source["SequenceId"].ToString();
            string sql1 = string.Format("SELECT  QuoteLineNo,GroupTitle,ChgCode,ChgDes, Currency, Price, Unit, MinAmt,Rmk FROM AirQuoteDet1 WHERE (QuoteId = '{0}') order by QuoteLineNo", docId);
            DataTable det = ConnectSql.GetTab(sql1);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                DataRow oldRow = det.Rows[i];
                DataRow newRow = tab_det.NewRow();

                newRow["InvoiceN"] = invN;
                newRow["LineN"] = oldRow["QuoteLineNo"];
                newRow["GroupTitle"] = SafeValue.SafeString(oldRow["GroupTitle"], "").Trim();
                //string chgCode = SafeValue.SafeString(oldRow["ChgCode"], "");
                //string sql_chgcode = string.Format("select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", chgCode);
                newRow["ChgCode"] = oldRow["ChgDes"];// SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_chgcode), chgCode);
                newRow["Currency"] = oldRow["Currency"];
                newRow["Price"] = SafeValue.SafeDecimal(oldRow["Price"], 0).ToString("0.00");
                newRow["Unit"] = oldRow["Unit"];
                totAmt += SafeValue.SafeDecimal(oldRow["Price"], 0);
                tab_det.Rows.Add(newRow);
            }


            DataRow row = tab_mast.NewRow();

            row["InvoiceN"] = invN;
            row["ToDate"] = SafeValue.SafeDate(row_Source["ExpireDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
            row["InvDate"] = SafeValue.SafeDate(row_Source["QuoteDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");

            cust_id = SafeValue.SafeString(row_Source["PartyTo"], "");
            string sql_cust = "select Name,Address,Tel1,Fax1,SalesmanId from XXParty where PartyId = '" + cust_id + "'";
            DataTable tab_cust = ConnectSql.GetTab(sql_cust);
            if (tab_cust.Rows.Count > 0)
            {
                // cust_id = tab_cust.Rows[0][0].ToString();
                address = tab_cust.Rows[0]["Address"].ToString().Trim();
                string tel = tab_cust.Rows[0]["Tel1"].ToString().Trim();
                string fax = tab_cust.Rows[0]["Fax1"].ToString().Trim();

                if (tel.Length > 0)
                {
                    address += "\nTel:" + tel;
                }
                if (fax.Length > 0)
                {
                    address += " Fax:" + fax;
                }
                row["Name"] = tab_cust.Rows[0]["Name"];
                row["Address"] = address;
                row["Saleman"] = tab_cust.Rows[0]["SalesmanId"];
                row["SalemanTel"] = C2.Manager.ORManager.ExecuteScalar(string.Format("select Tel from [XXSalesman] where Code='{0}'", tab_cust.Rows[0]["SalesmanId"]));
            }

            row["Title"] = row_Source["Title"];
            row["Rmk"] = row_Source["Rmk"];
            row["Note"] = row_Source["Note"];
            string impExpInd = row_Source["ImpExpInd"].ToString();
            row["Pod"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pod"] + "'");
            row["Pol"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pol"] + "'");
            if (row["Pol"] == System.Configuration.ConfigurationManager.AppSettings["PortName"].ToString())
            {
                row["ImpExpInd"] = "Import";
            }
            else
            {
                row["ImpExpInd"] = "Export";
            }
            row["ViaPort"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["ViaPort"] + "'"), "DIRECT");
            row["Currency"] = row_Source["CurrencyId"];
            row["Gp20"] = SafeValue.SafeDecimal(row_Source["Gp20"], 0).ToString("0.00");
            row["Gp40"] = SafeValue.SafeDecimal(row_Source["Gp40"], 0).ToString("0.00");
            row["Hc40"] = SafeValue.SafeDecimal(row_Source["Hc40"], 0).ToString("0.00");
            row["TsDay"] = row_Source["TransmitDay"];
            row["Frequency"] = row_Source["Frequency"];
            row["Attention"] = row_Source["Attention"];

            row["UserId"] = row_Source["UpdateUser"];
            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["CompanyInvHeader"] = System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader2"];

            row["ContType"] = row_Source["ContType"];
            decimal price = SafeValue.SafeDecimal(row_Source["ContPrice"], 0);
            decimal exRate = SafeValue.SafeDecimal(row_Source["ExRate"], 1);
            row["ContPrice"] = price.ToString("0.00");
            row["ExRate"] = exRate.ToString("0.000000");
            row["TotAmt"] = (totAmt + SafeValue.ChinaRound(price * exRate, 2)).ToString("0.00"); tab_mast.Rows.Add(row);

        }
        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_det);
        DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
        set.Relations.Add(r);

        return set;
    }

    public static DataSet dsQuote_AirLcl(string invN, string userName)
    {
        DataSet set = new DataSet();
        string cust_id = "2001";// 
        string address = "";


        DataTable tab_mast = new DataTable("Mast");
        DataTable tab_det = new DataTable("Detail");
        #region init column
        tab_mast.Columns.Add("InvoiceN");
        tab_mast.Columns.Add("ImpExpInd");
        tab_mast.Columns.Add("Title");
        tab_mast.Columns.Add("InvDate");
        tab_mast.Columns.Add("ToDate");
        tab_mast.Columns.Add("Name");
        tab_mast.Columns.Add("Address");
        tab_mast.Columns.Add("Attention");
        tab_mast.Columns.Add("Saleman");
        tab_mast.Columns.Add("SalemanTel");
        tab_mast.Columns.Add("Pol");
        tab_mast.Columns.Add("Pod");
        tab_mast.Columns.Add("ViaPort");
        tab_mast.Columns.Add("Currency");
        tab_mast.Columns.Add("Note");
        tab_mast.Columns.Add("Rmk");
        tab_mast.Columns.Add("UserId");
        tab_mast.Columns.Add("LclChg");
        tab_mast.Columns.Add("TsDay");
        tab_mast.Columns.Add("Frequency");
        tab_mast.Columns.Add("CompanyName");
        tab_mast.Columns.Add("CompanyInvHeader");

        tab_det.Columns.Add("InvoiceN");
        tab_det.Columns.Add("LineN");
        tab_det.Columns.Add("GroupTitle");
        tab_det.Columns.Add("ChgCode");
        tab_det.Columns.Add("Currency");
        tab_det.Columns.Add("Qty");
        tab_det.Columns.Add("Price");
        tab_det.Columns.Add("Unit");
        tab_det.Columns.Add("Amt");
        tab_det.Columns.Add("MinAmt");
        #endregion
        string sql = string.Format(@"SELECT SequenceId, Title,PartyTo, Pol,Pod,ViaPort, ImpExpInd, FclLclInd, Frequency, LclChg, Subject, QuoteNo, Status, CreateUser, CreateDate, UpdateUser, UpdateDate, 
                      QuoteDate, ExpireDate,Note, Rmk, Attention, TransmitDay, Gp20, Gp40, Hc40, CurrencyId, SalesmanId
FROM AirQuote1 WHERE (QuoteNo = '{0}') ", invN);


        DataTable master = ConnectSql.GetTab(sql);
        if (master.Rows.Count == 1)
        {
            DataRow row_Source = master.Rows[0];
            DataRow row = tab_mast.NewRow();

            row["InvoiceN"] = invN;
            row["ToDate"] = SafeValue.SafeDate(row_Source["ExpireDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");
            row["InvDate"] = SafeValue.SafeDate(row_Source["QuoteDate"], new DateTime(1900, 1, 1)).ToString("dd/MM/yyyy");

            cust_id = SafeValue.SafeString(row_Source["PartyTo"], "");
            string sql_cust = "select Name,Address,Tel1,Fax1,SalesmanId from XXParty where PartyId = '" + cust_id + "'";
            DataTable tab_cust = ConnectSql.GetTab(sql_cust);
            if (tab_cust.Rows.Count > 0)
            {
                // cust_id = tab_cust.Rows[0][0].ToString();
                address = tab_cust.Rows[0]["Address"].ToString().Trim();
                string tel = tab_cust.Rows[0]["Tel1"].ToString().Trim();
                string fax = tab_cust.Rows[0]["Fax1"].ToString().Trim();

                if (tel.Length > 0)
                {
                    address += "\nTel:" + tel;
                }
                if (fax.Length > 0)
                {
                    address += " Fax:" + fax;
                }
                row["Name"] = tab_cust.Rows[0]["Name"];
                row["Address"] = address;
                row["Saleman"] = tab_cust.Rows[0]["SalesmanId"];
                row["SalemanTel"] = C2.Manager.ORManager.ExecuteScalar(string.Format("select Tel from [XXSalesman] where Code='{0}'", tab_cust.Rows[0]["SalesmanId"]));
            }

            row["Title"] = row_Source["Title"];
            row["Rmk"] = row_Source["Rmk"];
            row["Note"] = row_Source["Note"];
            string impExpInd = row_Source["ImpExpInd"].ToString();
            row["Pod"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pod"] + "'");
            row["Pol"] = ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["Pol"] + "'");
            if (row["Pol"].ToString() == System.Configuration.ConfigurationManager.AppSettings["PortName"].ToString())
            {
                row["ImpExpInd"] = "Import";
            }
            else
            {
                row["ImpExpInd"] = "Export";
            }
            row["ViaPort"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("select name from XXPort where Code='" + row_Source["ViaPort"] + "'"), "DIRECT");
            row["Currency"] = row_Source["CurrencyId"];
            row["LclChg"] = SafeValue.SafeDecimal(row_Source["LclChg"], 0).ToString("0.00");
            row["TsDay"] = row_Source["TransmitDay"];
            row["Frequency"] = row_Source["Frequency"];
            row["Attention"] = row_Source["Attention"];

            row["UserId"] = row_Source["UpdateUser"];
            row["CompanyName"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["CompanyInvHeader"] = System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader1"] + "\n" + System.Configuration.ConfigurationManager.AppSettings["CompanyInvHeader2"];
            tab_mast.Rows.Add(row);

            string docId = row_Source["SequenceId"].ToString();
            string sql1 = string.Format("SELECT  QuoteLineNo,GroupTitle,ChgCode,ChgDes, Currency, Qty,Price, Unit,Amt, MinAmt,Rmk FROM AirQuoteDet1 WHERE QuoteId = '{0}' order by QuoteLineNo", docId);
            DataTable det = ConnectSql.GetTab(sql1);
            for (int i = 0; i < det.Rows.Count; i++)
            {
                DataRow oldRow = det.Rows[i];
                DataRow newRow = tab_det.NewRow();

                newRow["InvoiceN"] = invN;
                newRow["LineN"] = oldRow["QuoteLineNo"];
                newRow["GroupTitle"] = SafeValue.SafeString(oldRow["GroupTitle"], "").Trim();
                //string chgCode = SafeValue.SafeString(oldRow["ChgCode"], "");
                //string sql_chgcode = string.Format("select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", chgCode);
                newRow["ChgCode"] = oldRow["ChgDes"];// SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql_chgcode), chgCode);
                newRow["Currency"] = oldRow["Currency"];
                newRow["Qty"] = SafeValue.SafeDecimal(oldRow["Qty"], 0).ToString("0.000");
                newRow["Price"] = SafeValue.SafeDecimal(oldRow["Price"], 0).ToString("0.00");
                newRow["Unit"] = oldRow["Unit"];
                newRow["Amt"] = SafeValue.SafeDecimal(oldRow["Amt"], 0).ToString("0.00");
                decimal minAmt = SafeValue.SafeDecimal(oldRow["MinAmt"], 0);
                newRow["MinAmt"] = minAmt.ToString("0.00");
                if (minAmt == 0)
                    newRow["MinAmt"] = "";
                tab_det.Rows.Add(newRow);
            }
        }
        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_det);
        DataRelation r = new DataRelation("", tab_mast.Columns["InvoiceN"], tab_det.Columns["InvoiceN"]);
        set.Relations.Add(r);

        return set;
    }

    public static DataSet PrintSeaQuotation(string invN, string userName)
    {
        DataSet ds = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintSeaQuotation '{0}','{1}','{2}','{3}','{4}'", invN, "", "", userName, System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable Mast = ds_temp.Tables[0].Copy();
            Mast.TableName = "Mast";
            DataTable Detail1 = ds_temp.Tables[1].Copy();
            Detail1.TableName = "Detail1";
            DataTable Detail2 = ds_temp.Tables[2].Copy();
            Detail2.TableName = "Detail2";
            ds.Tables.Add(Mast);
            ds.Tables.Add(Detail1);
            ds.Tables.Add(Detail2);
            DataRelation r = new DataRelation("", Mast.Columns["QuoteNo"], Detail1.Columns["QuoteNo"]);
            DataRelation r1 = new DataRelation("", Mast.Columns["QuoteNo"], Detail2.Columns["QuoteNo"]);
            ds.Relations.Add(r);
            ds.Relations.Add(r1);
        }
        catch (Exception ex) { }
        return ds;

    }

    #endregion

    #region Air Import
    public static DataTable PrintAWB(string refN, string jobN)
    {
        string sql = "";
        if (jobN == "")
        {
            sql = string.Format("select * from air_ref  where RefNo='{0}'", refN);
        }
        else
        {
            sql = string.Format("select ref.MAWB,job.* from air_job as job inner join air_ref as ref on job.RefNo=ref.RefNo where job.RefNo='{0}' and job.JobNo='{1}'", refN, jobN);
        }
        DataTable tab_mast = ConnectSql.GetTab(sql);
        tab_mast.TableName = "Mast";
        return tab_mast;
    }
    public static DataSet Print_AirPlImport(string refN, string userId)
    {
        DataSet set = new DataSet();
        DataTable tab_mast = PlAir_Import_Mast(refN, userId);
        DataTable tab_Inv = PlAirImport_Inv(refN);
        DataTable tab_Dn = PlAirImport_Dn(refN);
        DataTable tab_Ts = PlAirImport_Ts(refN);
        DataTable tab_Cn = PlAirImport_Cn(refN);
        DataTable tab_Pl = PlAirImport_Pl(refN);
        DataTable tab_Vo = PlAirImport_Vo(refN);
        DataTable tab_Cost = PlAir_Import_Cost(refN);
        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_Inv);
        set.Tables.Add(tab_Dn);
        set.Tables.Add(tab_Ts);
        set.Tables.Add(tab_Cn);
        set.Tables.Add(tab_Pl);
        set.Tables.Add(tab_Vo);
        set.Tables.Add(tab_Cost);
        return set;
    }
    private static DataTable PlAir_Import_Mast(string refN, string userId)
    {
        DataTable tab = new DataTable("PlMast");
        tab.Columns.Add("RefN");
        tab.Columns.Add("NowD");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Currency");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("RefType");
        tab.Columns.Add("TsM3");
        tab.Columns.Add("LocalM3");
        tab.Columns.Add("BkgNo");
        tab.Columns.Add("Agent");
        tab.Columns.Add("Company");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Eta");
        tab.Columns.Add("Qty");
        tab.Columns.Add("Wt");

        tab.Columns.Add("Rev1");
        tab.Columns.Add("Rev2");
        tab.Columns.Add("Rev3");
        tab.Columns.Add("Rev4");
        tab.Columns.Add("Rev");

        tab.Columns.Add("Cost1");
        tab.Columns.Add("Cost2");
        tab.Columns.Add("Cost3");
        tab.Columns.Add("Cost");
        tab.Columns.Add("Profit");
        string sql = string.Format(@"SELECT RefNo AS RefN, MAWB AS MAWB,CarrierBkgNo as BkgNo, AirportCode0 AS Code, AirportName0 AS Name, (FlightDate1+' '+FlightTime1) As Eta ,
                      GrossWeight AS Wt, AgentId AS Cust, Currency AS Currency, Total,CurrencyRate AS Rate, RefType,CreateBy as UserId  FROM air_ref WHERE (RefNo = '{0}')", refN);
        // ts wt/1000 or m3 * tsagt rate * exrate
        string sql4 = string.Format(@"SELECT sum(CAST(GrossWeight as int)) FROM air_job  WHERE (RefNo = '{0}')", refN);
        //dn
        string sql5 = " SELECT sum(LineLocAmt) FROM XaArInvoiceDet WHERE MastRefNo = '" + refN + "' and MastType = 'AI' and DocType='DN'";
        //
        string sql7 = "  SELECT sum(round(LocAmt,2)) FROM air_costing WHERE RefNo = '" + refN + "' and JobType ='AI'";
        //invoice
        string sql8 = " SELECT sum(LineLocAmt) FROM XaArInvoiceDet WHERE MastRefNo = '" + refN + "' and MastType = 'AI' and DocType='IV'";
        //cr note
        string sql9 = " SELECT sum(LineLocAmt) FROM XaArInvoiceDet WHERE MastRefNo = '" + refN + "' and MastType = 'AI' and DocType='CN'";
        //voucher
        string sql10 = " SELECT sum(LineLocAmt) FROM XAApPayableDet WHERE MastRefNo = '" + refN + "' and MastType = 'AI' and DocType<>'SC'";
        string sql11 = " SELECT sum(LineLocAmt) FROM XAApPayableDet WHERE MastRefNo = '" + refN + "' and MastType = 'AI' and DocType='SC'";


        DataRow row = tab.NewRow();
        DataTable dt = ConnectSql.GetTab(sql);

        row["RefN"] = refN;
        row["NowD"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        row["UserId"] = userId;
        if (dt.Rows.Count > 0)
        {
            row["Currency"] = dt.Rows[0]["Currency"];
            row["ExRate"] = SafeValue.SafeDecimal(dt.Rows[0]["Rate"], 0).ToString("0.000000");
            row["RefType"] = dt.Rows[0]["RefType"];
            row["BkgNo"] = dt.Rows[0]["BkgNo"];
            row["Agent"] = EzshipHelper.GetPartyName(dt.Rows[0]["Cust"]);
            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["Ves"] = string.Format("{0}/{1}", dt.Rows[0]["Code"], dt.Rows[0]["Name"]);
            row["Eta"] = SafeValue.SafeDateTimeStr(dt.Rows[0]["Eta"]);
            row["Qty"] = dt.Rows[0]["Total"];
            row["Wt"] = SafeValue.SafeDecimal(dt.Rows[0]["Wt"], 0).ToString("0.000");

            decimal rev1 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql8), 0);
            decimal rev2 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql4), 0) * SafeValue.SafeDecimal(dt.Rows[0]["Rate"], 0);
            decimal rev3 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql5), 0);
            decimal rev4 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql9), 0);
            row["Rev1"] = rev1.ToString("###,##0.00");
            row["Rev2"] = rev2.ToString("###,##0.00");
            row["Rev3"] = rev3.ToString("###,##0.00");
            row["Rev4"] = "(" + rev3.ToString("###,##0.00") + ")";
            decimal sumRev = rev1 + rev2 - rev3;
            row["Rev"] = sumRev.ToString("###,##0.00");


            decimal cost1 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql10), 0) - SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql11), 0);
            decimal cost2 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql4), 0) * SafeValue.SafeDecimal(dt.Rows[0]["Rate"], 0);
            decimal cost3 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql7), 0);
            row["Cost1"] = cost1.ToString("###,##0.00");
            row["Cost2"] = cost2.ToString("###,##0.00");
            row["Cost3"] = cost3.ToString("###,##0.00");
            decimal cost = cost1 + cost2 + cost3;
            row["Cost"] = cost.ToString("###,##0.00");
            row["Profit"] = (sumRev - cost).ToString("###,##0.00");
        }
        tab.Rows.Add(row);
        return tab;
    }

    private static DataTable PlAir_Import_Cost(string refN)
    {
        string sql = string.Format(@"SELECT  ChgCodeDes+Remark as Des, LocAmt as Amount FROM air_costing Where RefNo='{0}' and JobType='AI'", refN);
        DataTable dt = ConnectSql.GetTab(sql);

        decimal gstA = 0;
        DataTable tab = new DataTable("Costing");
        tab.Columns.Add("Des");
        tab.Columns.Add("Amount");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Des"] = dt.Rows[i]["Des"];
            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }


    private static DataTable PlAirImport_Inv(string refN)
    {
        DataTable tab = new DataTable("Invoice");
        tab.Columns.Add("JobN");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("Cust");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("InvN");
        tab.Columns.Add("Amount");
        tab.Columns.Add("Receipt");

        tab.Columns.Add("Frt");
        tab.Columns.Add("Agy");
        tab.Columns.Add("Thc");
        tab.Columns.Add("Lcl");
        tab.Columns.Add("DoFee");
        tab.Columns.Add("Other");

        string sql = string.Format(@"SELECT import.JobNo, import.HblNo, import.Weight, import.Volume,  inv.PartyTo as CustomerId, inv.DocNo, inv.SequenceId
FROM     XAArInvoice AS inv   left JOIN  SeaImport AS import  ON import.JobNo = inv.JobRefNo AND import.RefNo = inv.MastRefNo
WHERE     (inv.DocType = 'IV') AND (inv.MastType = 'AI') and  inv.MastRefNo='{0}' order by inv.JobRefNo,inv.DocNo", refN);
        DataTable tab_Inv = ConnectSql.GetTab(sql);
        decimal gstA = 0;
        for (int i = 0; i < tab_Inv.Rows.Count; i++)
        {
            string jobNo = SafeValue.SafeString(tab_Inv.Rows[i]["JobNo"]);
            string hbl = SafeValue.SafeString(tab_Inv.Rows[i]["HblNo"]);
            string cust = EzshipHelper.GetPartyName(tab_Inv.Rows[i]["CustomerId"]);
            decimal wt = SafeValue.SafeDecimal(tab_Inv.Rows[i]["Weight"], 0);
            decimal m3 = SafeValue.SafeDecimal(tab_Inv.Rows[i]["Volume"], 0);
            string billNo = tab_Inv.Rows[i]["DocNo"].ToString();
            string billId = tab_Inv.Rows[i]["SequenceId"].ToString();

            decimal frt = 0;
            decimal agy = 0;
            decimal thc = 0;
            decimal lcl = 0;
            decimal doFee = 0;
            decimal other = 0;
            decimal amt = 0;
            string sqlBillDet = string.Format("SELECT DocNo, DocType, ChgCode, Currency, ExRate, Gst, GstAmt,DocAmt, LocAmt,LineLocAmt FROM XAArInvoiceDet where DocId='{0}'", billId);
            DataTable tab_InvDet = ConnectSql.GetTab(sqlBillDet);
            for (int j = 0; j < tab_InvDet.Rows.Count; j++)
            {
                string chgCode = tab_InvDet.Rows[j]["ChgCode"].ToString();
                if (chgCode.ToUpper() == "FRTOC")
                {
                    frt += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "AGY")
                {
                    agy += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "THC")
                {
                    thc += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "LCL")
                {
                    lcl += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "DOFEE")
                {
                    doFee += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else//other
                {
                    other += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                amt += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
            }

            DataRow row = tab.NewRow();
            row["JobN"] = jobNo;
            row["Hbl"] = hbl;
            row["Cust"] = cust;
            row["Wt"] = wt.ToString("0.000");
            row["M3"] = m3.ToString("0.000");
            row["InvN"] = billNo;
            row["Amount"] = amt.ToString("0.00");
            row["Frt"] = frt.ToString("0.00");
            row["Agy"] = agy.ToString("0.00");
            row["Thc"] = thc.ToString("0.00");
            row["Lcl"] = lcl.ToString("0.00");
            row["DoFee"] = doFee.ToString("0.00");
            row["Other"] = other.ToString("0.00");

            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirImport_Dn(string refN)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT MAX(mast.MastRefNo) AS RefNo, MAX(mast.PartyTo) AS CustId, mast.DocNo AS DrN, SUM(det.LocAmt * mast.ExRate) AS Amount
FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.MastType = 'AI') and mast.DocType='DN' GROUP BY mast.DocNo", refN);


        decimal gstA = 0;
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["BillNo"] = dt.Rows[i]["RefNo"];
            row["CustName"] = EzshipHelper.GetPartyName(dt.Rows[i]["CustId"]);

            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirImport_Ts(string refN)
    {
        string sql = @"SELECT HBLNo AS Hbl, TsVessel+'/'+TsVoyage as Ves,TsPod as Pod,Volume AS M3, Weight AS WT, TsAgtRate
FROM SeaImport WHERE (RefNo = '" + refN + "') AND (TsAgtRate > 0) ";
        DataTable tab = new DataTable("Ts");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Pod");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("AgtRate");
        tab.Columns.Add("Amount");
        tab.Columns.Add("Currency");

        decimal gstA = 0;
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Hbl"] = dt.Rows[i]["Hbl"];
            row["Ves"] = dt.Rows[i]["Ves"];
            row["Pod"] = ConnectSql.ExecuteScalar(string.Format("Select Name from XXPort where Code='{0}'", dt.Rows[i]["Pod"]));
            decimal wt = SafeValue.SafeDecimal(dt.Rows[i]["Wt"], 0);
            decimal m3 = SafeValue.SafeDecimal(dt.Rows[i]["M3"], 0);
            row["Wt"] = wt.ToString("0.000");
            row["M3"] = m3.ToString("0.000");
            decimal agtRate = SafeValue.SafeDecimal(dt.Rows[i]["TsAgtRate"], 0);
            decimal amt = m3 * agtRate;
            if (wt / 1000 > m3)
            {
                amt = agtRate * wt / 1000;
            }
            gstA += amt;
            row["Amount"] = amt.ToString("0.00");


            row["Currency"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("Select CurrencyId from SeaImportRef where RefNo='" + refN + "'"), "SGD");
            tab.Rows.Add(row);
        }
        return tab;
    }

    private static DataTable PlAirImport_Cn(string refN)
    {
        DataTable tab = new DataTable("Cn");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT MAX(mast.MastRefNo) AS RefNo, MAX(mast.PartyTo) AS CustId, mast.DocNo AS DrN, SUM(det.LocAmt * mast.ExRate) AS Amount
FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.MastType = 'AI') and mast.DocType='CN' GROUP BY mast.DocNo", refN);
        decimal gstA = 0;
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["BillNo"] = dt.Rows[i]["RefNo"];
            row["CustName"] = EzshipHelper.GetPartyName(dt.Rows[i]["CustId"]);

            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirImport_Pl(string refN)
    {
        string sql = string.Format(@" SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, mast.DocType,det.LocAmt*mast.ExRate AS Amount
FROM         XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
WHERE     (mast.MastRefNo = '{0}') AND (mast.DocType = 'PL' or mast.DocType = 'SC' or mast.DocType = 'SD') AND (mast.MastType = 'AI')", refN);
        DataTable dt = ConnectSql.GetTab(sql);

        decimal gstA = 0;
        DataTable tab = new DataTable("Payable");
        tab.Columns.Add("Gd");
        tab.Columns.Add("Vn");
        tab.Columns.Add("DocN");
        tab.Columns.Add("Amount");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Gd"] = dt.Rows[i]["Gd"];
            string docType = dt.Rows[i]["DocType"].ToString();
            row["Vn"] = dt.Rows[i]["Vn"].ToString() + "(" + docType + ")";
            row["DocN"] = dt.Rows[i]["DocN"].ToString();
            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);

            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            if (docType == "SC")
                row["Amount"] = (-SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0)).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirImport_Vo(string refN)
    {
        string sql = string.Format(@" SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, det.LocAmt*mast.ExRate AS Amount
FROM         XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
WHERE     (mast.MastRefNo = '{0}') AND (mast.DocType = 'VO') AND (mast.MastType = 'AI')", refN);
        DataTable dt = ConnectSql.GetTab(sql);

        decimal gstA = 0;
        DataTable tab = new DataTable("Voucher");
        tab.Columns.Add("Gd");
        tab.Columns.Add("Vn");
        tab.Columns.Add("DocN");
        tab.Columns.Add("Amount");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Gd"] = dt.Rows[i]["Gd"];
            row["Vn"] = dt.Rows[i]["Vn"];
            row["DocN"] = dt.Rows[i]["DocN"];
            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirImport_Cost(string refN)
    {
        string sql = string.Format(@"SELECT  ChgCodeDes+Remark as Des, LocAmt as Amount FROM air_costing Where RefNo='{0}' and JobType='AI'", refN);
        DataTable dt = ConnectSql.GetTab(sql);

        decimal gstA = 0;
        DataTable tab = new DataTable("Costing");
        tab.Columns.Add("Des");
        tab.Columns.Add("Amount");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Des"] = dt.Rows[i]["Des"];
            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }

    

    #endregion

    #region Air Export
    public static DataSet Print_AirPlExport(string refN, string userId)
    {
        DataSet set = new DataSet();
        DataTable tab_mast = PlAir_Export_Mast(refN, userId);
        DataTable tab_Inv = PlAirExport_Inv(refN);
        DataTable tab_Dn = PlAirExport_Dn(refN);
        DataTable tab_Ts = PlAirExport_Ts(refN);
        DataTable tab_Cn = PlAirExport_Cn(refN);
        DataTable tab_Pl = PlAirExport_Pl(refN);
        DataTable tab_Vo = PlAirExport_Vo(refN);
        DataTable tab_Cost = PlAir_Export_Cost(refN);
        set.Tables.Add(tab_mast);
        set.Tables.Add(tab_Inv);
        set.Tables.Add(tab_Dn);
        set.Tables.Add(tab_Ts);
        set.Tables.Add(tab_Cn);
        set.Tables.Add(tab_Pl);
        set.Tables.Add(tab_Vo);
        set.Tables.Add(tab_Cost);
        return set;
    }
    private static DataTable PlAir_Export_Mast(string refN, string userId)
    {
        DataTable tab = new DataTable("PlMast");
        tab.Columns.Add("RefN");
        tab.Columns.Add("NowD");
        tab.Columns.Add("UserId");
        tab.Columns.Add("Currency");
        tab.Columns.Add("ExRate");
        tab.Columns.Add("RefType");
        tab.Columns.Add("TsM3");
        tab.Columns.Add("LocalM3");
        tab.Columns.Add("BkgNo");
        tab.Columns.Add("Agent");
        tab.Columns.Add("Company");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Eta");
        tab.Columns.Add("Qty");
        tab.Columns.Add("Wt");

        tab.Columns.Add("Rev1");
        tab.Columns.Add("Rev2");
        tab.Columns.Add("Rev3");
        tab.Columns.Add("Rev4");
        tab.Columns.Add("Rev");

        tab.Columns.Add("Cost1");
        tab.Columns.Add("Cost2");
        tab.Columns.Add("Cost3");
        tab.Columns.Add("Cost");
        tab.Columns.Add("Profit");
        string sql = string.Format(@"SELECT RefNo AS RefN, MAWB AS MAWB,CarrierBkgNo as BkgNo, AirportCode0 AS Code, AirportName0 AS Name, (FlightDate1+' '+FlightTime1) As Eta,
                      GrossWeight AS Wt, AgentId AS Cust,Total, Currency AS Currency, CurrencyRate AS Rate, RefType,CreateBy as UserId  FROM air_ref WHERE (RefNo = '{0}')", refN);
        // ts wt/1000 or m3 * tsagt rate * exrate
        string sql4 = string.Format(@"SELECT sum(CAST(GrossWeight as int)) FROM air_job  WHERE (RefNo = '{0}')", refN);
        //dn
        string sql5 = " SELECT sum(LineLocAmt) FROM XaArInvoiceDet WHERE MastRefNo = '" + refN + "' and MastType = 'AE' and DocType='DN'";
        //
        string sql7 = "  SELECT sum(round(LocAmt,2)) FROM air_costing WHERE RefNo = '" + refN + "' and JobType ='AE'";
        //invoice
        string sql8 = " SELECT sum(LineLocAmt) FROM XaArInvoiceDet WHERE MastRefNo = '" + refN + "' and MastType = 'AE' and DocType='IV'";
        //cr note
        string sql9 = " SELECT sum(LineLocAmt) FROM XaArInvoiceDet WHERE MastRefNo = '" + refN + "' and MastType = 'AE' and DocType='CN'";
        //voucher
        string sql10 = " SELECT sum(LineLocAmt) FROM XAApPayableDet WHERE MastRefNo = '" + refN + "' and MastType = 'AE' and DocType<>'SC'";
        string sql11 = " SELECT sum(LineLocAmt) FROM XAApPayableDet WHERE MastRefNo = '" + refN + "' and MastType = 'AE' and DocType='SC'";


        DataRow row = tab.NewRow();
        DataTable dt = ConnectSql.GetTab(sql);

        row["RefN"] = refN;
        row["NowD"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        row["UserId"] = userId;
        if (dt.Rows.Count > 0)
        {
            row["Currency"] = dt.Rows[0]["Currency"];
            row["ExRate"] = SafeValue.SafeDecimal(dt.Rows[0]["Rate"], 0).ToString("0.000000");
            row["RefType"] = dt.Rows[0]["RefType"];
            row["BkgNo"] = dt.Rows[0]["BkgNo"];
            row["Agent"] = EzshipHelper.GetPartyName(dt.Rows[0]["Cust"]);
            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
            row["Ves"] = string.Format("{0}/{1}", dt.Rows[0]["Code"], dt.Rows[0]["Name"]);
            row["Eta"] = SafeValue.SafeDateTimeStr(dt.Rows[0]["Eta"]);
            row["Qty"] = dt.Rows[0]["Total"];
            row["Wt"] = SafeValue.SafeDecimal(dt.Rows[0]["Wt"], 0).ToString("0.000");

            decimal rev1 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql8), 0);
            decimal rev2 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql4), 0) * SafeValue.SafeDecimal(dt.Rows[0]["Rate"], 0);
            decimal rev3 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql5), 0);
            decimal rev4 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql9), 0);
            row["Rev1"] = rev1.ToString("###,##0.00");
            row["Rev2"] = rev2.ToString("###,##0.00");
            row["Rev3"] = rev3.ToString("###,##0.00");
            row["Rev4"] = "(" + rev3.ToString("###,##0.00") + ")";
            decimal sumRev = rev1 + rev2 - rev3;
            row["Rev"] = sumRev.ToString("###,##0.00");


            decimal cost1 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql10), 0) - SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql11), 0);
            decimal cost2 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql4), 0) * SafeValue.SafeDecimal(dt.Rows[0]["Rate"], 0);
            decimal cost3 = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql7), 0);
            row["Cost1"] = cost1.ToString("###,##0.00");
            row["Cost2"] = cost2.ToString("###,##0.00");
            row["Cost3"] = cost3.ToString("###,##0.00");
            decimal cost = cost1 + cost2 + cost3;
            row["Cost"] = cost.ToString("###,##0.00");
            row["Profit"] = (sumRev - cost).ToString("###,##0.00");
        }
        tab.Rows.Add(row);
        return tab;
    }

    private static DataTable PlAir_Export_Cost(string refN)
    {
        string sql = string.Format(@"SELECT  ChgCodeDes+Remark as Des, LocAmt as Amount FROM air_costing Where RefNo='{0}' and JobType='AE'", refN);
        DataTable dt = ConnectSql.GetTab(sql);

        decimal gstA = 0;
        DataTable tab = new DataTable("Costing");
        tab.Columns.Add("Des");
        tab.Columns.Add("Amount");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Des"] = dt.Rows[i]["Des"];
            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }

    private static DataTable PlAirExport_Inv(string refN)
    {
        DataTable tab = new DataTable("Invoice");
        tab.Columns.Add("JobN");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("Cust");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("InvN");
        tab.Columns.Add("Amount");
        tab.Columns.Add("Receipt");

        tab.Columns.Add("Frt");
        tab.Columns.Add("Agy");
        tab.Columns.Add("Thc");
        tab.Columns.Add("Lcl");
        tab.Columns.Add("DoFee");
        tab.Columns.Add("Other");

        string sql = string.Format(@"SELECT import.JobNo, import.HblNo, import.Weight, import.Volume,  inv.PartyTo as CustomerId, inv.DocNo, inv.SequenceId
FROM     XAArInvoice AS inv   left JOIN  SeaImport AS import  ON import.JobNo = inv.JobRefNo AND import.RefNo = inv.MastRefNo
WHERE     (inv.DocType = 'IV') AND (inv.MastType = 'AE') and  inv.MastRefNo='{0}' order by inv.JobRefNo,inv.DocNo", refN);
        DataTable tab_Inv = ConnectSql.GetTab(sql);
        decimal gstA = 0;
        for (int i = 0; i < tab_Inv.Rows.Count; i++)
        {
            string jobNo = SafeValue.SafeString(tab_Inv.Rows[i]["JobNo"]);
            string hbl = SafeValue.SafeString(tab_Inv.Rows[i]["HblNo"]);
            string cust = EzshipHelper.GetPartyName(tab_Inv.Rows[i]["CustomerId"]);
            decimal wt = SafeValue.SafeDecimal(tab_Inv.Rows[i]["Weight"], 0);
            decimal m3 = SafeValue.SafeDecimal(tab_Inv.Rows[i]["Volume"], 0);
            string billNo = tab_Inv.Rows[i]["DocNo"].ToString();
            string billId = tab_Inv.Rows[i]["SequenceId"].ToString();

            decimal frt = 0;
            decimal agy = 0;
            decimal thc = 0;
            decimal lcl = 0;
            decimal doFee = 0;
            decimal other = 0;
            decimal amt = 0;
            string sqlBillDet = string.Format("SELECT DocNo, DocType, ChgCode, Currency, ExRate, Gst, GstAmt,DocAmt, LocAmt,LineLocAmt FROM XAArInvoiceDet where DocId='{0}'", billId);
            DataTable tab_InvDet = ConnectSql.GetTab(sqlBillDet);
            for (int j = 0; j < tab_InvDet.Rows.Count; j++)
            {
                string chgCode = tab_InvDet.Rows[j]["ChgCode"].ToString();
                if (chgCode.ToUpper() == "FRTOC")
                {
                    frt += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "AGY")
                {
                    agy += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "THC")
                {
                    thc += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "LCL")
                {
                    lcl += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else if (chgCode.ToUpper() == "DOFEE")
                {
                    doFee += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                else//other
                {
                    other += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
                }
                amt += SafeValue.SafeDecimal(tab_InvDet.Rows[j]["LineLocAmt"], 0);
            }

            DataRow row = tab.NewRow();
            row["JobN"] = jobNo;
            row["Hbl"] = hbl;
            row["Cust"] = cust;
            row["Wt"] = wt.ToString("0.000");
            row["M3"] = m3.ToString("0.000");
            row["InvN"] = billNo;
            row["Amount"] = amt.ToString("0.00");
            row["Frt"] = frt.ToString("0.00");
            row["Agy"] = agy.ToString("0.00");
            row["Thc"] = thc.ToString("0.00");
            row["Lcl"] = lcl.ToString("0.00");
            row["DoFee"] = doFee.ToString("0.00");
            row["Other"] = other.ToString("0.00");

            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirExport_Dn(string refN)
    {
        DataTable tab = new DataTable("DN");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT MAX(mast.MastRefNo) AS RefNo, MAX(mast.PartyTo) AS CustId, mast.DocNo AS DrN, SUM(det.LocAmt * mast.ExRate) AS Amount
FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.MastType = 'AE') and mast.DocType='DN' GROUP BY mast.DocNo", refN);


        decimal gstA = 0;
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["BillNo"] = dt.Rows[i]["RefNo"];
            row["CustName"] = EzshipHelper.GetPartyName(dt.Rows[i]["CustId"]);

            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirExport_Ts(string refN)
    {
        string sql = @"SELECT HBLNo AS Hbl, TsVessel+'/'+TsVoyage as Ves,TsPod as Pod,Volume AS M3, Weight AS WT, TsAgtRate
FROM SeaImport WHERE (RefNo = '" + refN + "') AND (TsAgtRate > 0) ";
        DataTable tab = new DataTable("Ts");
        tab.Columns.Add("Hbl");
        tab.Columns.Add("Ves");
        tab.Columns.Add("Pod");
        tab.Columns.Add("Wt");
        tab.Columns.Add("M3");
        tab.Columns.Add("AgtRate");
        tab.Columns.Add("Amount");
        tab.Columns.Add("Currency");

        decimal gstA = 0;
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Hbl"] = dt.Rows[i]["Hbl"];
            row["Ves"] = dt.Rows[i]["Ves"];
            row["Pod"] = ConnectSql.ExecuteScalar(string.Format("Select Name from XXPort where Code='{0}'", dt.Rows[i]["Pod"]));
            decimal wt = SafeValue.SafeDecimal(dt.Rows[i]["Wt"], 0);
            decimal m3 = SafeValue.SafeDecimal(dt.Rows[i]["M3"], 0);
            row["Wt"] = wt.ToString("0.000");
            row["M3"] = m3.ToString("0.000");
            decimal agtRate = SafeValue.SafeDecimal(dt.Rows[i]["TsAgtRate"], 0);
            decimal amt = m3 * agtRate;
            if (wt / 1000 > m3)
            {
                amt = agtRate * wt / 1000;
            }
            gstA += amt;
            row["Amount"] = amt.ToString("0.00");


            row["Currency"] = SafeValue.SafeString(ConnectSql.ExecuteScalar("Select CurrencyId from SeaImportRef where RefNo='" + refN + "'"), "SGD");
            tab.Rows.Add(row);
        }
        return tab;
    }

    private static DataTable PlAirExport_Cn(string refN)
    {
        DataTable tab = new DataTable("Cn");
        tab.Columns.Add("BillNo");
        tab.Columns.Add("CustName");
        tab.Columns.Add("Amount");
        string sql = string.Format(@"SELECT MAX(mast.MastRefNo) AS RefNo, MAX(mast.PartyTo) AS CustId, mast.DocNo AS DrN, SUM(det.LocAmt * mast.ExRate) AS Amount
FROM XAArInvoice AS mast INNER JOIN XAArInvoiceDet AS det ON mast.SequenceId = det.DocId
WHERE (mast.MastRefNo = '{0}') AND (mast.MastType = 'AE') and mast.DocType='CN' GROUP BY mast.DocNo", refN);
        decimal gstA = 0;
        DataTable dt = ConnectSql.GetTab(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["BillNo"] = dt.Rows[i]["RefNo"];
            row["CustName"] = EzshipHelper.GetPartyName(dt.Rows[i]["CustId"]);

            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirExport_Pl(string refN)
    {
        string sql = string.Format(@" SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, mast.DocType,det.LocAmt*mast.ExRate AS Amount
FROM         XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
WHERE     (mast.MastRefNo = '{0}') AND (mast.DocType = 'PL' or mast.DocType = 'SC' or mast.DocType = 'SD') AND (mast.MastType = 'AE')", refN);
        DataTable dt = ConnectSql.GetTab(sql);

        decimal gstA = 0;
        DataTable tab = new DataTable("Payable");
        tab.Columns.Add("Gd");
        tab.Columns.Add("Vn");
        tab.Columns.Add("DocN");
        tab.Columns.Add("Amount");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Gd"] = dt.Rows[i]["Gd"];
            string docType = dt.Rows[i]["DocType"].ToString();
            row["Vn"] = dt.Rows[i]["Vn"].ToString() + "(" + docType + ")";
            row["DocN"] = dt.Rows[i]["DocN"].ToString();
            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);

            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            if (docType == "SC")
                row["Amount"] = (-SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0)).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirExport_Vo(string refN)
    {
        string sql = string.Format(@" SELECT det.ChgDes1 + '/' + det.ChgDes2 AS Gd, mast.DocNo AS Vn, mast.SupplierBillNo AS DocN, det.LocAmt*mast.ExRate AS Amount
FROM         XAApPayable AS mast INNER JOIN  XAApPayableDet AS det ON mast.SequenceId = det.DocId
WHERE     (mast.MastRefNo = '{0}') AND (mast.DocType = 'VO') AND (mast.MastType = 'AE')", refN);
        DataTable dt = ConnectSql.GetTab(sql);

        decimal gstA = 0;
        DataTable tab = new DataTable("Voucher");
        tab.Columns.Add("Gd");
        tab.Columns.Add("Vn");
        tab.Columns.Add("DocN");
        tab.Columns.Add("Amount");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Gd"] = dt.Rows[i]["Gd"];
            row["Vn"] = dt.Rows[i]["Vn"];
            row["DocN"] = dt.Rows[i]["DocN"];
            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }
    private static DataTable PlAirExport_Cost(string refN)
    {
        string sql = string.Format(@"SELECT  ChgCodeDes+Remark as Des, LocAmt as Amount FROM air_costing Where RefNo='{0}' and JobType='AE'", refN);
        DataTable dt = ConnectSql.GetTab(sql);

        decimal gstA = 0;
        DataTable tab = new DataTable("Costing");
        tab.Columns.Add("Des");
        tab.Columns.Add("Amount");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow row = tab.NewRow();
            row["Des"] = dt.Rows[i]["Des"];
            gstA += SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0);
            row["Amount"] = SafeValue.SafeDecimal(dt.Rows[i]["Amount"], 0).ToString("0.00");
            tab.Rows.Add(row);
        }
        return tab;
    }
    #endregion

    #region commercial/packing/shipping request/joborder
    public static DataSet PrintExpCommercial(string exportN)
    {
        DataSet set1 = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintExpCommercial '{0}','{1}','{2}','{3}','{4}'", "", exportN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set1.Tables.Add(mast);
            set1.Tables.Add(det);
            set1.Relations.Add("Rela", mast.Columns["ExpNo"], det.Columns["ExpNo"]);
        }
        catch (Exception ex) { }
        return set1;
    }
    public static DataSet PrintExpPacking(string exportN)
    {
        DataSet set1 = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintExpPacking '{0}','{1}','{2}','{3}','{4}'", "", exportN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set1.Tables.Add(mast);
            set1.Tables.Add(det);
            set1.Relations.Add("Rela", mast.Columns["ExpNo"], det.Columns["ExpNo"]);
        }
        catch (Exception ex) { }
        return set1;
    }
    public static DataSet PrintExpShippingRequest(string exportN)
    {
        DataSet set1 = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintExpShippingRequest '{0}','{1}','{2}','{3}','{4}'", "", exportN, "", "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set1.Tables.Add(mast);
            set1.Tables.Add(det);
            set1.Relations.Add("Rela", mast.Columns["ExpNo"], det.Columns["ExpNo"]);
        }
        catch (Exception ex) { }
        return set1;
    }
    public static DataSet PrintJobOrder(string exportN, string jobType)
    {
        DataSet set1 = new DataSet();
        try
        {
            string strsql = string.Format(@"exec proc_PrintJobOrder1 '{0}','{1}','{2}','{3}','{4}'", "", exportN, jobType, "", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString());
            DataSet ds_temp = ConnectSql.GetDataSet(strsql);
            DataTable mast = ds_temp.Tables[0].Copy();
            mast.TableName = "Mast";
            DataTable det = ds_temp.Tables[1].Copy();
            det.TableName = "Detail";
            set1.Tables.Add(mast);
            set1.Tables.Add(det);
            set1.Relations.Add("Rela", mast.Columns["JobNo"], det.Columns["JobNo"]);
        }
        catch (Exception ex) { }
        return set1;
    } 
    #endregion

    #region no ap
    //    public static DataSet PrintPl_SeaRef_NoAp(string refN, string refType, string userId)
    //    {
    //        DataSet set = new DataSet();
    //        DataTable tab_mast = Pl_Mast_NoAp(refN, refType, userId);
    //        DataTable tab_Inv = Pl_Inv(refN, refType);
    //        DataTable tab_Dn = Pl_Dn(refN, refType);
    //        DataTable tab_Ts = Pl_Ts(refN, refType);
    //        DataTable tab_Cn = Pl_Cn(refN, refType);
    //        DataTable tab_Pl =new DataTable("PL");
    //        DataTable tab_Vo = new DataTable("");
    //        DataTable tab_Cost = Pl_Cost(refN, refType);
    //        tab_mast.TableName = "Mast";
    //        tab_Inv.TableName = "IV";
    //        tab_Ts.TableName = "TS";
    //        tab_Cn.TableName = "CN";
    //        tab_Dn.TableName = "DN";
    //        tab_Pl.TableName = "PL";
    //        tab_Vo.TableName = "VO";
    //        tab_Cost.TableName = "COST";

    //        set.Tables.Add(tab_mast);
    //        set.Tables.Add(tab_Inv);
    //        set.Tables.Add(tab_Dn);
    //        set.Tables.Add(tab_Ts);
    //        set.Tables.Add(tab_Cn);
    //        set.Tables.Add(tab_Pl);
    //        set.Tables.Add(tab_Vo);
    //        set.Tables.Add(tab_Cost);
    //        return set;
    //    }
    //    private static DataTable Pl_Mast_NoAp(string refN, string refType, string userId)
    //    {
    //        DataTable tab = new DataTable("PlMast");
    //        tab.Columns.Add("RefN");
    //        tab.Columns.Add("NowD");
    //        tab.Columns.Add("UserId");
    //        tab.Columns.Add("Currency");
    //        tab.Columns.Add("ExRate");
    //        tab.Columns.Add("JobType");
    //        tab.Columns.Add("TsM3");
    //        tab.Columns.Add("LocalM3");

    //        tab.Columns.Add("Agent");
    //        tab.Columns.Add("Company");
    //        tab.Columns.Add("Obl");
    //        tab.Columns.Add("Ves");
    //        tab.Columns.Add("Eta");
    //        tab.Columns.Add("Qty");
    //        tab.Columns.Add("Pack");
    //        tab.Columns.Add("Wt");
    //        tab.Columns.Add("M3");
    //        tab.Columns.Add("Pol");
    //        tab.Columns.Add("Pod");
    //        tab.Columns.Add("ContN");

    //        tab.Columns.Add("Rev1");
    //        tab.Columns.Add("Rev2");
    //        tab.Columns.Add("Rev3");
    //        tab.Columns.Add("Rev4");
    //        tab.Columns.Add("Rev");

    //        tab.Columns.Add("Cost1");
    //        tab.Columns.Add("Cost2");
    //        tab.Columns.Add("Cost3");
    //        tab.Columns.Add("Cost");
    //        tab.Columns.Add("Profit");
    //        string sql3 = "SELECT distinct ContainerNo + '/' + SealNo + '/' + ContainerType FROM SeaImportMkg WHERE RefNo = '" + refN + "' and MkgType='Cont'";

    //        string sql = string.Format(@"SELECT mast.RefNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,mast.CreateBy as UserId,Mast.CurrencyId as Currency,mast.ExRate,mast.JobType
    //,dbo.fun_GetPartyName(mast.AgentId) as Agent,'' as Company,mast.OblNo as Obl,mast.Vessel+'/'+mast.Voyage as Ves,convert(nvarchar(10),mast.Eta,103) as Eta,dbo.fun_GetPortName(mast.Pol) as Pol,dbo.fun_GetPortName(mast.Pod) as Pod,'' ContN
    //,mast.Qty,mast.PackageType as Pack,mast.Weight as Wt,mast.Volume as M3
    //,isnull((select sum(volume) from SeaImport where refNo=mast.RefNo and TsInd='Y'),0) as TsM3
    //,isnull((select sum(volume) from SeaImport where refNo=mast.RefNo and TsInd='N'),0) as LocalM3
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='IV') Rev1
    //,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsAgtRate),2)),0) FROM SeaImport  WHERE (RefNo = mast.RefNo) AND (TsAgtRate > 0))*mast.ExRate as Rev2
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='DN') Rev3
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SI' and DocType='CN') Rev4
    //,0 as Cost1
    //,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsImpRate),2)),0) FROM SeaImport WHERE (RefNo = mast.RefNo) AND (TsImpRate > 0))*mast.ExRate as Cost2
    //,( SELECT sum(CostLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobType ='SI') Cost3
    //FROM SeaImportRef mast
    //where mast.RefNo='{0}'", refN);
    //        if (refType == "SE")
    //        {
    //            sql = string.Format(@"SELECT mast.RefNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,mast.CreateBy as UserId,Mast.CurrencyId as Currency,mast.ExRate,mast.JobType
    //,dbo.fun_GetPartyName(mast.AgentId) as Agent,'' as Company,mast.OblNo as Obl,mast.Vessel+'/'+mast.Voyage as Ves,convert(nvarchar(10),mast.Eta,103) as Eta,dbo.fun_GetPortName(mast.Pol) as Pol,dbo.fun_GetPortName(mast.Pod) as Pod,'' ContN
    //,mast.Qty,mast.PackageType as Pack,mast.Weight as Wt,mast.Volume as M3
    //,isnull((select sum(volume) from SeaExport where refNo=mast.RefNo and TsInd='Y'),0) as TsM3
    //,isnull((select sum(volume) from SeaExport where refNo=mast.RefNo and TsInd='N'),0) as LocalM3
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SE' and DocType='IV') Rev1
    //,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * ImpCharge),2)),0) FROM SeaExport  WHERE (RefNo = mast.RefNo) AND (ImpCharge > 0))*mast.ExRate as Rev2
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SE' and DocType='DN') Rev3
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and MastType = 'SE' and DocType='CN') Rev4
    //,0  as Cost1
    //,0 as Cost2
    //,( SELECT sum(CostLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobType ='SE') Cost3
    //FROM SeaExportRef mast
    //where mast.RefNo='{0}'", refN);

    //            sql3 = "SELECT distinct ContainerNo + '/' + SealNo + '/' + ContainerType FROM SeaExportMkg WHERE RefNo = '" + refN + "' and MkgType='Cont'";
    //        }

    //        DataTable dt = ConnectSql.GetTab(sql);
    //        DataRow row1 = tab.NewRow();
    //        for (int i = 0; i < dt.Columns.Count; i++)
    //        {
    //            string colName = dt.Columns[i].ColumnName;
    //            row1[colName] = dt.Rows[0][i];
    //        }
    //        tab.Rows.Add(row1);
    //        if (tab.Rows.Count > 0)
    //        {
    //            DataRow row = tab.Rows[0];
    //            decimal allM3 = SafeValue.SafeDecimal(tab.Rows[0]["M3"], 0);
    //            decimal transM3 = SafeValue.SafeDecimal(tab.Rows[0]["TsM3"], 0);

    //            if (allM3 == 0)
    //            {
    //                row["TsM3"] = "";
    //                row["LocalM3"] = "";
    //            }
    //            else
    //            {
    //                row["TsM3"] = transM3 + " - " + (transM3 * 100 / allM3).ToString("0.00") + "%";
    //                row["LocalM3"] = (allM3 - transM3).ToString("0.000") + " - " + ((allM3 - transM3) * 100 / allM3).ToString("0.00") + "%";
    //            }

    //            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
    //            string contN = "";
    //            DataTable dt3 = ConnectSql.GetTab(sql3);
    //            for (int i = 0; i < dt3.Rows.Count; i++)
    //            {
    //                contN += dt3.Rows[i][0].ToString();
    //                if (i != dt3.Rows.Count - 1)
    //                    contN += "\n";
    //            }
    //            row["ContN"] = contN;


    //            decimal rev1 = SafeValue.SafeDecimal(tab.Rows[0]["Rev1"], 0); //iv
    //            decimal rev2 = SafeValue.SafeDecimal(tab.Rows[0]["Rev2"], 0); //ts
    //            decimal rev3 = SafeValue.SafeDecimal(tab.Rows[0]["Rev3"], 0); //dn
    //            decimal rev4 = SafeValue.SafeDecimal(tab.Rows[0]["Rev4"], 0); //cn
    //            row["Rev1"] = rev1.ToString("###,##0.00");
    //            row["Rev2"] = rev2.ToString("###,##0.00");
    //            row["Rev3"] = rev3.ToString("###,##0.00");
    //            row["Rev4"] = "(" + rev4.ToString("###,##0.00") + ")";
    //            decimal sumRev = rev1 + rev2 + rev3 - rev4;
    //            row["Rev"] = sumRev.ToString("###,##0.00");


    //            decimal cost1 = SafeValue.SafeDecimal(tab.Rows[0]["Cost1"], 0); //pl/sc/sc
    //            decimal cost2 = SafeValue.SafeDecimal(tab.Rows[0]["Cost2"], 0); //vo
    //            decimal cost3 = SafeValue.SafeDecimal(tab.Rows[0]["Cost3"], 0); //costing

    //            row["Cost1"] = cost1.ToString("###,##0.00");
    //            row["Cost2"] = cost2.ToString("###,##0.00");
    //            row["Cost3"] = cost3.ToString("###,##0.00");
    //            decimal cost = cost1 + cost2 + cost3;
    //            row["Cost"] = cost.ToString("###,##0.00");
    //            row["Profit"] = (sumRev - cost).ToString("###,##0.00");
    //        }
    //        return tab;
    //    }
    //    public static DataSet PrintPl_house_NoAp(string refN, string jobNo,string refType, string userId)
    //    {
    //        int jobCnt = 1;
    //        decimal m3Percent = 0;
    //        if (refType == "SI")
    //        {
    //            jobCnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(string.Format("select count(jobNo) from SeaImport where RefNo='{0}'", refN)), 1);
    //            m3Percent = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(string.Format(@"select (case when job.Weight/1000>job.volume then job.Weight/1000 else job.volume end)/ case when mast.Weight>0 then (case when mast.Weight/1000>mast.volume then mast.Weight/1000 else mast.volume end)  when mast.volume>0 then (case when mast.Weight/1000>mast.volume then mast.Weight/1000 else mast.volume end) else 1 end
    // from SeaImport as job inner join SeaImportRef mast on mast.RefNo=job.RefNo where job.RefNo='{0}' and Job.JobNo='{1}'", refN, jobNo)), 1);
    //        }
    //        else if (refType == "SE")
    //        {
    //            jobCnt = SafeValue.SafeInt(ConnectSql.ExecuteScalar(string.Format("select count(jobNo) from SeaExport where RefNo='{0}'", refN)), 1);
    //            m3Percent = SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(string.Format(@"select (case when job.Weight/1000>job.volume then job.Weight/1000 else job.volume end)/ case when mast.Weight>0 then (case when mast.Weight/1000>mast.volume then mast.Weight/1000 else mast.volume end)  when mast.volume>0 then (case when mast.Weight/1000>mast.volume then mast.Weight/1000 else mast.volume end) else 1 end
    // from SeaExport as job inner join SeaExportRef mast on mast.RefNo=job.RefNo where job.RefNo='{0}' and Job.JobNo='{1}'", refN, jobNo)), 1);
    //        }
    //        if (m3Percent == 0)
    //            m3Percent = 1;
    //        DataSet set = new DataSet();
    //        DataTable tab_mast = Pl_Mast_house_NoAp(refN, jobNo,refType, jobCnt, m3Percent, userId);
    //        DataTable tab_Inv = Pl_Inv_house(refN, jobNo, refType, jobCnt, m3Percent);
    //        DataTable tab_Dn = Pl_Dn_house(refN, jobNo, refType, jobCnt, m3Percent);
    //        DataTable tab_Ts = Pl_Ts_house(refN, jobNo, refType);
    //        DataTable tab_Cn = Pl_Cn_house(refN, jobNo, refType, jobCnt, m3Percent);
    //        DataTable tab_Pl = new DataTable();
    //        DataTable tab_Vo = new DataTable();
    //        DataTable tab_Cost = Pl_Cost_house(refN, jobNo, refType, jobCnt, m3Percent);
    //        tab_mast.TableName = "Mast";
    //        tab_Inv.TableName = "IV";
    //        tab_Ts.TableName = "TS";
    //        tab_Cn.TableName = "CN";
    //        tab_Dn.TableName = "DN";
    //        tab_Pl.TableName = "PL";
    //        tab_Vo.TableName = "VO";
    //        tab_Cost.TableName = "COST";
    //        set.Tables.Add(tab_mast);
    //        set.Tables.Add(tab_Inv);
    //        set.Tables.Add(tab_Dn);
    //        set.Tables.Add(tab_Ts);
    //        set.Tables.Add(tab_Cn);
    //        set.Tables.Add(tab_Pl);
    //        set.Tables.Add(tab_Vo);
    //        set.Tables.Add(tab_Cost);
    //        return set;
    //    }
    //    private static DataTable Pl_Mast_house_NoAp(string refN, string jobNo,string refType, int jobCnt, decimal m3Percent, string userId)
    //    {
    //        DataTable tab = new DataTable("PlMast");
    //        tab.Columns.Add("RefN");
    //        tab.Columns.Add("NowD");
    //        tab.Columns.Add("UserId");
    //        tab.Columns.Add("Currency");
    //        tab.Columns.Add("ExRate");
    //        tab.Columns.Add("JobType");
    //        tab.Columns.Add("TsM3");
    //        tab.Columns.Add("LocalM3");

    //        tab.Columns.Add("Agent");
    //        tab.Columns.Add("Company");
    //        tab.Columns.Add("Obl");
    //        tab.Columns.Add("Ves");
    //        tab.Columns.Add("Eta");
    //        tab.Columns.Add("Qty");
    //        tab.Columns.Add("Pack");
    //        tab.Columns.Add("Wt");
    //        tab.Columns.Add("M3");
    //        tab.Columns.Add("Pol");
    //        tab.Columns.Add("Pod");
    //        tab.Columns.Add("ContN");

    //        tab.Columns.Add("Rev1");
    //        tab.Columns.Add("Rev2");
    //        tab.Columns.Add("Rev3");
    //        tab.Columns.Add("Rev4");
    //        tab.Columns.Add("Rev");

    //        tab.Columns.Add("Cost1");
    //        tab.Columns.Add("Cost2");
    //        tab.Columns.Add("Cost3");
    //        tab.Columns.Add("Cost");
    //        tab.Columns.Add("Profit");
    //        string sql = string.Format(@"SELECT job.JobNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,mast.CreateBy as UserId,Mast.CurrencyId as Currency,mast.ExRate,mast.JobType
    //,dbo.fun_GetPartyName(job.customerid) as Agent,'' as Company,mast.OblNo as Obl,mast.Vessel+'/'+mast.Voyage as Ves,convert(nvarchar(10),mast.Eta,103) as Eta,dbo.fun_GetPortName(mast.Pol) as Pol,dbo.fun_GetPortName(mast.Pod) as Pod,'' ContN
    //,job.Qty,job.PackageType as Pack,job.Weight as Wt,job.Volume as M3
    //,(Case when Job.TsInd='Y' then job.Volume else 0 end) as TsM3
    //,(Case when Job.TsInd='N' then job.Volume else 0 end) as LocalM3
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SI' and DocType='IV') Rev1
    //,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsAgtRate),2)),0) FROM SeaImport  WHERE (RefNo = mast.RefNo) AND (TsAgtRate > 0))*mast.ExRate as Rev2
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SI' and DocType='DN') Rev3
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SI' and DocType='CN') Rev4
    //,0 as Cost1
    //,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * TsImpRate),2)),0) FROM SeaImport WHERE (RefNo = mast.RefNo) AND (TsImpRate > 0))*mast.ExRate as Cost2
    //,( SELECT sum(CostLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobNo=job.JobNo and JobType ='SI') Cost3
    //FROM SeaImport job inner join SeaImportRef mast on job.RefNo=mast.RefNo
    //where job.RefNo='{0}' and job.JobNo='{1}'", refN, jobNo);

    //        string sql3 = string.Format("SELECT distinct ContainerNo + '/' + SealNo+'/'+ContainerType FROM SeaImportMkg WHERE RefNo = '{0}' and JobNo='{1}' and MkgType='Cont'", refN, jobNo); ;
    //        if (refType == "SE")
    //        {
    //            sql = string.Format(@"SELECT job.JobNo as RefN,convert(nvarchar(10),GetDate(),103) as NowD,mast.CreateBy as UserId,Mast.CurrencyId as Currency,mast.ExRate,mast.JobType
    //,dbo.fun_GetPartyName(job.customerid) as Agent,'' as Company,mast.OblNo as Obl,mast.Vessel+'/'+mast.Voyage as Ves,convert(nvarchar(10),mast.Eta,103) as Eta,dbo.fun_GetPortName(mast.Pol) as Pol,dbo.fun_GetPortName(mast.Pod) as Pod,'' ContN
    //,job.Qty,job.PackageType as Pack,job.Weight as Wt,job.Volume as M3
    //,(Case when Job.TsInd='Y' then job.Volume else 0 end) as TsM3
    //,(Case when Job.TsInd='N' then job.Volume else 0 end) as LocalM3
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SE' and DocType='IV') Rev1
    //,(SELECT isnull(sum(round(((CASE WHEN Weight / 1000 > Volume THEN Weight / 1000 ELSE Volume END) * ImpCharge),2)),0) FROM SeaExport  WHERE (RefNo = mast.RefNo) AND JobNo=job.JobNo AND (ImpCharge > 0))*mast.ExRate as Rev2
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SE' and DocType='DN') Rev3
    //,(SELECT isnull(sum(LineLocAmt),0) FROM XaArInvoiceDet WHERE MastRefNo = mast.RefNo and JobRefNo=job.JobNo and MastType = 'SE' and DocType='CN') Rev4
    //,0 as Cost1
    //,0 as Cost2
    //,( SELECT sum(CostLocAmt) FROM SeaCosting WHERE RefNo = mast.RefNo and JobNo=job.JobNo and JobType ='SE') Cost3
    //FROM SeaExport job inner join SeaExportRef mast on job.RefNo=mast.RefNo
    //where job.RefNo='{0}' and job.JobNo='{1}'", refN, jobNo);

    //            sql3 = string.Format("SELECT distinct ContainerNo + '/' + SealNo+'/'+ContainerType FROM SeaExportMkg WHERE RefNo = '{0}' and JobNo='{1}' and MkgType='Cont'", refN, jobNo); ;
    //        }
    //        DataTable dt = ConnectSql.GetTab(sql);
    //        DataRow row1 = tab.NewRow();
    //        for (int i = 0; i < dt.Columns.Count; i++)
    //        {
    //            string colName = dt.Columns[i].ColumnName;
    //            row1[colName] = dt.Rows[0][i];
    //        }
    //        tab.Rows.Add(row1);
    //        if (tab.Rows.Count > 0)
    //        {
    //            DataRow row = tab.Rows[0];
    //            decimal allM3 = SafeValue.SafeDecimal(tab.Rows[0]["M3"], 0);
    //            decimal transM3 = SafeValue.SafeDecimal(tab.Rows[0]["TsM3"], 0);

    //            if (allM3 == 0)
    //            {
    //                row["TsM3"] = "";
    //                row["LocalM3"] = "";
    //            }
    //            else
    //            {
    //                row["TsM3"] = transM3 + " - " + (transM3 * 100 / allM3).ToString("0.00") + "%";
    //                row["LocalM3"] = (allM3 - transM3).ToString("0.000") + " - " + ((allM3 - transM3) * 100 / allM3).ToString("0.00") + "%";
    //            }

    //            row["Company"] = System.Configuration.ConfigurationManager.AppSettings["CompanyName"];
    //            string contN = "";
    //            DataTable dt3 = ConnectSql.GetTab(sql3);
    //            for (int i = 0; i < dt3.Rows.Count; i++)
    //            {
    //                contN += dt3.Rows[i][0].ToString();
    //                if (i != dt3.Rows.Count - 1)
    //                    contN += "\n";
    //            }
    //            row["ContN"] = contN;


    //            decimal rev1 = SafeValue.SafeDecimal(tab.Rows[0]["Rev1"], 0); //invocie
    //            decimal rev2 = SafeValue.SafeDecimal(tab.Rows[0]["Rev2"], 0); //ts rate
    //            decimal rev3 = SafeValue.SafeDecimal(tab.Rows[0]["Rev3"], 0); //dn
    //            decimal rev4 = SafeValue.SafeDecimal(tab.Rows[0]["Rev4"], 0); //cn
    //            string sql_rev = string.Format(@"SELECT DocType
    //,isnull(sum(Case when SplitType='Set' then LineLocAmt/{2} else LineLocAmt*{3} end ),0) as Amt
    // FROM XaArInvoiceDet  WHERE MastRefNo = '{0}' and (JobRefNo='0' or JobRefNo='') and MastType = '{4}' group by DocType", refN,jobNo,jobCnt,m3Percent,refType);
    //            DataTable tab_rev = ConnectSql.GetTab(sql_rev);
    //            for (int j = 0; j < tab_rev.Rows.Count; j++)
    //            {
    //                string docType = SafeValue.SafeString(tab_rev.Rows[j]["DocType"]).ToUpper() ;
    //                decimal amt=SafeValue.SafeDecimal(tab_rev.Rows[j]["Amt"],0);

    //                if (docType == "IV")
    //                    rev1 += amt;
    //                else if (docType == "DN")
    //                    rev3 += amt;
    //                else if (docType == "CN")
    //                    rev4 += amt;
    //            }
    //            row["Rev1"] = rev1.ToString("###,##0.00");
    //            row["Rev2"] = rev2.ToString("###,##0.00");
    //            row["Rev3"] = rev3.ToString("###,##0.00");
    //            row["Rev4"] = "(" + rev4.ToString("###,##0.00") + ")";
    //            decimal sumRev = rev1 + rev2 + rev3 - rev4;
    //            row["Rev"] = sumRev.ToString("###,##0.00");


    //            decimal cost1 = SafeValue.SafeDecimal(tab.Rows[0]["Cost1"], 0); //ap
    //            decimal cost2 = SafeValue.SafeDecimal(tab.Rows[0]["Cost2"], 0); //
    //            decimal cost3 = SafeValue.SafeDecimal(tab.Rows[0]["Cost3"], 0); //costing
    ////            string sql_ap = string.Format(@"SELECT DocType
    ////,isnull(sum(Case when SplitType='Set' then LineLocAmt/{2} else LineLocAmt*{3} end ),0) as Amt
    //// FROM XaApPayableDet  WHERE MastRefNo = '{0}' and (JobRefNo='0' or JobRefNo='') and MastType = '{4}' group by DocType", refN, jobNo, jobCnt, m3Percent,refType);
    ////            DataTable tab_ap = ConnectSql.GetTab(sql_ap);
    ////            for (int j = 0; j < tab_ap.Rows.Count; j++)
    ////            {
    ////                string docType = SafeValue.SafeString(tab_ap.Rows[j]["DocType"]).ToUpper();
    ////                decimal amt = SafeValue.SafeDecimal(tab_ap.Rows[j]["Amt"], 0);
    ////                if (docType == "SC")
    ////                    cost1 -= amt;
    ////                else
    ////                    cost1 += amt;
    ////            }
    //            string sql_cost = string.Format(@"SELECT sum(Cast(case when SplitType='Set' then CostLocAmt/{2}
    //	       else CostLocAmt*{3}  end as numeric(38,2))) as Amount FROM SeaCosting  where RefNo='{0}' and (JobNo='0' or JobNo='') and JobType='{4}'", refN,jobNo,jobCnt,m3Percent,refType);
    //            cost3 += SafeValue.SafeDecimal(ConnectSql.ExecuteScalar(sql_cost),0);
    //            row["Cost1"] = cost1.ToString("###,##0.00");
    //            row["Cost2"] = cost2.ToString("###,##0.00");
    //            row["Cost3"] = cost3.ToString("###,##0.00");
    //            decimal cost = cost1 + cost2 + cost3;
    //            row["Cost"] = cost.ToString("###,##0.00");
    //            row["Profit"] = (sumRev - cost).ToString("###,##0.00");
    //        }
    //        return tab;
    //    }

    #endregion
}
