using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject StartObj;
    public GameObject PlayObj;
    HighscoreKeeper hsKeeper;

    void Start() {
        hsKeeper = GameObject.Find("HighscoreKeeper").GetComponent<HighscoreKeeper>();
    }

    public void Play()
    {
        hsKeeper.inMenu = false;
        SceneManager.LoadScene("SampleScene");
        
    }

  
    
}
