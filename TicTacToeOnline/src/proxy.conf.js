const PROXY_CONFIG = [
  {
    context: [
      "/api",
      "/gameHub",
    ],
    target: "https://tic-tac-toe-online.azurewebsites.net",
    secure: false,
    ws: true
  }
]

module.exports = PROXY_CONFIG;
