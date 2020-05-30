function CargaLocation(_url, _departamento, _provincia, _distrito) {
    this.url = _url
    this.departamento = _departamento;
    this.provincia = _provincia;
    this.distrito = _distrito;
}

CargaLocation.prototype.CargaProvincia = function (id) {
    this.CallLocation(this.provincia, id, true);
}

CargaLocation.prototype.CargaDistrito = function (id) {
    this.CallLocation(this.distrito, id, false);
    this.finalyChangeLocation();
}

CargaLocation.prototype.CallLocation = function (_idLocation, _id, prov) {
    idLocaton = $(_idLocation);
    this.changeLocation();

    $.ajax({
        type: "POST",
        url: this.url,
        data: { Id: _id },
        cache: false,
        success: function (msg) {
            idLocaton.find('option').remove();

            $(msg).each(function () {
                var option = $(document.createElement('option'));

                option.text(this.text);
                option.val(this.value);

                idLocaton.append(option);
            });

            if (prov) {
                PivoteDistrito(msg[0].value);
            }
        },
        error: function (msg) {
            console.log("Error al llenar el combo");
        }
    });
}

CargaLocation.prototype.changeLocation = function () {
    $(this.departamento).prop('disabled', true);
    $(this.provincia).prop('disabled', true);
    $(this.distrito).prop('disabled', true);
}

CargaLocation.prototype.finalyChangeLocation = function () {
    $(this.departamento).prop('disabled', false);
    $(this.provincia).prop('disabled', false);
    $(this.distrito).prop('disabled', false);
}

function Pivote(id) {
    CargaDistrito(id);
}

function CargaTree(_objtree, _url, _css) {
    console.log("aasdasd");
    this.objtree = _objtree;
    this.url = _url;
    this.css = _css;
}

CargaTree.prototype.Viewtree = function () {
    treeModCRL = $(this.objtree).tree({
        imageCssClassField: this.css,
        primaryKey: 'id',
        uiLibrary: 'bootstrap4',
        dataSource: this.url
    });
}

CargaTree.prototype.Updtree = function () {
    treeModCRL = $(this.objtree).tree({
        primaryKey: 'id',
        uiLibrary: 'bootstrap4',
        dataSource: this.url,
        checkboxes: true
    });
}