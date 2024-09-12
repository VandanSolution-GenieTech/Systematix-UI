
function ConvertCSV(nStr) {
    let x = String(nStr).split('.');
    let x1 = x[0];
    let x2 = x[1] || '0';

    let lastThree = x1.substring(x1.length - 3);
    let otherNumbers = x1.substring(0, x1.length - 3);

    if (otherNumbers !== '' && otherNumbers !== '-') {
        lastThree = ',' + lastThree;
    }

    let res = otherNumbers.replace(/\B(?=(\d{2})+(?!\d))/g, ",") + lastThree;
    return res + '.' + x2;
}

$(function () {
    $(".Comma").each(function (c, obj) {
        $(obj).text(ConvertCSV(parseFloat($(obj).text()).toFixed(2)));
    });

    $(".3decimal").each(function (c, obj) {
        $(obj).text(ConvertCSV(parseFloat($(obj).text()).toFixed(3)));
    });
});
