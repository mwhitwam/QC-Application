var lastUnassigned = '0';
$(document).ready(function() {
    CheckAssignStatus();
    CheckWorkAssigned();
    window.setInterval(function() {
        CheckAssignStatus();
    }, 1000);
});

function CheckAssignStatus() {
    $.ajax({
        type: "POST",
        url: "/LPRoutePlanner/AssignStatus/SVC_LP",
        data: null,
        dataType: "xml",
        async: false,
        success: function(xml) {
            var unassigned = $(xml).find('unassigned').text();
            if (unassigned != lastUnassigned) {
                lastUnassigned = unassigned;
                $('#unassigned').empty();
                $('#unassigned').append(unassigned);
                CheckWorkAssigned();
            }
            var check = $(xml).find('check').text();
            $('#check').empty();
            $('#check').append(check);
        }
    });
}

function CheckWorkAssigned() {
    $.ajax({
        type: "POST",
        url: "/LPRoutePlanner/AssignWork/SVC_LP",
        data: null,
        dataType: "xml",
        async: false,
        success: function(xml) {
            var html = "";
            $(xml).find("Team").each(function() {
                html += "<h1>Routing Plan</h1>";
                $(this).find("size").each(function() {
                    html += "<h2> Number of routes: " + $(this).text() + "</h2>";
                });
                $(this).find("routes Route").each(function() {
                    html += "<br> Total straight-line distance: " + $(this).find("totalDistanceInKM").text() + "km";
                    html += "<table><tr><th>Property</th><th>X</th><th>Y</th></tr>";
                    $(this).find("route Point").each(function() {
                        html += "<tr>";
                        $(this).find("*").each(function() {
                            html += "<td>" + $(this).text() + "</td>";
                        });
                        html += "</tr>";
                    });
                    html += "</table>";
                    html += "<br/>";
                });
                $('#dvMain').empty();
                $('#dvMain').append(html);
            });
        },
        error: function(jqXHR, exception) {
            alert(exception);
            $('body').html('<pre><span class="tag">' + jqXHR.responseText + '</span></pre>');
        }
    });
}