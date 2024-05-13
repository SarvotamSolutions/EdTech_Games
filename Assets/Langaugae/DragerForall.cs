using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragerForall : MonoBehaviour
{
    protected bool clicked;
    public SpriteRenderer Border;
    public SpriteRenderer Icon;
    public TextMeshPro text;
    public string no;
    private bool canChnagepos;
    public int maxmimumorderlayer;
    [HideInInspector]public Vector3 lastpos;


    public virtual void Start()
    {
        lastpos = transform.position;
        if (text)
            text.text = no;
    }
    protected virtual void OnMouseDown()
    {

     
        clicked = true;
        Debug.Log("SELECTION");
        GameControllerforAll.Instance.selectedoption = this;

        AddSetOrderLayer(transform.GetComponent<SpriteRenderer>());
        if (Border)
        {
            Border.color = GameControllerforAll.Instance.sellect_answer_color;
            AddSetOrderLayer(Icon);
            AddSetOrderLayer(Border);
        }

        if (text)
            AddSetOrderLayer(text);

    }
  
    protected virtual void OnMouseUp()
    {

        if (!clicked)
            return;
        Debug.Log("selection");
        clicked = false;
        RemoveSetOrderLayer(transform.GetComponent<SpriteRenderer>());

        if (Border)
        {
         //   Border.color = Color.white;
            RemoveSetOrderLayer(Icon);
            RemoveSetOrderLayer(Border);
        }
        if (text)
            RemoveSetOrderLayer(text);

    }
    protected virtual void OnMouseDrag()
    {
       
    }
    protected virtual void Update()
    {

        if (Input.GetMouseButton(0) && clicked)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
        }


    }

    public void AddSetOrderLayer(SpriteRenderer sprite)
    {

        sprite.sortingOrder += maxmimumorderlayer;
    }
    public void RemoveSetOrderLayer(SpriteRenderer removeingsprite)
    {

        removeingsprite.sortingOrder -= maxmimumorderlayer;
    }
    public void AddSetOrderLayer(TextMeshPro sprite)
    {

        sprite.sortingOrder += maxmimumorderlayer;
    }
    public void RemoveSetOrderLayer(TextMeshPro removeingsprite)
    {

        removeingsprite.sortingOrder -= maxmimumorderlayer;
    }


}
