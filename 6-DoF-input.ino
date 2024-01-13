const int accel_sense = 16;
int accel_range = accel_sense * 9.81 * 10 * 2; //10 for scaling, 2 to accomondate ±

void setup() {
  // initialize serial:
  Serial.begin(2000000);
}

void loop() {
  Serial.println(1300000 + accel_sense); //declare sensitivity
  //To read from sensor, accelerometer data must add by accel_sense * 9.81
  simulate(1310000, accel_range, 25, 10); //accel X
  simulate(1320000, accel_range, 25, 10); //accel Y
  simulate(1330000, accel_range, 25, 10); //accel Z
  simulate(1410000, 360, 25, 1); //gyro X
  simulate(1420000, 360, 25, 1); //gyro Y
  simulate(1430000, 360, 25, 1); //gyro Z
}

void simulate(int header, int range, int wait_time, int precision){
  for(int i=1; i < range; i += precision){
    int data = header + i;
    Serial.println(data);
    delay(wait_time);
  }
}


