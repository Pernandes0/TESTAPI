function myFunction() {
    var x = document.getElementById("Password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function myLogin() {
    debugger;
    var validate = new Object();
    validate.Email = $('#Email').val();
    validate.Password = $('#Password').val();
    $.ajax({
        type: 'POST',
        url: "/validate/",
        cache: false,
        dataType: "JSON",
        data: validate
    }).then((result) => {

        if (result.status == true) {
            window.location.href = "/dashboard";
        } else {
            toastr.warning(result.msg)
        }
    })
};
function Register() {
    debugger;
    var confirm = $("#confirmPassword").val();
    var pw = $("#password").val();
    if (confirm == pw) {
        var dataRegister = {
            username: $("#username").val(),
            email: $("#email").val(),
            password: $("#password").val(),
            phone: $("#phone").val(),
            confirmPassword: $("#confirmPassword").val()
        };
        console.log(dataRegister);
        $.ajax({
            type: 'POST',
            url: "/regisvalidate/",
            cache: false,
            dataType: "JSON",
            data: dataRegister
        }).then((result) => {
            if (result.status == true) {
                toastr.success("Please check your email to continue your registration proccess.")
                window.location.href = "/verify";
            } else {
                toastr.warning(result.msg)
            }
            console.log(result);
        })
    }
};

function Verify() {
    debugger;
    var validate = {
        SecurityStamp: $('#verifyId').val(),
        Email: $('#Email').val()
    };
    console.log(validate);
    $.ajax({
        type: 'POST',
        url: "/verif/",
        cache: false,
        dataType: "JSON",
        data: validate
    }).then((result) => {
        if (result.status == true) {
            window.location.href = "/login";
        } else {
            toastr.warning(result.msg)
        }
        console.log(result);
    });
};
