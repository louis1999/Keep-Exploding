using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerCalculator : MonoBehaviour
{
    public List<Item> items;
    public float powerLevel;
    public int primaryItems;
    public int usefulItems;
    public int uselessItems;


    List<float> additions = new List<float>();
    List<float> multiplications = new List<float>();
    List<float> divisions = new List<float>();




    // Start is called before the first frame update
    void Start()
    {
        Item[] array = GetComponentsInChildren<Item>();
        foreach (Item item in array)
        {
            items.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePowerLevel();
    }

    bool IsInBomb(string name) {
        foreach (Item item in items)
        {
            if(item.name == name) {
                return true;
            }
        }
        return false;
    }

    bool IsInRoom(string name) {
        GameObject[] itemObjs = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject obj in itemObjs)
        {
            Item item = obj.GetComponent<Item>();
            if(item.name == name) {
                return true;
            }
        }
        return false;
    }

    void CheckCombinations() {
        if(primaryItems == 0) {
            additions.Add(usefulItems + uselessItems);
            if(IsInBomb("battery") && IsInBomb("multitool") && IsInBomb("wires")) {
                multiplications.Add(3);
            }
            if(IsInBomb("hammer")) {
                additions.Add(-50);
            }
        }
        else if(primaryItems == 1) {
            if(IsInBomb("nails")) {
                additions.Add(3);
                if(IsInBomb("tape")) {
                    additions.Add(3);
                }
            }

        } else if(primaryItems == 2) {
            if(IsInRoom("vase")) {
                additions.Add(-100);
            }

        } else if (primaryItems >= 3) {
            if(IsInBomb("office chair") && uselessItems == 1) {
                multiplications.Add(10);
            }
        }


        if(IsInBomb("nitrogen")) {
            if(IsInBomb("wine bottle") || IsInBomb("wine glass")) {
                additions.Add(2);
            }
        }
        if(IsInBomb("plutonium")) {
            if(IsInBomb("water bottle")) {
                if(IsInBomb("water pistol")) {
                    additions.Add(-3);
                } else {
                    additions.Add(3);
                }
            }

        } 
        if(IsInBomb("oxygen")) {
            if(IsInBomb("matchbox")) {
                if(IsInBomb("water bottle") || IsInBomb("water pistol")) {
                    divisions.Add(2);
                } else {
                    multiplications.Add(2);
                }
            }
        }
        if(IsInBomb("dynamite")) {
            if(IsInBomb("matchbox")) {
                if(IsInBomb("water bottle") || IsInBomb("water pistol")) {
                    multiplications.Add(2);
                } else {
                    additions.Add(4);
                }
            }

        }
        if(IsInBomb("gas can")) {
            if(IsInRoom("matchbox")) {
                if(IsInBomb("wires") || IsInBomb("hammer")) {
                    additions.Add(-5);
                } else if(IsInBomb("matchbox")) {
                    additions.Add(5);
                }
            } else {
                if(IsInBomb("wires") && IsInBomb("hammer")) {
                    multiplications.Add(3);
                }
            }
        }


        if(IsInRoom("first aid kit")) {
            if(primaryItems != 0) {
                divisions.Add(primaryItems*2);
            }
        }
        
    }

    void CalculatePowerLevel() {
        powerLevel = 0f;
        primaryItems = 0;
        usefulItems = 0;
        uselessItems = 0;
        additions = new List<float>();
        multiplications = new List<float>();
        divisions = new List<float>();
        foreach (Item item in items)
        {
            switch (item.objectType)
            {
                case Item.ObjectType.Primary:
                    primaryItems++;
                    additions.Add(10);
                    break;
                case Item.ObjectType.Useful:
                    usefulItems++;
                    additions.Add(5);
                    break;
                case Item.ObjectType.Useless:
                    uselessItems++;
                    additions.Add(1);
                    break;
                default:
                    break;
            }
        }

        CheckCombinations();
        foreach (float power in additions)
        {
            powerLevel += power;
        }

        foreach (float power in multiplications)
        {
            powerLevel *= power;
        }
        foreach (float power in divisions)
        {
            powerLevel /= power;
        }
    }
}
