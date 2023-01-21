/*
 Create By Ray : ray@raymix.net @ 极视教育
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    public Rigidbody2D rigidbodyBird;

    public Animator ani;

    public float force = 100f;

    private bool death = false;

    public delegate void DeathNotify();

    public event DeathNotify OnDeath;

    public UnityAction<int> OnScore;

    private Vector3 initPos;

    // Use this for initialization
    void Start () {
        this.ani = this.GetComponent<Animator>();
        this.Idle();
        initPos = this.transform.position;

    }

    public void Init()
    {
        this.transform.position = initPos;
        this.Idle();
        this.death = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (this.death)
            return;


        if (Input.GetMouseButtonDown(0))
        {
            rigidbodyBird.velocity = Vector2.zero;
            rigidbodyBird.AddForce(new Vector2(0, force), ForceMode2D.Force);
        }
    }


    public void Idle()
    {
        this.rigidbodyBird.simulated = false;
        this.ani.SetTrigger("Idle");
    }

    public void Fly()
    {
        this.rigidbodyBird.simulated = true;
        this.ani.SetTrigger("Fly");
    }


    public void Die()
    {
        this.death = true; 
        if(this.OnDeath!=null)
        {
            this.OnDeath();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D : "  + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        this.Die();
  
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.name.Equals("ScoreArea"))
        {

        }
        else
            this.Die();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("OnTriggerExit2D : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.name.Equals("ScoreArea"))
        {
            if (this.OnScore != null)
                this.OnScore(1);
        }
    }
}
