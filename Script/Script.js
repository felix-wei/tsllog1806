var msg = "";
function KeyDown(e1) {
    var isie = (document.all) ? true : false;
    var e = e1.htmlEvent;
    if (isie) {
        msg = msg + " " + e.keyCode;
        if (e.keyCode == 17 && e.keyCode == 13 && e.keyCode == 74) // ctrl+j&& event.keyCode == 74
        {
            e.returnValue = false;
        }
        if (e.keyCode == 17) // ctrl+j&& event.keyCode == 74
        {
            e.returnValue = false;
        }
        if (e.keyCode == 74) // ctrl+j&& event.keyCode == 74
        {
            e.returnValue = false;
        }
        if (e.keyCode == 13) // ctrl+j
        {
            e.returnValue = false;
        }
        if (e.keyCode == 69) // ctrl+j&& event.keyCode == 74
        {
            alert(msg);
        }
    } else {
        if (e.ctrlKey && (e.which == 74 || e.which == 72)) // ctrl+j
        {
            e.returnValue = false;
            e.preventDefault();
        }
        if (e.which == 13) // ctrl+j
        {
            e.returnValue = false;
            e.preventDefault();
        }
    }
} 
