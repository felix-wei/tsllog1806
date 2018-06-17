// simple toolip plugin
this.tooltip = function(){	
	/* CONFIG */		
		xOffset = 10;
		yOffset = 20;		
		// these 2 variable determine popup's distance from the cursor
		// you might want to adjust to get the right result		
	/* END CONFIG */		
	$("a.tooltip").hover(function(e){											  
		this.t = this.title;
		this.title = "";									  
		$("body").append("<p id='tooltip'>"+ this.t +"</p>");
		$("#tooltip")
			.css("top",(e.pageY - xOffset) + "px")
			.css("left",(e.pageX + yOffset) + "px")
			.fadeIn("fast");		
    },
	function(){
		this.title = this.t;		
		$("#tooltip").remove();
    });	
	$("a.tooltip").mousemove(function(e){
		$("#tooltip")
			.css("top",(e.pageY - xOffset) + "px")
			.css("left",(e.pageX + yOffset) + "px");
	});			
};

// simple tab plugin
this.simpleTab = function (group) {

    $('#tabs' + group + ' a.tab').live('click', function () {
        // Get the tab name
        var contentname = $(this).attr("id") + "_page";
       
        // hide all other tabs
        $("#pages" + group + " p").hide();
        $("#tabs" + group + " li").removeClass("current");

        // show current tab
        $("#" + contentname).show();
        $(this).parent().addClass("current");
    });

    $('#tabs' + group + ' a.remove').live('click', function () {
        // Get the tab name
        var tabid = $(this).parent().find(".tab").attr("id");
        // remove tab and related content
        var contentname = tabid + "_page";
        $("#" + contentname).remove();
        $(this).parent().remove();

        // if there is no current tab and if there are still tabs left, show the first one
        if ($("#tabs" + group + " li.current").length == 0 && $("#tabs" + group + " li").length > 0) {

            // find the first tab    
            var firsttab = $("#tabs" + group + " li:first-child");
            firsttab.addClass("current");

            // get its link name and show related content
            var firsttabid = $(firsttab).find("a.tab").attr("id");
            $("#" + firsttabid + "_page").show();
        }
    });
    // plugin
};

function addSimpleTab(group, id, color, url, title, canClose) {
    // If tab already exist in the list, return
    if ($("#" + id).length != 0)
	{
	$('#' + id).click();
        return;
	}
    var closeA = "";
    if (canClose)
        closeA = "<a href='#' class='remove'>x</a>";
    // hide other tabs
    $("#tabs" + group + " li").removeClass("current");
    $("#pages" + group + " p").hide();

    // add new tab and related content
    $("#tabs" + group).append("<li class='current' style='background:" + color + "'><a class='tab' id='" +
                id + "' href='#'>" + title +
                "</a>" + closeA + "</li>");

    $("#pages" + group).append("<p style='width:100%;height:100%;background:" + color + "' id='" + id + "_page' >" +
                "<iframe src='" + url + "' width=100% height=100% frameborder=0></iframe>" + "</p>");

    // set the newly added tab as current
    $("#" + id + "_content").show();
}
