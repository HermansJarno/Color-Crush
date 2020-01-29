using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineRenderController : MonoBehaviour
{

	public LineRenderer lineRenderer;
	public float lineDrawSpeed = 6f;

	public Color colorRed = Color.red;
	public Color colorGreen = Color.green;
	public Color colorBlue = Color.blue;
	public Color colorYellow = Color.yellow;
	public Color colorCyan = Color.cyan;
	public Color colorMagenta = Color.magenta;

	private void Start()
	{
		lineRenderer.sortingOrder = 4;
		lineRenderer.sortingLayerName = "UI";
	}

	public void UpdatePoints(Block[] blocks, Vector3 touchPoint){
		if(blocks.Length == 1){
			SetLineColor(blocks[0]);
		}
		if(blocks.Length > 1){
			lineRenderer.positionCount = blocks.Length;
			if (touchPoint != Vector3.zero)
			{
				touchPoint = Camera.main.ScreenToWorldPoint(touchPoint);
				lineRenderer.positionCount++;
				lineRenderer.SetPosition(0, touchPoint);
			}
			for (int i = 1; i <= blocks.Length; i++)
			{
				lineRenderer.SetPosition(i, blocks[i - 1].GetLinkedUIGameObject().transform.position);
			}	
		}
		else if (blocks.Length > 0 && touchPoint != Vector3.zero)
		{
			touchPoint = Camera.main.ScreenToWorldPoint(touchPoint);
			lineRenderer.positionCount = blocks.Length + 1;
			lineRenderer.SetPosition(0, touchPoint);
			for (int i = 1; i <= blocks.Length; i++)
			{
				lineRenderer.SetPosition(i, blocks[i - 1].GetLinkedUIGameObject().transform.position);
			}
		}
		else
		{
			ClearPoints();
		}
	}

	public void ClearPoints(){
		lineRenderer.positionCount = 0;
	}

	private void SetLineColor(Block block){
		switch(block.ColorType){
			case ColorType.Red:
				lineRenderer.startColor = colorRed;
				lineRenderer.endColor = colorRed;
				break;
			case ColorType.Green:
				lineRenderer.startColor = colorGreen;
				lineRenderer.endColor = colorGreen;
				break;
			case ColorType.Blue:
				lineRenderer.startColor = colorBlue;
				lineRenderer.endColor = colorBlue;
				break;
			case ColorType.Yellow:
				lineRenderer.startColor = colorYellow;
				lineRenderer.endColor = colorYellow;
				break;
			case ColorType.Cyan:
				lineRenderer.startColor = colorCyan;
				lineRenderer.endColor = colorCyan;
				break;
			case ColorType.Magenta:
				lineRenderer.startColor = colorMagenta;
				lineRenderer.endColor = colorMagenta;
				break;
			default:
				break;
		}
	}
}
