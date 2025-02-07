const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = ({ production: isProduction }) => {
	return {
		context: path.resolve(__dirname, "./"),
		cache: true,
		target: ['web', 'es6'],
		mode: 'development',
		devtool: isProduction ? false : 'eval-cheap-module-source-map',
		entry: {
			app: {
				import: ['./src/index.tsx'],
				dependOn: ['vendor']
			},
			vendor: ['react', 'react-dom', 'leaflet', 'react-leaflet']
		},
		watchOptions: {
			ignored: ['**/node_modules'],
			aggregateTimeout: 200,
			poll: false
		},
		resolve: {
			modules: ['node_modules', path.resolve(__dirname, "src")],
			extensions: ['.tsx', '.ts', 'jsx', '.js', '.json', '.css', '.less', '.svg'],
			alias: {
				"@": path.resolve(__dirname, 'src/')
			}
		},
		output: {
			path: path.resolve(__dirname, 'dist'),
			filename: '[name].[contenthash].bundle.js',
			chunkFilename: '[name].[contenthash].chunk.js',
			clean: true
		},
		module: {
			rules: [
				{
					test: /\.svg$/,
					type: 'asset/source',
				},
				{
					test: /\.(ts|tsx)$/,
					exclude: /node_modules/,
					include: path.resolve(__dirname, 'src'),
					use: [
						{
							loader: "ts-loader",
							options: {
								transpileOnly: true
							}
						}
					]
				},
				{
					test: /\.less$|css$/,
					use: [
						{
							loader: isProduction ? MiniCssExtractPlugin.loader : 'style-loader'
						},
						{
							loader: 'css-loader',
							options: {
								importLoaders: 1,
								modules: false
							}
						},
						{
							loader: 'less-loader',
						}
					]
				}
			]
		},
		plugins: [
			isProduction && new MiniCssExtractPlugin(),
			new ForkTsCheckerWebpackPlugin({
				typescript: {
					diagnosticOptions: {
						semantic: true,
						syntactic: true,
					}
				}
			}),
			new HtmlWebpackPlugin({
				cache: false,
				inject: false,
				minify: false,
				chunks: ['vendor', 'app'],
				filename: path.join(__dirname, "./dist/index.html"),
				template: "./templates/index.html"
			})
		].filter(Boolean),
		optimization: {
			runtimeChunk: {
				name: 'runtime',
			},
			moduleIds: 'deterministic',
			removeEmptyChunks: true,
			mergeDuplicateChunks: true,
			splitChunks: {
                chunks: 'all'            
			}
		}
	}
};