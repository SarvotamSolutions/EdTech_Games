using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlection : MonoBehaviour
{
    public puzzle selectedpuzzle;
    public puzzle currectepuzzle;


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("ZZZZ" + transform.name);
            Vector3 temppos = selectedpuzzle.transform.position;
           
            
            if (currectepuzzle.changed)
            {
                selectedpuzzle.transform.position = currectepuzzle.transform.position - currectepuzzle.postion;
            }
            else
            {
                selectedpuzzle.transform.position = currectepuzzle.transform.position + currectepuzzle.postion;
            }

            if (selectedpuzzle.changed)
            {
                currectepuzzle.transform.position = temppos - selectedpuzzle.postion;
            }
            else
            {
                currectepuzzle.transform.position = temppos + selectedpuzzle.postion;
            }


            selectedpuzzle.changed = !selectedpuzzle.changed;
            currectepuzzle.changed = !currectepuzzle.changed;
            //allpuzle.selectedpuzzle = null;
           // selectedpuzzle = null;
        }
    }
}
