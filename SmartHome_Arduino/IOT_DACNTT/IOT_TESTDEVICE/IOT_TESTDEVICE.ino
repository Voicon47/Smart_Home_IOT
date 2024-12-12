#include <ESP32Servo.h>
//#define FLAME_PIN 22
#define MOTION_PIN 22
#define LIGHT_PIN 23
static const int SERVO_PIN = 18;
int value;
Servo servo_1;
void setup() {
    servo_1.attach(18);
//    Serial.println(18);
    servo_1.write(0);
    pinMode(MOTION_PIN,INPUT);
    pinMode(LIGHT_PIN,OUTPUT);
    digitalWrite(LIGHT_PIN,LOW);
    
    Serial.begin(9600);
}
void loop() {
  value = digitalRead(MOTION_PIN);
  if (value == HIGH) { // Motion detected
      Serial.println("Motion detected: Turning light ON");
      digitalWrite(LIGHT_PIN, HIGH); // Turn light ON
      servo_1.write(90); // Example: Move servo to 90 degrees
  } else { // No motion
      Serial.println("No motion detected: Turning light OFF");
      digitalWrite(LIGHT_PIN, LOW); // Turn light OFF
      servo_1.write(0); // Example: Reset servo to 0 degrees
  }

  // Debugging: Print motion sensor value
  Serial.print("Motion Sensor Value: ");
  Serial.println(value);
  delay(200);
//  for(int posDegrees = 0; posDegrees <= 180; posDegrees++) {
//    servo_1.write(posDegrees);
//    Serial.println(posDegrees);
//    delay(20);
//  }
//
//  for(int posDegrees = 180; posDegrees >= 0; posDegrees--) {
//    servo_1.write(posDegrees);
//    Serial.println(posDegrees);
//    delay(20);
//  }
}
