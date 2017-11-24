const gulp = require('gulp');
const ts = require('gulp-typescript');
const clean = require('gulp-clean');
const sourcemaps = require('gulp-sourcemaps');

const less = require('gulp-less');
const lessGlob = require('gulp-less-glob');
const mocha = require('gulp-mocha');

// Pull in the project TypeScript config.
const test_typescript = ts.createProject('test.tsconfig.json');
const release_typescript = ts.createProject('tsconfig.json');

let typescript;

gulp.task('clean-scripts', function () {
    return gulp.src(getDest(), { read: false })
        .pipe(clean());
});

gulp.task('scripts', ['styles'], () => {
    return typescript.src()
        .pipe(sourcemaps.init())
        .pipe(typescript())
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(getDest()));
});

gulp.task('styles', ['clean-scripts'], () => {
    return gulp
        .src('src/Vee/Theme/main.less')
        .pipe(lessGlob())
        .pipe(less())
        .pipe(gulp.dest(getDest('Theme')));
});

gulp.task('default', ['build']);

gulp.task('build', ['releaseSetup', 'scripts']);

gulp.task('test', ['testSetup', 'scripts'], () => {
    return gulp
        .src([getDest('Test/**/*.test.js')], { read: false })
        .pipe(mocha({
            reporter: 'spec',
            require: ['./src/testConfig.js']
        }))
        .once('error', () => {
            process.exit(1);
        })
        .once('end', () => {
            process.exit();
        });
});

/**
 * We need to change the ts target for test.
 */
gulp.task('testSetup', () => {
    typescript = test_typescript;
    setDest("test");
});

/**
 * We need to change the ts target for release.
 */
gulp.task('releaseSetup', () => {
    typescript = release_typescript;
    setDest("release");
});

var destType = "";

var setDest = function (path) {
    destType = path;
};

var getDest = function (path) {
    if (path === undefined) {
        return "build/" + destType;
    }
    return "build/" + destType + "/" + path;
};