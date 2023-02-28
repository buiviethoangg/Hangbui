using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocKet : MonoBehaviour
{
    [SerializeField] public float rocketSpeed;
    private Rigidbody2D rocketBody;
    private GameObject rooket;
    public GameObject Explo;
    private void Awake()
    {
        rocketBody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "vatcan")
        {
            
            Instantiate(Explo, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "vatcan")
        {
            
            Instantiate(Explo, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
   

}
