using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPos;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;

    public List<Stage> allStage = new List<Stage>();
    private float helixDistane;
    private List<GameObject> spawnedLevels = new List<GameObject>();





    void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistane = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(0);
    }


    void Update()
    {
		if (Input.GetMouseButton(0))
		{
            Vector2 curTapPos = Input.mousePosition;

            if(lastTapPos == Vector2.zero)
			{
                lastTapPos = curTapPos;
			}

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta);
		}

		if (Input.GetMouseButtonUp(0))
		{
            lastTapPos = Vector2.zero;
		}
    }

    public void LoadStage(int stageNumber)
	{
        Stage stage = allStage[Mathf.Clamp(stageNumber, 0, allStage.Count - 1)];

        if(stage == null)
		{
            Debug.LogError("No stage " + stageNumber + " found in allStages List. Are all stages assined in List?");
            return;
		}

        //Change color of background of the stage
        Camera.main.backgroundColor = allStage[stageNumber].stageBackgroundColor;
        //Change color of the ball in stage
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStage[stageNumber].stageBallColor;

        // Reset helix Rotation
        transform.localEulerAngles = startRotation;

        //Destroy the old levels if there are any
        foreach (GameObject go in spawnedLevels)
            Destroy(go);

        // Create new level / platforms
        float levelDistance = helixDistane / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for(int i = 0; i<stage.levels.Count; i++)
		{
            spawnPosY -= levelDistance;
            //Create level in our scene
            GameObject level = Instantiate(helixLevelPrefab, transform);
            Debug.Log("Levels spawned");
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(level);

            
            // Creating the Gapls
            int partsToDisable = 12 - stage.levels[i].partCount;
            List<GameObject> disableParts = new List<GameObject>();

            while(disableParts.Count < partsToDisable)
			{
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
				if (!disableParts.Contains(randomPart))
				{
                    randomPart.SetActive(false);
                    disableParts.Add(randomPart);
				}
			}

            // Creating the deathparts
		}
	}
}
