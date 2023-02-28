using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Scriptmenager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tele;
    void Start()
    {
        this.tele = GameObject.Find("tele");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClicktele()
    {
        PlayerAttack.playerat.Bullet = tele;
        Debug.Log(tele);
        Debug.Log(PlayerAttack.playerat.Bullet.name);
    }
    
}
