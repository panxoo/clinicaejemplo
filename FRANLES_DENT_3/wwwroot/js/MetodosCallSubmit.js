async function MensajeDecision(title, mensj) {
    let result = await Swal.fire({
        title: title,
        text: mensj,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Aceptar',
        cancelButtonText: 'Cancelar'
    });

    if (result.value)
        return true;
    else
        return false;
}

function AjaxSubmit(_url, _btnSubmit, _titleError) {
    this.url = _url;
    this.btnSubmit = _btnSubmit;
    this.btnCancel;
    this.partial;
    this.titleError = _titleError;
    this.divBlock;
    this.inputClear;
}

AjaxSubmit.prototype.AjaxPop = function (_dt) {
    _btnCancel = this.btnCancel;
    _partial = this.partial;
    _titleError = this.titleError;
    _btnSubmit = this.btnSubmit;
    _inputClear = this.inputClear;

    $(this.btnSubmit).prop("disabled", true);

    $.ajax({
        url: this.url,
        data: _dt,
        type: "post",
        cache: false,
        success: function (result) {
            if (result != null) {                
                $(_btnCancel).click();
                $(_partial).html(result);
                NotificaSave();
                if (_inputClear != null) {
                    _inputClear.forEach(function (input) {
                        $(input).val("");
                    });
                };
            };
        },
        error: function (result) {
            MensajeError(_titleError, result.responseJSON.mnsj);
            if (result.responseJSON.redir) {
                setTimeout(function () { window.location.replace(result.responseJSON.redirectToUrl); }, 3000);
            }
        },
        complete: function () {
            $(_btnSubmit).prop("disabled", false);
        }
    });
}

AjaxSubmit.prototype.AjaxPopRedirec = function (_dt) {
    _titleError = this.titleError;
    _btnSubmit = this.btnSubmit;

    $(this.btnSubmit).prop("disabled", true);

    $.ajax({
        url: this.url,
        data: _dt,
        type: "post",
        cache: false,
        success: function (result) {
            if (result != null) {
                NotificaSave();
            };
        },
        error: function (result) {
            MensajeError(_titleError, result.responseJSON.mnsj);
            if (result.responseJSON.redir) {
                setTimeout(function () { window.location.replace(result.responseJSON.redirectToUrl); }, 3000);
            }
        },
        complete: function (res) {
            $(_btnSubmit).prop("disabled", false);

            if (res.responseJSON.redir) {
                setTimeout(function () { window.location.replace(res.responseJSON.redirectToUrl); }, 3000);
            }
        }
    });
}

AjaxSubmit.prototype.AjaxPopCall = function (_dt) {
    _partial = this.partial;
    _divBlock = this.divBlock;
    _titleError = this.titleError;

    $(_divBlock).append('<div class="qt-block-ui"></div>');
    _block = $(_divBlock).find(".qt-block-ui");

    $.ajax({
        url: this.url,
        data: _dt,
        type: "post",
        cache: false,
        success: function (result) {
            if (result != null) {
                $(_partial).html(result);
            };
        },
        error: function (result) {
            MensajeError(_titleError, result.responseJSON.mnsj);
        },
        complete: function (res) {
            _block.fadeOut("1", function () {
                _block.remove();
            });
        }
    });
}

AjaxSubmit.prototype.AjaxGetDato = function (_dt) {
    _partial = this.partial;
    _divBlock = this.divBlock;

    $(_divBlock).append('<div class="qt-block-ui"></div>');
    _block = $(_divBlock).find(".qt-block-ui");

    $.ajax({
        url: this.url,
        data: _dt,
        type: "post",
        cache: false,
        success: function (result) {
            if (result != null) {
                $(_partial).html(result);
            };
        },
        error: function (result) {
            console.log("error llamar datos");
        },
        complete: function (res) {
            _block.fadeOut("1", function () {
                _block.remove();
            });
        }
    });
}

AjaxSubmit.prototype.AjaxPopCargaScheduler = function (_dt) {
    _btnCancel = this.btnCancel;
    _partial = this.partial;
    _titleError = this.titleError;
    _btnSubmit = this.btnSubmit;
    _inputClear = this.inputClear;

    $(this.btnSubmit).prop("disabled", true);

    $.ajax({
        url: this.url,
        data: _dt,
        type: "post",
        cache: false,
        success: function (result) {
            if (result != null) {
                $(_btnCancel).click();
                $(_partial).html(result);
                NotificaSave();
                scheduler.deleteMarkedTimespan();
                scheduler.updateView();

                console.log(result);
                //if (_inputClear != null) {
                //    _inputClear.forEach(function (input) {
                //        $(input).val("");
                //    });
                //};
            };
        },
        error: function (result) {
            MensajeError(_titleError, result.responseJSON.mnsj);
            if (result.responseJSON.redir) {
                setTimeout(function () { window.location.replace(result.responseJSON.redirectToUrl); }, 3000);
            }
        },
        complete: function () {
            $(_btnSubmit).prop("disabled", false);
        }
    });
}