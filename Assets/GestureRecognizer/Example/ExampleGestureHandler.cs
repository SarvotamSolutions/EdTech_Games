using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Linq;
using TMPro;

public class ExampleGestureHandler : MonoBehaviour
{

    public TextMeshPro textResult;
    public TextMeshProUGUI textResultUI;
    public string notext;
    public int no;

    GesturePatternDraw[] references;

    void Start()
    {
    }

    public void OnRecognize(RecognitionResult result)
    {
        StopAllCoroutines();
        if (result != RecognitionResult.Empty)
        {

            notext += result.gesture.id;
            if (int.TryParse(notext, out no))
            {
                if (textResult)
                {
                    textResult.text = no.ToString();
                }
                else if (textResultUI)
                    textResultUI.text = no.ToString();
            }
            else
            {
                if (textResult)
                {
                    textResult.text = notext;
                }
                else if (textResultUI)
                    textResultUI.text = notext;
            }
        }
        else
        {
            if (textResult && textResult.text == "")
                textResult.text = "?";
            else if (textResultUI)
                textResultUI.text = "?";
        }
    }
}