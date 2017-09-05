$(document).ready(function() {
    var menuButtons = document.getElementsByClassName("menuButton");
    var ButtonClicked = function() {
        alert(this.id);
    };
    for (var i = 0; i < menuButtons.length; i++) {
        menuButtons[i].addEventListener('click', ButtonClicked, true);
    }
})