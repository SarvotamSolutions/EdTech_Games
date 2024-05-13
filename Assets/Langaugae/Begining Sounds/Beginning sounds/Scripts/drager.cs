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
        public SpriteRenderer fromArrow;
       private SpriteRenderer statiing_box;
      

        private void OnMouseDown()
        {
            if (answered)
                return;
            GameController.Instance.selectedline = line;
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
            arowrender.color = GameController.Instance.blue;
            Color tmp = arowrender.color;
            tmp.a = 1f;
            arowrender.color = tmp;
        }

        // Start is called before the first frame update
        void Start()
        {
            statiing_box=GetComponent<SpriteRenderer>();
           // transform.SetSiblingIndex(Random.Range(0, transform.parent.childCount));

            //  line.SetPosition(0, transform.position);
        }
        SpriteRenderer border;
        // Update is called once per frame
        void Update()
        {
            arrow.transform.position = new Vector3(line.GetPosition(1).x + .1f, line.GetPosition(1).y);
         
            

            if (GameController.Instance.selectedline == line && !answered )
            {
                arrow.transform.LookAt(line.GetPosition(0),Vector3.up);
                for (int i = 0; i < GameController.Instance.iconOption.Length; i++)
                {
                    Transform tempobj = GameController.Instance.iconOption[i].transform;
                    
                    if (Vector3.Distance(line.GetPosition(1),
                        tempobj.transform.position) <= 2  && tempobj.GetChild(0).gameObject.activeInHierarchy)
                    {
                       
                        tempobj.GetChild(1).GetComponent<SpriteRenderer>().color = GameController.Instance.blue;

                    }
                    else if (tempobj.GetChild(1).GetComponent<SpriteRenderer>().color == GameController.Instance.blue)
                    {


                        tempobj.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
                        //  Debug.Log(Answeroption.transform.parent.GetChild(i).GetComponentInChildren<TextMeshPro>().color);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && !answered && GameController.Instance.selectedline == line)
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
                    border = Answeroption.transform.GetChild(1).GetComponent<SpriteRenderer>();
                    border.color = GameController.Instance.red;
                    GameController.Instance.selectedline = null;
                    statiing_box.sprite = GameController.Instance.Currectanswer;
                    line.SetPosition(1, Answeroption.transform.GetChild(0).transform.position);
                    Answeroption.transform.GetChild(1).GetComponent<SpriteRenderer>().color 
                        = GameController.Instance.green;
                    line.material = GameController.Instance.currectanswer;
                    arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.green;
                    arrow.GetComponent<SpriteRenderer>().sprite = GameController.Instance.currectanswerarrow;
                    fromArrow.color = GameController.Instance.green;
                    Answeroption.transform.GetChild(0).gameObject.SetActive(false);
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
                        GameController.Instance.selectedline = null;
                        statiing_box.sprite = GameController.Instance.wronganswer;
                        line.SetPosition(1, GameController.Instance.iconOption[i].transform.GetChild(0).transform.position);
                //        arrow.transform.position = GameController.Instance.iconOption[i].transform.GetChild(0).transform.position;
                        border =GameController.Instance.iconOption[i].transform.GetChild(1).GetComponent<SpriteRenderer>();
                        border.color = GameController.Instance.red;
                        line.material = GameController.Instance.WrongAnswer;
                        fromArrow.color = GameController.Instance.red;
                        arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.red;
                        arrow.GetComponent<SpriteRenderer>().sprite = GameController.Instance.wronganswerarrow;
                        StartCoroutine(ResetingWrongAnswer());
                        break;
                    }

                }

                if(!answered)
                {
                    GameController.Instance.selectedline.SetPosition(1, GameController.Instance.selectedline.GetPosition(0));
                    SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    Color tmp = arowrender.color;
                    tmp.a = 0f;
                    arowrender.color = tmp;
                    GameController.Instance.selectedline = null;

                }

            }
         
          

         
        }
   
        IEnumerator ResetingWrongAnswer()
        {
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            line.material = GameController.Instance.SelectMaterail;
            statiing_box.sprite = GameController.Instance.Selectanswer;
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
           // GameController.Instance.selectedline = null;
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
            Color tmp = arowrender.color;
            tmp.a = 0f;
            arowrender.color = tmp;
            border.color = Color.white;
            fromArrow.color = GameController.Instance.vilot;
            Answeroption.transform.GetChild(0).gameObject.SetActive(true);
            line.SetPosition(1, line.GetPosition(0));
            answered = false;
        }
    }
}
