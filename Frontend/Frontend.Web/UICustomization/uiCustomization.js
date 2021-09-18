(function () {
    var uiCustomization = {};

    uiCustomization.SetWidthInPercent = function (percent) {
        ASPxClientUtils.SetCookie(uiCustomization.CustomMaxWithMarkerCookieKey, percent);

        var myElements = document.querySelectorAll(".customMaxWithMarker");

        for (let i = 0; i < myElements.length; i++)
            myElements[i].style.maxWidth = percent + "%";
    };

    window.uiCustomization = uiCustomization;
})();