using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{

     public static TagManager instance = null; // Экземпляр объекта
      void Awake () 
      {
        // Теперь, проверяем существование экземпляра
        if (instance == null) { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта
        } else if(instance == this){ // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }

        // Теперь нам нужно указать, чтобы объект не уничтожался
        // при переходе на другую сцену игры
            DontDestroyOnLoad(gameObject);
     }

    public const string LADDER="ladder";
    public const string SPIKE="spike";
    public const string ENEMY_POINT="enemy_point";
    public const string PLAYER="Player";
    public const string ICE="Ice";
    public const string GROUND="ground";
    public const string FURANCE="Furance";
    public const string SPRING = "Spring";



}
