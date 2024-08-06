using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameCompletedAnimation : MonoBehaviour
{
    public GameObject Uhoh;
    public float speed;

    private void Update()
    {
        Uhoh.transform.Rotate(Vector3.forward * speed * Time.deltaTime); ;
    }
}
