// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//toggle class active
const navbarNav = document.querySelector("div.navbar-nav");

//ketika hamburger registration di click
document.querySelector("#hamburger-registration").onclick = () => {
    navbarNav.classList.toggle("active");
};

//klik diluar sidebar untuk menghilangkan nav

const hamburger = document.querySelector("#hamburger-registration");

document.addEventListener("click", (e) => {
    if (!hamburger.contains(e.target) && !navbarNav.contains(e.target)) {
        navbarNav.classList.remove("active");
    }
});
