using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]              //自動幫你創造一個這樣的component,此例為Rigidbody2D

public class TapController : MonoBehaviour {
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float TapForce = 225;
    public float tiltSmooth = 2;
    Rigidbody2D rb;
    Quaternion downRotation;            //面下
    Quaternion forwardRotation;         //面上
    Vector2 startPos = new Vector2(-2.25f,0);                   //default (0,0)
    GameManager1 Game;                  //呼叫GameManager用?
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        Game = GameManager1.Instance;
        rb.simulated = false;
    }

    private void Update()
    {
        if (Game.GameOver){return;}             //死了就不再update 鳥會定住        GaveOver : 一唯讀的布林值
        if (Input.GetMouseButtonDown(0))
        {
            transform.rotation = forwardRotation;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * TapForce, ForceMode2D.Force);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ScoreZone")
        {
            //regester a score event
            OnPlayerScored(); //event send to GameManager
            //play a sound
        }
        if(collision.tag == "DeadZone")
        {
            rb.simulated = false;
            //regester a dead event
            OnPlayerDied(); //event send to GameManager
            //play a sound
        }
    }
    void OnEnable()     //鳥一直都在  所以一值都Enable
    {
        GameManager1.OnGameStarted += OnGameStarted;
        GameManager1.OnGameOverConfirmed += OnGameOverConfirmed;
    }
    void OnDisable()
    {
        GameManager1.OnGameStarted -= OnGameStarted;
        GameManager1.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted(){
        rb.velocity = Vector2.zero;
        rb.simulated = true;
    }
    void OnGameOverConfirmed(){
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }
}
