#include "HueHandler.h"
#include <WiFiClientSecure.h>
#include <HTTPClient.h>
#include <vector>

// BRIDGE IP AND USERNAME NEEDS TO BE DYNAMIC, WILL FIX LATER:
const char* hueBridgeIP = "";
const char* apiUsername = "";
int numberOfLights;
bool lightsOn;
std::vector<int> hueID;

void getHueID() {
    WiFiClientSecure client;
    HTTPClient http;
    String url = String("https://") + hueBridgeIP + "/api/" + apiUsername + "/lights";
    client.setInsecure();
    http.begin(client, url);
    http.addHeader("Content-Type", "application/json");

    int httpResponseCode = http.GET();
    if (httpResponseCode > 0) {
        String response = http.getString();
        JSONVar jsonData = JSON.parse(response);

        if (JSON.typeof(jsonData) == "undefined") {
            Serial.println("Parsing JSON failed!");
        }

        numberOfLights = jsonData.keys().length();
        hueID.resize(numberOfLights);
        int lightsOnCounter = 0;

        for (int i = 0; i < numberOfLights; i++) {
            String lightID = jsonData.keys()[i];
            hueID[i] = lightID.toInt();
            JSONVar lightState = jsonData[lightID]["state"];

            if (lightState.hasOwnProperty("on") && bool(lightState["on"])) {
                lightsOnCounter++;
            }
        }

        lightsOn = (lightsOnCounter > numberOfLights / 2);
        http.end();
    } else {
        Serial.print("GET ERROR: ");
        Serial.println(httpResponseCode);
    }
}

void lightsDefault() {
    WiFiClientSecure client;
    HTTPClient http;

    for (int i = 0; i < numberOfLights; i++) {
        String url = String("https://") + hueBridgeIP + "/api/" + apiUsername + "/lights/" + String(hueID[i]) + "/state";
        http.begin(client, url);
        http.addHeader("Content-Type", "application/json");
        client.setInsecure();

        String payload = lightsOn ? "{\"on\": false}" : "{\"on\": true, \"bri\": 254, \"ct\": 370}";
        lightsOn = !lightsOn;

        int httpResponseCode = http.PUT(payload);

        if (httpResponseCode > 0) {
            Serial.print("PUT request sent for light ");
            Serial.print(i);
            Serial.println(httpResponseCode);
            String response = http.getString();
            Serial.println(response);
        } else {
            Serial.print("Error on PUT request for light ");
            Serial.print(i);
            Serial.print(": ");
            Serial.println(httpResponseCode);
        }

        http.end();
    }
}
