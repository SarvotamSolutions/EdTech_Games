using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Laguage.Reading_sentences
{
    public class Drag : DragerForall
    {
        public Sprite selct;
        SpriteRenderer m_render;
        RectTransform m_transform;

      
        public override void Start()
        {
          
            base.Start();


            transform.SetSiblingIndex(Random.Range(0, transform.parent.childCount));
            m_render = GetComponent<SpriteRenderer>();
            m_transform = GetComponent<RectTransform>();
            m_render.size = m_transform.sizeDelta;
        }

        private void OnEnable()
        {
            
        }

        public void sizeseting()
        {
            m_render = GetComponent<SpriteRenderer>();
            m_transform = GetComponent<RectTransform>();
            m_render.size = m_transform.sizeDelta;
        }

        protected override void Update()
        {
            if (m_render.size != m_transform.sizeDelta)
                m_render.size = m_transform.sizeDelta;

            base.Update();
        }
        protected override void OnMouseDown()
        {
            m_render.size = m_transform.sizeDelta;
            if (!GameController.Instance.gamePlay)
                return;


            base.OnMouseDown();
            sound.clip = pickup;
            sound.Play();
            background.sprite = selct;
            GameController.Instance.Boarder.color = GameController.Instance.sellect_answer_color;
            lastpos = transform.position;

        }


        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay || !clicked)
                return;


            base.OnMouseUp();
            sound.clip = drop;
            sound.Play();
            GameController.Instance.Boarder.color = Color.white;
            if (!GameController.Instance.Neartodestination())
            {
                transform.position = lastpos;
            }
        }
    }
}