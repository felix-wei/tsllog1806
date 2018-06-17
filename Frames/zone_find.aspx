<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="zone">

  <div class="menuGroup"  id="menu-Find">
        Document Box
        </div>
    <div id="menu-Find-tree" class="menuBox"  style="border:solid 1px #cccccc;" >

    <table cellpadding="2">
    <tr>
    <td>
        <select id="search_type" onchange="$('#search_no').val('');" style="width:100px;font-family:verdana;font-size:10px;">
            <option value="-">Doc Type</option>
            <option value="DN">Import DN</option>
            <option value="IM">Import Job</option>
            <option value="EX">Export Job</option>
            <option value="CO">Coload Job</option>
        </select>
    </td>
    <td >
        <button style="width:40px;font-family:verdana;font-size:10px;" id='new_button'>New</button>
    </td>
    </tr>
    <tr>
    <td >
        <input type="text" id="search_no" style="width:100px;font-family:verdana;font-size:10px;" />
    </td>
    <td >
        <button style="width:40px;font-family:verdana;font-size:10px;" id='search_button'>Find</button>
    </td>
    </tr>
    </table>
    </div>

    
    </div>
    </form>
</body>
</html>
