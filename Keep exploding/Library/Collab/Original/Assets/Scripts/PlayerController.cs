using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{

    public Vector2 mousePos;
    public Rigidbody pickedItem;
    
    public float forceAmount = 500;

    Vector3 originalScreenTargetPosition;
    Vector3 originalRigidbodyPos;
    float selectionDistance;
    float originalSelectionDistance;
    public ParticleSystem ps;
    public GameObject table;

    

    void Start() {
        table = GameObject.FindWithTag("Table");
    }

    void Update() {
    }

    void FixedUpdate()
    {
        if (pickedItem && table)
        {
            pickedItem.gameObject.GetComponent<Item>().selected=true; // that line is for changing the color of the picked item

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, selectionDistance));
            
            float distanceRatio =  Vector2.Distance(new Vector2(mousePosition.x, mousePosition.y), new Vector2(table.transform.position.x, table.transform.position.y)) / Vector2.Distance(originalScreenTargetPosition, table.transform.position);

            selectionDistance = Mathf.Lerp(table.transform.position.z, originalSelectionDistance , distanceRatio);

            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, selectionDistance));
            Vector3 mousePositionOffset = mousePosition - originalScreenTargetPosition;
            pickedItem.velocity = (originalRigidbodyPos +  mousePositionOffset - pickedItem.transform.position) * forceAmount * Time.deltaTime;
        }
    }

    public void UpdateMousePosition(InputAction.CallbackContext context) {
     
        if(context.performed) {
           
            mousePos = context.ReadValue<Vector2>();
        }
    }

    public void OnLeftMouseDown(InputAction.CallbackContext context) {
       
        if (context.performed) {
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            bool hit = Physics.Raycast(ray, out hitInfo);
            if (hit)
            {
                if (hitInfo.collider.gameObject.GetComponent<Rigidbody>() && hitInfo.collider.gameObject.tag == "Item" && !hitInfo.collider.gameObject.GetComponent<Item>().in_bomb)
                {
                    originalSelectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                    originalScreenTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, originalSelectionDistance));
                    originalRigidbodyPos = hitInfo.collider.transform.position;
                    pickedItem = hitInfo.collider.gameObject.GetComponent<Rigidbody>();
                }
            }

        }
    }

    public void OnLeftMouseUp(InputAction.CallbackContext context){
        
        
     
        if (context.performed) {
            // first check if the user want's to drop the item in the suite case 
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0F);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if(hit.collider.gameObject.name=="suitcase_up"){
                    
                    if(pickedItem && !pickedItem.gameObject.GetComponent<Item>().in_bomb){
                        Vector3 scaleChange = new Vector3(0.7f, 0.7f, 0.7f);
                        pickedItem.gameObject.transform.localScale = new Vector3((float)pickedItem.gameObject.transform.localScale.x*0.7f, pickedItem.gameObject.transform.localScale.y*0.7f, pickedItem.gameObject.transform.localScale.z*0.7f);
                        pickedItem.gameObject.transform.position =new Vector3(-1.9f, -0f, 4.3f);
                        //pickedItem.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        //pickedItem.gameObject.transform.rotation = Quaternion.identity;
                        pickedItem.gameObject.transform.SetParent(hit.collider.gameObject.transform);
                        pickedItem.gameObject.GetComponent<Item>().in_bomb=true;
                        

                        GameObject go = GameObject.FindWithTag("Bomb");
                        PowerCalculator powerCalc = go.GetComponent<PowerCalculator>();
                        powerCalc.items.Add(pickedItem.GetComponent<Item>());
                        ps.Play();
                    }
                    print("ayayay suite case");
                    // put the item inside the 
                    break;
                }
            }

            if(pickedItem){
                pickedItem.gameObject.GetComponent<Item>().selected=false; // that line remove the picked_up color
                pickedItem = null;
            }



        }
    }


    public void OnRightMouseDown(InputAction.CallbackContext context){
     
        
    }
}
