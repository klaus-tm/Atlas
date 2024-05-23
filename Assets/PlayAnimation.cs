using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launc : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject theRocket;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Launch")){
            theRocket.GetComponent<Animator>().Play("LaunchRocket");
        }
    }
}
