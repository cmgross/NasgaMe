$(document).ready(function () {
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
                    "<tr><td>" + link + "</td>" +
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
                //copy delete and name columns to be fixed
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
    });
});
