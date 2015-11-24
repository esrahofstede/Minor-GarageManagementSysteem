$(function () {
    $("#Exist").on("change", function () {
        if ($(this).is(":checked")) {
            $("#existing-leasemaatschappijen").fadeIn(500);
            $(".new-leasemaatschappijen").fadeOut(500);

        }
        else {
            $("#existing-leasemaatschappijen").fadeOut(500);
            $(".new-leasemaatschappijen").fadeIn(500);
        }
    });
});