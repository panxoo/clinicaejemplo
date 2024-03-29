﻿function UlListItemElemntAdd(_lstDest, _lstOrigen) {
    this.lstDest = _lstDest;
    this.lstOrigen = _lstOrigen;
}

UlListItemElemntAdd.prototype.ForComboAdd = function () {
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

UlListItemElemntAdd.prototype.ForListAdd = function () {
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

function ClearAtributo() {
    this.inputClear;
    this.selectFirst;
    this.listClear;
    this.divHidden;
}

ClearAtributo.prototype.ExecClearAttr = function () {

    if (this.inputClear != null) {
        this.inputClear.forEach(function (input) {
            $(input).val("");
        });
    };

    if (this.selectFirst != null) {
        this.selectFirst.forEach(function (input){
            $(input).prop('selectedIndex', 0);
        })
    }

    if (this.listClear != null) {
        this.listClear.forEach(function (input) {
            $(input).empty();
        })
    }

    if (this.divHidden != null) {
        this.divHidden.forEach(function (input) {
            VisibleDivCheck(false, input);
        })
    }

}

function CheckedCheckbox(objet, decicion) {
    if (decicion) {
        $(objet).attr("checked", "checked");
        $(objet).prop("checked", true);
    }
    else {
        $(objet).prop("checked", false);
    }
}

function SetSessionArray(name, valor) {
    sessionStorage.setItem(name, JSON.stringify(valor));
}

function GetSession(name) {
   return JSON.parse(sessionStorage.getItem(name));
}

function RemSession(name) {
    sessionStorage.removeItem(name);
}
