using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageSync : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public UnityEngine.UI.Image img;

    // Update is called once per frame
    void Update()
    {
        img.sprite = spriteRenderer.sprite;
    }
}
