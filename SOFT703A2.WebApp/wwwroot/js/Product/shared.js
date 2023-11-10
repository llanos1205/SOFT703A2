$(document).ready(function () {
    // Attach an event listener to the input field
    $("#photoUrlInput").on("input", function () {
        // Get the value from the input field
        var imageUrl = $(this).val();

        // Set the image source to the input value
        $("#previewImage").attr("src", imageUrl);
    });
});