using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_tele : MonoBehaviour
{
    // Start is called before the first frame update
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            PlayerController.instance.transform.position = transform.position;
            Destroy(gameObject);
            
        }
    }
}
