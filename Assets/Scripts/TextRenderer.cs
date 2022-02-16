using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRenderer : MonoBehaviour
{
    public string TextValue;
    public Text TextElement;

    // Start is called before the first frame update
    void Start() {
        TextElement.text = TextValue;
    }

    // Update is called once per frame
    void Update()
    {
        TextElement.text = TextValue;
    }
}
