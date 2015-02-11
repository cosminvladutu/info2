function changeSubcategory(subCateg) {

    var url = $("#changeSubcategory").val();

    $.ajax({
        data: { subCateg: subCateg },
        url: url,
        type: 'GET',
        success: function (data) {
            $("#ProductAndFiltersOnWardrobe").html(data);
        }
    });
};