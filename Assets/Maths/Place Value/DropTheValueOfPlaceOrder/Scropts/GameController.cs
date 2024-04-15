using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maths.placeValue.inofDroper
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public GameObject[] dropbox;

        public Transform Holder;

        public Sprite currectanswer, wronganswer;

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

        
            return false;
        }


        IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);

        }
    }
}