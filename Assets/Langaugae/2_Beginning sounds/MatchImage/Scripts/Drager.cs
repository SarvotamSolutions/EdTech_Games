using UnityEngine;


namespace Laguage.beginning_sounds.match_images
{
    public class Drager : DragerForall
    {
        private bool clicked;
        private bool canChnagepos;
        public GameController controler;
        public Color imagecolor;
      //  public Transform background;
        public int rotionno;

        public override void Start()
        {
            base.Start();
            Icon.color = imagecolor;
        }

        protected override void OnMouseDown()
        {


            if (!GameController.Instance.gamePlay  || GameController.Instance.totorial.totorialplaying)
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
            if (!GameController.Instance.gamePlay || GameController.Instance.totorial.totorialplaying || !clicked)
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
                sound.PlayOneShot(drop);
                letterSoundClip.clip = GameController.Instance.lettersound;
                letterSoundClip.PlayDelayed(.2f);
                GameController.Instance.gamePlay = false;

                GameController.Instance.droping_place[0].enabled = false;
                 transform.position = GameController.Instance.droping_place[0].transform.GetChild(rotionno + 1).transform.position;
               
                transform.parent = GameController.Instance.droping_place[0].transform;
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

                Icon.color = Color.white;
                if (no == GameController.Instance.letter)
                {
                    if (controler.rotate == false)
                    {
                        Border.sprite = GameController.Instance.currect_answer[0];
                    }
                    GameController.Instance.CurrectAnswer();
                }
                else
                {
                    if (controler.rotate == false)
                    {
                        Debug.Log("the Rotaion option set to off");
                        Border.sprite = GameController.Instance.wrong_answer[0];
                    }
                   
                    
                    GameController.Instance.WrongAnswer();
                }
                // gameObject.SetActive(false);
            }
            else
            {
                sound.PlayOneShot(drop);
                Border.color = Color.white;
                Icon.color = imagecolor;
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