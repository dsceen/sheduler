var ArrOfTObj = [];

$.fn.statusUpdater = function () {
    this.each(function () {
        AddToPool($(this));
    });
};

function AddToPool(obj) {
    ArrOfTObj.push(obj);
}

function UpdateObj() {
    $.ajax({
        url: "/status",
        cache: false,
        success: function (data) {
            $.each(ArrOfTObj, function (key) {
                console.log("currunt state = " + data);
                if (data === "True")
                    $(ArrOfTObj[key]).html("Started");
                else
                    $(ArrOfTObj[key]).html("Stoped");
            });
        },
        error: function () {
            console.log("error: cant recive data");
        }
    });
}

setInterval(UpdateObj, 1000);

