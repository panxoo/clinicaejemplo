function Notifica(tit, asd) {
    $.notify({
        title: '<strong>' + tit + '</strong>',
        message: asd,
        icon: 'zmdi zmdi-check zmdi-hc-fw',
    }, {
        type: 'success',
        animate: {
            enter: 'animated zoomInDown',
            exit: 'animated zoomOutUp'
        },
        offset: {
            x: 5,
            y: 70
        },
        timer: 1000,
        delay: 500
    });
}

function NotificaSave() {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: "Registro almacenado con exito.",
        showConfirmButton: false,
        timer: 1500
    })
}

function MensajeAction(tit, tex, typ, con) {
    swal({
        title: tit,
        text: tex,
        type: typ,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: con,
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        return result.value;
    })
}

function MensajeError(tit, tex) {
    swal.fire({
        icon: 'error',
        title: tit,
        text: tex,
        timer: 50000,
        confirmButtonText: 'Aceptar',
        timerProgressBar: true
    });
}