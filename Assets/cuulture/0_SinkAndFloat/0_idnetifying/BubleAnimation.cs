using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BubleAnimation : MonoBehaviour
{

    public Transform sendpos, startpos;
    public float speed;
    void Start()
    {
        //transform.position = startpos.position;

        transform.DOMoveY(sendpos.position.y, speed);
        StartCoroutine(waitforreset());
    }
    IEnumerator waitforreset()
    {
        yield return new WaitForSeconds(speed);
        transform.position = new Vector3(transform.position.x,startpos.position.y,0);
        transform.DOMoveY(sendpos.position.y, speed);
        StartCoroutine(waitforreset());
    }


}
