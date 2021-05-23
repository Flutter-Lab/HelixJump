using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private bool ignoreNextCollision;
    private Rigidbody rb;
    [SerializeField]
    private float impulseForce = 5f;

    private Vector3 startPos;

   
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }


    void Update()
    {
        
    }


	private void OnCollisionEnter(Collision target)
	{
        if (ignoreNextCollision)
            return;

        //Adding ResetLevel functionality via Deathpart - initialized when deathpart is hit;
        DeathPart deathPart = target.transform.GetComponent<DeathPart>();
        if (deathPart)
            deathPart.HitDeathPart();

        
        //Debug.Log("Ball touched something");

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

	}



    private void AllowCollision()
	{
        ignoreNextCollision = false;
	}


    public void ResetBall()
	{
        transform.position = startPos;
	}
}
