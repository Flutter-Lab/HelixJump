using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{



	private void OnCollisionEnter(Collision target)
	{
		GameManager.singleton.NextLevel();
	}

}
