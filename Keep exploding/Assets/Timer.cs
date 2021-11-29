using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float timeRemaining = 10;
    public int secondsRemaining = 10;
    public Text timerText;
    public List<Rigidbody> policeCars;
    public GameObject wastedPanel;
    public PlayerInput input;
    public AudioSource tick;
    public AudioSource EpicGameSound;

    public Transform epicenter;
    public ItemSpawner spawner;
    public float explosionForce;
    public List<GameObject> toDesactivate;
    public MeshRenderer table;
    public GameObject people;
    public GameObject score;
    public GameObject timer;
    public bool gameOver = false;
    void Update()
    {

        float oldSeconds = secondsRemaining;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            secondsRemaining = (int)timeRemaining;
        } else
        {
            if(!gameOver)
            {
                print("HEEEEEEEEY");
                GameOver();
                gameOver = true;
            }
            
            return;
        }

        if (secondsRemaining == oldSeconds) return;

        string text = "0";
        int minutes = secondsRemaining / 60;
        text += minutes.ToString();
        int seconds = secondsRemaining % 60;
        text += ":";
        if (seconds < 10)
        {
            text += "0";
        }
        text += seconds.ToString();
        timerText.text = text;

        tick.Play();

        if(secondsRemaining == 60+60+39){
            EpicGameSound.Play();
        }
        
    }

    public void GameOver()
    {
        wastedPanel.SetActive(true);
        input.DeactivateInput();
        foreach(Rigidbody car in policeCars)
        {
            car.useGravity = true;
            car.isKinematic = false;
        }

         
        Invoke("Explode", 2f);
        score.SetActive(true);
        timer.SetActive(false);
    }

    public void Win()
    {
       

        wastedPanel.SetActive(false) ;
        foreach (GameObject sceneObject in toDesactivate)
        {
            sceneObject.SetActive(false);
        }
        table.enabled = false;
        people.SetActive(true);
        Invoke("Explode", 2f);
        score.SetActive(true);
        timer.SetActive(false);
        //victoryScreen.SetActive(true);
    }
    private void Explode()
    {
        foreach(GameObject item in spawner.spawnedPrefabs)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            //Vector3 direction = item.transform.position - epicenter.position;
            rb.AddExplosionForce(explosionForce, epicenter.position, 50f);
           // rb.AddForce(direction * explosionForce, ForceMode.Force);
        }

        
    }
}
