const express = require("express");
const app = express();

const webpack = require("webpack");
const webpackDevMiddleware = require("webpack-dev-middleware");
const webpackHotMiddleware = require("webpack-hot-middleware")

const config = require("./webpack.config.dev");
const compiler = webpack(config);

const path = require("path");
const port = process.env.PORT || 8080;

app.use(webpackDevMiddleware(compiler));
app.use(webpackHotMiddleware(compiler));

app.use(express.static(path.join(__dirname, "./ClientApp/src")));

app.get("/", (req, res) => {
    res.sendFile(path.join(__dirname, "./ClientApp/src/index.html"));
});

/*eslint-disable no-console*/
app.listen(port, () => {
    console.log("Development server is listening on port: " + port);
});
