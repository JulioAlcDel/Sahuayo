

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })

}
jQueryAjaxPost = form => {
    debugger

    try {
        let formData = new FormData(form);
        var valueCheck = document.getElementById('TieneEnfermedad').checked.toString()
        $('#TieneEnfermedad').attr('value', valueCheck)
        formData.delete('TieneEnfermedad');
        $('#TieneEnfermedad').prop('checked', true)
        formData.append('TieneEnfermedad', valueCheck);
        $.ajax({
            type: 'POST',
            url: form.action,
            data: formData,
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    successValidate(true, "Registro Exitoso")
                    $('#myTable').dataTable().fnDestroy();
                    loadData()
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                successValidate(false, "Ocurrio un error favor de consulta con administrador")
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}
changeCheck = () => {

}
loadData = () => {
    $.ajax({
        url: '/Persona/GetPersonas',
        type: "POST",
        dataType: 'json',
        success: function (data) {

            assignToEventsColumns(data);
        }
    });
}
 assignToEventsColumns =(data) => {
    var table = $('#myTable').dataTable({
        "bAutoWidth": false,
        "aaData": data.data,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
        },
        "columns": [{
            "sTitle": "Id de Persona",
            "data": "idPersona"
        }, {
            "sTitle": "Nombre",
            "data": "apellidoPaterno"
        },
        {
            "sTitle": "Apellido Materno",
            "data": "apellidoPaterno"
        }, {
            "sTitle": "Apellido Paterno",
            "data": "apellidoMaterno"
        }, {
            "sTitle": "Descripcion",
            "data": "descripcion"
        }, {
            "sTitle": "Sueldo",
            "data": "sueldo"
            },
            {
                "sTitle": "Tiene enfermedad",
                "data": null, "render": function (data, type, row) {
                    if (row.tieneEnfermedad) {
                        return '<p>SI</p>'
                    } else {
                        return '<p>NO</p>'
                    }
                }
            },
        {
            "sTitle": "Eliminar",
            "data": null, "render": function (data, type, row) {
                return "<a href='#' class='btn btn-danger' onclick=DeleteData('" + row.idPersona + "'); >Eliminar</span></a>";
            }
        }, {
            "sTitle": "Editar",
            "data": null, "render": function (data, type, row) {
                return '<a class="btn btn-info text-white" onclick=showInPopup("/Persona/AddOrEdit/' + row.idPersona + '","Editar")>Editar</a>';
            }
        },

        ]
    })

}


function DeleteData(id) {
    Swal
        .fire({
            title: "Esta seguro de realizar operación",
            text: "¿Eliminar?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar",
        })
        .then(resultado => {
            if (resultado.value) {
                $.ajax({
                    type: "POST",
                    url: "/Persona/Delete?id=" + id,
                    async: false,
                    dataType: "json",
                    traditional: true,
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response.isValid == true) {
                            $('#myTable').dataTable().fnDestroy();
                            loadData()
                            successValidate(true, "Se elimino correctamente")
                        } else {
                            successValidate(true, "No se elimino correctamente")
                        }
                    }, error: function (err) {
                        successValidate(false, "Ocurrio un error favor de consulta con administrador")
                    }
                });
            } else {

                return false;
            }
        });
}
successValidate = (tipo, menssage) => {
    if (tipo == true) {

        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: menssage,
            showConfirmButton: false,
            timer: 1500
        })
    } else {
        Swal.fire({
            title: menssage,
            type: 'error',
            confirmButtonText: 'Cerrar'
        })
    }
}