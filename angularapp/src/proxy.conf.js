const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target: "https://tic-tac-toe-online.azurewebsites.net/",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
