// wwwroot/js/custom.js
window.getNavbarHeight = () => {
    const navbar = document.querySelector('.top-nav'); 
    return navbar ? navbar.offsetHeight : 0;
};
