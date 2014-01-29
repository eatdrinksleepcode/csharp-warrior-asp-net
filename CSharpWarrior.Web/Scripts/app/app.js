define([
      'angular',
      'angular-route',
      './controllers/index'
  ], function (ng) {
         'use strict';
      
          return ng.module('app', [
              'app.controllers',
              'ngRoute'
     ]);
      });
