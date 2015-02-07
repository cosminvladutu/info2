Sys.Application.add_load(LoadSimplaJS);
function LoadSimplaJS() {
    //Minimize Content Box

    $(".nav-tabs h3").css({ "cursor": "s-resize" }); // Give the h3 in Content Box Header a different cursor
    $(".closed-box .tab-content").hide(); // Hide the content of the header if it has the class "closed"
    $(".closed-box .alltabs").hide(); // Hide the tabs in the header if it has the class "closed"

    //$(".content-box-header h3").click( // When the h3 is clicked...
	//		function () {
	//		    $(this).parent().next().toggle(); // Toggle the Content Box
	//		    $(this).parent().parent().toggleClass("closed-box"); // Toggle the class "closed-box" on the content box
	//		    $(this).parent().find(".content-box-tabs").toggle(); // Toggle the tabs
	//		}
	//	);

    $('.nav-tabs-custom ul li').click( // When a tab is clicked...
			function () {
			    $(this).parent().find("li").removeClass('current active'); // Remove "current" class from all tabs
			    $(this).addClass('current active'); // Add class "current" to clicked tab
			    var currentTab = $(this).attr('href'); // Set variable "currentTab" to the value of href of clicked tab
			    $(currentTab).siblings().hide(); // Hide all content divs
			    $(currentTab).show(); // Show the content div with the id equal to the id of clicked tab
			    return false;
			}
		);

    //Close button:

    $(".close").click(
			function () {
			    $(this).parent().fadeTo(400, 0, function () { // Links with the class "close" will close parent
			        $(this).slideUp(400);
			    });
			    return false;
			}
		);

    // Alternating table rows:

    $(".table-content tr:even").addClass("table-alt-row"); // Add class "alt-row" to even table rows
    $(".table-content tr:odd").addClass("table-row"); // Add class "alt-row" to even table rows
    $(".table-details table:even").addClass("table-alt-row");
    //    $(".sortableTable tr:odd").addClass("table-alt-row");

    //select.multiple-line-text-box
    $("select.multiple-line-text-box option").click(function () {
        $("select.multiple-line-text-box option.option-selected").removeClass("option-selected");
        $(this).addClass("option-selected");
    });
}

$(document).ready(function () {

    // Content box tabs:

    $('.nav-tabs-custom .tab-content div.tab-content').hide(); // Hide the content divs
    $('ul.nav-tabs li a.default-tab').addClass('current'); // Add the class "current" to the default tab
    $('.tab-content div.default-tab').show(); // Show the div with class "default-tab"

    //Sidebar Accordion Menu:

    $(".main-nav li ul").hide(); // Hide all sub menus
    $(".main-nav li a.current").parent().find("ul").slideToggle("slow"); // Slide down the current menu item's sub menu

    $(".main-nav li a.nav-top-item").click( // When a top menu item is clicked...
			function () {
			    $(this).parent().siblings().find("ul").slideUp("normal"); // Slide up all sub menus except the one clicked
			    $(".main-nav li a").each(
                function () {
                    $(this).removeClass("current");
                });
			    $(this).next().slideToggle("normal"); // Slide down the clicked sub menu
			    $(this).addClass("current");
			    return false;
			}
		);

    $(".main-nav li a.no-submenu").click( // When a menu item with no sub menu is clicked...
			function () {
			    window.location.href = (this.href); // Just open the link instead of a sub menu
			    return false;
			}
		);

    // Sidebar Accordion Menu Hover Effect:

    //$(".sidebar-menu li .nav-top-item").hover(
	//		function () {
	//		    $(this).stop().animate({ paddingRight: "25px" }, 200);
	//		},
	//		function () {
	//		    $(this).stop().animate({ paddingRight: "15px" });
	//		}
	//	);

    $(".emailTextBox").keyup(function () {
        if (!isValidEmailAddress($(".emailTextBox").val())) {
            $(".emailTextBox").addClass("errorTextBox");
            $(".emailTextBox").removeClass("successTextBox");
        }
        else {
            $(".emailTextBox").removeClass("errorTextBox");
            $(".emailTextBox").addClass("successTextBox");
        }
    });

    $(".emailTextBox").focusout(function () {
        if (!isValidEmailAddress($(".emailTextBox").val())) {
            $(".emailTextBox").addClass("errorTextBox");
            $(".emailTextBox").removeClass("successTextBox");
        }
        else {
            $(".emailTextBox").removeClass("errorTextBox");
            $(".emailTextBox").addClass("successTextBox");
        }
    });

    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-z0-9]{1}[a-z0-9\-]{0,62}[a-z0-9]{1})|[a-z])\.)+[a-z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$/i);
        return pattern.test(emailAddress);
    }

    var icons = {
        header: "accordionHeaderUnselected",
        headerSelected: "accordionHeaderSelected"
    };

    $('.accordionExpanded').accordion({
        icons: icons,
        collapsible: true,
        autoHeight: false, 

        active: 0
    });

    $('.accordion').accordion({
        icons: icons,
        collapsible: true,
        autoHeight: false,
        active: false
    });

    $('.datePicker').datePicker({ clickInput: true }).dpSetOffset(25, 0);
    $('.datePicker-default').datePicker({ clickInput: true, createButton: false }).dpSetStartDate('01/01/2005').dpSetOffset(25, 0);
    $('.datePicker-scheduler').datePicker({ clickInput: true }).dpSetOffset(25, 0);

    $(".timepicker").timepicker({ hourGrid: 3, minuteGrid: 10 });

});