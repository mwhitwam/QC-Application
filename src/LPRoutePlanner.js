$(document).ready(function() {
    $.ajax({
        type: "POST",
        url: "/LPRoutePlanner/query/Strap,X,Y,Complexity,Age,Neighbourhood,Usage",
        data: null,
        dataType: "xml",
        async: false,
        success: function(xml) {
            $(xml).find("Result").each(function() {
                var html = "<tr>";
                $(this).find("*").each(function() {
                    html += "<td>" + $(this).text() + "</td>";
                });
                html += "</tr>";
                $("#tblCoordinates").append(html);
            });
        },
        error: function(jqXHR, exception) {
            alert(exception);
            $('body').html('<pre><span class="tag">' + jqXHR.responseText + '</span></pre>');
            //alert(jqXHR.responseText);
        }
    });
});