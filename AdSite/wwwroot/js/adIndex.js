$(document).ready(function () {
    declareIonRangeSlider();
});

$(function () {
    $("#form").submit(function (e) {
        e.preventDefault();
        debugger;
        var minMaxPricesArray = $('#priceSlider').val().split(';');
        var cityIds = $('input[type="checkbox"][name="cityIds"]:checked').map(function () { return this.value; }).get();

        var formAction = this.action;
        var model = {
            MinPriceValue: minMaxPricesArray[0],
            MaxPriceValue: minMaxPricesArray[1],
            SortColumn: $('#SortColumn option:selected').html(),
            ColumnName: $('#ColumnName option:selected').html(),
            PageSize: $('#PageSize option:selected').html(),
            SearchString: $('#SearchString').val(),
            CategoryId: $('#hiddenCategoryId').val(),
            CityIds: cityIds,
        };

        $.ajax({
            type: 'get',
            url: formAction,
            data: model,
            traditional: true,
            contentType: 'application/json',
            success: function (data) {
                var doc = new DOMParser().parseFromString(data, "text/html");
                var container = doc.querySelector('#adLoadContainer');
                $("#adLoadContainer").html(container.innerHTML);
                declareIonRangeSlider();
                registerCategoriesEvents();
            }
        });
    });
});

function declareIonRangeSlider() {
    $(".price-slider").ionRangeSlider({
        type: "double",
        grid: $(this).data('grid'),
        min: $(this).data('min'),
        max: $(this).data('max'),
        from: $(this).data('from'),
        to: $(this).data('to'),
        to_min: 1,
        prefix: $(this).data('prefix'),
        onFinish: function (data) {
            $("#minPriceValue").val(data.from);
            $("#maxPriceValue").val(data.to);

            $('#form').submit();
        }
    });
}


