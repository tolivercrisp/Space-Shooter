using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Variables --
    public float scrollSpeed;

    private new Renderer renderer;
    private Vector2 savedOffset;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(0, x);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
