$(document).ready(function () {
    $('#NameAndClass').typeahead({
        minLength: 4,
        source: function (term, process) {
            var url = "/Home/GetSearchResults";
            $.ajax({
                dataType: "json",
                cache: true,
                url: url,
                data: { term: term },
                success: function (data) {
                    return process(data);
                }
            }
            );
        }
    });
});