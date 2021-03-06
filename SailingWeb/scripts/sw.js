﻿self.addEventListener('install', function (event) {
    var CACHE_NAME = 'my-site-cache-v1';
    var urlsToCache = [
        '/site.js'
    ];

    self.addEventListener('install', function (event) {
        // Perform install steps
        event.waitUntil(
            caches.open(CACHE_NAME)
                .then(function (cache) {
                    alert("opened");
                    console.log('Opened cache');
                    return cache.addAll(urlsToCache);
                })
        );
    });

});