<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void Page_Init(object sender, EventArgs e)
    {




        //var reader = new StreamReader(Lic.LStream, Encoding.UTF8);

        //string value1 = reader.ReadToEnd();
        //Response.Write(value1);
        //return;
        // Do something with the value   


        Aspose.Cells.License lic = new Aspose.Cells.License();
        //lic.SetLicense(Lic.LStream);
        lic.SetLicense(HttpContext.Current.Server.MapPath(@"~\Api\Aspose.lic"));

        string dataDir = Server.MapPath("/api/");

        //Aspose.Cells.LoadOptions loadOptions4 = new  Aspose.Cells.LoadOptions(Aspose.Cells.LoadFormat.CSV);
        //Aspose.Cells.LoadDataOption loadOptions4 = new Aspose.Cells.FindOptions(Aspose.Cells.FileFormatType.CSV);
        //Aspose.Cells.Workbook wbCSV = new Aspose.Cells.Workbook(dataDir + "psa-bill-sample.csv", loadOptions4);

        Aspose.Cells.Workbook wbCSV = new Aspose.Cells.Workbook();
        wbCSV.LoadData(dataDir + "psa-bill-sample.csv");
        Aspose.Cells.Worksheet worksheet = wbCSV.Worksheets[0];
        Aspose.Cells.Cell cell = worksheet.Cells["C4"];

        string value = cell.Value.ToString();

        Response.Write(value);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div >
     </div>
    </form>
</body>
</html>
