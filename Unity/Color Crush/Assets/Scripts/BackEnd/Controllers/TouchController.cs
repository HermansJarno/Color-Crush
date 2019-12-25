using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{
	GridController gridController;
	GraphicRaycaster raycaster;

	private void Start() {
		gridController = FindObjectOfType<GridController>();
		raycaster = FindObjectOfType<GraphicRaycaster>();
	}

    private void Update()
	{
		if (!gridController.DeadLock) // && !GM.GameEnded
		{
			//We check if we have more than one touch happening.
			//We also check if the first touches phase is Ended (that the finger was lifted)
			if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Began))
			{
				List<RaycastResult> rayResults = new List<RaycastResult>();
				PointerEventData ped = new PointerEventData(null);
				ped.position = Input.GetTouch(0).position;
				raycaster.Raycast(ped, rayResults);
				foreach (RaycastResult result in rayResults)
				{
					GameObject resultObj = result.gameObject;
					gridController.PassGameObject(resultObj);
                }
            }

			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				gridController.SubmitSelection();
			}
        }
    }
}
