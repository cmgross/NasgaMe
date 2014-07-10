$(document).ready(function () {
    $(window).resize(function () {
        if ($("#ddlThrows").css("display") == "none") {
            $("#ddlThrows").val(String.empty).change();
            $(".throwsColumn").removeClass("showCell");
            $(".throwsColumn").removeClass("hide");
        }
    });

    $("#ddlThrows").change(function () {
        var selectedThrow = $("#ddlThrows").val();
        $(".throwsColumn").addClass("hide");
        $(".throwsColumn").removeClass("showCell");
        if (selectedThrow === "") return;
        var selectedThrowClass = "." + selectedThrow;
        $(selectedThrowClass).removeClass("hide");
        $(selectedThrowClass).addClass("showCell");
    });


    $("#addForm2").submit(function (event) {
        event.preventDefault();
        var url = "/Home/GetAthletePRs";
        var nameAndClass = $("#NameAndClass").val();
        $.ajax({
            dataType: "json",
            cache: true,
            url: url,
            data: { nameAndClass: nameAndClass },
            beforeSend: function () {
                $.blockUI({
                    message: "Loading",
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#000',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        opacity: .5,
                        color: '#fff'
                    }
                });
            },
            success: function (data) {
                var athlete = $.parseJSON(data);
                var link = "<a href='javascript:;' class='delete'>[X]</a>";
                $("#tbAthletePRs tbody").append(
                    "<tr>" +
                    "<td>" + link + "</td>" +
                    "<td>" + athlete.Name + "</td>" +
                    "<td>" + athlete.Class + "</td>" +
                    "<td class='throwsColumn Braemar'>" + athlete.Braemar + "</td>" +
                    "<td class='throwsColumn Open'>" + athlete.Open + "</td>" +
                    "<td class='throwsColumn HWFD'>" + athlete.HeavyWeight + "</td>" +
                    "<td class='throwsColumn LWFD'>" + athlete.LightWeight + "</td>" +
                    "<td class='throwsColumn HH'>" + athlete.HeavyHammer + "</td>" +
                    "<td class='throwsColumn LH'>" + athlete.LightHammer + "</td>" +
                    "<td class='throwsColumn Caber'>" + athlete.Caber + "</td>" +
                    "<td class='throwsColumn Sheaf'>" + athlete.Sheaf + "</td>" +
                    "<td class='throwsColumn WFH'>" + athlete.Wfh + "</td>" +
                    "</tr>"
                );
                $.bootstrapSortable(true);
                //clone();
                toggleTable();
            },
            complete: function () {
                $.unblockUI();
                $("#NameAndClass").val('');
            }
        });
    });

    $("#tbAthletePRs").on('click', '.delete', function (e) {
        e.preventDefault();
        $(this).closest("tr").remove();
        $.bootstrapSortable(true);
        toggleTable();
        //var $cloneTable = $("#clonedTable");
        //$cloneTable.remove();
        //clone();
    });

    function toggleTable() {
        var rowCount = $("#tbAthletePRs tbody tr").length;
        if (rowCount > 0) {
            $("#tbAthletePRs").removeClass("hide");
        } else {
            $("#tbAthletePRs").addClass("hide");
            $("#ddlThrows").val(String.empty).change();
        }
    }
    function clone() {
        var $cloneTable = $("#clonedTable");
        $cloneTable.remove();
        var $table = $("#tbAthletePRs");
        $table.css("z-index", 1);
        var $fixedColumn = $table.clone().prop({ id: "clonedTable", name: "clonedTable" }).insertBefore($table).addClass("fixed-column");
        $fixedColumn.find('th:not(:first-child),td:not(:first-child)').remove();
        $fixedColumn.find('tr').each(function (i, elem) {
            $(this).height($table.find('tr:eq(' + i + ')').height());
        });
        $cloneTable.css("z-index", 2);
        //$fixedColumn.find('th:not(:nth-child(-n+2)),td:not(:nth-child(-n+2))').remove(); 
    }
});
