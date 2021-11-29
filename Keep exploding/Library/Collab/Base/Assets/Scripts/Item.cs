using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour
{
    public enum ObjectType {Primary, Useful, Useless};
    public ObjectType objectType;
    public string name;
    public ItemSize size;
    public bool in_bomb=false;
    public bool no_gravity=false;
    public DateTime endGravity;
    public bool selected = false;
    public AudioSource droppingSound;
    public bool audioPlayed=false;

    
    public Material selectedMaterial;
    public Material initialMaterial;

    //public  Material[] childsSelectedMaterials;
    public  Material[] childsInitialMaterials;


    private Rigidbody rb;
    private MeshRenderer mr;
    private BoxCollider bc;
    private MeshRenderer[] childsMrenderer;
    // Start is called before the first frame update
    void Start()
    {
        print("hihi");
        droppingSound = GameObject.Find("DroppingSound").GetComponent<AudioSource>();
        print(droppingSound);
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        bc = GetComponent<BoxCollider>();
        initialMaterial = mr.material;

       childsMrenderer = GetComponentsInChildren<MeshRenderer>();
        int meshCount = childsMrenderer.Length;
        
        childsInitialMaterials = new Material[meshCount];

        for(int i = 0; i < meshCount; i++)
        {
            childsInitialMaterials[i] = childsMrenderer[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(selected){
            mr.material = selectedMaterial;
            for(int i = 0; i < childsMrenderer.Length; i++)
                childsMrenderer[i].material = selectedMaterial;
        }
        if(!selected){
            mr.material = initialMaterial;
            for (int i = 0; i < childsMrenderer.Length; i++)
                childsMrenderer[i].material = childsInitialMaterials[i];
        }


        if(in_bomb && !no_gravity){
            endGravity = DateTime.Now.AddSeconds(1.3);
            no_gravity=true;
        }

        if(no_gravity && DateTime.Now>endGravity){
            
            

            rb.gameObject.GetComponent<Rigidbody>().useGravity=false;
            rb.gameObject.GetComponent<Rigidbody>().isKinematic=true;
        }

        

        
    }

    void OnCollisionEnter(Collision collision)
    {

        if(in_bomb && !audioPlayed){
            droppingSound.Play();
            audioPlayed = true;
        }
    }
}
