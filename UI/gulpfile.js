const gulp = require('gulp');
const ts = require('gulp-typescript');
const clean = require('gulp-clean');
const sourcemaps = require('gulp-sourcemaps');

const less = require('gulp-less');
const lessGlob = require('gulp-less-glob');

// const JSON_FILES = ['src/Data/**/*.json'];

const BUILD_DIR = "build";

// Pull in the project TypeScript config.
const typescript = ts.createProject('tsconfig.json');

gulp.task('clean-scripts', function () {
    return gulp.src(BUILD_DIR, { read: false })
        .pipe(clean());
});

gulp.task('scripts', ['styles'], () => {
    return typescript.src()
        .pipe(sourcemaps.init())
        .pipe(typescript())
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(BUILD_DIR));
});

// gulp.task('assets', ['clean-scripts'], () => {
//     return gulp.src(JSON_FILES)
//         .pipe(gulp.dest('dist/Data'));
// });

gulp.task('styles', ['clean-scripts'], () => {
    return gulp
        .src('src/UEye/Theme/main.less')
        .pipe(lessGlob())
        .pipe(less())
        .pipe(gulp.dest('build/Theme'));
});

gulp.task('default', ['scripts', 'styles']);

gulp.task('build', ['scripts']);

// gulp.task('test', ['build'], () => {
//     return gulp
//         .src(['dist/Spec/**/*.spec.js'], { read: false })
//         .pipe(mocha({ reporter: 'spec' }))
//         .once('error', () => {
//             process.exit(1);
//         })
//         .once('end', () => {
//             process.exit();
//         });
// });