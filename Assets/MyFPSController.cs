using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFPSController : MonoBehaviour
{
    public Vector3 moveVector = new Vector3((float)0.8, 0, 1);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetMouseButton(0)) {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { 
            this.transform.Translate(moveVector * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            this.transform.Translate(-moveVector * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
