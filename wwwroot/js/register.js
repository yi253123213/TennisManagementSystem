document.addEventListener('DOMContentLoaded', function () {
    const userRoleSelect = document.getElementById('userRoleSelect');
    const coachOnlyFields = document.querySelectorAll('.coach-only');

    // Initial check
    toggleCoachFields();

    // Add change event listener
    userRoleSelect.addEventListener('change', toggleCoachFields);

    function toggleCoachFields() {
        const selectedRole = userRoleSelect.value;
        coachOnlyFields.forEach(field => {
            field.style.display = selectedRole === 'Coach' ? 'block' : 'none';
        });
    }
});

