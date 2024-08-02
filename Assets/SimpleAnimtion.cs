using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimtion : MonoBehaviour
{
    public bool right, left, up, down;
    public float POWER;
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
        // dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(0f, 1);
        transform.DOMoveY(transform.position.y + POWER, 1);
        yield return new WaitForSeconds(1);
        //     dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(1f, 1);
        transform.DOMoveY(transform.position.y - POWER, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(downMoveAnimation());
    } 
    
    
    IEnumerator UpMoveAnimation()//Up animtion
    {
        // dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(0f, 1);
        transform.DOMoveY(transform.position.y - POWER, 1);
        yield return new WaitForSeconds(1);
        //     dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(1f, 1);
        transform.DOMoveY(transform.position.y + POWER, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(UpMoveAnimation());
    } 
    
    IEnumerator RightMoveAnimation()//righ animtion
    {
        // dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(0f, 1);
        transform.DOMoveX(transform.position.x + POWER, 1);
        yield return new WaitForSeconds(1);
        //     dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(1f, 1);
        transform.DOMoveX(transform.position.x - POWER, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(RightMoveAnimation());
    } 
    
    
    IEnumerator LeftMoveAnimation()//down animtion
    {
        // dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(0f, 1);
        transform.DOMoveX(transform.position.x - POWER, 1);
        yield return new WaitForSeconds(1);
        //     dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(1f, 1);
        transform.DOMoveX(transform.position.x + POWER, 1);
        yield return new WaitForSeconds(1);
        StartCoroutine(LeftMoveAnimation());
    }

}
