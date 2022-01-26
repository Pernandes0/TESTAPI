var table = null;
var tablecustomer = null;
$(document).ready(function () {
    debugger;
    tablecustomer = $("#barangadd").DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/Barang/LoadBarang",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "data": "id",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                    //return meta.row + 1;
                }
            },
            { "data": "name" },
            {
                "data": "deskripsi"
            },
            {
                "data": "quantity"
            },
            {
                "data": "harga"
            },
        ],
    })
});

$(document).ready(function () {
    debugger;
    table = $("#barang").DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/Barang/LoadBarang",
            type: "GET",
            dataType: "json",
            dataSrc: "",
        },
        "columns": [
            {
                "data": "id",
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                    //return meta.row + 1;
                }
            },
            { "data": "name" },
            {
                "data": "deskripsi"
            },
            {
                "data": "quantity"
            },
            {
                "data": "harga"
            },
            {
                "sortable": false,
                "data": "id",
                "render": function (data, type, row, meta) {
                    console.log(row);
                    console.log(data);
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button class="btn btn-outline-warning btn-circle" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + meta.row + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                        + '&nbsp;'
                        + '<button class="btn btn-outline-danger btn-circle" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + meta.row + ')" ><i class="fa fa-lg fa-times"></i></button>'
                }
            },
        ],
    })
});

function validate() {
    var isValid = true;
    if ($('#name').val().trim() == "") {
        $('#name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#name').css('border-color', 'lightgrey');
    }
    if ($('#quantity').val().trim() == "") {
        $('#quantity').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#quantity').css('border-color', 'lightgrey');
    }
    if ($('#harga').val().trim() == "") {
        $('#harga').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#harga').css('border-color', 'lightgrey');
    }
    if ($('#deskripsi').val().trim() == "") {
        $('#deskripsi').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#deskripsi').css('border-color', 'lightgrey');
    }
    return isValid;
}

function ClearScreen() {
    $('#id').val('');
    $('#name').val('');
    $('#quantity').val('');
    $('#harga').val('');
    $('#deskripsi').val('');
    $('#update').hide();
    $('#add').show();
}

function GetById(idrow) {
    debugger;
    var getId = table.row(idrow).data().id;
    $.ajax({
        url: "/Barang/GetById/",
        data: { id: getId }
    }).then((result) => {
        $('#id').val(result.id);
        $('#name').val(result.name);
        $('#quantity').val(result.quantity);
        $('#harga').val(result.harga);
        $('#deskripsi').val(result.deskripsi);

        $('#add').hide();
        $('#update').show();
        $('#myModal').modal('show');
    })
}

function Save() {
    debugger;
    Swal.showLoading()
    var check = validate();
    if (check == false) {
        $('#myModal').modal('show');
        Swal.fire('Error', 'Invalid Data Input !', 'error');
        return false;
    }
    var barang = new Object();
    barang.Id = null;
    barang.Name = $('#name').val();
    barang.Quantity = $('#quantity').val();
    barang.Deskripsi = $('#deskripsi').val();
    barang.Harga = $('#harga').val();

    $.ajax({
        type: 'POST',
        url: "/Barang/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: barang

    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            ClearScreen();
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data inserted Successfully',
                showConfirmButton: false,
                timer: 1500,
            })
            table.ajax.reload(null, false);
            window.location.href = "/barang";
        } else if (result.statusCode == 400) {
            Swal.fire('Error', 'Barang name has already in use', 'error');
            ClearScreen();
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Update() {
    debugger;
    Swal.showLoading()
    var check = validate();
    if (check == false) {
        $('#myModal').modal('show');
        Swal.fire('Error', 'Invalid Data Input !', 'error');
        return false;
    }
    var barang = new Object();
    barang.Id = $('#id').val();
    barang.Name = $('#name').val();
    barang.Quantity = $('#quantity').val();
    barang.Harga = $('#harga').val();
    barang.Deskripsi = $('#deskripsi').val();
    $.ajax({
        type: 'POST',
        url: "/Barang/InsertOrUpdate/",
        cache: false,
        dataType: "JSON",
        data: barang
    }).then((result) => {
        debugger;
        if (result.statusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data Updated Successfully',
                showConfirmButton: false,
                timer: 1500,
            });
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    })
}

function Delete(id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
    }).then((resultSwal) => {
        if (resultSwal.value) {
            debugger;
            var getId = table.row(id).data().id;
            $.ajax({
                url: "/Barang/Delete/",
                data: { id: getId }
            }).then((result) => {
                debugger;
                if (result.statusCode == 200) {
                    debugger;
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Delete Successfully',
                        showConfirmButton: false,
                        timer: 1500,
                    });
                    table.ajax.reload(null, false);
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
    });
}