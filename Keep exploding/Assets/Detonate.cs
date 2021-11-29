using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonate : MonoBehaviour
{
    public float threshold = 100f;
    public PowerCalculator power;
    public Timer timer;
    public void DetonateBomb()
    {
        print(power.powerLevel);
        if (power.powerLevel >= threshold)
        {
            print(power.powerLevel);
            timer.Win();
        } else
        {
            timer.GameOver();
        }
    }

}
