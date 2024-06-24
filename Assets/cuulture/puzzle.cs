using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle : MonoBehaviour
{
    public puzzle[] swipe;
    public PuzzleSlection allpuzle;
    public Vector3 postion;
    public bool changed;
    private void Start()
    {
       // postion = transform.position;
    }
    private void OnMouseDown()
    {
        allpuzle.selectedpuzzle = this;
    }
    private void OnMouseEnter()
    {
        allpuzle.currectepuzzle = this;
      
    }
    private void OnMouseUp()
    {



    }
}
