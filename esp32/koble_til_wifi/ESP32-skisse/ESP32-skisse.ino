#include <WiFi.h>
#include <AsyncTCP.h>
#include <ESPAsyncWebServer.h>
#include <SPIFFS.h>
#include <ArduinoJson.h>
#include <HTTPClient.h>

// AP-innstillinger
const char* apSSID = "ESP32_AccessPoint"; // husk å endre
const char* apPassword = "12345678";
const char* serverUrl = "http://192.168.1.2:3000/api/esp32";

const char* ssid = "ZyXEL17B116";
const char* password = "F84498DC6DAE";

AsyncWebServer server(80);

/*
// Koble til hjemmenettverket med gitte verdier
void connectToWiFi(const char* ssid, const char* password) {
    WiFi.begin(ssid, password);
    Serial.print("Kobler til WiFi ");
    Serial.println(ssid);
    
    while (WiFi.status() != WL_CONNECTED) {
        Serial.print(".");
        delay(1000);
    }
    
    Serial.println("\nTilkoblet til WiFi!");
    Serial.print("Lokal IP: ");
    Serial.println(WiFi.localIP());
}
*/

void setup() {
    Serial.begin(115200);

    WiFi.begin(ssid, password);

    while (WiFi.status() != WL_CONNECTED) {
        delay(1000);
        Serial.println("Kobler til WiFi...");
    }

    Serial.println("Tilkoblet til WiFi!");

/*
    // Initialiser SPIFFS
    if (!SPIFFS.begin(true)) {
        Serial.println("SPIFFS monteringsfeil");
        return;
    }

    
    // Start AP-modus
    WiFi.softAP(apSSID, apPassword);
    Serial.print("AP IP adresse: ");
    Serial.println(WiFi.softAPIP());
    

    // Tjener statiske filer fra SPIFFS
    server.serveStatic("/", SPIFFS, "/").setDefaultFile("index.html");
    server.serveStatic("/styles.css", SPIFFS, "/styles.css");


    // Mottar WiFi konfigurasjonsdata fra nettsiden
    server.on("/configWiFi", HTTP_POST, [](AsyncWebServerRequest *request){}, 
    nullptr,
    [](AsyncWebServerRequest *request, uint8_t *data, size_t len, size_t index, size_t total) {
        String body = String((char*)data).substring(0, len);
        Serial.print("Mottatt body: ");
        Serial.println(body);

        DynamicJsonDocument doc(256);
        DeserializationError error = deserializeJson(doc, body);
        if (error) {
            Serial.print("JSON Feil: ");
            Serial.println(error.c_str());
            request->send(400, "text/plain", "Feil i JSON-format.");
            return;
        }

        const char* ssid = doc["ssid"];
        const char* password = doc["password"];
        Serial.print("SSID: ");
        Serial.println(ssid);
        Serial.print("Password: ");
        Serial.println(password);

        connectToWiFi(ssid, password);
        request->send(200, "text/plain", "WiFi konfigurering fullført.");
    }
);

    server.begin();
    Serial.println("Server startet"); */
}

void loop() {
    if (WiFi.status() == WL_CONNECTED) {
        HTTPClient http;
        http.begin(serverUrl);
        http.addHeader("Content-Type", "application/json");

        String jsonData = "{\"name\":\"ESP32\",\"status\":\"Online\"}";
        int httpResponseCode = http.POST(jsonData);

        if (httpResponseCode > 0) {
            String response = http.getString();
            Serial.println("Respons fra server: " + response);
        } else {
            Serial.print("Feil ved sending av POST: ");
            Serial.println(httpResponseCode);
        }

        http.end();
    }
    delay(10000);
}
