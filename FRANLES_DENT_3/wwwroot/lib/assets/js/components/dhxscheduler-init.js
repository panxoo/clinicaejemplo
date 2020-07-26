function schedulerHorario()  {


    let formatHeader = scheduler.date.date_to_str(scheduler.config.week_date);

    scheduler.templates.week_scale_date = function (date) {
        return formatHeader(date);
    };

    let formatHour = scheduler.date.date_to_str("%H:%i");
    scheduler.config.hour_size_px = 88;

    scheduler.templates.hour_scale = function (date) {
        let step = 30;
        var html = "";
        for (var i = 0; i < 60 / step; i++) {
            html += "<div style='height:44px;line-height:44px;'>" + formatHour(date) + "</div>";
            date = scheduler.date.add(date, step, "minute");
        }
        return html;
    }

            scheduler.config.readonly = true;

    //scheduler.xy.scale_height = 50;
    scheduler.xy.nav_height = 0;
    scheduler.config.time_step = 30;

}