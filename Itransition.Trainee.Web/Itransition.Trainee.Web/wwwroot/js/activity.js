$(document).ready(function () {
    // Select all functionality
    $("#selectAll").change(function () {
        $(".user-select").prop('checked', $(this).prop('checked'));
        updateButtonState();
    });

    // Individual checkbox change
    $(".user-select").change(function () {
        updateButtonState();

        // Update "select all" checkbox
        const totalCheckboxes = $(".user-select").length;
        const checkedCheckboxes = $(".user-select:checked").length;
        $("#selectAll").prop('checked', totalCheckboxes === checkedCheckboxes);
    });

    // Show timestamp popup on hover
    $("tr:has(.timestamp-popup)").hover(function () {
        $(this).find(".timestamp-popup").addClass("show");
    }, function () {
        $(this).find(".timestamp-popup").removeClass("show");
    });

    // Filter functionality
    $("#filterInput").on("keyup", function () {
        const value = $(this).val().toLowerCase();
        $("tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    // Update button state based on selection
    function updateButtonState() {
        const hasSelection = $(".user-select:checked").length > 0;
        $("#blockBtn, #pinBtn, #deleteBtn").prop('disabled', !hasSelection);
    }

    // Initial button state
    updateButtonState();

    // Button click handlers
    $("#blockBtn").click(function () {
        alert("Block action triggered for selected users");
    });

    $("#pinBtn").click(function () {
        alert("Pin action triggered for selected users");
    });

    $("#deleteBtn").click(function () {
        if (confirm("Are you sure you want to delete the selected users?")) {
            $(".user-select:checked").each(function () {
                $(this).closest("tr").fadeOut(300, function () {
                    $(this).remove();
                });
            });
        }
    });
});