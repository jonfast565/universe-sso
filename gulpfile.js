const { src, dest, series, parallel } = require('gulp');
var del = require('del');
var spawn = require('child_process').spawn;
var merge = require('merge-stream');

function preClean() {
    return del([
        './build/**/*',
    ]);
}

function rustClean() {
    return del([
        './backend/logs/**/*'
    ]);
}

function cargoBuild(cb) {
    spawn('cargo', ['build'], { cwd: './backend', stdio: 'inherit', env: process.env })
        .on('close', cb);
}

function rustCopy() {
    let executable = src('./backend/target/debug/*.exe')
        .pipe(dest('./build/'));
    let configs = src('./backend/Settings.toml')
        .pipe(dest('./build/'));
    return merge(executable, configs);
}

function dartClean() {
    return del([
        './frontend/build/**/*'
    ]);
}

function dartPullPackages(cb) {
    spawn('pub.bat', ['get'], { cwd: './frontend', stdio: 'inherit', env: process.env })
        .on('close', cb);    
}

function dartBuild(cb) {
    spawn('webdev.bat', ['build'], { cwd: './frontend', stdio: 'inherit', env: process.env })
        .on('close', cb);
}

function dartCopy() {
    return src('./frontend/build/**/*')
        .pipe(dest('./build/static/'));
}

function deleteDartBuild() {
    return del([
        './frontend/build/**/*'
    ]);
}

let rustBuildSteps = series(rustClean, cargoBuild, rustCopy);
let dartBuildSteps = series(dartPullPackages, dartClean, dartBuild, dartCopy);
exports.default = series(preClean, parallel(rustBuildSteps, dartBuildSteps), deleteDartBuild);