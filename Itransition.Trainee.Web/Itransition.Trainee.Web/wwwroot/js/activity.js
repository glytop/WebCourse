$(document).ready(function () {
    $('#selectAll').click(function () {
        $('.user-select').prop('checked', this.checked);
    });
});