function registerResizeHandler(dotNetReference) {
    function resizeHandler() {
        if (window.innerWidth < 600) {
            dotNetReference.invokeMethodAsync('CloseMenu');
        }
    }

    window.addEventListener('resize', resizeHandler);

    // Return a cleanup function to be called when the .NET reference is disposed
    return {
        dispose: function () {
            window.removeEventListener('resize', resizeHandler);
        }
    };
}
