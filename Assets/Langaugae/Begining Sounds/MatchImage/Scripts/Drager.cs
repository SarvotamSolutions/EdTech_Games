using UnityEngine;


namespace Laguage.beginning_sounds.match_images
{
    public class Drager : MonoBehaviour
    {
        private bool clicked;
        public string no;
        private bool canChnagepos;
        public Vector3 lastpos;

        private void Start()
        {
            lastpos = transform.position;
        }

        private void OnMouseDown()
        {
            clicked = true;
            GameController.Instance.droping_place.sortingOrder = 0;
            GameController.Instance.droping_place.color = GameController.Instance.selected;
            GameController.Instance.dropingoutline.color = GameController.Instance.selected;
          
        }

        private void OnMouseUp()
        {
            GameController.Instance.droping_place.sortingOrder = 2;
            GameController.Instance.droping_place.color =Color.white;
            GameController.Instance.dropingoutline.color = Color.white;
            clicked = false;
            if (GameController.Instance.Neartodestination(this.gameObject))
            {
                transform.position = GameController.Instance.droping_place.transform.position;
                transform.parent = GameController.Instance.droping_place.transform;
                if(no == GameController.Instance.lettter)
                {
                    GameController.Instance.CurrectAnswer();
                }
                else
                {
                    GameController.Instance.WrongAnswer();
                }
               // gameObject.SetActive(false);
            }
            else
                transform.position = lastpos;

        }

        private void OnMouseDrag()
        {
            if (GameController.Instance.Neartodestination(this.gameObject))
            {

                // gameObject.SetActive(false);
            }
        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && clicked)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                transform.position = pos;
            }


        }

    }
}