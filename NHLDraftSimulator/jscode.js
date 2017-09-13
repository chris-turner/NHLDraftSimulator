$(document).ready(function () {
    $("#aboutsub").show();
    $("#aboutheader").click(function () {
        $("#aboutsub").slideToggle();
    });
    $("#linksheader").click(function () {
        $("#linkssub").slideToggle();
    });
    $("#projectsheader").click(function () {
        $("#projectssub").slideToggle();
    });
    $("#skillsheader").click(function () {
        $("#skillssub").slideToggle();
    });
    $("#experienceheader").click(function () {
        $("#experiencesub").slideToggle();
    });


});