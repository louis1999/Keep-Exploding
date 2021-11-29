using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class HighscoreKeeper : MonoBehaviour
{

    public float score;
    public bool inMenu = true;
    public TextMeshProUGUI text;

    private static HighscoreKeeper hsKeeper;
    public List<float> highscores = new List<float>();

    void Awake() {
        DontDestroyOnLoad (this);
        if (hsKeeper == null) {
            hsKeeper = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Highscores").GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inMenu) {
            GameObject obj = GameObject.Find("Highscores");
            if(obj) {
                text = obj.GetComponent<TextMeshProUGUI>();
                text.text = "Highscore: \n";
                highscores.Sort();
                highscores.Reverse();
                for (int i = 0; i < highscores.Count && i < 5; i++)
                {
                    text.text += highscores[i].ToString() + "\n";
                }
            }
            
        }
        
    }
}
