const withBundleAnalyzer = require('@next/bundle-analyzer')({
  enabled: process.env.ANALYZE === 'true',
});

const nextConfig = {
  images: {
    domains: ["images.unsplash.com"],
  },
  async redirects() {
    return [
      {
        source: "/",
        destination: "/home",
        permanent: true,
      },
    ];
  },
  webpack(config, { isServer }) {
    const { BundleAnalyzerPlugin } = require('webpack-bundle-analyzer');

    config.plugins.push(
      new BundleAnalyzerPlugin({
        analyzerMode: 'static',
        reportFilename: isServer
          ? '../analyze/server-report.html'
          : '../analyze/client-report.html',
        openAnalyzer: false,
      })
    );

    return config;
  },
};

module.exports = withBundleAnalyzer(nextConfig);
