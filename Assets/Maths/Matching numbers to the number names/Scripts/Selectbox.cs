using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Maths.matchingNumbers
{
    public class Selectbox : MonoBehaviour
    {
        public LineRenderer line;
        public GameObject Answeroption;
        private bool answered;
        public GameObject arrow;
        public Color alpanull;
        private SpriteRenderer maintexture;
        private void Start()
        {
            line = transform.GetChild(2).GetComponent<LineRenderer>();
            maintexture = GetComponent<SpriteRenderer>();
        }
        private void OnMouseUpAsButton()
        {
          

        }
        private void OnMouseDown()
        {
            if (answered||!GameController.Instance.gamePlay)
                return;

            if (GameController.Instance.textmatch)
            {
                transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.selectedanswer_colors;
            
            }
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();

            arowrender.color =GameController.Instance.selectedanswer_colors;
            Color tmp = arowrender.color;
            tmp.a = 1f;
            arowrender.color = tmp;

            GameController.Instance.selected_line = line;
        }
        private void Update()
        {
            arrow.transform.position = new Vector3(line.GetPosition(1).x+.1f, line.GetPosition(1).y);
            if (GameController.Instance.textmatch && GameController.Instance.selected_line == line)
            {
                arrow.transform.LookAt(line.GetPosition(0));
                for (int i = 0; i < Answeroption.transform.parent.childCount; i++)
                {
                    Transform tempobj = Answeroption.transform.parent.GetChild(i).transform;

                    if (tempobj.GetChild(0).gameObject.activeInHierarchy && 
                        Vector3.Distance(line.GetPosition(1),
                        tempobj.GetChild(0).transform.position) <= 1)
                    {
                       
                        tempobj.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.selectedanswer_colors;
                    }
                    else if(tempobj.GetComponentInChildren<TextMeshPro>().color == GameController.Instance.selectedanswer_colors)
                    {

                       
                        tempobj.GetComponentInChildren<TextMeshPro>().color = Color.white;
                      //  Debug.Log(Answeroption.transform.parent.GetChild(i).GetComponentInChildren<TextMeshPro>().color);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && !answered && GameController.Instance.selected_line == line)
            {
                if (Vector3.Distance(line.GetPosition(1), Answeroption.transform.GetChild(0).transform.position) <= 1)
                {
                    line.material = GameController.Instance.currectmateral;
                    arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.currectanswer_Colors;
                    answered = true;
                    if (GameController.Instance.textmatch)
                    {
                        transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.currectanswer_Colors;
                        Answeroption.transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.currectanswer_Colors;
                    }
                    GameController.Instance.selected_line = null;
                    line.SetPosition(1, Answeroption.transform.GetChild(0).transform.position);
                    Answeroption.transform.GetChild(0).gameObject.SetActive(false);
                    maintexture.sprite = GameController.Instance.currectanswer;
                    Answeroption.GetComponent<SpriteRenderer>().sprite = GameController.Instance.currectAnswerOption;
                    GameController.Instance.totalanswered++;

                    if (GameController.Instance.totalanswered >= 5)
                    {
                        GameController.Instance.ScenecChange();
                    }
                    return;
                }

                for (int i = 0; i < Answeroption.transform.parent.childCount; i++)
                {

                    if (Answeroption.transform.parent.GetChild(i).transform.GetChild(0).gameObject.activeInHierarchy && Vector3.Distance(line.GetPosition(1), Answeroption.transform.parent.GetChild(i).transform.GetChild(0).transform.position) <= 1)
                    {
                        
                        GameController.Instance.gamePlay = false;
                        line.SetPosition(1, Answeroption.transform.parent.GetChild(i).transform.GetChild(0).transform.position);
                        Answeroption.transform.parent.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                        GetComponent<SpriteRenderer>().sprite = GameController.Instance.wronganswer;
                        Answeroption.transform.parent.GetChild(i).GetComponent<SpriteRenderer>().sprite = GameController.Instance.wrongansweroption;
                        line.material = GameController.Instance.wrongmatrial;
                        StartCoroutine(waitForReset(Answeroption.transform.parent.GetChild(i).gameObject));
                        return;
                    }

                }
                GameController.Instance.selected_line = null;
                if (GameController.Instance.textmatch)
                {
                    transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                    Answeroption.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                }
                Color tmp = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                tmp.a = 0f;
                arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
                line.SetPosition(1, line.GetPosition(0));
            }
        }

        IEnumerator waitForReset(GameObject obj)
        {
            GameController.Instance.gamePlay = false;
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
            GameController.Instance.selected_line = null;

            if (GameController.Instance.textmatch)
            { 
                transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.wronganswer_colors;
                obj.transform.GetComponentInChildren<TextMeshPro>().color = GameController.Instance.wronganswer_colors;
            }

            arowrender.color = GameController.Instance.wronganswer_colors;
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
            line.material = GameController.Instance.normalmateral;
            obj.transform.GetChild(0).gameObject.SetActive(true);

            if (GameController.Instance.textmatch)
            {
                transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                obj.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
            }

            GetComponent<SpriteRenderer>().sprite = GameController.Instance.selectanswer;
            obj.GetComponent<SpriteRenderer>().sprite = GameController.Instance.selectOption;
  
            Color tmp = arowrender.color;
            tmp.a = 0f;
            arowrender.color = tmp;
            line.SetPosition(1, line.GetPosition(0));
            GameController.Instance.gamePlay = true;
            GameController.Instance.gamePlay = true;
        }

        public void Reseting()
        {
            line.material = GameController.Instance.normalmateral;
            if (GameController.Instance.textmatch)
            {
                transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
                Answeroption.transform.GetComponentInChildren<TextMeshPro>().color = Color.white;
            }
            answered = false;
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
            Color tmp = arowrender.color;
            tmp.a = 0f;
            arowrender.color = tmp;
            GameController.Instance.selected_line = null;
            Answeroption.transform.GetChild(0).gameObject.SetActive(true);
            line.SetPosition(1,line.GetPosition(0));
        }


    }
}