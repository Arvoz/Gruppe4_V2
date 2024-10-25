#include "ServerHandler.h"
#include <AsyncTCP.h>
#include <ESPAsyncWebServer.h>
#include <Arduino_JSON.h>
#include "InputHandler.h"
// Async webserver + socket
AsyncWebServer server(80);
AsyncWebSocket ws("/ws");

void serverStart(){
  server.addHandler(&ws);
  server.begin();
}

void serverCleanup(){
  ws.cleanupClients();
}

const char index_html[] PROGMEM = R"rawliteral(
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>IoT Remote Hub</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      margin: 0;
      padding: 20px;
      background-color: #f4f4f9;
    }
    h1 {
      color: #333;
    }
    p {
      color: #555;
    }
    .sensor {
      font-size: 1.5em;
      margin: 10px 0;
    }
  </style>
</head>
<body>
  <h1>IoT Remote</h1>
  <p class="sensor">Potentiometer Value: <span id="potValue">0</span></p>
  <p class="sensor">Mode Selection: <span id="modeSelect">0</span></p>
  <script>
    var websocket;
    function initWebSocket() {
      websocket = new WebSocket('ws://' + window.location.hostname + '/ws');
      websocket.onmessage = function(event) {
        var data = JSON.parse(event.data);
        document.getElementById('potValue').innerText = data.pot;
        document.getElementById('modeSelect').innerText = data.mode
      };
    }
    window.onload = function() {
      initWebSocket();
    };
  </script>
</body>
</html>
)rawliteral";

void serverHTML(){
  server.on("/", HTTP_GET, [](AsyncWebServerRequest *request) {
    request->send_P(200, "text/html", index_html);
  });
}

JSONVar jsonData;
void JSONUpdate(){
  jsonData["pot"] = map(analogRead(potPin), 0, 4095, 0, 100);
  jsonData["mode"] = modeSelect;
  ws.textAll(JSON.stringify(jsonData));
}