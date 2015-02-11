$(document).ready(function () {
    $("#Brand_SelectedItem").change(function () {
        var url = $('#onDropDownChangeOnProductsFilter').val();
        var selectedBrand = $("#Brand_SelectedItem :selected").val();
        var selectedColour = $("#Colour_SelectedItem :selected").val();
        var selectedGender = $("#Gender_SelectedItem :selected").val();
        var selectedStyle = $("#Style_SelectedItem :selected").val();
        var selectedSeason = $("#Season_SelectedItem :selected").val();

        $.ajax({
            data: { selectedBrand: selectedBrand, selectedColour: selectedColour, selectedGender: selectedGender, selectedStyle: selectedStyle, selectedSeason: selectedSeason },
            url: url,
            type: 'GET',
            success: function (data) {
                $("#ProductsPartialView").html(data);
            }
        });
    });
});

$(document).ready(function () {
    $("#Colour_SelectedItem").change(function () {
        var url = $('#onDropDownChangeOnProductsFilter').val();
        var selectedBrand = $("#Brand_SelectedItem :selected").val();
        var selectedColour = $("#Colour_SelectedItem :selected").val();
        var selectedGender = $("#Gender_SelectedItem :selected").val();
        var selectedStyle = $("#Style_SelectedItem :selected").val();
        var selectedSeason = $("#Season_SelectedItem :selected").val();

        $.ajax({
            data: { selectedBrand: selectedBrand, selectedColour: selectedColour, selectedGender: selectedGender, selectedStyle: selectedStyle, selectedSeason: selectedSeason },
            url: url,
            type: 'GET',
            success: function (data) {
                $("#ProductsPartialView").html(data);
            }
        });
    });
});

$(document).ready(function () {
    $("#Gender_SelectedItem").change(function () {
        var url = $('#onDropDownChangeOnProductsFilter').val();
        var selectedBrand = $("#Brand_SelectedItem :selected").val();
        var selectedColour = $("#Colour_SelectedItem :selected").val();
        var selectedGender = $("#Gender_SelectedItem :selected").val();
        var selectedStyle = $("#Style_SelectedItem :selected").val();
        var selectedSeason = $("#Season_SelectedItem :selected").val();

        $.ajax({
            data: { selectedBrand: selectedBrand, selectedColour: selectedColour, selectedGender: selectedGender, selectedStyle: selectedStyle, selectedSeason: selectedSeason },
            url: url,
            type: 'GET',
            success: function (data) {
                $("#ProductsPartialView").html(data);
            }
        });
    });
});

$(document).ready(function () {
    $("#Style_SelectedItem").change(function () {
        var url = $('#onDropDownChangeOnProductsFilter').val();
        var selectedBrand = $("#Brand_SelectedItem :selected").val();
        var selectedColour = $("#Colour_SelectedItem :selected").val();
        var selectedGender = $("#Gender_SelectedItem :selected").val();
        var selectedStyle = $("#Style_SelectedItem :selected").val();
        var selectedSeason = $("#Season_SelectedItem :selected").val();

        $.ajax({
            data: { selectedBrand: selectedBrand, selectedColour: selectedColour, selectedGender: selectedGender, selectedStyle: selectedStyle, selectedSeason: selectedSeason },
            url: url,
            type: 'GET',
            success: function (data) {
                $("#ProductsPartialView").html(data);
            }
        });
    });
});


$(document).ready(function () {
    $("#Season_SelectedItem").change(function () {
        var url = $('#onDropDownChangeOnProductsFilter').val();
        var selectedBrand = $("#Brand_SelectedItem :selected").val();
        var selectedColour = $("#Colour_SelectedItem :selected").val();
        var selectedGender = $("#Gender_SelectedItem :selected").val();
        var selectedStyle = $("#Style_SelectedItem :selected").val();
        var selectedSeason = $("#Season_SelectedItem :selected").val();

        $.ajax({
            data: { selectedBrand: selectedBrand, selectedColour: selectedColour, selectedGender: selectedGender, selectedStyle: selectedStyle, selectedSeason: selectedSeason },
            url: url,
            type: 'GET',
            success: function (data) {
                $("#ProductsPartialView").html(data);
            }
        });
    });
});


function changeSubcategory(subCateg) {
    
    var url = $("#changeSubcategory").val();

    $.ajax({
        data: { subCateg: subCateg },
        url: url,
        type: 'GET',
        success: function (data) {
            $("#ProductAndFilters").html(data);
        }
    });
};