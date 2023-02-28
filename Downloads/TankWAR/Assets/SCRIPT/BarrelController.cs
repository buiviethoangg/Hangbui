using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarrelController : MonoBehaviour
{
    [SerializeField] public float rotalteY;
   [SerializeField] public float speed = 10f;
    public int Angles;
    public float Rad_Angles;
   [SerializeField] public Transform ShootTip;
    [SerializeField]public GameObject rocket;
    float FireRate = 0.5f;
    public float NextFire = 0.1f;
    public TMPro.TMP_Text Show_power;
  
    // Start is called before the first frame update
    public Image[] PowerPoint;
    public float maxPower = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotett();
        Attack();
        HealthBarFiller();

    }

    public void Rotett()
    {
        float Inputy = Input.GetAxis("Vertical");
        if (Inputy > 0)
        {
            transform.Rotate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
            
        }
        if (Inputy < 0)
        {
            transform.Rotate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
            
        }
        Angles = (int)transform.eulerAngles.z;
        Rad_Angles = Angles * Mathf.Deg2Rad;

        Debug.Log("ang" + Angles);
        Debug.Log("Quaterni" + transform.rotation);

    }
   

    public float power,V=0;
    protected virtual void Attack()
    {
        if (Input.GetButton("Jump"))
        {

            power += (15 * Time.deltaTime);

        }
        V = power;
        if (!Input.GetButton("Jump") && V > 1)
        {
            //Attacking();
            //
            Fire1();

            V = 0;
            power = 0;


        }
        Show_power.text = $"{(int)V}";


    }


    public void Fire1()
    {
        // tạo lực bắn ra vien đạn.
        GameObject B = Instantiate(rocket, ShootTip.position, Quaternion.identity);
        Vector3 Force = Vector3.zero;
        //if (PlayerController.instance.transform.localScale.x == -1)
        //{

        //    Force.x = -V * 50 * Mathf.Cos(Rad_Angles);
        //    Force.y = V * 50 * Mathf.Sin(Rad_Angles);// cái y không cần -gt^2/2 nữa bởi nó có 
        //                                          //ridgridbody cho trọng lực rồi.
        //}
        //if (PlayerController.instance.transform.localScale.x == 1)
        //{
            Force.x = V * 10 * Mathf.Cos(Rad_Angles);
            Force.y = V * 10 * Mathf.Sin(Rad_Angles);// cái y không cần -gt^2/2 nữa bởi nó có 
                                                    //ridgridbody cho trọng lực rồi.
       // }


        B.GetComponent<Rigidbody2D>().AddForce(Force);
    }
    bool DisplayPowerPoint(float x, int pointNumber)
    {
        return ((pointNumber * 14) >= power);
    }
    void HealthBarFiller()
    {


        for (int i = 0; i < PowerPoint.Length; i++)
        {
            PowerPoint[i].enabled = !DisplayPowerPoint(power, i);
        }
    }

}
