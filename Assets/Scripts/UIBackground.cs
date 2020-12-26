using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var image = GetComponent<Image>();
		float red = image.color.r;
		float green = image.color.g;
		float blue = image.color.b;
		image.color = new Color(red, green, blue, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
