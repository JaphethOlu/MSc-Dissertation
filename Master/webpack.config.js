const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CleanWebpackPlugin = require("clean-webpack-plugin");

let pathsToClean = [
    "./wwwroot/build"
];

module.exports = {
    output: {
        path: path.join(__dirname, "wwwroot", "build"),
        filename: "./js/bundle.js",
        publicPath: "/build/"
    },
    mode: "development",
    entry: {
        app: "./ClientApp/src/index.js",
        styles: "./ClientApp/src/styles/styles.scss"
    },
    plugins: [
        new CleanWebpackPlugin(pathsToClean),
        new webpack.HotModuleReplacementPlugin(),
        new MiniCssExtractPlugin({
            filename: "./css/styles.css",
        })
    ],
    devtool: "source-map",
    watch: true,
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
                        options: { sourceMap: true }
                    },
                    {
                        loader: "postcss-loader",
                        options: {
                            plugins: () => [
                                require("autoprefixer")
                            ],
                            sourceMap: true
                        }
                    },
                    {
                        loader: "sass-loader",
                        options: { sourceMap: true }
                    }
                ]
            }
        ]
    }
};