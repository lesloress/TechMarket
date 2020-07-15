// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function addProductToCart(id) {
    $.post('/ShoppingCart/AddToCart', { productId: id });
}

async function deleteFromCart(id) {
    $.post('/ShoppingCart/DeleteFromCart', { cartItemId: id });
    var elem = document.getElementById(id);
    elem.parentNode.removeChild(elem);
}