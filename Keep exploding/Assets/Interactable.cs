using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    public bool pickup = false;
    public Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }
}
