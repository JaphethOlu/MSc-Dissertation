const path = require("path");
const webpack = require("webpack");
const UglifyJsPlugin = require("uglifyjs-webpack-plugin");
const CleanWebpackPlugin = require("clean-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCssAssetsPlugin = require("optimize-css-assets-webpack-plugin");

let pathsToClean = [
    "./wwwroot/dist"
]

module.exports = {
    output: {
        path: path.join(__dirname, "wwwroot", "dist"),
        filename: "./js/bundle.min.js",
        publicPath: "/dist/"
    },
    entry: [ 
        "./ClientApp/src/index.js",
        "./ClientApp/src/styles/styles.scss"
    ],
    plugins: [
        new CleanWebpackPlugin(pathsToClean),
        new MiniCssExtractPlugin({
            filename: "./css/styles.min.css",
        })        
    ],
    mode: "production",
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: "babel-loader"
            },
            {
                test: /(\.css|\.scss|\.sass)$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader
                    },
                    {
                        loader: "css-loader",
                        options: { sourceMap: false }
                    },
                    {
                        loader: "postcss-loader",
                        options: {
                            plugins: () => [
                                require("autoprefixer")
                            ],
                            sourceMap: false
                        }
                    },
                    {
                        loader: "sass-loader",
                        options: { sourceMap: false }
                    }
                ]
            }
        ]
    },
    optimization: {
        minimizer: [
            new UglifyJsPlugin(),
            new OptimizeCssAssetsPlugin({})
        ]
    }
};