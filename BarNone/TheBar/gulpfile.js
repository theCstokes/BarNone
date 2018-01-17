const gulp = require('gulp');
const ts = require('gulp-typescript');
const clean = require('gulp-clean');
const sourcemaps = require('gulp-sourcemaps');
const gnf = require('gulp-npm-files');

const less = require('gulp-less');
const lessGlob = require('gulp-less-glob');
const mocha = require('gulp-mocha');

// Pull in the project TypeScript config.
const test_typescript = ts.createProject('test.tsconfig.json');
const release_typescript = ts.createProject('tsconfig.json');

let typescript;

gulp.task('clean-scripts', function () {
    return gulp.src(getDest(), { read: false })
        .pipe(clean({ force: true }));
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
        .src('src/UEye/Theme/main.less')
        .pipe(lessGlob())
        .pipe(less())
        .pipe(gulp.dest(getDest('Theme')));
});

gulp.task('default', ['build', "gnf", "res", "libs", "index"]);

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

gulp.task('gnf', () => {
    gulp
        .src(gnf(true, './package.json'), { base: './' })
        .pipe(gulp.dest(outputLocation));
});

gulp.task('res', () => {
    return gulp
        .src('./res/**/*')
        .pipe(gulp.dest(outputLocation + "/res"));
});

gulp.task('libs', () => {
    return gulp
        .src('./libs/**/*')
        .pipe(gulp.dest(outputLocation + "/libs"));
});

gulp.task('index', () => {
    return gulp
        .src('./index.html')
        .pipe(gulp.dest(outputLocation));
});

var destType = "";
var outputLocation = "../TheRack/BarNone.TheRack.ResourceServer.API/wwwroot/UI";

var setDest = function (path) {
    destType = path;
};

var getDest = function (path) {
    if (path === undefined) {
        return outputLocation + "/build/" + destType;
    }
    return outputLocation + "/build/" + destType + "/" + path;
};