$(document).ready(function () {
    // Password toggle functionality
    $('.password-toggle, .confirm-password-toggle').on('click', function () {
        const $inputField = $(this).hasClass('password-toggle')
            ? $('#password')
            : $('#confirmPassword');

        const type = $inputField.attr('type') === 'password' ? 'text' : 'password';
        $inputField.attr('type', type);

        // Toggle the eye icon
        const $eyeIcon = $(this).find('i');
        $eyeIcon.toggleClass('bi-eye bi-eye-slash');
    });

    // Form submission
    $('#signupForm').on('submit', function (e) {
        e.preventDefault();

        const password = $('#password').val();
        const confirmPassword = $('#confirmPassword').val();
        const $submitButton = $(this).find('button[type="submit"]');
        const $spinner = $submitButton.find('.spinner-border');

        if (password !== confirmPassword) {
            alert("Passwords don't match");
            return;
        }

        // Show loading state
        $submitButton.prop('disabled', true);
        $spinner.removeClass('d-none');
        $submitButton.contents().filter(function () {
            return this.nodeType === 3; // Node.TEXT_NODE
        }).remove(); // Remove text
        $submitButton.prepend($spinner).append(' Creating account...');

        // Simulate API call
        setTimeout(function () {
            console.log('Sign up attempt with:', {
                name: $('#name').val(),
                email: $('#email').val(),
                password: password
            });

            // Reset button state
            $submitButton.prop('disabled', false);
            $spinner.addClass('d-none');
            $submitButton.text('Create Account');

            // Redirect or show success (just for demo)
            // window.location.href = 'dashboard.html';
        }, 1500);
    });
});
