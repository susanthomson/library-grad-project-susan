module.exports = {
  entry: ['./public/src/app.js'],
  output: {
    path: "./public",
    filename: 'bundle.js'
  },
  externals: {
    'cheerio': 'window',
    'react/lib/ExecutionEnvironment': true,
    'react/lib/ReactContext': true,
  },
  module: {
    loaders: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loader: 'babel',
        query: { presets: [ 'airbnb', 'es2015', 'react' ] }
      }
    ]
  },
};