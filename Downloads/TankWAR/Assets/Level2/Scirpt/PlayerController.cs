using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    //public PlayerMovement playerMovement;
    public Transform playerAttack;
    public Animator animatorTank;
    public Animator animatorExhaust;
    //public Renderer Exhaust;
    public Transform Exhaust;
    public Transform Player;



    // Start is called before the first frame update
    public void Awake()
    {
        if (PlayerController.instance != null) return;
        PlayerController.instance = this;
    }
    /*void Start()
    {
        
    }*/

    // Update is called once per frame

    public void Reset()
    {
        LoadCharacter();
    }
    public virtual void LoadCharacter()
    {

        this.Player = this.transform;
        this.animatorTank = this.transform.Find("Model").GetComponent<Animator>();
        this.animatorExhaust = this.transform.Find("Exhaust").GetComponent<Animator>();
        //this.playerMovement = this.transform.Find("PlayerMovement").GetComponent<PlayerMovement>();
        this.Exhaust = this.transform.Find("Exhaust");
        this.playerAttack = this.transform.Find("PlayerAttack");
    }

    private void OnDrawGizmos()
    {
      
    }
}
