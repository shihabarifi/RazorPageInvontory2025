
$(document).ready(function () {
    addProductToInvoice();

    //////////////////////////////////////////////////////
    //              select2 تهيئة
    /////////////////////////////////////////////////////


    //$('.select2').select2({
    //    placeholder: "اختر عنصرًا",
    //    allowClear: true
    //});


    /////////////////////////////////////////////////////
    //       وضع رقم العميل عند إختيار رقم العميل
    /////////////////////////////////////////////////////


    // عند تغيير الخيار في القائمة المنسدلة
    $('#customerSelect').on('change', function () {
        // جلب القيمة المختارة (رقم العميل)
        const customerNumber = $(this).val();

        // تعيين القيمة في حقل رقم العميل
        $('#CustomerNumber').val(customerNumber || '');
    });



    //////////////////////////////////////////////////////
    //  جلب جميع العملاء والمنتجات عند تحميل الصفحة
    /////////////////////////////////////////////////////



    $('#PaySelect').on('change', function () {

        var selectedValue = $(this).val();

        if (selectedValue === "Cash") {
            $('#selectName').text('اسم العميل');
            GetCustomers();
        }
        else if (selectedValue === "Credit") {
            $('#selectName').text('حساب العميل');
            GetAccounts();
        }
        else if (selectedValue === "Cheek") {
            $('#selectName').text('حساب البنك');

            GetBanks();
        }
        else {
            $('#selectName').text('اختيار غير صالح');
        }
    });



    //////////////////////////////////////////////////////////////////////
    //  دالة تقوم بجلب البيانات لجداول الحسابات
    //////////////////////////////////////////////////////////////////////

    function GetAccounts() {

        $.ajax({
            url: "/POS?handler=GetAccounts",
            method: "GET",
            success: function (data) {
                let list = $("#customerSelect");
                list.empty();
                /*              list.append($('<option>', { value: '', text: 'اختر عنصرًا' }));*/
                data.forEach(Account => {
                    list.append(`<option value="${Account.id}">${Account.name}</option>`);
                });
                list.select2();
            },
            error: function () {
                alert("حدث خطأ أثناء جلب البيانات.");
            }
        });
    }


    ////////////////////////////////////////////////////////////////////////////////////////

    function GetCustomers() {

        $.ajax({
            url: "/POS?handler=GetCustomers",
            method: "GET",
            success: function (data) {
                let list = $("#customerSelect");
                list.empty();
                /*                list.append($('<option>', { value: '', text: 'اختر عنصرًا' }));*/
                data.forEach(customer => {
                    list.append(`<option value="${customer.id}">${customer.name}</option>`);
                });
                list.select2();
            },
            error: function () {
                alert("حدث خطأ أثناء جلب البيانات.");
            }
        });
    }

    ///////////////////////////////////////////////////////////////////////////////////////


    function GetBanks() {

        $.ajax({
            url: "/POS?handler=GetBanks",
            method: "GET",
            success: function (data) {
                let list = $("#customerSelect");
                list.empty();
                data.forEach(Bank => {
                    list.append(`<option value="${Bank.id}">${Bank.name}</option>`);
                });
                list.select2();
            },
            error: function () {
                alert("حدث خطأ أثناء جلب البيانات.");
            }
        });
    }



   //////////////////////////////////////////////////////
  //      وضع رقم المنتج عند الإختيار من القائمة
 /////////////////////////////////////////////////////

    $('#productSelect').on('change', function () {    
        var selectedProductValue = $(this).val();
        $('#ProductID').val(selectedProductValue);
    });

  ///////////////////////////////////////////////////////////////////////////////////////////////////
 //    // مراقبة التغييرات على الحقول داخل الجدول
///////////////////////////////////////////////////////////////////////////////////////////////////
   
    $('#productsTable').on('input', '.itemQuantity, .unitPrice, .itemDescountRatio', function () {
        updateTotals();
    });

    // مراقبة تغيير نسبة الخصم العامة
    $('#discount').on('input', updateNetAmount);


    //  تحديث الإجمالي عند تغيير حقل الكمية
    $('#productsTable tbody').on('input', '.quantity', function () {
        updateTotals();
        updateNetAmount();
    });

  /////////////////////////////////////////////////////////////////////////////////////////////////////
 //       تحديث إجمالي الكمية مع السعر
////////////////////////////////////////////////////////////////////////////////////////////////////


    function updateTotals() {

        let totalAmount = 0;

        $('#productsTable tbody tr').each(function () {
            let quantity = parseFloat($(this).find('.itemQuantity').val()) || 0; 
            let unitPrice = parseFloat($(this).find('.unitPrice').val()) || 0; 
            let discountRatio = parseFloat($(this).find('.itemDescountRatio').val()) || 0; 

            // احتساب الإجمالي قبل الخصم
            let rowTotal = quantity * unitPrice;

            // احتساب قيمة الخصم
            let discountAmount = (rowTotal * discountRatio) / 100;
            $(this).find('.itemDescountAmount').val(discountAmount.toFixed(2));

            // احتساب الإجمالي بعد الخصم
            let netRowTotal = rowTotal - discountAmount;
            $(this).find('.SubTotal').val(netRowTotal.toFixed(2));

            // إضافة الإجمالي إلى المجموع الكلي
            totalAmount += netRowTotal;
        });

        // تحديث الإجمالي العام
        $('#totalAmount').val(totalAmount.toFixed(2));

        // تحديث المبلغ الصافي
        updateNetAmount();
    }


  ////////////////////////////////////////////////////////////////////////////////////////////////////
 //  تحديث الإجمالي العام بعد الخصم
///////////////////////////////////////////////////////////////////////////////////////////////////


    function updateNetAmount() {

        const totalAmount = parseFloat($('#totalAmount').val()) || 0;
        const discount = parseFloat($('#discount').val()) || 0;
        const PaidAmoint = parseFloat($('#PaidAmoint').val()) || 0;

        // احتساب المبلغ الصافي بعد الخصم العام
        const netAmount = totalAmount - ((totalAmount * discount) / 100);
        $('#netAmount').val(netAmount.toFixed(2));
     
        //  احتساب قيمة الخصم الكلي
        let discountAmountTotal = (totalAmount * discount) / 100;
        $('#DiscountAmount').val(discountAmountTotal.toFixed(2));
    }

  //////////////////////////////////////////////////////////////////////////////////////////////////////////
 //  الاحداث
/////////////////////////////////////////////////////////////////////////////////////////////////////////

    //  عند التعديل على نسبة الخصم الكلية 
    $('#discount').on('change', function (e) {
        updateNetAmount();
    });

    // عند كتابة المبلغ المدفوع 
    $('#PaidAmoint').on('change', function (e) {
        updateNetAmount();
    });

  //////////////////////////////////////////////////////////////////////////////////////////////////////////
 //  دالة تقوم بإضافة صف منتج جديد للفاتورة
/////////////////////////////////////////////////////////////////////////////////////////////////////////

    function addProductToInvoice() {

        let newRow = $(`
        <tr>
            <td><input type="text" class="form-control ProductID" placeholder="رقم الصنف"></td>

            <td>
                <select class="form-select productSelect ">
                    <option value="">اسم الصنف</option>
                </select>
            </td>

            <td>
                <select class="form-select unitName select2">
                    <option value="">اسم الوحدة</option>
                </select>
            </td>

            <td><input type="number" class="form-control unitPrice" placeholder="السعر"></td>
            <td><input type="number" class="form-control itemQuantity" value="0" placeholder="الكمية"></td>
            <td><input type="number" class="form-control itemDescountRatio" value="0" placeholder="نسبة الخصم"></td>
            <td style="display: none;"><input type="number" class="form-control itemDescountAmount" value="0.00" required ReadOnly></td>
            <td><input type="number" class="form-control SubTotal" value="0.00" required readonly></td>


       
            <td><button type="button" class="btn btn-label-danger removeProduct"><i class="bx bx-trash"></i></button></td>
        </tr>
    `);

        // إضافة الصف إلى الجدول
        $('#productsTable tbody').append(newRow);

        // تهيئة الحقول الجديدة باستخدام Select2
        newRow.find('.productSelect').select2({
            placeholder: "اختر عنصرًا",
            allowClear: true
        });

        // جلب بيانات المنتجات للـ productSelect
        fetchProducts(newRow.find('.productSelect'));

        // جلب بيانات الوحدات عند تغيير المنتج
        newRow.find('.productSelect').on('change', function () {
            let productId = $(this).val();
            newRow.find('.ProductID').val(productId);
            let unitSelect = newRow.find('.unitName');
            fetchUnits(productId, unitSelect);
        });
    }


    
  /////////////////////////////////////////////////////////////////////////////////////////////////////////////
 //    دالة تقوم بجلب بيانات المنتجات
/////////////////////////////////////////////////////////////////////////////////////////////////////////////

    function fetchProducts(selector) {
  
        $.ajax({
            url: "/POS?handler=GetProducts",
            method: "GET",
            success: function (products) {
               
                const productSelect = $(selector);
                productSelect.empty().append('<option value="">اسم المنتج</option>');
                products.forEach(product => {
                    productSelect.append(
                        `<option value="${product.id}">${product.className}</option>`
                    );
                });
            },
            error: function () {
                alert("حدث خطأ أثناء جلب المنتجات.");
            }
        });
    }

    
  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
 //    دالة تقوم بجلب بيانات الوحدات بناءً على المنتج المحدد
////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    function fetchUnits(productID, selector) {
        $.ajax({
            url: "/POS?handler=GetUnits",
            method: "GET",
            data: { productID: productID },
            success: function (data) {
                const unitSelect = $(selector);
                unitSelect.empty();

                data.forEach(unit => {
                    unitSelect.append(`
                    <option value="${unit.id}" data-price="${unit.partingPrice}" data-quantity="99" data-exchangeFactor="${unit.exchangeFactor}" >${unit.unitName}</option>
                `);
                });

                // تحديد أول عنصر
                const firstOption = unitSelect.find('option:first');
                if (firstOption.length) {
                    unitSelect.val(firstOption.val()).change(); // تعيين القيمة وتطبيق حدث change
                    let currentRow = selector.closest('tr'); // تحديد الصف الحالي
   
      
                    // تخزين الكمية المتاحة كخاصية مخصصة لحقل الكمية
                    currentRow.find('.itemQuantity').val(1).change();
                }
            },
            error: function () {
                alert("حدث خطأ أثناء جلب الوحدات.");
            }
        });
    }



  //////////////////////////////////////////////////////////////////
 //        // ربط زر إضافة المنتج بحدث إضافة صف جديد
////////////////////////////////////////////////////////////////

    $('#addProduct').on('click', function (e) {
        e.preventDefault();
        addProductToInvoice();
    });

  //////////////////////////////////////////////////////////////////
 //            // التعامل مع زر حذف الصف
////////////////////////////////////////////////////////////////


    $(document).on('click', '.removeProduct', function () {
        $(this).closest('tr').remove(); // حذف الصف الحالي
        updateTotals(); // تحديث الإجمالي بعد الحذف
        updateNetAmount();
    });

    //$('#addInv').on('click', function (e) {
    //    e.preventDefault();
    //    saveInvoice();
    //});
  

///////////////////////////////////////////////////////////////////////////////////////////////////////
//  //     حدث عند تغيير الوحدة في القائمة  
///////////////////////////////////////////////////////////////////////////////////////////////////////
     
        $(document).on('change', '.unitName', function () {

            let currentRow = $(this).closest('tr'); // تحديد الصف الحالي
            let selectedOption = $(this).find(':selected');
            let unitPrice = selectedOption.data('price');
            let unitQuantity = selectedOption.data('quantity');

            // وضع السعر في الحقل
            currentRow.find('.unitPrice').val(unitPrice || '');

            // تخزين الكمية المتاحة كخاصية مخصصة لحقل الكمية
            currentRow.find('.itemQuantity').data('max-quantity', unitQuantity || 0);
        });

 //////////////////////////////////////////////////////////////////////
//          //    حدث عند إدخال كمية 
//////////////////////////////////////////////////////////////////////

        $(document).on('input', '.itemQuantity', function () {

            let maxQuantity = $(this).data('max-quantity'); // قراءة الكمية المتاحة
            let enteredQuantity = $(this).val(); // الكمية المدخلة

            if (enteredQuantity > maxQuantity) {
                alert('الكمية المدخلة أكبر من المتاحة!');
                $(this).val(maxQuantity); // إعادة الكمية إلى الحد الأقصى
            }
            updateTotals();
            updateNetAmount();
        });



});
/////////////////////////////////////////////////////////////////////////////////////////////////////////
// دالة لجمع تفاصيل الفاتورة
/////////////////////////////////////////////////////////////////////////////////////////////////////////



