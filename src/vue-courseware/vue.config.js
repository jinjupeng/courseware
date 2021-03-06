
module.exports = {
    devServer: {
        disableHostCheck: true,
        open: true,
        port: 8000,
        proxy: {
            '/api': {
                target: 'http://localhost:5000',
                changeOrigin: true,
                pathRewrite: {
                    '/api': ''
                }
            }
        }
    },
    publicPath: './',
};