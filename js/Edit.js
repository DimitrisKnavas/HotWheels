$(document).ready(function () {
    CarModule.loadForm();
    CarModule.validateForm();
    //getBrandsForDropDown();
});

$("#Brand").change(function () {
    var selectedBrand = $("#Brand").val();
    $("#Model").empty();
    CarModule.refreshModels(selectedBrand);
    //getModelsForBrandOnChange(selectedBrand);
});

$('#submit-btn').on('click',function (e) {
    
    //if data sent manually i must prevent default form submit
    e.preventDefault();
    CarModule.editCar();
    ////manually get data from form
    ////var id = parseInt(getCarId());
    //var id = parseInt(getCarId());
    //var brand = $("#Brand").val();
    //var model = $("#Model").val();
    //var color = $("#Color").val();
    //var car = {
    //    Id: id,
    //    Brand: brand,
    //    Model: model,
    //    Color: color
    //}

    ////antiforgery token
    //var token = $('input[name="__RequestVerificationToken"]', $('#carform')).val();
    //console.log(token);
    //$.ajax({
    //    url: $(this).data('url'),
    //    type: 'post',
    //    data: {
    //        __RequestVerificationToken: token,
    //        Car: car
    //    },
    //    success: function (response) {
    //        //alert(response);
    //        window.location.replace("/Cars/Index");

    //    },
    //    error: function (response) {
    //        console.log(response.error);
    //    }
    //});
});

////get id from url(not in use here)
//function getCarId() {
//    var path = window.location.pathname;
//    var parts = path.split("/");
//    return parts[parts.length - 1];
//}

//function getBrandsForDropDown() {
//    $.ajax({
//        type: "get",
//        url: "/Brands/GetBrands",
//        success: function (response) {

//            var brands = response;

//            brands.forEach(function (item) {

//                $("#Brand").append('<option>' + item.Name + '</option>');

//            });
//            getCarDetails();
//        }
//    });
//}

//function getModelsForBrand(car) {
//    $.ajax({
//        type: "get",
//        url: "/Models/GetModelsFromBrandName",
//        data: { brand: car.Brand },
//        success: function (response) {

//            var models = response;

//            models.forEach(function (item) {
//                $("#Model").append('<option>' + item.Name + '</option>');
//            });

//            $("#Brand").val(car.Brand);
//            $("#Model").val(car.Model);
//            $("#Color").val(car.Color);
//        }
//    });
//}

//function getModelsForBrandOnChange(brand) {
//    $.ajax({
//        type: "get",
//        url: "/Models/GetModelsFromBrandName",
//        data: { brand: brand },
//        success: function (response) {

//            var models = response;

//            models.forEach(function (item) {
//                $("#Model").append('<option>' + item.Name + '</option>');
//            });

//        }
//    })
//}

////function getCarDetails() {
////    var id = getCarId();
////    $.ajax({
////        type: "get",
////        url: "/Cars/GetCarById",
////        data: { id: id },
////        success: function (response) {

////            getModelsForBrand(response);
            
////        }
////    });
////}