$(document).ready(function () {
    // Form submission
    const $forgotPasswordForm = $('#forgotPasswordForm');
    const $submitButton = $forgotPasswordForm.find('button[type="submit"]');
    const $spinner = $submitButton.find('.spinner-border');
    const $successMessage = $('#successMessage');
    const $emailSent = $('#emailSent');

    $forgotPasswordForm.on('submit', function (e) {
        e.preventDefault();

        const email = $('#email').val();

        // Show loading state
        $submitButton.prop('disabled', true);
        $spinner.removeClass('d-none');
        $submitButton.contents().filter(function () {
            return this.nodeType === 3; // Node.TEXT_NODE
        }).remove(); // Remove text
        $submitButton.prepend($spinner).append(' Sending email...');

        // Simulate API call
        setTimeout(function () {
            console.log('Password reset requested for:', email);

            // Hide form and show success message
            $forgotPasswordForm.addClass('d-none');
            $emailSent.text(email);
            $successMessage.removeClass('d-none');

        }, 1500);
    });
});
