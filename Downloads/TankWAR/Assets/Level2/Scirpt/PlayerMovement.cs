using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isMoving;
 
    [SerializeField] private Vector3 movement;
    [SerializeField] private float speed = 3f;


  
    public float MoveHorizontal;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        this.Animation();
        this.Turning();
     
        
      
    

    }
    public virtual void Move()
    {  
        movement=PlayerController.instance.Player.position;
        
        if (this.IsMoving()&& Input.GetAxis("Horizontal") > 0)
        {
            movement.x += Input.GetAxis("Horizontal")* speed * Time.deltaTime;
        }
        if (this.IsMoving()&& Input.GetAxis("Horizontal") < 0)
        {
            movement.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        }
        
        PlayerController.instance.Player.position = movement;

    }
    public virtual bool IsMoving()
    {
        return Input.GetAxis("Horizontal") != 0;

    }
    public virtual void Animation()
    {

        if (!this.IsMoving())
        {
            this.AniIdle();
            //Debug.Log("IDle");
        }
        else
        {
            this.AniMove(); 
           // Debug.Log("Move");
        }


    }
    protected virtual void AniIdle()
    {
        PlayerController.instance.animatorTank.SetInteger("Stage", 0);
        PlayerController.instance.Exhaust.GetComponent<Animator>().enabled = false;
        PlayerController.instance.Exhaust.GetComponent<Renderer>().enabled = false;
    }
    protected virtual void AniMove()
    {
        PlayerController.instance.Exhaust.GetComponent<Animator>().enabled = true;
        PlayerController.instance.Exhaust.GetComponent<Renderer>().enabled = true;
        PlayerController.instance.animatorTank.SetInteger("Stage", 1);

    }
    protected virtual void Turning()
    {
       // Vector3 scale = PlayerController.instance.Player.localScale;

        if (Input.GetAxis("Horizontal") > 0)
        {
            PlayerController.instance.Player.localScale = new Vector3(1f, 1f, 1f);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            PlayerController.instance.Player.localScale = new Vector3(-1f, 1f, 1f);
        }
        //PlayerController.instance.Player.localScale = scale;



    }
    
}
