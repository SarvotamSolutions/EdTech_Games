using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maths.placeValue.inofDroper
{
    public class GameController : Singleton<GameController>
    {
        public static GameController instance;
        public GameObject[] dropbox;

        public Transform Holder;

        public Sprite currectanswer, wronganswer,normlAnswer;

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
            if (Vector3.Distance(obj.transform.position, dropbox[drag.no].transform.position) < 1)
            {
            
              //  Transform tempstorefile = obj.transform.parent;
                obj.transform.parent = dropbox[drag.no].transform;
                obj.transform.position = dropbox[drag.no].transform.position;
                obj.GetComponent<SpriteRenderer>().sprite = currectanswer;

                if(Holder.childCount == 0)
                {
                    StartCoroutine(LevelCompleted());
                }
                return true;
            }
            for (int i = 0; i < dropbox.Length; i++)
            {
                if (Vector3.Distance(obj.transform.position, dropbox[i].transform.position) < 1)
                {
                    gamePlay = false;
                    obj.GetComponent<SpriteRenderer>().sprite = wronganswer;
                    obj.transform.position = dropbox[i].transform.position;
                    StartCoroutine(WrongAnswer(obj));
                    return true;
                }
            }
     



            return false;
        }

        IEnumerator WrongAnswer(GameObject obj)
        {
            wrongAnswer_animtion.SetActive(true);
            yield return new WaitForSeconds(2);
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