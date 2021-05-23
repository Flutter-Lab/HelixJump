using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{



	private void OnTriggerEnter(Collider target)
	{
		GameManager.singleton.AddScore(2);
		FindObjectOfType<BallController>().perfectPass++;
		Debug.Log("Perfectpass is increased");
	}
}
