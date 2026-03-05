/*
  Code Written by Ivan Modest Tega and Syed Mohammed Jafer Hussain
  No AI has been used in this code
  
  Ultrasonic Slide and Shooting System

  This program uses 2 ultrasonic sensors:
  - One for sliding
  - One for shooting

  It checks:
  - How far the object moves
  - How fast it moves
  - If it is a small or large shot

  Then it prints the shot type in Serial Monitor.
*/

// Pins
#define slideTrigPin 5
#define slideEchoPin 4
#define shootTrigPin 7
#define shootEchoPin 6

#define BULLET_MAX_DISTANCE 14.0  // max shooting distance allowed
#define BULLET_MIN_DISTANCE 5.5f // distance to trigger a shot
#define LARGE_THRESHOLD 11.0  // above this is a large shot
#define FAST_TIME_THRESHOLD 400 // time in ms to check if shot is fast

#define SHOOTING_COOLDOWN 1000  // delay between shots

// -------- Variables --------
float maxDistance = 0;      // highest shoot distance reached
float timeAtMax = 0;        // time when max distance happened
int hasShot = 0;            // stops multiple shots
float whenShot = 0;         // time of last shot

float slideDistance = 0;    // slide sensor distance
float shootDistance = 0;    // shoot sensor distance

void setup() 
{
  Serial.begin (9600);

   // set trig pins as output and echo as input
  pinMode(slideTrigPin, OUTPUT);
  pinMode(slideEchoPin, INPUT);
  pinMode(shootTrigPin, OUTPUT);
  pinMode(shootEchoPin, INPUT);
}

void loop() 
{
  GetDistances();       // read both sensors

  PrintSlideDistance(); // print slide value
  CheckMaxDistance();   // track max distance
  CapShooting();        // limit shooting distance

  // reset if object moves away
  if (shootDistance > BULLET_MIN_DISTANCE + 0.5f && hasShot) ResetHasShot();
  // check if we should shoot
  if (shootDistance <= BULLET_MIN_DISTANCE && !hasShot && millis() - whenShot >= SHOOTING_COOLDOWN) Shoot();

  delay(10);
}

float MapSliding(float distance_cm) 
{
  // maps slide distance to a range between -7f and +7f
    if (distance_cm < 3) distance_cm = 3;
    else if (distance_cm > 11) distance_cm = 11;

    return 1.75f * distance_cm - 12.25f;
}

void GetDistances()
{
  // gets distance from both ultrasonic sensors

  // slide sensor
  digitalWrite(slideTrigPin, 0);
  delayMicroseconds(2);
  digitalWrite(slideTrigPin, 1);
  delayMicroseconds(10);
  digitalWrite(slideTrigPin, 0); 
  float slideDuration = pulseIn(slideEchoPin, 1); 

  // shoot sensor
  digitalWrite(shootTrigPin, 0);
  delayMicroseconds(2);
  digitalWrite(shootTrigPin, 1);
  delayMicroseconds(10);
  digitalWrite(shootTrigPin, 0); 
  float shootDuration = pulseIn(shootEchoPin, 1);

  // converts time to distance
  slideDistance = (slideDuration / 2) * 0.0343; 
  shootDistance = (shootDuration / 2) * 0.0343; 
}

void PrintSlideDistance()
{
  // prints slide value after mapping

  float slideValue = MapSliding(slideDistance);
  Serial.print("Slide: ");
  Serial.println(slideValue); 
}

void CheckMaxDistance()
{
  // keeps track of the highest distance before shooting
  
  if (hasShot) return; 
  
  if (shootDistance >= maxDistance + 0.5f) 
  {
    maxDistance = shootDistance;
    timeAtMax = millis();
  }
  else if (shootDistance == maxDistance) timeAtMax = millis();
}

void Shoot()
{
  /* uses criteria to figure out what shot to shoot
      critera: 
        1. if small and fast, 0th shot
        2. if small and slow, 1st shot
        3. if large and fast, 2nd shot
        4. if large and slow, 3rd shot
  */
  
  whenShot = millis();
  float timeDiff = millis() - timeAtMax;
  int isFast = 0;
  int isLarge = (maxDistance > LARGE_THRESHOLD);
  if (!isLarge) isFast = (timeDiff < FAST_TIME_THRESHOLD / 2);
  else isFast = (timeDiff < FAST_TIME_THRESHOLD);

  int shotCode;

  if (!isLarge && isFast)
      shotCode = 0;   // Small Fast
  else if (!isLarge && !isFast)
      shotCode = 1;   // Small Slow
  else if (isLarge && isFast)
      shotCode = 2;   // Large Fast
  else
      shotCode = 3;   // Large Slow

  Serial.print("Shot: ");
  Serial.println(shotCode);
  hasShot = 1;
}
  
void ResetHasShot()
{
  // resets shooting system 

  hasShot = 0;
  maxDistance = shootDistance; 
}

float CapShooting()
{
  // makes sure shooting distance stays in range
  
  if (shootDistance > BULLET_MAX_DISTANCE) shootDistance = BULLET_MAX_DISTANCE;
  if (shootDistance < 4) shootDistance = 4;
}