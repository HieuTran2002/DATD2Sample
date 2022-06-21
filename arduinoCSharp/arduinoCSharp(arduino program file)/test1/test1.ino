

void setup() {
  Serial.begin(9600);
  pinMode(13, OUTPUT);
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
    
  }
  out = readString;
  if(out == "on"){digitalWrite(13, HIGH);}
  if(out == "off"){digitalWrite(13, LOW);}

}
