using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Variables --
    [SerializeField]
    private float _scrollSpeed = 0.3f;

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
        float x = Mathf.Repeat(Time.time * _scrollSpeed, 1);
        Vector2 offset = new Vector2(0, x);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
