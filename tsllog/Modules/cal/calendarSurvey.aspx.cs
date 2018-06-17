using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class _CalendarSurvey : System.Web.UI.Page
{
    //this method only updates title and description
    //this is called when a event is clicked on the calendar

  
    private static bool CheckAlphaNumeric(string str)
    {

        return Regex.IsMatch(str, @"^[a-zA-Z0-9 ]*$");


    }

}
