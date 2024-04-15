using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laguage.beginning_sounds.matchtheSound
{
    public class drager : MonoBehaviour
    {
        public LineRenderer line;
        public GameObject Answeroption;
        private bool answered;
        public GameObject arrow;
        public Color alpanull;
        private void OnMouseDown()
        {
            if (answered)
                return;
            GameController.Instance.selectedline = line;
            Color tmp = arrow.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            arrow.GetComponent<SpriteRenderer>().color = tmp;
        }

        // Start is called before the first frame update
        void Start()
        {
          //  transform.parent.SetSiblingIndex(Random.Range(0, transform.parent.childCount));
        }

        // Update is called once per frame
        void Update()
        {
            arrow.transform.position = new Vector3(line.GetPosition(1).x + .1f, line.GetPosition(1).y);
            if (Input.GetMouseButtonUp(0) && !answered)
            {
                //if (Vector3.Distance(line.GetPosition(1), Answeroption.transform.position) > 1)
                //{
                //    GameController.Instance.selectedline = null;
                //    Color tmp = arrow.GetComponent<SpriteRenderer>().color;
                //    tmp.a = 0f;
                //    arrow.GetComponent<SpriteRenderer>().color = tmp;
                //    line.SetPosition(1, line.GetPosition(0));
                //}
                //else
                if(Vector3.Distance(line.GetPosition(1), Answeroption.transform.position) <= 2)
                {
                    answered = true;
                    GameController.Instance.selectedline = null;
                    line.SetPosition(1, Answeroption.transform.GetChild(0).transform.position);
                    line.material = GameController.Instance.currectanswer;
                    arrow.GetComponent<SpriteRenderer>().sprite = GameController.Instance.currectanswerarrow;
                    //   Answeroption.transform.GetChild(0).gameObject.SetActive(false);
                    //GetComponent<SpriteRenderer>().sprite = GameController.instance.currectanswer;
                    //Answeroption.GetComponent<SpriteRenderer>().sprite = GameController.instance.currectAnswerOption;
                    GameController.Instance.totalanswered++;
                    if (GameController.Instance.totalanswered >= 4)
                    {
                        GameController.Instance.ScenecChange();
                    }
                    return;
                }

                for (int i = 0; i < GameController.Instance.iconOption.Length; i++)
                {

                    if (Vector3.Distance(line.GetPosition(1), GameController.Instance.iconOption[i].transform.position) <= 2)
                    {
                        answered = true;
                        line.SetPosition(1, GameController.Instance.iconOption[i].transform.GetChild(0).transform.position);
                        line.material = GameController.Instance.WrongAnswer;
                        arrow.GetComponent<SpriteRenderer>().sprite = GameController.Instance.wronganswerarrow;
                        StartCoroutine(ResetingWrongAnswer());
                        break;
                    }

                }

                if(!answered)
                {
                    GameController.Instance.selectedline = null;
                    Color tmp = arrow.GetComponent<SpriteRenderer>().color;
                    tmp.a = 0f;
                    arrow.GetComponent<SpriteRenderer>().color = tmp;
                    line.SetPosition(1, line.GetPosition(0));
                }

            }
         
          

         
        }
   
        IEnumerator ResetingWrongAnswer()
        {
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
            GameController.Instance.selectedline = null;
            Color tmp = arrow.GetComponent<SpriteRenderer>().color;
            tmp.a = 0f;
            arrow.GetComponent<SpriteRenderer>().color = tmp;
            line.SetPosition(1, line.GetPosition(0));
            answered = false;
        }
    }
}
