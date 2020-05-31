$(document).ready(() => {
    control = $("#add-note-control");
    main = $("#add-note-main");

    control.text("显示");
    main.css("display", "none");

    control.click(() => {
        main.slideToggle();
        control.text(control.text() == "隐藏" ? "显示" : "隐藏");
    });
});
