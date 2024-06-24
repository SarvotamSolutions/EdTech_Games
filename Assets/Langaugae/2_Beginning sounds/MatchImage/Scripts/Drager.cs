using UnityEngine;


namespace Laguage.beginning_sounds.match_images
{
    public class Drager : DragerForall
    {
        private bool clicked;
        private bool canChnagepos;
        public GameController controler;
      //  public Transform background;
        public int rotionno;

        public override void Start()
        {
            base.Start();
        }

        protected override void OnMouseDown()
        {

            if (!GameController.Instance.gamePlay)
                return;
            clicked = true;
           // transform.GetComponent<SpriteRenderer>().sprite = controler.alldirectionanswer[0];
           // transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = controler.border[0];
            //GameController.Instance.droping_place.sortingOrder = 0;
            foreach (var item in GameController.Instance.droping_place)
            {
                item.color = GameController.Instance.sellect_answer_color;
            }
            GameController.Instance.droping_place[0].color = GameController.Instance.sellect_answer_color;
            Border.color = GameController.Instance.sellect_answer_color;
            base.OnMouseDown();
        }

        //public override void OnMouseDown()
        //{
        //    clicked = true;
        //    GameController.Instance.droping_place.sortingOrder = 0;
        //    GameController.Instance.droping_place.color = GameController.Instance.selected;
        //    GameController.Instance.dropingoutline.color = GameController.Instance.selected;
        //    base.OnMouseDown();
        //}

        protected override void OnMouseUp()
        {
            if (!GameController.Instance.gamePlay)
                return;
            //GameController.Instance.droping_place.sortingOrder = 2;

            foreach (var item in GameController.Instance.droping_place)
            {
                item.color = Color.white;
            }
            //   GameController.Instance.droping_place.color =Color.white;
            
            clicked = false;
            if (GameController.Instance.Neartodestination())
            {
                GameController.Instance.gamePlay = false;

               
                 transform.position = GameController.Instance.droping_place[0].transform.GetChild(rotionno + 1).transform.position;
               
                transform.parent = GameController.Instance.droping_place[0].transform;
                transform.localScale = new Vector3(.9f, .9f, .9f);
                if (no == GameController.Instance.letter)
                {
                    Border.color = GameController.Instance.currect_answer_color;
                    GameController.Instance.CurrectAnswer();
                }
                else
                {
                    Border.color = GameController.Instance.wrong_answer_color;
                    Debug.Log("bORDER");
                    
                    GameController.Instance.WrongAnswer();
                }
                // gameObject.SetActive(false);
            }
            else
            {
                Border.color = Color.white;
                // transform.GetComponent<SpriteRenderer>().sprite = controler.alldirectionanswer[rotionno];
                //  transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = controler.border[rotionno];
                transform.position = lastpos;
            }
            base.OnMouseUp();

        }

        protected override void OnMouseDrag()
        {
            //if (GameController.Instance.Neartodestination(this.gameObject))
            //{

            //    // gameObject.SetActive(false);
            //}
        }
        private void Update()
        {
            if (!GameController.Instance.gamePlay)
                return;
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }

    }
}