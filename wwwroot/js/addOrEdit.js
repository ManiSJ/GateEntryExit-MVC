var selectedGateIds = [];
var selectedGateNames = [];

function showGateModal(event, url, title, isSingleSelection) {
    event.preventDefault();      
    if (isSingleSelection == true) {
        var selectedGateValue = $('#selectedGateValue').val();
        url = url + "&selectedGateValue=" + selectedGateValue;
    }
    else {
        var selectedGateValues = $('#selectedGateValues').val();
        url = url + "&selectedGateValues=" + selectedGateValues;
    }
    console.log('Url', url);
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#gate-modal .modal-body").html(res);
            $("#gate-modal .modal-title").html(title);
            $("#gate-modal").modal('show');
            selectedGateIds = [];
            selectedGateNames = [];
        },
        error: function (xhr, textStatus, error) {
            console.log("Xhr status code:", xhr.status);
            console.log("Xhr status text:", xhr.statusText);
            console.log("Text status:", textStatus);
            console.log("Error:", error);
        }
    })

    // Javascript http request

    //sendHttpRequest(url, 'GET', function (error, response) {
    //    if (error) {
    //        console.error('Error:', error.message);
    //    } else {
    //        console.log('Response:', response);
    //        $("#gate-modal .modal-body").html(res);
    //        $("#gate-modal .modal-title").html(title);
    //        $("#gate-modal").modal('show');
    //    }
    //});    
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

function handleGateMultipleSelection(event) {
    console.log('event', event);
    if (event.checked) {
        var selectedGateName = $('#gate-' + event.value).text();
        selectedGateIds.push(event.value);
        selectedGateNames.push(selectedGateName);
        $('#selectedGateValues').val(selectedGateIds);
        $('#selectedGateNames').html(selectedGateNames.join(', '));
    }
    else {
        selectedGateIds.pop(event.value);
        selectedGateNames.pop(selectedGateName);
        $('#selectedGateValues').val(selectedGateIds);
        $('#selectedGateNames').html(selectedGateNames.join(', '));
    }
}