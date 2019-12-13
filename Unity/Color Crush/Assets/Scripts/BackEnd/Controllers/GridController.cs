using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    Grid grid = new Grid();

    private void Start() {
        grid.Initialize(5,5);
    }
}
