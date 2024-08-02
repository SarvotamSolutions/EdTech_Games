using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maths.placeValue.inofDroper
{
    public class GameController : Singleton<GameController>
    {
        public Totorial totorial;
        public static GameController instance;
        public GameObject[] dropbox;
        List<int> dropanswerint = new List<int>();

        public Transform Holder;

        public Sprite currectanswer, wronganswer,normlAnswer,normalQuestion;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            for (int i = 0; i < Holder.childCount; i++)
            {
                Holder.GetChild(i).transform.SetSiblingIndex(Random.Range(0, Holder.childCount));
            }
        }
        public bool Neartodestination(GameObject obj)
        {
            Drager drag = obj.GetComponent<Drager>();
            Debug.Log(Vector3.Distance(obj.transform.position, dropbox[drag.no].transform.position));
            if (Vector3.Distance(obj.transform.position, dropbox[drag.no].transform.position) < 3)
            {

                dropbox[drag.no].transform.GetComponent<SpriteRenderer>().sprite = currectanswer;
                dropbox[drag.no].transform.GetComponentInChildren<TextMeshPro>().text = obj.GetComponentInChildren<TextMeshPro>().text;
              //  Transform tempstorefile = obj.transform.parent;
               // obj.transform.parent = dropbox[drag.no].transform;
               // obj.transform.position = dropbox[drag.no].transform.position;
                obj.GetComponent<SpriteRenderer>().sprite = currectanswer;
                Destroy(obj);
                dropanswerint.Add(drag.no);

                Debug.Log(Holder.childCount);
                if(Holder.childCount == 1)
                {
                    StartCoroutine(LevelCompleted());
                }
                return true;
            }
            for (int i = 0; i < dropbox.Length; i++)
            {
                for (int j = 0; j <dropanswerint.Count; j++)
                {
                    if (dropanswerint[j] == i)
                    {
                        return false;
                    }

                }
                if (Vector3.Distance(obj.transform.position, dropbox[i].transform.position) < 1f )
                {
                    gamePlay = false;
                    dropbox[i].transform.GetComponent<SpriteRenderer>().sprite = wronganswer;
                    dropbox[i].transform.GetComponentInChildren<TextMeshPro>().text = obj.GetComponentInChildren<TextMeshPro>().text;

                    obj.gameObject.SetActive(false);
                    //obj.transform.position = dropbox[i].transform.position;
                    StartCoroutine(WrongAnswer(obj,i));
                    return true;
                }
            }
     



            return false;
        }

        IEnumerator WrongAnswer(GameObject obj,int L)
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
            Drager drag = obj.GetComponent<Drager>();
            dropbox[L].transform.GetComponent<SpriteRenderer>().sprite = normalQuestion;
            dropbox[L].transform.GetComponentInChildren<TextMeshPro>().text = "";
            obj.SetActive(true);
            wrongAnswer_animtion.SetActive(false);
            obj.GetComponent<SpriteRenderer>().sprite = normlAnswer;
            obj.transform.position = obj.GetComponent<Drager>().lastpos;
            gamePlay = true;
        }
        IEnumerator LevelCompleted()
        {

            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
    }
}