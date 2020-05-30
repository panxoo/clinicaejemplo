function UlListItemElemnt(_lstDest, _lstOrigen) {
    this.lstDest = _lstDest;
    this.lstOrigen = _lstOrigen;
}

UlListItemElemnt.prototype.ForComboAdd = function () {
    selected = $(this.lstOrigen).find('option:selected');

    if (!selected.val().length) {
        return;
    }

    if ($(this.lstDest).find('#' + selected.val()).length) {
        $(this.lstOrigen).get(0).selectedIndex = 0;
        return;
    }

    AddUlListItemElemnt(this.lstDest, selected.val(), selected.text());
    $(this.lstOrigen).get(0).selectedIndex = 0;
}

UlListItemElemnt.prototype.ForListAdd = function () {
    $(this.lstDest).empty();
    destAux = this.lstDest;

    $.each($(this.lstOrigen + " li"), function () {
        AddUlListItemElemnt(destAux, this.id, $(this).data('text'));
    });
}

function AddUlListItemElemnt(lstDest, id, text) {
    $(lstDest).append('<li id=' + id + ' class="list-group-item d-flex justify-content-between align-items-center">' + text + ' <a href="javascript:void(0)" data-name="' + id + '"><i class="far fa-times-circle mr-5 fa-2x text-danger"></i></a>');
    $(lstDest).on('click', 'a', function () {
        var selected = $(this).data('name');
        console.log(selected);
        console.log(lstDest + ' #' + selected);

        $(lstDest + ' #' + selected).remove();
    });
}

function VisibleTwoDivCheck(check, divp, divs) {
    if (check) {
        $(divp).removeClass('d-none');
        $(divs).addClass('d-none');
    }
    else {
        $(divs).removeClass('d-none');
        $(divp).addClass('d-none');
    }
}

function VisibleDivCheck(check, divp) {
    check ? $(divp).removeClass('d-none') : $(divp).addClass('d-none');
}

function VisibleTwoDivCheckUpdBoll(check, divp, divs, UpdBool) {
    if (check) {
        $(divp).addClass('d-none');
        $(divs).removeClass('d-none');
        $(UpdBool).val(true);
    }
    else {
        $(divs).addClass('d-none');
        $(divp).removeClass('d-none');
        $(UpdBool).val(false);
    }
}

function ReverseClass(decicion, objet, classelmPrim, classelemSec) {
    if (decicion) {
        $(objet).addClass(classelmPrim);
        $(objet).removeClass(classelemSec);
    }
    else {
        $(objet).removeClass(classelmPrim);
        $(objet).addClass(classelemSec);
    }
}