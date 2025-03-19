$(document).ready(function () {
    // Password toggle functionality
    $('.password-toggle').on('click', function () {
        const $passwordField = $('#password');
        const type = $passwordField.attr('type') === 'password' ? 'text' : 'password';
        $passwordField.attr('type', type);

        // Toggle the eye icon
        const $eyeIcon = $(this).find('i');
        $eyeIcon.toggleClass('bi-eye bi-eye-slash');
    });

    // Form submission
    $('#loginForm').on('submit', function (e) {
        e.preventDefault();

        const $submitButton = $(this).find('button[type="submit"]');
        const $spinner = $submitButton.find('.spinner-border');

        // Show loading state
        $submitButton.prop('disabled', true);
        $spinner.removeClass('d-none');
        $submitButton.contents().filter(function () {
            return this.nodeType === 3; // Node.TEXT_NODE
        }).remove(); // Remove text
        $submitButton.prepend($spinner).append(' Signing in...');

        // Simulate API call
        setTimeout(function () {
            console.log('Login attempt with:', {
                email: $('#email').val(),
                password: $('#password').val(),
                rememberMe: $('#rememberMe').is(':checked')
            });

            // Reset button state
            $submitButton.prop('disabled', false);
            $spinner.addClass('d-none');
            $submitButton.text('Sign In');
        }, 1500);
    });
});
