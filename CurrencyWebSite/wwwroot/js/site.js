

function Get(){$.ajax({
    url: "http://businessapi/KurCache/GetAllRedisCache",
    dataType: 'json',
    success: function (data) {
        alert("Veriler console da")
        console.log(data)
    }
})};

Get();