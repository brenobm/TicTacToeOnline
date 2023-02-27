const PROXY_CONFIG = [
  {
    context: [
      "/api",
      "/gameHub",
    ],
    target: "https://localhost:7150",
    secure: false,
    ws: true
  }
]

module.exports = PROXY_CONFIG;
