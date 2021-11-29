using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent (typeof(TextMeshProUGUI))]
public class ShowScore : MonoBehaviour
{
    TextMeshProUGUI text;
    PowerCalculator powerCalculator;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        powerCalculator = GameObject.FindWithTag("Bomb").GetComponent<PowerCalculator>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = powerCalculator.powerLevel.ToString();
        
    }
}
