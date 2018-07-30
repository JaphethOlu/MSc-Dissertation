const webpack = require("webpack");
const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const dist = "./wwwroot/dist";

module.exports = {
    output: {
        path: path.resolve(__dirname, dist),
        filename: "./js/bundle.js",
        publicPath: dist
    },
    mode: "development",
    entry: [ 
        "./ClientApp/src/index.js",
        "./ClientApp/src/styles/styles.scss"
    ],
    plugins: [
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