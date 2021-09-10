// This is the Arduino sketch that I upload to my Ardunio Uno. The Ardunio has
// a switch attached to pin 13. The Ardunio prints a constant 0 to the serial
// port if the switch is open or 1 if the switch is closed.

int switchInput = 13;

int switchState;
int lastSwitchState = LOW;
unsigned long lastDebounceTime = 0;
unsigned long debounceDelay = 50;

void setup() {
  Serial.begin(9600);
  pinMode(switchInput, INPUT_PULLUP);
}

// the loop routine runs over and over again forever:
void loop() {
  int switchReading = digitalRead(switchInput);

  if (switchReading != lastSwitchState) {
    lastDebounceTime = millis();
  }

  if ((millis() - lastDebounceTime) > debounceDelay) {
    if (switchReading != switchState) {
      switchState = switchReading;
    }
  }
  int switchClosed = 1 - switchState;
  Serial.println(switchClosed);
 
  lastSwitchState = switchReading;
  delay(10);
}
