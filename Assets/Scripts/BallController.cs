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
    public int perfectPass = 0;
    public bool isSuperSpeedActive;

   
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

	private void Update()
	{
		if(perfectPass >= 3 && !isSuperSpeedActive)
		{
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
		}
	}



	private void OnCollisionEnter(Collision target)
	{
        if (ignoreNextCollision)
            return;

		if (isSuperSpeedActive)
		{
			if (!target.transform.GetComponent<Goal>())
			{
                Destroy(target.transform.parent.gameObject);
                Debug.Log("Destroying Platform");
			}
			else
			{
                //Adding ResetLevel functionality via Deathpart - initialized when deathpart is hit;
                DeathPart deathPart = target.transform.GetComponent<DeathPart>();
                if (deathPart)
                    deathPart.HitDeathPart();
            }
		}

        

        
        //Debug.Log("Ball touched something");

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;

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
