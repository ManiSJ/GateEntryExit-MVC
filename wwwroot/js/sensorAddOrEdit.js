function handleGateSelection(event) {
    var selectedGateValue = $('input[type="radio"]:checked').val();
    var selectedGateName = $('#gate-' + selectedGateValue).text();
    $('#selectedGateValue').val(selectedGateValue);
    $('#selectedGateName').val(selectedGateName);
}