/////////////////////////////////////////////////////////////////////////////////////////////////////////
// دالة لإرسال الفاتورة إلى الخادم
/////////////////////////////////////////////////////////////////////////////////////////////////////////
function collectInvoiceDetails() {
    let invoiceDetails = [];

    // المرور على كل صف في الجدول
    $('#productsTable tbody tr').each(function () {
        let row = $(this);
        let detail = {
            ClassID: row.find('.ProductID').val(), // رقم الصنف
            UnitID: row.find('.unitName').val(), // رقم الوحدة
            Quantity: parseFloat(row.find('.itemQuantity').val()) || 0, // الكمية
            UnitPrice: parseFloat(row.find('.unitPrice').val()) || 0, // السعر
            SubDescount: parseFloat(row.find('.itemDescountAmount').val()) || 0, // الخصم
            TotalAMount: parseFloat(row.find('.SubTotal').val()) || 0 // الإجمالي
        };

        invoiceDetails.push(detail); // إضافة التفاصيل إلى المصفوفة
    });

    return invoiceDetails;
}
$("#addInv").on("click", function () {
    // استخراج رمز AntiForgery من النموذج
    const antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    alert("sssss");
    let sPSellInvoice = {
        ID: -1,
        PeriodNumber: $('#PeriodNumber').val(),
        SalePointID: parseInt($('#SalePointID').val()),
        TheNumber: $('#TheNumber').val(),
        TheDate: $('#TheDate').val(),
        ThePay: $('#ThePay').val(),
        StoreID: parseInt($('#StoreID').val()),
        AccountID: parseInt($('#AccountID').val()),
        CustomerName: $('#CustomerName').val(),
        Notes: $('#Notes').val(),
        UserID: 1,
        Descount: parseFloat($('#DiscountAmount').val()) || 0,
        Debited: 1,
        PayAmount: parseFloat($('#PaidAmoint').val()) || 0,
        InvoiceDetails: collectInvoiceDetails() // جمع تفاصيل الفاتورة
    };
    // إرسال الطلب باستخدام jQuery.ajax
    $.ajax({
        url: "/POS?handler=Save", // تحقق من أن هذا المسار صحيح
        method: "POST",
        contentType: "application/json",
        headers: {
            "RequestVerificationToken": antiForgeryToken, // استخدام الرمز المستخرج
        },
        data: JSON.stringify(sPSellInvoice), // إرسال بيانات employeeData
        success: function (response) {
            $("#result").html(`<p>${response.message}</p>`); // عرض رسالة النجاح
        },
        error: function (xhr) {
            $("#result").html(`<p style="color: red;">Error: ${xhr.responseText}</p>`); // عرض رسالة الخطأ
        }
    });
});
//function saveInvoice() {
//    alert("sssss");
//    let invoice = {
//        ID: -1,
//        PeriodNumber: $('#PeriodNumber').val(),
//        SalePointID: parseInt($('#SalePointID').val()),
//        TheNumber: $('#TheNumber').val(),
//        TheDate: $('#TheDate').val(),
//        ThePay: $('#ThePay').val(),
//        StoreID: parseInt($('#StoreID').val()),
//        AccountID:1,
//        CustomerName: $('#CustomerName').val(),
//        Notes: $('#Notes').val(),
//        UserID: 1,
//        Descount: parseFloat($('#DiscountAmount').val()) || 0,
//        Debited: 1,
//        PayAmount: parseFloat($('#PaidAmoint').val()) || 0,
//        InvoiceDetails: collectInvoiceDetails() // جمع تفاصيل الفاتورة
//    };
//    alert("sdfsfdsf");
//    $.ajax({
//        url: "/POS?handler=Save", // يجب أن يكون الرابط صحيحًا
//        type: 'POST',
//        contentType: 'application/json',
////        data: JSON.stringify(invoice), // تحويل البيانات إلى JSON
//        data: { invoice: invoice },
//        success: function (response) {
//            if (response.success) {
//                alert(response.message);
//            } else {
//                alert(response.message);
//            }
//        },
//        error: function (xhr, status, error) {
//           // console.error(error);
//            alert('حدث خطأ أثناء حفظ الفاتورة');
//        }
//    });
//}



