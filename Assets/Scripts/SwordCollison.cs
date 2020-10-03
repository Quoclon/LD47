using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollison : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER");
        if(transform.localScale.x == 0)
        {
            transform.localScale = new Vector3(1, 0, 0);
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
