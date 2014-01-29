require.config({

    paths: {
        'angular': '../angular',
        'angular-route': '../angular-route',
        'domReady': '../domReady'
    },

    shim: {
        'angular': {
            exports: 'angular'
        },
        'angular-route': {
            deps: ['angular']
        }
    },

    deps: [
        './bootstrap'
    ]
});