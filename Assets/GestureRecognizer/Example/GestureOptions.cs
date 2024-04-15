using System.Collections;
using System.Collections.Generic;
using GestureRecognizer;
using UnityEngine;
using UnityEngine.UI;

public class GestureOptions : MonoBehaviour
{


  //  public Text textTime;

    public DrawDetector[] detectors;

    public Recognizer recognizer;

    void OnEnable()
    {
        foreach (var detector in detectors)
        {
            detector.OnRecognize.AddListener(RecognitionResult);
        }
    }
    void OnDisable()
    {
        if (Application.isPlaying)
        {
            foreach (var detector in detectors)
            {
                detector.OnRecognize.RemoveListener(RecognitionResult);
            }
        }
    }

    void RecognitionResult(RecognitionResult result)
    {
	//	textTime.text = string.Format("{0:0.000}s",result.recognitionTime);
    }

    public void OnChangeMinMax()
    {
        int min = 1;
        int max = 4;
        foreach (var detector in detectors)
        {
            detector.MinLines = min;
            detector.MaxLines = max;
            detector.ClearLines();
        }
    }
    public void OnChangeThreads()
    {
        int n_threads = 1;
        recognizer.numberOfThreads = n_threads;
    }

}
