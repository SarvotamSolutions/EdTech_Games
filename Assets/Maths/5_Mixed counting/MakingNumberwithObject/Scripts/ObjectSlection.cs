using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Maths.Number1to10.numberWithObject
{
    public class ObjectSlection : MonoBehaviour
    {

        private SpriteRenderer this_Sprite;

        [SerializeField] private GameObject SpawnObj;
        [SerializeField] private GameObject dropingobject;

        // Start is called before the first frame update
        private void Awake()
        {
            this_Sprite = GetComponent<SpriteRenderer>();

        }
        void Start()
        {

        }


        private void OnMouseDown()
        {
            if (!GameManager.Instance.gamePlay)
                return;
            this_Sprite.sprite = GameManager.Instance.ClickedSprite[GameManager.Instance.IconspriteID];
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            ObjectDraging obj = Instantiate(SpawnObj, pos, Quaternion.identity).GetComponent<ObjectDraging>();
            obj.Normalsprite.sprite = GameManager.Instance.normal_sprite[GameManager.Instance.IconspriteID];
            obj.dropingobj = dropingobject;
        }


    }
}