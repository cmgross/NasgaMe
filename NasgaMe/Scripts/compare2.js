﻿$(document).ready(function () {
    $('#myTab a').click(function(e) {
        e.preventDefault();
        $(this).tab('show');
    });

    $("#addForm2").submit(function(event) {
        event.preventDefault();
        $("#dAthletePRs").removeClass("hide");
    });

    var hidWidth;
    var scrollBarWidths = 40;

    var widthOfList = function () {
        var itemsWidth = 0;
        $('.list li').each(function () {
            var itemWidth = $(this).outerWidth();
            itemsWidth += itemWidth;
        });
        return itemsWidth;
    };

    var widthOfHidden = function () {
        return (($('.wrapper').outerWidth()) - widthOfList() - getLeftPosi()) - scrollBarWidths;
    };

    var getLeftPosi = function () {
        return $('.list').position().left;
    };

    var reAdjust = function () {
        if (($('.wrapper').outerWidth()) < widthOfList()) {
            $('.scroller-right').show();
        }
        else {
            $('.scroller-right').hide();
        }

        if (getLeftPosi() < 0) {
            $('.scroller-left').show();
        }
        else {
            $('.item').animate({ left: "-=" + getLeftPosi() + "px" }, 'slow');
            $('.scroller-left').hide();
        }
    }

    reAdjust();

    $(window).on('resize', function (e) {
        reAdjust();
    });

    $('.scroller-right').click(function () {

        $('.scroller-left').fadeIn('slow');
        $('.scroller-right').fadeOut('slow');

        $('.list').animate({ left: "+=" + widthOfHidden() + "px" }, 'slow', function () {

        });
    });

    $('.scroller-left').click(function () {

        $('.scroller-right').fadeIn('slow');
        $('.scroller-left').fadeOut('slow');

        $('.list').animate({ left: "-=" + getLeftPosi() + "px" }, 'slow', function () {

        });
    });
    //OLD
    $("#addForm").submit(function (event) {
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
                    "<td>" + athlete.Braemar + "</td>" +
                    "<td>" + athlete.Open + "</td>" +
                    "<td>" + athlete.HeavyWeight + "</td>" +
                    "<td>" + athlete.LightWeight + "</td>" +
                    "<td>" + athlete.HeavyHammer + "</td>" +
                    "<td>" + athlete.LightHammer + "</td>" +
                    "<td>" + athlete.Caber + "</td>" +
                    "<td>" + athlete.Sheaf + "</td>" +
                    "<td>" + athlete.Wfh + "</td>" +
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