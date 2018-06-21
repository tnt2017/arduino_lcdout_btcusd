#include <LiquidCrystalRus.h>
//#include <LiquidCrystal.h>

// инициализируем объект-экран, передаём использованные 
// для подключения контакты на Arduino в порядке:
// RS, E, DB4, DB5, DB6, DB7
LiquidCrystalRus lcd(12, 11, 5, 4, 3, 2);

 int val = 0;

void setup()
{
    Serial.begin(9600); 
    
    // устанавливаем размер (количество столбцов и строк) экрана
    lcd.begin(16, 2);
    // печатаем первую строку
    lcd.print("АБВГДЕЁЖЗИЙКЛМНО");

    // устанавливаем курсор в колонку 0, строку 1
    // на самом деле это вторая строка, т.к. нумерация начинается с нуля
    lcd.setCursor(0, 1);
    // печатаем вторую строку
    lcd.print("Do It Yourself");
}
 
void loop()
{
  String inString;

  while(Serial.available()) {
    String ttydata = Serial.readString(); // ttyData - информация с серийного порта
    Serial.print("String: ");
    Serial.println(ttydata);
    lcd.begin(16, 2);
    lcd.print(ttydata);
}
    

}
