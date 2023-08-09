const _modeloEmpleados={
    idEmpleados: 0,
    nombreCompleto: "",
    idDepartamento: 0,
    edad:0,
    sueldo: 0,
}
function MostrarEmpleados() {
    fetch("/Home/listaEmpleado")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        }).
        then(responseJson => {
            if (responseJson.length > 0) {
                $("#tablaEmpleado tbody").html("");
                responseJson.forEach((empleado) => {
                    $("tablaEmpleado tbody").append(
                        $("<tr>").append(
                            $("<td>").text(empleado.nombreCompleto),
                            $("<td>").text(empleado.edad),
                            $("<td>").text(empleado.sueldo),
                            $("<td>").text(empleado.cargo),
                            $("<td>").text(empleado.idDepartamento),
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
document.addEventListener("DOMContentLoaded", function () {
    MostrarEmpleados()
    fetch("/Home/listaEmpleado")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        }).then(responseJson => { if (responseJson.length > 0) { responseJson.forEach(item)=> { $(#cboDepartamento).append($("<option>").val(item.idDepartamento).text(item.nombre)) } } })

    $("#txtFechaContrato").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight:true
        })
}, false)

function MostraModal() {
    $("#txtNombreCompleto").val(_modeloEmpleados.nombreCompleto);
    $("#cboDepartamento").val(_modeloEmpleados.idDepartamento == 0 ? $("#cboDepartamento option:first").val() : _modeloEmpleados.idDepartamento);
    $("#txtSueldo").val(_modeloEmpleados);
    $("#txtFechaContrato").val(_modeloEmpleados.fechaContrato)
    $("#modalEmpleado").modal("show");
}