using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScrollingTexture : MonoBehaviour {
    private Vector2 ScrollSpeed = new Vector2(0.2f, 0.2f);
 
    private void OnEnable() {
        GetComponent<SpriteRenderer>().material.SetVector("_ScrollSpeed", ScrollSpeed);
    }
}