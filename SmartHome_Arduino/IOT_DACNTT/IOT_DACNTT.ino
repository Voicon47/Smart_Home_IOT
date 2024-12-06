#include "DHTesp.h"
#include <WiFi.h>
#include <WebSocketsClient.h>
#include <ArduinoJson.h>
#define LIGHT_PIN 22 
DHTesp dht;
const char* ssid = "Tryitout"; // Wi-Fi SSID
const char* password = "20022004"; // Wi-Fi Password

WebSocketsClient webSocket; // WebSocket client

// Connection status variable
bool isConnected = false;

// Previous sensor readings
float previousTemperature = NAN;
float previousHumidity = NAN;

unsigned long lastTime = 0;
unsigned long timerDelay = 2000; // Check sensor every 2 seconds
// Function to handle incoming WebSocket messages
void handleIncomingMessage(String message) {
  Serial.println("Received: " + message);

  // Parse the JSON message
  StaticJsonDocument<200> jsonDoc;
  DeserializationError error = deserializeJson(jsonDoc, message);

  if (error) {
    Serial.print("Error parsing JSON: ");
    Serial.println(error.f_str());
    return;
  }

  // Extract Type and Data from the message
  String type = jsonDoc["Type"];
  String data = jsonDoc["Data"];

  // Handle different message types
  if (type == "Light") {
    if (data == "Light_on") {
      digitalWrite(LIGHT_PIN, HIGH); // Turn light ON
      Serial.println("Light turned ON");
    } else if (data == "Light_off") {
      digitalWrite(LIGHT_PIN, LOW); // Turn light OFF
      Serial.println("Light turned OFF");
    } else {
      Serial.println("Unknown light command");
    }
  } else if (type == "SensorData") {
    // Handle sensor configuration (example)
//    int newInterval = jsonDoc["Data"]["interval"];
    Serial.printf("Sensor update interval set to %d ms\n");
  } else {
    Serial.println("Unknown message type");
  }
}
//void handleIncomingMessage(String message) {
//  Serial.println("Received: " + message);
//
//  if (message == "light_on") 
//  {
//    digitalWrite(LIGHT_PIN, HIGH);
//    Serial.println("Light turned ON by server.");
//  } 
//  else if(message == "light_off")
//  {
//    digitalWrite(LIGHT_PIN, LOW);
//    Serial.println("Light turned OFF by server.");
//  }else
//  {
//    Serial.println("SensorData");
//  }
//}

void webSocketEvent(WStype_t type, uint8_t* payload, size_t length) {
  switch (type) 
  {
    case WStype_DISCONNECTED:
      Serial.println("WebSocket disconnected");
      isConnected = false;  // Update connection status
      break;
    case WStype_CONNECTED:
      Serial.println("WebSocket connected");
      isConnected = true;  // Update connection status
      break;
    case WStype_TEXT:
    {
      String message = String((char*)payload);
      Serial.printf("Message received: %s\n", payload);
      handleIncomingMessage(message);
      break;
    }
    case WStype_ERROR:
      Serial.println("WebSocket error occurred");
      break;
    case WStype_PONG:
      Serial.println("Received PONG message");
      break;
    default:
      Serial.println("Unknown WebSocket event");
      break;
  }
}
// Function to send a generic message
void sendMessage(const char* type, const String& data) {
    StaticJsonDocument<256> jsonDoc;
    // Add Type field
    jsonDoc["Type"] = type;
    // Add Data field with the incoming message as a string
    jsonDoc["Data"] = data;

    // Serialize JSON to a string
    char buffer[256];
    serializeJson(jsonDoc, buffer);
    
    // Send the message if WebSocket is connected
    if (isConnected) {
        webSocket.sendTXT(buffer);
        Serial.println("Message sent: " + String(buffer));
    } else {
        Serial.println("WebSocket is not connected. Message not sent.");
    }
}

String GetSensorData() {
    // Create JSON document to store sensor data
    StaticJsonDocument<256> jsonDoc;

    // Read temperature and humidity
    float temperature = dht.getTemperature();
    float humidity = dht.getHumidity();

    // Check if sensor values are valid
    if (isnan(temperature) || isnan(humidity)) {
        Serial.println("Failed to read from DHT sensor!");
        //jsonDoc["Error"] = "Failed to read from sensor";
    }else{
        if (temperature != previousTemperature || humidity != previousHumidity) {
          previousTemperature = temperature;
          previousHumidity = humidity;
          // Populate JSON document with sensor data
          jsonDoc["Temperature"] = temperature;
          jsonDoc["Humidity"] = humidity;
        }
    }

    // Serialize JSON to a string
    String jsonString;
    serializeJson(jsonDoc, jsonString);

    // Return the JSON string
    return jsonString;
}
String GetLightData() {
    if (digitalRead(LIGHT_PIN) == HIGH) {
        // LED is ON, send "light_on"
        return "light_on";
    }    
    return "light_off";
}

void setup() {
  Serial.begin(9600);

  // Connect to Wi-Fi
  WiFi.begin(ssid, password);
  Serial.println("Connecting to Wi-Fi");
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to Wi-Fi. IP Address: ");
  Serial.println(WiFi.localIP());

  // Initialize WebSocket connection
  webSocket.begin("192.168.0.222", 5222, "/api/ws"); // Replace with your WebSocket server IP and port
  webSocket.onEvent(webSocketEvent);
  // Initialize DHT sensor
  dht.setup(23, DHTesp::DHT11);
  while (!webSocket.isConnected()) {
      webSocket.loop();
      delay(100);
      Serial.println("Waiting for WebSocket connection...");
  }
  String sensorData = GetSensorData();
  // Send the data as a SensorData type
  sendMessage("SensorData", sensorData);
  Serial.println("Send first message");

  pinMode(LIGHT_PIN,OUTPUT);
  digitalWrite(LIGHT_PIN, HIGH);
  String lightData = GetLightData();
  sendMessage("Light",lightData);
  Serial.println("Send second message");
  
  Serial.println("Setup complete. Monitoring sensor for changes.");
}

void loop() {
  webSocket.loop(); // Handle WebSocket events
  if ((millis() - lastTime) > timerDelay) {
    String sensorData = GetSensorData();
    // Send the data as a SensorData type
    sendMessage("SensorData", sensorData);

    lastTime = millis();
  }
}
