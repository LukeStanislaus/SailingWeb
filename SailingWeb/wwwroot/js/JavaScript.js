if ('serviceWorker' in navigator) {
    window.addEventListener('load', function () {
        navigator.serviceWorker.register('js/sw.js').then(function (registration) {
            // Registration was successful
            alert("success");
            console.log('ServiceWorker registration successful with scope: ', registration.scope);
        }, function (err) {
            // registration failed :(
            alert("fail ", err);
            console.log('ServiceWorker registration failed: ', err);
        });
    });
}