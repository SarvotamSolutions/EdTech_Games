using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameCompletedAnimation : MonoBehaviour
{
    public GameObject Uhoh;
    public float speed;
    //private void OnEnable()
    //{
    //    //Uhoh.transform.DOLocalMoveY(200f, .75f);
    //    Vector3 jusmp = new Vector3(0, 200f, 0);

    //    Uhoh.transform.DOLocalJump(jusmp, 200f, 3, 2f);
    //}
    private void Update()
    {
        Uhoh.transform.Rotate(Vector3.forward *speed* Time.deltaTime); ;
    }
}
