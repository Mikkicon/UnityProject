using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour
{
    //Стандартна функція, яка викличеться, //коли поточний об’єкт зіштовхнеться із іншим 
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Намагаємося отримати компонент кролика
        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        //Впасти міг не тільки кролик
        if (rabit != null)
        {
            //print(rabit);
            //Повідомляємо рівень, про смерть кролика 
            LevelController.current.onRabitDeath(rabit);
        }
    }
}

