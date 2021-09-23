function expandSection(sectionId) {
    $(document).ready(function () {
        $(sectionId).toggle();
    });
}

function showElement(elementId) {
    $(document).ready(function () {
        $(elementId).show();
    });
}

function hideElement(elementId) {
    $(document).ready(function () {
        $(elementId).hide();
    });
}

function scrollToId(elementId) {
    $('html, body').animate({
        scrollTop: $(elementId).offset().top
    }, 0);
}

function setOverflowAuto(elementId) {
    $(document).ready(function () {
        $(elementId).setOverflowAuto(elementId);
    });
}

function ToggleSection(elementId) {
    $(document).ready(function () {
        $(elementId).toggle();
    });
}

function expandSectionTwo() {
    $(document).ready(function () {
        $("#expandExisting").toggle();
    });
}

function expandSectionThree() {
    $(document).ready(function () {
        $("#expandNext").toggle();
    });
}