function hideLoadingBarAndShowApp() {
    var loadingBar = document.querySelector('.loading-bar-container'); // Adjust the selector as necessary
    loadingBar.style.display = 'none';

    var app = document.getElementById('app');
    app.style.display = 'block';
    setTimeout(function () {
        app.style.opacity = '1';
    }, 50); // Delay slightly for a smooth fade-in effect
}
function smoothTransitionToEndOfLoading() {
    requestAnimationFrame(() => {
        // Gradually finish the loading bar animation here

        // After a short delay, hide the loading bar and show the app
        setTimeout(() => {
            hideLoadingBarAndShowApp();
        }, 500);  // Delay of 500ms; adjust as needed for smooth transition
    });
}
