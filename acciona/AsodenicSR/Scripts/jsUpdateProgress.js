//<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
//    <Scripts>
//        <asp:ScriptReference Path="~/Scripts/jsUpdateProgress.js" />
//    </Scripts>
//</asp:ToolkitScriptManager>



jQuery.browser = {};
jQuery.browser.mozilla = /mozilla/.test(navigator.userAgent.toLowerCase()) && !/webkit/.test(navigator.userAgent.toLowerCase());
jQuery.browser.webkit = /webkit/.test(navigator.userAgent.toLowerCase());
jQuery.browser.opera = /opera/.test(navigator.userAgent.toLowerCase());
jQuery.browser.msie = /msie/.test(navigator.userAgent.toLowerCase());

var prm = Sys.WebForms.PageRequestManager.getInstance();
//prm.add_beginRequest(beginReq);
prm.add_initializeRequest(beginReq);
prm.add_endRequest(endReq);

function beginReq(sender, args) {
    // shows the Popup
    if (ModalProgress != '') {
        $find(ModalProgress).show();
    }
}

function endReq(sender, args) {
    //  shows the Popup
    if (ModalProgress != '') {
        $find(ModalProgress).hide();
    }
}

//function AbortPostBack() {
//    if (prm.get_isInAsyncPostBack()) {
//        endReq();
//        prm.abortPostBack();
//        document.forms[0].submit();
//    }
//}

$(document).bind('keydown keyup', function (e) {
    if (e.which === 116) {
        console.log('blocked');
        return false;
    }
    if (e.which === 82 && e.ctrlKey) {
        console.log('blocked');
        return false;
    }
    return true;
});

var ModalProgress = '';
var heightVentana;
//alert('maximo height=' + screen.availHeight + '\nmaximo width=' + screen.availWidth + '\nOuter height=' + window.outerHeight + '\nOuter Width=' + window.outerWidth + '\ninner height=' + window.innerHeight + '\ninner Width=' + window.innerWidth + '\nResolucion de pantalla height=' + screen.height + '\nResolucion de pantalla width=' + screen.width);
if (window.innerHeight) {
    //navegadores basados en mozilla
    heightVentana = window.innerHeight;
} else {
    //navegadores basados en IExplorer 
    heightVentana = document.documentElement.clientHeight;
}


String.prototype.replaceAll = function(find, replace) {
    return this.replace(new RegExp(find, 'g'), replace);
};


// Extend the default Number object with a formatMoney() method:
// usage: someVar.formatMoney(decimalPlaces, symbol, thousandsSeparator, decimalSeparator)
// defaults: (2, "$", ",", ".")
Number.prototype.formatMoney = function (places, symbol, thousand, decimal) {
    places = !isNaN(places = Math.abs(places)) ? places : 2;
    symbol = symbol !== undefined ? symbol : "C$";
    thousand = thousand || ",";
    decimal = decimal || ".";
    var number = this,
	    negative = number < 0 ? "-" : "",
	    i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
	    j = (j = i.length) > 3 ? j % 3 : 0;
    return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
};


//jQuery(document).ready(function () {
//    $(document).on('submit', 'form', function (e) {
//        ShowProgressAnimation();
//    });
//});

jQuery(document).ready(function (e) {
//    $(document).delegate($('input.btnProcesando'), 'click', SetProcesando);
    var btn = $('.btnProcesando');
    if (btn.length != 0) {
        for (var i = 0; i < btn.length; i++) {
            //            AddOptions(btn[i], 'onclick');
            //AddEvent(btn[i], 'onclick', 'ControlEvent = this;');
            $(btn[i]).on('click', SetProcesando);
        }
    }
    //    window.setTimeout("$('.btnProcesando').click(function () { return ShowProgressAnimation(this); });", 250);

});

var SetProcesando = function(e) {
    ControlEvent = this;
};

$(document).on('submit', 'form', function (e) {
    //    var srcButton = window.event.srcElement;
        return ShowProgressAnimation();
});

function ShowProgressAnimation() {
    var obj = ControlEvent;
    if (obj != null && obj.PostBackOptions != null) {
        if (obj.PostBackOptions.validation) {
            if (typeof (Page_ClientValidate) == 'function' && !Page_IsValid) {
                return false;
            }
        }
    } else if (obj != null && typeof (obj.noty) == "object") {
        return false;
    }
    else if (obj == null) {
        return true;
    }
    if (typeof (Page_IsValid) != "undefined" && (obj.title == "" ? (obj.value != "Cancelar") : (obj.title != "Cancelar")))
        if (!Page_IsValid) return false;

    showProcesando();
    
    return true;
}

function showProcesando() {
    $('#modalBackground').delay(5).height(heightVentana).width(screen.availWidth).css('opacity', 0).show().animate({ opacity: 0.6 }, 100);
}

function hideProcesando() {
    $('#modalBackground').hide(100);
}

var ControlEvent = null;

function AddEvent(control, eventType, functionPrefix) {
    var ev = control[eventType];
    if (typeof (ev) == "function") {
        ev = ev.toString();
        ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
        try {
            eval('control.PostBackOptions = ' + ev.substring(ev.indexOf("(") + 1, ev.lastIndexOf(")")));
        } catch (e) {
            try {
                eval('control.PostBackOptions = ' + ev.substring(ev.indexOf("DoPostBackWithOptions(") + 22, ev.lastIndexOf(")")));
            } catch (e) {

            }
        }
        //        
        
    }
    else {
        ev = "";
    }
    control[eventType] = new Function("event", ev + " " + functionPrefix);
}

//function AddOptions(control, eventType) {
//    var ev = control[eventType];
//    if (typeof (ev) == "function") {
//        ev = ev.toString();
//        ev = ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}"));
//        eval('control.PostBackOptions = ' + ev.substring(ev.indexOf("(") + 1, ev.lastIndexOf(")")))
//    }
//    else {
//        ev = "";
//    }
//}

//jQuery(document).ready(function (e) {
//    var btn = $('.btnProcesando');
//    if (btn.length != 0) {
//        for (var i = 0; i < btn.length; i++) {
//            AddOptions(btn[i], 'onclick');
////            AddEvent(btn[i], 'onclick', 'javascript:return ShowProgressAnimation(this);')
//        }
//    }
////    window.setTimeout("$('.btnProcesando').click(function () { return ShowProgressAnimation(this); });", 250);
//   
//});

//function ShowProgressAnimation(obj) {
//    if (obj.PostBackOptions != null) {
//        if (obj.PostBackOptions.validation) {
//            if (typeof (Page_ClientValidate) == 'function' && !Page_IsValid) {
//                return false;
//            }
//        }
//    } else if (typeof (obj.noty) == "object") {
//        return false;
//    }
//    $('#modalBackground').delay(5).height(heightVentana).width(screen.availWidth).css('opacity', 0).show().animate({ opacity: 0.6 }, 100);
//    return true;
//}

//AddEvent($('form')[0], 'onsubmit', 'javascript:return false;');
////window.onload = function () {
////    for (var i = 0; i < document.forms.length; i++) {
////        (function (p) {
////            var form = document.forms[i];
////            var originFn = form.submit;
////            form.submit = function () {
////                //do something you like
////                alert("submitting " + form.id + " using submit method !");
//////                originFn();
////            }
////            form.onsubmit = function () {
////                alert("submitting " + form.id + " with onsubmit event !");
////            }
////        })(i);


////    }
////}