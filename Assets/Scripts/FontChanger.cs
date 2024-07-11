using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontChanger : MonoBehaviour
{

    public TMP_FontAsset sourceFont;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI[] allTMPComponents = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI tmpComponent in allTMPComponents)
        {
            tmpComponent.font = sourceFont;
        }
    }
}
