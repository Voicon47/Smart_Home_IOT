/*
  Blink

  Turns an LED on for one second, then off for one second, repeatedly.

  Most Arduinos have an on-board LED you can control. On the UNO, MEGA and ZERO
  it is attached to digital pin 13, on MKR1000 on pin 6. LED_BUILTIN is set to
  the correct LED pin independent of which board is used.
  If you want to know what pin the on-board LED is connected to on your Arduino
  model, check the Technical Specs of your board at:
  https://www.arduino.cc/en/Main/Products

  modified 8 May 2014
  by Scott Fitzgerald
  modified 2 Sep 2016
  by Arturo Guadalupi
  modified 8 Sep 2016
  by Colby Newman

  This example code is in the public domain.

  https://www.arduino.cc/en/Tutorial/BuiltInExamples/Blink
*/
#include "DHTesp.h"
#include <WiFi.h>
#include <HTTPClient.h>
#include "NetworkClientSecure.h"

DHTesp dht;
const char* ssid = "Tryitout"; //Tên điểm phát wifi
const char* password = "20022004";//Mật khẩu wifi
const char* serverName = "http://192.168.0.221:5222/api/sensor";

// the following variables are unsigned longs because the time, measured in
// milliseconds, will quickly become a bigger number than can be stored in an int.
unsigned long lastTime = 0;
// Timer set to 10 minutes (600000)
//unsigned long timerDelay = 600000;
// Set timer to 5 seconds (5000)
unsigned long timerDelay = 5000;
// Chọn loại cảm biến cho phù hợp ---------------------------------------------------------------------------------------
float temperature, humidity;

//DHT dht(DHTPIN, DHTTYPE);

void setup() {
  Serial.begin(9600);
  
  WiFi.begin(ssid, password);
  Serial.println("Connecting");
  while(WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to WiFi network with IP Address: ");
  Serial.println(WiFi.localIP());
 
  Serial.println("Timer set to 5 seconds (timerDelay variable), it will take 5 seconds before publishing the first reading.");
  Serial.println("DHTxx test!");
  dht.setup(23, DHTesp::DHT11);
}

void loop() {
  if ((millis() - lastTime) > timerDelay) {
    if (WiFi.status() == WL_CONNECTED) {
      WiFiClient client;
      HTTPClient http;
      // Read sensor data
      float h = dht.getHumidity();
      float t = dht.getTemperature();

      if (isnan(h) || isnan(t)) {
        Serial.println("Failed to read from DHT sensor!");
      } else {
        // Compute heat index
        float hic = dht.computeHeatIndex(t, h);
        Serial.printf("Humidity: %.2f%%, Temperature: %.2f°C, Heat Index: %.2f°C\n", h, t, hic);

        // Construct JSON payload
        String jsonPayload = "{\"Temperature\":" + String(t) + ",\"Humidity\":" + String(h) + "}";
        Serial.println("JSON Payload: " + jsonPayload);

        // Send HTTP POST request
        http.begin(client,serverName); // Specify server URL
        //http.setInsecure();  // Bypass SSL certificate validation
        http.addHeader("Content-Type", "application/json"); // Specify content-type header
        Serial.println(serverName);
        int httpResponseCode = http.POST(jsonPayload);

        if (httpResponseCode == 307) {
          // Handle redirection
          String redirectURL = http.header("Location");
          Serial.println("Redirect URL: " + redirectURL);
  
          // Re-send request to the redirected URL
          http.begin(redirectURL);
          httpResponseCode = http.POST(jsonPayload);
  
          if (httpResponseCode > 0) {
              Serial.println("HTTP Response code after redirect: " + String(httpResponseCode));
              Serial.println("Server Response: " + http.getString());
          } else {
              Serial.println("HTTP POST failed after redirect: " + String(http.errorToString(httpResponseCode).c_str()));
          }
        } else if (httpResponseCode > 0) {
            Serial.println("HTTP Response code: " + String(httpResponseCode));
            Serial.println("Server Response: " + http.getString());
        } else {
            Serial.println("HTTP POST failed: " + String(http.errorToString(httpResponseCode).c_str()));
        }
        http.end(); // Free resources
      }
    } else {
      Serial.println("WiFi Disconnected. Reconnecting...");
      WiFi.begin(ssid, password);
    }
    lastTime = millis();
  }
}
