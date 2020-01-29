using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : Block
{
    private new IceBlockUI blockUI;

    public IceBlock (int x, int y) : base(x, y, BlockType.Ice) {
        lifes = 3;
        CreateUI();
    }
    
    public override void CreateUI(){
        GameObject column = GameObject.Find("Column_" + x);
        GameObject blockPrefab = Resources.Load("IceBlock") as GameObject;

        GameObject instance = MonoBehaviour.Instantiate(blockPrefab, new Vector3(0, y * blockOffset, 0), blockPrefab.transform.rotation) as GameObject;
		instance.transform.SetParent(column.transform, false);
		instance.name = string.Format("{0}{1}", x, y);

        blockUI = instance.GetComponent<IceBlockUI>();
        blockUI.Initialize(this);
        blockUI.CreateIndex(x,y);
        linkedUIGameObject = blockUI.gameObject;
    }

    public override void LowerLifes(){
        lifes--;
        blockUI.UpdateImage(lifes);
    }

    public override void DeleteMyself(){
        blockUI.DeleteMyself();
    }

    public int Lifes{
        get{
            return lifes;
        }
    }
}
