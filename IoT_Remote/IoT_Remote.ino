#include <Arduino.h>
#include <Arduino_JSON.h>

#include "ServerHandler.h"
#include "WiFiHandler.h"
#include "HueHandler.h"
#include "InputHandler.h"

void setup() {
  Serial.begin(115200);
  inputSetup(); // Setting up inputs from InputHandler
  initWiFi(); // Connect to Wi-Fi from WiFiHandler
  getHueID(); // Get Hue Lights status from HueHandler
  serverStart(); // Start Async server from ServerHandler
  serverHTML();
}

void loop() {
  serverCleanup();

  if (button1Pressed()){
    lightsDefault();
  }

  JSONUpdate();
  delay(10);
}
