using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Laguage.Ryming_words.Matching
{
    public class Drager : DragerForall
    {
        public LineRenderer line;
        public GameObject Answeroption;
        public bool answered;
        public GameObject arrow;
  
        private SpriteRenderer statiing_box;

        protected override void OnMouseDown()
        {
            // base.OnMouseDown();
            if (answered|| !GameController.Instance.gamePlay || GameController.Instance.totorial.totorialplaying)
                return;


            sound.PlayOneShot(lettersound);
            GameController.Instance.selected_Line = line;
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
            //arowrender.color = GameController.Instance.blue;
            arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.sellect_answer_color;
            Color tmp = arowrender.color;
            Border.color = GameController.Instance.sellect_answer_color;
            GetComponent<SpriteRenderer>().color = GameController.Instance.sellect_answer_color;
            line.startColor = GameController.Instance.sellect_answer_color;
            line.endColor = GameController.Instance.sellect_answer_color;
            tmp.a = 1f;
            arowrender.color = tmp;

        }
        //private void OnMouseDown()
        //{
        //    if (answered)
        //        return;
        //    GameController.Instance.selected_Line = line;
        //    SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
        //    //arowrender.color = GameController.Instance.blue;
        //    Color tmp = arowrender.color;
        //    tmp.a = 1f;
        //    arowrender.color = tmp;
        //}
        public Color tempcolr, tempColor2;
        // Start is called before the first frame update
        void Start()
        {
            statiing_box = GetComponent<SpriteRenderer>();
            // transform.SetSiblingIndex(Random.Range(0, transform.parent.childCount));

            //  line.SetPosition(0, transform.position);
        }
        
        int nos;
        protected override void Update()
        {
            if (GameController.Instance.totorial.totorialplaying)
                return;
            // base.Update();
            arrow.transform.position = new Vector3(line.GetPosition(1).x + .1f, line.GetPosition(1).y);



            if (GameController.Instance.selected_Line == line && !answered )
            {
                arrow.transform.LookAt(line.GetPosition(0), Vector3.up);
                for (int i = 0; i < GameController.Instance.droping_place.Length; i++)
                {
                    Transform tempobj = GameController.Instance.droping_place[i].transform;

                    if (Vector3.Distance(line.GetPosition(1),
                        tempobj.transform.position) <= 2 && tempobj.GetComponent<SpriteRenderer>().color != GameController.Instance.currect_answer_color)
                    {

                        tempobj.GetComponent<SpriteRenderer>().color = GameController.Instance.sellect_answer_color;

                    }
                    else if (tempobj.GetComponent<SpriteRenderer>().color == GameController.Instance.sellect_answer_color)
                    {


                        tempobj.GetComponent<SpriteRenderer>().color = Color.white;
                        //  Debug.Log(Answeroption.transform.parent.GetChild(i).GetComponentInChildren<TextMeshPro>().color);
                    }
                }
            }//sellectd option

            if (Input.GetMouseButtonUp(0) && !answered && GameController.Instance.selected_Line== line )//currect ansswer
            {

                
                
                if (Vector3.Distance(line.GetPosition(1), Answeroption.transform.position) <= 2)//correct answer
                {
                    sound.PlayOneShot(drop);
                    text.color = Color.white;
                    Answeroption.GetComponentInChildren<TextMeshPro>().color = Color.white;
                    answered = true;
                 // GameController.Instance.gamePlay = false;
                    Border.color = GameController.Instance.currect_answer_color;
                    GetComponent<SpriteRenderer>().color = GameController.Instance.currect_answer_color;
                    Answeroption.GetComponent<SpriteRenderer>().color = GameController.Instance.currect_answer_color;
                    Answeroption.transform.GetChild(3).GetComponent<SpriteRenderer>().color = GameController.Instance.currect_answer_color;
                    GameController.Instance.selected_Line = null;

                    
                    line.SetPosition(1, Answeroption.transform.GetChild(2).transform.position);
      
                    GameController.Instance.reloding++;
                    if (GameController.Instance.reloding >= 4)
                    {
                        GameController.Instance.CurrectAnswer();
                        GameController.Instance.reloding = 0;
                    //   StartCoroutine(GameController.Instance.lev)
                    }
                    return;
                }

                for (int i = 0; i < GameController.Instance.droping_place.Length; i++)
                {

                    if (Vector3.Distance(line.GetPosition(1), GameController.Instance.droping_place[i].transform.position) <= 2 && GameController.Instance.droping_place[i].color != GameController.Instance.currect_answer_color)
                    {
                        sound.PlayOneShot(drop);
                        GameController.Instance.gamePlay = false;
                        answered = true;
                        GameController.Instance.selected_Line = null;
                        Border.color = GameController.Instance.wrong_answer_color;
                        GetComponent<SpriteRenderer>().color = GameController.Instance.wrong_answer_color;
                        GameController.Instance.droping_place[i].GetComponent<SpriteRenderer>().color = GameController.Instance.wrong_answer_color;
                        GameController.Instance.droping_place[i].transform.GetChild(3).GetComponent<SpriteRenderer>().color = GameController.Instance.wrong_answer_color;
                       // arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.wrong_answer_color;
                        GameController.Instance.droping_place[i].transform.GetChild(1).GetComponent<TextMeshPro>().color = Color.white;
                        //statiing_box.sprite = GameController.Instance.wronganswer;
                        nos = i;
                        line.SetPosition(1, GameController.Instance.droping_place[i].transform.GetChild(2).transform.position);
                     //   line.startColor = GameController.Instance.wrong_answer_color;
                     //   line.endColor = GameController.Instance.wrong_answer_color;

                        //        arrow.transform.position = GameController.Instance.iconOption[i].transform.GetChild(0).transform.position;
                        //    Border = GameController.Instance.iconOption[i].transform.GetChild(1).GetComponent<SpriteRenderer>();
                        // border.color = GameController.Instance.red;
                        //     line.material = GameController.Instance.WrongAnswer;
                        //     fromArrow.color = GameController.Instance.red;
                        //     arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().color = GameController.Instance.red;
                        //      arrow.GetComponent<SpriteRenderer>().sprite = GameController.Instance.wronganswerarrow;
                        StartCoroutine(ResetingWrongAnswer());
                        break;
                    }

                }


                if (!answered)
                {
                    GameController.Instance.selected_Line.SetPosition(1, GameController.Instance.selected_Line.GetPosition(0));
                    SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    Color tmp = arowrender.color;
                    tmp.a = 0f;
                    arowrender.color = tmp;
                    Border.color = tempcolr;
                    GetComponent<SpriteRenderer>().color = Color.white;
                    GameController.Instance.selected_Line = null;

                }

            }
        }

        
        // Update is called once per frame
        //void
        //{






        // }

        IEnumerator ResetingWrongAnswer()
        {
            Color tempcolor = text.color;
            text.color = Color.white;
           
            GameController.Instance.wrongAnswer_animtion.SetActive(true);
            GameController.Instance.wrongAnswer_animtion.GetComponent<AudioSource>().PlayDelayed(1f);
            yield return new WaitForSeconds(3);
            text.color = tempcolor;
            GameController.Instance.droping_place[nos].transform.GetChild(1).GetComponent<TextMeshPro>().color = tempcolor;
          //  line.material = GameController.Instance.SelectMaterail;
          //  statiing_box.sprite = GameController.Instance.Selectanswer;
            GameController.Instance.wrongAnswer_animtion.SetActive(false);
            // GameController.Instance.selectedline = null;
            SpriteRenderer arowrender = arrow.transform.GetChild(0).GetComponent<SpriteRenderer>();
            Color tmp = arowrender.color;
            tmp.a = 0f;
            arowrender.color = tmp;
            Border.color = tempcolr;
            GetComponent<SpriteRenderer>().color = Color.white;
            GameController.Instance.droping_place[nos].GetComponent<SpriteRenderer>().color = Color.white;
            GameController.Instance.droping_place[nos].transform.GetChild(3).GetComponent<SpriteRenderer>().color = tempColor2;
            // Answeroption.transform.GetChild(0).gameObject.SetActive(true);
            line.SetPosition(1, line.GetPosition(0));
            GameController.Instance.gamePlay = true;
            answered = false;
        }
    }

}