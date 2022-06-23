
  int currentState= 0;
  int lastState= 0;
void setup() {
  Serial.begin(9600);
  pinMode(13, OUTPUT);
  pinMode(7, INPUT_PULLUP);
}

void loop() {
  String out;
  String readString;

  while(Serial.available())
  {
    delay(1);
    if(Serial.available() > 0)
    {
      char c = Serial.read(); 
      if(isControl(c))  {break;}
      
      readString += c;
    }
      out = readString;
      
      if(out == "on"){digitalWrite(13, HIGH);}
      if(out == "off"){digitalWrite(13, LOW);}

      delay(100);

  }
      currentState = digitalRead(7);
      if(currentState != lastState)
      {
        Serial.println(currentState); 
      }
      lastState = currentState;

  
 


}
