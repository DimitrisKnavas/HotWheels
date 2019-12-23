var CarModule = (function () {
    //private variables and functions
    var urlBrands = "/Brands/GetBrands";
    var urlModels = "/Models/GetModelsFromBrandName";
    var urlCar = "/Cars/GetCarById";

    var getBrands = function () {
        $.ajax({
            type: "get",
            url: urlBrands,
            success: function (response) {

                var brands = response;

                brands.forEach(function (item) {

                    $("#Brand").append('<option value="' +item.Name +'">' + item.Name + '</option>');

                });
                getCarDetails();
            }
        });
    };

    var getModelsForBrand = function (car) {
        
            $.ajax({
                type: "get",
                url: urlModels,
                data: { brand: car.Brand },
                success: function (response) {

                    var models = response;

                    models.forEach(function (item) {
                        $("#Model").append('<option value="' + item.Name +'">' + item.Name + '</option>');
                    });

                    $("#Brand").val(car.Brand);
                    $("#Model").val(car.Model);
                    $("#Color").val(car.Color);
                    $("#cc").val(car.cc);
                    $("#Price").val(car.Price);
                    if (car.IsNegotiable) {
                        $("#IsNegotiable").prop("checked", true);
                    }
                    $("#Description").val(car.Description);
                }
            });
        
    };

    var getCarDetails = function () {
            var id = getCarId();
            $.ajax({
                type: "get",
                url: urlCar,
                data: { id: id },
                success: function (response) {
                    
                    //if we are in the Details view page
                    if ($("dd > p").length) {
                        if (response.ImageUrl == null) {
                            $("img").attr("src", "https://localhost:44356/Images/notfound.png");
                        }
                        else {
                            $("img").attr("src", "https://localhost:44356/Images/" + response.ImageUrl);
                        }
                        $("#Brand").text(response.Brand);
                        $("#Model").text(response.Model);
                        $("#Color").text(response.Color);
                        $("#cc").text(response.cc);
                        $("#Price").text(response.Price);
                        if (response.IsNegotiable) {
                            $("#Neg").text("Yes");
                        }
                        else {
                            $("#Neg").text("No");
                        }
                        $("#Desc").text(response.Description);
                    }
                    //if we are not in the Details view page
                    else getModelsForBrand(response);
                }
            });
    };

    var getCarId = function () {
            var path = window.location.pathname;
            var parts = path.split("/");
            return parts[parts.length - 1];
    }

    var getModelsForBrandOnChange = function (brand) {
        
            $.ajax({
                type: "get",
                url: urlModels,
                data: { brand: brand },
                success: function (response) {

                    var models = response;

                    models.forEach(function (item) {
                        $("#Model").append('<option>' + item.Name + '</option>');
                    });

                }
            })
        
    }

    var urlIndex = "/Cars/Index";

    var postForm = function () {
        //manually get data from form
        //var id = parseInt(getCarId());
        var id = parseInt(getCarId());
        var brand = $("#Brand").val();
        var model = $("#Model").val();
        var color = $("#Color").val();
        var cc = $("#cc").val();
        var price = $("#Price").val();
        var neg = $('input[name=IsNegotiable]:checked').val();
        var description = $("#Description").val();
        var car = {
            Id: id,
            Brand: brand,
            Model: model,
            Color: color,
            cc: cc,
            Price: price,
            IsNegotiable: neg,
            Description: description
        }

        //antiforgery token
        var token = $('input[name="__RequestVerificationToken"]', $('#carform')).val();
        console.log(token);
        $.ajax({
            url: $(this).data('url'),
            type: 'post',
            data: {
                __RequestVerificationToken: token,
                Car: car
            },
            success: function (response) {
                //alert(response);
                window.location.replace(urlIndex);

            },
            error: function (response) {
                console.log(response.error);
            }
        });
    }

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[a-z]+$/i.test(value);
    }, "Letters only please");

    var validateForm = function () {
        $('#carform').validate({ // initialize the plugin
            rules: {
                Brand: {
                    required: true
                },
                Model: {
                    required: true
                },
                Color: {
                    required: true,
                    minlength: 3,
                    lettersonly:true
                },
                cc: {
                    required: true,
                    number: true
                },
                Price: {
                    required: true,
                    number: true
                }
            }
        });
    }

    return {
        loadForm: getBrands,
        refreshModels: getModelsForBrandOnChange,
        getCarDetails: getCarDetails,
        createCar: postForm,
        editCar: postForm,
        validateForm: validateForm
    };

    
})();