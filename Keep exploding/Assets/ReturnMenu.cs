using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour
{
    PowerCalculator powerCalculator;
    HighscoreKeeper hsKeeper;

    void Start() {
        powerCalculator = GameObject.FindWithTag("Bomb").GetComponent<PowerCalculator>();
        hsKeeper = GameObject.Find("HighscoreKeeper").GetComponent<HighscoreKeeper>();
    }

    public void OnClick() {
        hsKeeper.score = powerCalculator.powerLevel;
        hsKeeper.highscores.Add(powerCalculator.powerLevel);
        hsKeeper.inMenu = true;
        SceneManager.LoadScene("StartMenu");
    }
}
