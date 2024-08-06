using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public LineRenderer line;
    private List<Transform> pointer = new List<Transform>();
    public Transform[] allpointer;
    public bool Entered;

    private void OnMouseDown()
    {
        pointer.Clear();
        for (int i = 0; i < allpointer.Length; i++)
        {
            pointer.Add(allpointer[i]);
        }
        DragingStarted();
    }
    Coroutine draw_line;


    void DragingStarted()
    {
        Entered = true;
       
        Vector3 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Position.z = 0;
    
        if (Entered)
            StartLine();
    }
    private void OnMouseDrag()
    {
        foreach (var item in pointer)
        {
            if (Vector3.Distance(item.position, line.GetPosition(line.positionCount - 1)) < 1f)
            {
                pointer.Remove(item);
            }
        }
    }
    private void OnMouseUp()
    {
        FinishLine();
    }

    private void OnMouseExit()
    {
        FinishLine();
    }
    void StartLine()
    {
        if (draw_line != null)
            StopCoroutine(draw_line);
        draw_line = StartCoroutine(DrawLine());
    }
    void FinishLine()
    {
        if (draw_line != null)
            StopCoroutine(draw_line);
        line.positionCount = 0;
    }

    IEnumerator DrawLine()
    {
        line.transform.gameObject.SetActive(true);
        while (true)
        {
            Vector3 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Position.z = 0;
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, Position);
            yield return null;
        }
    }
}
