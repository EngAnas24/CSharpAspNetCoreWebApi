$(document).ready(function () {
    // Fetch categories and populate dropdown
    $.ajax({
        url: 'https://localhost:44368/api/category', // Adjust the URL as needed
        type: 'GET',
        success: function (data) {
            var dropdown = $('#categoriesDropdown');
            data.forEach(function (category) {
                dropdown.append($('<option></option>').attr('value', category.id).text(category.name));
            });
        },
        error: function (error) {
            console.error('Error fetching categories', error);
        }
    });

    // Handle form submission
    $('#itemForm').submit(function (event) {
        event.preventDefault();

        var formData = new FormData(this);

        // Add the selected category to the form data
        var selectedCategoryId = $('#categoriesDropdown').val();
        formData.append('Category', selectedCategoryId);

        $.ajax({
            url: 'https://localhost:44368/api/item', // Adjust the URL as needed
            type: 'POST',
            processData: false,
            contentType: false,
            data: formData,
            success: function (data) {
                alert('Item created successfully!');
                $('#itemForm')[0].reset();
            },
            error: function (error) {
                console.error('Error creating item', error);
            }
        });
    });
});
