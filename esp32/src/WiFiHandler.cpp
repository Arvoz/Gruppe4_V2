#include "WiFiHandler.h"
#include <WiFi.h>

// WiFi credentials (INSECURE FOR NOW HAVE TO FIX LATER)
String ssid = "";
String pwd = "";

void initWiFi() {
  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, pwd);
  Serial.print("Connecting to WiFi ..");
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print('.');
    delay(1000);
  }
  Serial.println(WiFi.localIP());  // Print local IP address
}
