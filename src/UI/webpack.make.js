var path = require('path');
var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = function make(env) {
    return {
        devtool: 'source-map',
        entry: {
            'polyfills': './root/polyfills.ts',
            'vendor': './root/vendor.ts',
            'app': './root/main.ts'
        },
        output: {
            path: path.resolve(__dirname, 'wwwroot'),
            filename: isProd(env) ? 'js/[name].[hash].js' : 'js/[name].js',
            sourceMapFilename: '[file].map'
        },
        resolve: {
            extensions: ['.ts', '.js', '.scss', '.css', '.json', '.html']
        },
        module: {
            rules: [
                {
                    enforce: 'pre',
                    test: /\.ts$/,
                    use: ['tslint-loader', 'source-map-loader'],
                    exclude: /node_modules/
                },
                {
                    test: /\.ts$/,
                    use: 'awesome-typescript-loader',
                    exclude: /node_modules/
                },
                {
                    test: /\.(css|scss)$/,
                    use: ['to-string-loader', 'css-loader', 'postcss-loader', 'sass-loader']
                },
                {
                    test: /\.html$/,
                    use: ['raw-loader']
                },
                { 
                    test: /\.woff2?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                    use: 'url-loader?limit=10000'
                },
                { 
                    test: /\.(ttf|eot|svg)(\?[\s\S]+)?$/,
                    use: 'file-loader' 
                },
                {
                    test: /bootstrap-sass\/assets\/javascripts\//, 
                    use: 'imports-loader' 
                }
            ]
        },
        plugins: getPlugins(env)
    };
}

function getPlugins(env) {
    var plugins = [
        new webpack.NoEmitOnErrorsPlugin(),
        new webpack.ProvidePlugin({
            '$': 'jquery',
            'jQuery': 'jquery',
            'jquery': 'jquery',
            'window.jQuery': 'jquery'
        }),
        new webpack.DefinePlugin({
            'process.env': {
                'ENV': JSON.stringify(env),
                'NODE_ENV': JSON.stringify(isProd(env) ? 'production' : 'dev')
            }
        }),
        new HtmlWebpackPlugin({
            template: './root/index.html',
            filename: 'index.html',
            inject: 'body'
        })
    ];

    if(isProd(env)) {
        plugins.push(new webpack.optimize.UglifyJsPlugin());
        plugins.push(new webapck.optimize.CommonsChunkPlugin({
            name: ['app', 'vendor', 'polyfills']
        }))
    }

    return plugins;
}

function isProd(env) {
    return env === 'prod';
}