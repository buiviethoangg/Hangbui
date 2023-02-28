using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerAttack playerat;
    //
    public Transform ani_Attack_barrel;// diễn hoạt hình cho cái barrel.
    public Transform ani_Shot_attack;// diễn hoạt hình cho khói súng
    public Transform Barrel;//Ví trị gameobj theo súng để t có thể quay.==Berrel


    
    public bool ispowering=false;
    public bool canattack = false;
    public float timer_attack = 0;
    
    public bool isAttack=false;
    public float couter = 0;// biến điểm thời gian diễn hoạt cảnh
   
    public float time_Reset=30; 
    public float count_time_Reset=0;
    //
    public TMP_Text txt;
    public float power,V;
    public int Angle;
    float Angle_Rad;
    public float R = 4;
    public GameObject Bullet;// phải là 1 list. hoặc là list của cái khác và sau đó gán cho bullet 
                                                                            // đều oke.
    public Transform point_to_fire;

    bool turn_on;
    public void Start()
    {
        turn_on = true;
    }
    // Update is called once per frame
    float rotationSpeed = 10;
    private void Awake()
    {
        if (PlayerAttack.playerat != null) return;
        PlayerAttack.playerat = this;
    }
    private void FixedUpdate()
    {
       // txt.text = $"{Barrel.eulerAngles.z}"; 

        
    }
    void Update()
    {


        // AttackDelay();
      Attack();
        Rotation_Barrel();
        /* if (this.Lost_turn()&&!ispowering)
             Finish_Attack();*/
        


    }
  
    protected virtual void Rotation_Barrel()
    {
        if (PlayerController.instance.transform.localScale.x == 1)
        {

            if (Input.GetKey(KeyCode.UpArrow))//&& Angle < 90
            {
                Barrel.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotationSpeed);

            }
            if (Input.GetKey(KeyCode.DownArrow))//&& Angle > 0
            {
                Barrel.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * rotationSpeed);
            }

          //  txt.text = $"{(int)Barrel.eulerAngles.z}";
            Angle = (int)Barrel.eulerAngles.z;
        }

        if (PlayerController.instance.transform.localScale.x == -1)
        {

            if (Input.GetKey(KeyCode.UpArrow))//&& Angle < 90
            {
                Barrel.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotationSpeed);

            }
            if (Input.GetKey(KeyCode.DownArrow))//&& Angle > 0 && (int)Barrel.eulerAngles.z < 359
            {
                Barrel.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * rotationSpeed);
            }

            Angle = 360 - (int)Barrel.eulerAngles.z;
            //txt.text = $"{Angle}";


        }
        Angle_Rad = Angle * Mathf.Deg2Rad;
    }
 /*   protected virtual void AttackDelay()
    {
        timer_attack += Time.deltaTime;
        if (timer_attack > 3)
        {
            canattack = true;
        }
        if (canattack == true)
        { 
            Attack(); 
        
        }
        
            
    }*/
    protected virtual void Attack()
    {
        if (Input.GetButton("Jump"))
        {
          
                power += (10 * Time.deltaTime);
           
        }
        V = power;
        if (!Input.GetButton("Jump") && V>1)
        {
            //Attacking();
           //
           Fire1();
            Finish_Attack();
           /* V = 0;
            power = 0;*/
            
            
        }
        txt.text = $"{V}";

    }

    /*  protected virtual void Attacking() //diễn ani trong vong 1s.
      {

          couter += Time.deltaTime;
          Animation_attack_Barrel_true();
          if (couter >= 0.5f)
          {
              Animation_attack_Barrel_false();
              Fire1();
              Finish_Attack();
          }


      }*/

    public void Fire1()
    {
        // tạo lực bắn ra vien đạn.
        GameObject B = Instantiate(Bullet, point_to_fire.position, Quaternion.identity);
        Vector3 Force = Vector3.zero;
        if (PlayerController.instance.transform.localScale.x == -1)
        {

            Force.x = -V * 50 * Mathf.Cos(Angle_Rad);
            Force.y = V * 50 * Mathf.Sin(Angle_Rad);// cái y không cần -gt^2/2 nữa bởi nó có 
                                                    //ridgridbody cho trọng lực rồi.
        }
        if (PlayerController.instance.transform.localScale.x == 1)
        {
            Force.x = V * 50 * Mathf.Cos(Angle_Rad);
            Force.y = V * 50 * Mathf.Sin(Angle_Rad);// cái y không cần -gt^2/2 nữa bởi nó có 
                                                    //ridgridbody cho trọng lực rồi.
        }



        B.GetComponent<Rigidbody2D>().AddForce(Force);
    }

  

    protected virtual void Finish_Attack()//reset
    {
        //this.canattack = true;
        /*this.timer_attack = 0;
        this.ispowering = false;
        this.canattack = false;
        
        this.couter = 0;
        this.count_time_Reset = 0;*/
        this.V = 0;
        this.power = 0;
       


    }
    /*
        public virtual bool Lost_turn()
        {
            count_time_Reset += Time.deltaTime;

            return count_time_Reset > time_Reset ? true : false;
        }

        protected virtual void Animation_attack_Barrel_true()
        {
            ani_Attack_barrel.GetComponent<Animator>().enabled = true;
            ani_Shot_attack.GetComponent<Animator>().enabled = true;
            ani_Shot_attack.GetComponent<Renderer>().enabled = true;
        }
        protected virtual void Animation_attack_Barrel_false()
        {
            ani_Attack_barrel.GetComponent<Animator>().enabled = false;
            ani_Shot_attack.GetComponent<Animator>().enabled = false;

            ani_Shot_attack.GetComponent<Renderer>().enabled = false;

        }
    */

    public int Trajectory_num = 100;
    private void OnDrawGizmos()
    {
        if (turn_on == true)
        {
            Vector2 Tam = new Vector2(PlayerController.instance.transform.position.x, PlayerController.instance.transform.position.y);

            Gizmos.color = Color.red;
            for (int i = 0; i < R * 2; i++)
            {


                float x = -R + i + PlayerController.instance.transform.position.x;
                float y = Mathf.Abs(Mathf.Sqrt(R * R - Mathf.Pow(x - Tam.x, 2))) + Tam.y;
                Vector3 pos1 = new Vector3(x, y, 0);


                x = x + 1;
                y = Mathf.Abs(Mathf.Sqrt(R * R - Mathf.Pow(x - Tam.x, 2))) + Tam.y;
                Vector3 pos2 = new Vector3(x, y, 0);
                Gizmos.DrawLine(pos1, pos2);


                float x1 = -R + i + PlayerController.instance.transform.position.x;
                float y1 = -Mathf.Abs(Mathf.Sqrt(R * R - Mathf.Pow(x1 - Tam.x, 2))) + Tam.y;
                Vector3 pos3 = new Vector3(x1, y1, 0);


                x1 = x1 + 1;
                y1 = -Mathf.Abs(Mathf.Sqrt(R * R - Mathf.Pow(x1 - Tam.x, 2))) + Tam.y;
                Vector3 pos4 = new Vector3(x1, y1, 0);
                Gizmos.DrawLine(pos3, pos4);

            }

            for (int i = 0; i < Trajectory_num; i++)
            {
                if (PlayerController.instance.transform.localScale.x == 1)
                { 
                    float time = i * 0.1f;
                    float X = V * Mathf.Cos(Angle_Rad) * time;
                    float Y = V * Mathf.Sin(Angle_Rad) * time - 0.5f * (10 * time * time);

                    Vector3 pos1 = PlayerController.instance.transform.position + new Vector3(X, Y, 0);

                    time = (i + 1) * 0.1f;
                    X = V * Mathf.Cos(Angle_Rad) * time;
                    Y = V * Mathf.Sin(Angle_Rad) * time - 0.5f * (10 * time * time);

                    Vector3 pos2 = PlayerController.instance.transform.position + new Vector3(X, Y, 0);
                    Gizmos.DrawLine(pos1, pos2);
                }
                if (PlayerController.instance.transform.localScale.x == -1)
                { 
                    float time = i * 0.1f;
                    float X =- V * Mathf.Cos(Angle_Rad) * time;
                    float Y = V * Mathf.Sin(Angle_Rad) * time - 0.5f * (10 * time * time);

                    Vector3 pos1 = PlayerController.instance.transform.position + new Vector3(X, Y, 0);

                    time = (i + 1) * 0.1f;
                    X = -V * Mathf.Cos(Angle_Rad) * time;
                    Y =V * Mathf.Sin(Angle_Rad) * time - 0.5f * (10 * time * time);

                    Vector3 pos2 = PlayerController.instance.transform.position + new Vector3(X, Y, 0);
                    Gizmos.DrawLine(pos1, pos2);
                }
                
            }
            if (PlayerController.instance.transform.localScale.x == 1)
            { 
                float xx = Mathf.Cos(Angle_Rad) * (R) + PlayerController.instance.transform.position.x;
                float yx = Mathf.Sin(Angle_Rad) * (R) + PlayerController.instance.transform.position.y;
                Gizmos.DrawLine(PlayerController.instance.transform.position, new Vector3(xx, yx, 0));
            }
            if (PlayerController.instance.transform.localScale.x == -1)
            { 
                float xx = -Mathf.Cos(Angle_Rad) * (R) + PlayerController.instance.transform.position.x;
                float yx = Mathf.Sin(Angle_Rad) * (R) + PlayerController.instance.transform.position.y;
                Gizmos.DrawLine(PlayerController.instance.transform.position, new Vector3(xx, yx, 0));
            }
                
        }
        
    }








}
