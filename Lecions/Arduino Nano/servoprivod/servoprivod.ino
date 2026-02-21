#include <Servo.h>
#define servoPin 5
Servo servo1;

void setup() {
  pinMode(servoPin, OUTPUT);
  servo1.attach(servoPin);

  servo1.write(10);
}

void loop() {
  // put your main code here, to run repeatedly:
  servo1.write(30);
  servo1.write(-30);
}
