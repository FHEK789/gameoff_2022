using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame1Slider : MonoBehaviour
{
    Vector2 spacePushPower = Vector2.zero;
    Rigidbody2D rb2d;
    bool presed = false;
    private void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){presed = true;}
    }
    void FixedUpdate()
    {
        if(presed){
            presed = false;
            rb2d.AddForce(spacePushPower, ForceMode2D.Impulse);
        }
    }
    public void Init(float power){
        spacePushPower = new Vector2(0, power);
    }
}
