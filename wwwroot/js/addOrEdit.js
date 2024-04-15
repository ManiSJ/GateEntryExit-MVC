var selectedGateIds = [];
var selectedGateNames = [];

function showGateModal(event, url, title, isSingleSelection, fromPagination) {
    event.preventDefault();      
    if (isSingleSelection == true) {
        var selectedGateValue = $('#selectedGateValue').val();
        url = url + "&selectedGateValue=" + selectedGateValue;

        $.ajax({
            type: "POST",
            url: url,
            success: function (res) {
                $("#gate-modal .modal-body").html(res);
                $("#gate-modal .modal-title").html(title);
                $("#gate-modal").modal('show');
            },
            error: function (xhr, textStatus, error) {
                console.log("Xhr status code:", xhr.status);
                console.log("Xhr status text:", xhr.statusText);
                console.log("Text status:", textStatus);
                console.log("Error:", error);
            }
        })
    }
    else {
        if (fromPagination == true) {
            var selectedGateIds = $('#selectedGateValues').val();
            var guidStrings = selectedGateIds.split(',');
            selectedGateIds = guidStrings.map(function (guidString) {
                return guidString.trim();
            });
        }
        else {
            selectedGateIds = [];
            selectedGateNames = [];
            $('#selectedGateValues').val('');
            $('#selectedGateNames').html("Selected gates - ");
        }

        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(selectedGateIds),
            contentType: 'application/json',
            success: function (res) {
                $("#gate-modal .modal-body").html(res);
                $("#gate-modal .modal-title").html(title);
                $("#gate-modal").modal('show');
            },
            error: function (xhr, textStatus, error) {
                console.log("Xhr status code:", xhr.status);
                console.log("Xhr status text:", xhr.statusText);
                console.log("Text status:", textStatus);
                console.log("Error:", error);
            }
        })
    }

    // Javascript http request

    // sendHttpRequest(url, 'GET', function (error, response) {
    //    if (error) {
    //        console.error('Error:', error.message);
    //    } else {
    //        console.log('Response:', response);
    //        $("#gate-modal .modal-body").html(res);
    //        $("#gate-modal .modal-title").html(title);
    //        $("#gate-modal").modal('show');
    //    }
    // });    
}

function sendHttpRequest(url, method, callback) {
    var xhr = new XMLHttpRequest();
    xhr.open(method, url);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status >= 200 && xhr.status < 300) {
                // Request was successful
                callback(null, xhr.responseText);
            } else {
                // Request failed
                callback(new Error('HTTP request failed with status ' + xhr.status));
            }
        }
    };
    xhr.onerror = function () {
        // Request failed due to network error
        callback(new Error('Network error'));
    };
    xhr.send();
}

function handleGateSingleSelection(event) {
    var selectedGateValue = $('input[type="radio"]:checked').val();
    var selectedGateName = $('#gate-' + selectedGateValue).text();
    $('#selectedGateValue').val(selectedGateValue);
    $('#selectedGateName').val(selectedGateName);
    $("#gate-modal").modal('hide');
}

function handleGateMultipleSelection(event, url) {
    if (event.checked) {
        var selectedGateName = $('#gate-' + event.value).text();
        selectedGateIds.push(event.value);
        selectedGateNames.push(selectedGateName);
        $('#selectedGateValues').val(selectedGateIds.join(','));
        $('#selectedGateNames').html("Selected gates - " + selectedGateNames.join(', '));
    }
    else {
        selectedGateIds.pop(event.value);
        selectedGateNames.pop(selectedGateName);
        $('#selectedGateValues').val(selectedGateIds.join(','));
        $('#selectedGateNames').html("Selected gates - " + selectedGateNames.join(', '));
    }
}