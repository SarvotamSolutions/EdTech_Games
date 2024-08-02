using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DragerForall : MonoBehaviour
{
    public AudioSource sound;
    public AudioSource letterSoundClip;
    public AudioClip pickup, drop,lettersound;
    protected bool clicked;
    public SpriteRenderer Border;
    public SpriteRenderer background;
    public SpriteRenderer Icon;
    public TextMeshPro text;
    public string no;
    private bool canChnagepos;
    public int maxmimumorderlayer;
    public bool TextUpdate = true;
    [HideInInspector]public Vector3 lastpos;


    public virtual void Start()
    {
        //sound = GetComponent<AudioSource>();
        lastpos = transform.position;
        if (text &&  TextUpdate)
            text.text = no;
    }
    protected virtual void OnMouseDown()
    {
        if (GameControllerforAll.Instance.totorial.totorialplaying)
            return;
     
        clicked = true;
        Debug.Log("SELECTION");
        GameControllerforAll.Instance.selectedoption = this;
        sound.clip = pickup;
        sound.Play();
        // AddSetOrderLayer(transform.GetComponent<SpriteRenderer>());
        if (Border)
        {
            Border.color = GameControllerforAll.Instance.sellect_answer_color;
            AddSetOrderLayer(Border);
        }
        if (Icon)
            AddSetOrderLayer(Icon);
        if(background)
        AddSetOrderLayer(background);
        if (text)
            AddSetOrderLayer(text);

    }
  
    protected virtual void OnMouseUp()
    {
    

        if (!clicked || GameControllerforAll.Instance.totorial.totorialplaying)
            return;
       
      
        Debug.Log("selection");
        clicked = false;
       // RemoveSetOrderLayer(transform.GetComponent<SpriteRenderer>());

        if (Border)
        {
         //   Border.color = Color.white;
            RemoveSetOrderLayer(Border);
        }
        if(Icon)
            RemoveSetOrderLayer(Icon);
        if(background)
        RemoveSetOrderLayer(background);
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
