using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [SerializeField] private Text TimeText; 
    [SerializeField] private float timeToDie=0f;
    
   public static GameManager instance = null; // Экземпляр объекта
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
            Inst();
    }


    private void FixedUpdate()
    {
        timeToDie-= Time.fixedDeltaTime;
        TimeText.text=timeToDie.ToString();
        if(timeToDie<=0) Death();
    }
    private void Inst()
    {

    }
    public void Death()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    [SerializeField]private GameObject ropeButtn;
    public void ShowButton()
    {
        ropeButtn.SetActive(true);
    }
    public void HideButton()
    {
        ropeButtn.SetActive(false);
    }
}
