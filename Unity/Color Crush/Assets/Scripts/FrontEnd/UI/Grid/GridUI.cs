using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour
{
    private GameObject[] columns;
    private GameObject root;
    private GameObject columnPrefab;

    private float yOffset = 50;

    private float xOffset = 85; //85 percent of the width of a block

    private float extraOffset = 100;

    public void Initialize (Grid grid){
        columnPrefab = Resources.Load("Column") as GameObject;
        root = this.gameObject;

        float gridXOffset = -((xOffset * (grid.XLength - 1)) / 2);
        float gridYOffset = -((xOffset * (grid.YLength - 1)) / 2) - extraOffset; // temporary test with extra offset

        root.transform.localPosition = new Vector3(gridXOffset, gridYOffset, 0);

        for (int i = 0; i < grid.XLength; i++)
        {
            if (i % 2 == 0) InitializeColumn(i, 0);
            else InitializeColumn(i, yOffset);
        }
    }

    private void InitializeColumn(int index, float offset){
		GameObject instance = Instantiate(columnPrefab, new Vector3(index * xOffset, offset, 0), columnPrefab.transform.rotation) as GameObject;
		instance.transform.SetParent(root.transform, false);
		instance.name = string.Format("{0}{1}", "Column_", index);
    }
}
