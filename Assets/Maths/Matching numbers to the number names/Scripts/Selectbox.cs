using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maths.matchingNumbers
{
    public class Selectbox : MonoBehaviour
    {
        public LineRenderer line;
        public GameObject Answeroption;
        private bool answered;
        public GameObject arrow;
        public Color alpanull;
        private void Start()
        {
            line = transform.GetChild(2).GetComponent<LineRenderer>();
        }
        private void OnMouseUpAsButton()
        {
          

        }
        private void OnMouseDown()
        {
            if (answered)
                return;
            Color tmp = arrow.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            arrow.GetComponent<SpriteRenderer>().color = tmp;
           // a = alpanull.al
            Debug.Log("AA");
            GameController.instance.selected_line = line;
        }
        private void Update()
        {
            arrow.transform.position = new Vector3(line.GetPosition(1).x+.1f, line.GetPosition(1).y);
            if (Input.GetMouseButtonUp(0) && !answered)
            {
                if (Vector3.Distance(line.GetPosition(1), Answeroption.transform.GetChild(0).transform.position) <= 1)
                {
                    line.material = GameController.instance.currectmateral;
                    answered = true;
                    GameController.instance.selected_line = null;
                    line.SetPosition(1, Answeroption.transform.GetChild(0).transform.position);
                    Answeroption.transform.GetChild(0).gameObject.SetActive(false);
                    GetComponent<SpriteRenderer>().sprite = GameController.instance.currectanswer;
                    Answeroption.GetComponent<SpriteRenderer>().sprite = GameController.instance.currectAnswerOption;
                    GameController.instance.totalanswered++;

                    if (GameController.instance.totalanswered >= 5)
                    {
                        GameController.instance.ScenecChange();
                    }
                    return;
                }

                for (int i = 0; i < Answeroption.transform.parent.childCount; i++)
                {
                    if (Vector3.Distance(line.GetPosition(1), Answeroption.transform.parent.GetChild(i).transform.GetChild(0).transform.position) <= 1)
                    {
                        line.SetPosition(1, Answeroption.transform.parent.GetChild(i).transform.GetChild(0).transform.position);
                        Answeroption.transform.parent.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                        GetComponent<SpriteRenderer>().sprite = GameController.instance.wronganswer;
                        Answeroption.transform.parent.GetChild(i).GetComponent<SpriteRenderer>().sprite = GameController.instance.wrongansweroption;
                        line.material = GameController.instance.wrongmatrial;
                        StartCoroutine(waitForReset(Answeroption.transform.parent.GetChild(i).gameObject));
                        return;
                    }

                }
                GameController.instance.selected_line = null;
                Color tmp = arrow.GetComponent<SpriteRenderer>().color;
                tmp.a = 0f;
                arrow.GetComponent<SpriteRenderer>().color = tmp;
                line.SetPosition(1, line.GetPosition(0));
            }
        }

        IEnumerator waitForReset(GameObject obj)
        {
            GameController.instance.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            GameController.instance.wrongAnswer_animtion.SetActive(false);
            line.material = GameController.instance.normalmateral;
            obj.transform.GetChild(0).gameObject.SetActive(true);

            GetComponent<SpriteRenderer>().sprite = GameController.instance.selectanswer;
            obj.GetComponent<SpriteRenderer>().sprite = GameController.instance.selectOption;
            GameController.instance.selected_line = null;
            Color tmp = arrow.GetComponent<SpriteRenderer>().color;
            tmp.a = 0f;
            arrow.GetComponent<SpriteRenderer>().color = tmp;
            line.SetPosition(1, line.GetPosition(0));
        }
    }
}