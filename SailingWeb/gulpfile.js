﻿/// <binding AfterBuild='default' Clean='clean' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var del = require('del');
var browserify = require('browserify')
var fs = require("fs");

var paths = {
    scripts: ['scripts/managerace.js', 'scripts/managerace.ts', 'scripts/managerace.js.map']
};

gulp.task('clean', function () {
    return del(['wwwroot/js/**/*']);
});

gulp.task('default', function () {
    gulp.src(paths.scripts).pipe(gulp.dest('./wwwroot/js'));
    browserify('wwwroot/js/managerace.js', {
    })
        .bundle()
        //.pipe(gulp.dest("wwwroot/js"));
        .pipe(fs.createWriteStream("./wwwroot/js/bundle.js"));
});