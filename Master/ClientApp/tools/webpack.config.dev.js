const path = require("path");
const webpack = require("webpack");
const HtmlWebPackPlugin = require("html-webpack-plugin");

module.exports = {
    entry: [ 
        "./ClientApp/src/index.js",
        "./ClientApp/src/styles/styles.scss",
        "webpack-hot-middleware/client?reload=true"
    ],
    output: {
        path: path.resolve(__dirname, "../../wwwroot/build"),
        filename: "./js/bundle.js"
    },
    mode: "development",
    devtool: "source-map",
    plugins: [
        new HtmlWebPackPlugin({
            title: "Contractor Job Board Aggregator",
            template: "./ClientApp/src/index.html"
        }),
        new webpack.HotModuleReplacementPlugin()   
    ],
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
                        loader: "style-loader",
                        options: { sourceMap: true }
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