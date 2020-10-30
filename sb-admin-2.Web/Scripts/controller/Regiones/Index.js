function FnDelete(id_) {
    Swal.fire({
        title: '¿Desea eliminar la región?',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Confirmar!',
        cancelButtonText: 'No, Cancelar'
    }).then((result) => {

        if (result.isConfirmed) {
            var token = $('[name=__RequestVerificationToken]').val();
            $.ajax({
                url: '../../../Regiones/Delete',
                type: 'POST',
                async: true,
                dataType: 'json',
                data: {
                    __RequestVerificationToken: token,
                    id: id_
                },
                beforeSend: function () {
                },
                success: function (res) {
                    if (res.resultado) {
                        Swal.fire('Región Eliminada!', '', 'success').then(function () { location.reload(); });
                    } else {
                        Swal.fire('Región NO Eliminada!', '', 'error');
                    }
                },
                error: function (data, err) {
                    Swal.fire('Región NO Eliminada!', err, 'error');
                },
                complete: function () {
                }
            });


        }
    })
}