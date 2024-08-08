using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimtion : MonoBehaviour
{
    public bool right, left, up, down;
    public float POWER;
    public float duration = 1;
    private void Start()
    {
        if (down)
            StartCoroutine(downMoveAnimation());
        if (up)
            StartCoroutine(UpMoveAnimation());
        if(right)
            StartCoroutine(RightMoveAnimation());
        if(left)
            StartCoroutine(LeftMoveAnimation());
    }
    IEnumerator downMoveAnimation()//down animtion
    {
        transform.DOMoveY(transform.position.y + POWER, duration);
        yield return new WaitForSeconds(duration);
        transform.DOMoveY(transform.position.y - POWER, duration);
        yield return new WaitForSeconds(duration);
        StartCoroutine(downMoveAnimation());
    } 
    
    
    IEnumerator UpMoveAnimation()//Up animtion
    {
        transform.DOMoveY(transform.position.y - POWER, duration);
        yield return new WaitForSeconds(duration);
        transform.DOMoveY(transform.position.y + POWER, duration);
        yield return new WaitForSeconds(duration);
        StartCoroutine(UpMoveAnimation());
    } 
    
    IEnumerator RightMoveAnimation()//righ animtion
    {
        transform.DOMoveX(transform.position.x + POWER, duration);
        yield return new WaitForSeconds(duration);
        transform.DOMoveX(transform.position.x - POWER, duration);
        yield return new WaitForSeconds(duration);
        StartCoroutine(RightMoveAnimation());
    } 
    
    
    IEnumerator LeftMoveAnimation()//down animtion
    {
        transform.DOMoveX(transform.position.x - POWER, duration);
        yield return new WaitForSeconds(duration);
        transform.DOMoveX(transform.position.x + POWER, duration);
        yield return new WaitForSeconds(duration);
        StartCoroutine(LeftMoveAnimation());
    }

}
