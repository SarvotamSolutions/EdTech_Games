using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonInfo : MonoBehaviour
{
    public UnityEvent Events;
    private void OnEnable()
    {
        StartCoroutine(addmoneyanimation());
    }
    IEnumerator addmoneyanimation()
    {
        // dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(0f, 1);
      
        transform.DOMoveY(transform.position.y + .1f, 1);
        yield return new WaitForSeconds(1);
        //     dropcoinfliker.GetComponent<SpriteRenderer>().DOFade(1f, 1);
        transform.DOMoveY(transform.position.y -.1f, 1);
       
        yield return new WaitForSeconds(1);
        StartCoroutine(addmoneyanimation());
    }
    public void OnMouseUpAsButton()
    {
   
        Events.Invoke();

    }
}
