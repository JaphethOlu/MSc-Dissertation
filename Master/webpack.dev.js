const path = require("path");
const merge = require("webpack-merge");
const common = require("./webpack.common");
const dist = "./wwwroot/dist";

module.exports = merge(common, {
    mode: "development",
    devtool: "source-map",
    output: {
        path: path.resolve(__dirname, dist),
        filename: "./js/bundle.js"
    }/*,
    watch: true
    */
});