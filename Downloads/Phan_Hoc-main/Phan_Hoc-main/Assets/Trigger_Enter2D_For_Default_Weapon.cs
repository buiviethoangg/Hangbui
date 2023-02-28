using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Enter2D_For_Default_Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            Destroy(gameObject);
            Instantiate(Explosion, transform.position, transform.rotation);
        }
    }
}
