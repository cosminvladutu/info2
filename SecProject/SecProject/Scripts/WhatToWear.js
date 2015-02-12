function whatToWearAction() {

    var url = $("#whatToWearAction").val();
    var selectedGender = $("#Gender_SelectedItem :selected").val();
    var selectedStyle = $("#Style_SelectedItem :selected").val();
    var selectedSeason = $("#Season_SelectedItem :selected").val();
    $.ajax({
        data: { selectedGender: selectedGender, selectedStyle: selectedStyle, selectedSeason: selectedSeason },
        url: url,
        type: 'GET',
        success: function (data) {
            $("#WhatToWearProducts").html(data);
        }
    });
};