const _modeloEmpleado={
    idEmpleados: 0,
    nombre: "",
    apellido:"",
   // idDepartamento: 0,
    edad: 0,
    cargo:"",
    sueldo: 0
}
function MostrarEmpleado() {
    fetch("/Home/listaEmpleado")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                $("#tablaEmpleado tbody").html("");
                responseJson.forEach((empleado) => {
                    $("#tablaEmpleado tbody").append(
                        $("<tr>").append(
                            $("<td>").text(empleado.nombre),
                            $("<td>").text(empleado.apellido),
                            $("<td>").text(empleado.edad),
                            $("<td>").text(empleado.cargo),
                            $("<td>").text(empleado.sueldo),
                         /*   $("<td>").text(empleado.idDepartamento),*/
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary btn-sm boton-editar-empleado").text("Editar").data("dataEmpleado", empleado),
                                $("<button>").addClass("btn btn-danger btn-sm ms-2 boton-eliminar-empleado").text("Eliminar").data("dataEmpleado", empleado),
                            )
                        )
                    )
                })
            }
        })
}
/*

   
    fetch("/Home/listaEmpleado")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then(responseJson => {
            if (responseJson.length > 0) { responseJson.forEach(item)=> { $(#cboDepartamento).append($("<option>").val(item.idDepartamento).text(item.nombre)) } }
        })

    $("#txtFechaContrato").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight:true
        })
}, false)*/
document.addEventListener("DOMContentLoaded", function () {
    MostrarEmpleado();

    /*fetch("/Home/listaEmpleado")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach(item)=> {
                    $("#cboDepartamento").append(
                        $("<option>").val(item.idDepartamento).text(item.nombre))
                })
            }
        })}
, false)*/
},false)
function MostrarModal() {
    $("#txtNombre").val(_modeloEmpleado.nombre);
   /* $("#cboDepartamento").val(_modeloEmpleados.idDepartamento == 0 ? $("#cboDepartamento option:first").val() : _modeloEmpleados.idDepartamento);*/
    $("#txtApellido").val(_modeloEmpleado.apellido);
    $("#txtEdad").val(_modeloEmpleado.edad);
    $("#txtCargo").val(_modeloEmpleado.cargo);
    /*$("#txtFechaContrato").val(_modeloEmpleados.fechaContrato)*/
    $("#txtSueldo").val(_modeloEmpleado.sueldo)
    $("#modalEmpleado").modal("show");
}
$(document).on("click", ".boton-nuevo-empleado", function () {

    _modeloEmpleado.idEmpleado = 0;
    _modeloEmpleado.nombre = "";
    // _modeloEmpleado.idDepartamento = 0;
    _modeloEmpleado.apellido = "";
    _modeloEmpleado.edad = 0;
    _modeloEmpleado.cargo = "";
    _modeloEmpleado.sueldo = 0;
   // _modeloEmpleado.fechaContrato = "";

    MostrarModal();

})

$(document).on("click", ".boton-editar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");

    _modeloEmpleado.idEmpleado = _empleado.idEmpleado;
    _modeloEmpleado.nombre = _empleado.nombre;
    _modeloEmpleado.apellido = _empleado.apellido;
    _modeloEmpleado.edad = _empleado.edad;
    _modeloEmpleado.cargo = _empleado.cargo;
    _modeloEmpleado.sueldo = _empleado.sueldo;
    MostrarModal();

})




$(document).on("click", ".boton-guardar-cambios-empleado", function () {

    const modelo = {
        idEmpleado: _modeloEmpleado.idEmpleado,
        nombre: $("#txtNombre").val(),
        apellido: $("txtApellido").val,
        edad: $("txtEdad").val,
      /*  refDepartamento: {
            idDepartamento: $("#cboDepartamento").val()
        },*/
        sueldo: $("#txtSueldo").val(),
        cargo: $("#txtCargo").val()
       // fechaContrato: $("#txtFechaContrato").val()
    }


    if (_modeloEmpleado.idEmpleado == 0) {

        fetch("/Home/guardarEmpleado", {
            method: "POST",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue creado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No puedo crear", "error");
            })

    } else {

        fetch("/Home/editarEmpleado", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {

                if (responseJson.valor) {
                    $("#modalEmpleado").modal("hide");
                    Swal.fire("Listo!", "Empleado fue actualizado", "success");
                    MostrarEmpleados();
                }
                else
                    Swal.fire("Lo sentimos", "No puedo actualizar", "error");
            })

    }

})

$(document).on("click", ".boton-eliminar-empleado", function () {

    const _empleado = $(this).data("dataEmpleado");

    Swal.fire({
        title: "Esta seguro?",
        text: `Eliminar empleado "${_empleado.nombre}"`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, volver"
    }).then((result) => {

        if (result.isConfirmed) {

            fetch(`/Home/eliminarEmpleado?idEmpleado=${_empleado.id}`, {
                method: "DELETE"
            })
                .then(response => {
                    return response.ok ? response.json() : Promise.reject(response)
                })
                .then(responseJson => {

                    if (responseJson.valor) {
                        Swal.fire("Listo!", "Empleado fue elminado", "success");
                        MostrarEmpleados();
                    }
                    else
                        Swal.fire("Lo sentimos", "No puedo eliminar", "error");
                })

        }



    })

})