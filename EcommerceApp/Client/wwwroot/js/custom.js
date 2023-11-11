document.addEventListener('DOMContentLoaded', (event) => {
    let allowScroll = true; // This flag will determine if the scroll should be processed

    console.log("DOMContentLoaded event fired, defining scrollInterop");
    window.scrollInterop = {
        registerScrollEvent: function (dotNetRef) {
            console.log("registerScrollEvent called");
            window.addEventListener("wheel", (e) => {
                if (allowScroll) {
                    if (e.deltaY < 0) {
                        // Scrolldown
                        dotNetRef.invokeMethodAsync("PrevItem");
                    } else if (e.deltaY > 0) {
                        // Scroll down
                        dotNetRef.invokeMethodAsync("NextItem");
                    }
                    // Immediately set allowScroll to false to block further scrolls
                    allowScroll = false;

                    // Set a timeout to re-allow scrolling after 1 second
                    setTimeout(() => {
                        allowScroll = true;
                    }, 1000); // Delay in milliseconds
                }
                 // If allowScroll is false, do nothing
            });
        }
    };

    window.getNavbarHeight = () => {
        const navbar = document.querySelector('.top-nav');
        return navbar ? navbar.offsetHeight : 0;
    };
    // Zoom effect for product images
    window.applyImageZoomEffect = function (selector) {
        const img = document.querySelector(selector);

        if (img) {
            img.addEventListener('mousemove', (e) => {
                const { left, top, width, height } = img.getBoundingClientRect();
                const x = ((e.clientX - left) / width) * 100;
                const y = ((e.clientY - top) / height) * 100;
                img.style.transformOrigin = `${x}% ${y}%`;
                img.style.transform = 'scale(2)';
            });

            img.addEventListener('mouseleave', (e) => {
                img.style.transformOrigin = 'center center';
                img.style.transform = 'scale(1)';
            });
        }
    };
});
