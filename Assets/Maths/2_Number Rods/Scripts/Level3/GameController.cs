
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
namespace Maths.NumberRoads.Making_10_with_Number_roads
{

    public class GameController : Singleton<GameController>
    {
        public GameObject[] dropplace;
        public int no;
        public Draging selecteddraging;
        public Vector2 Distance;
        public bool firstanswer;
        public int draginno;

        public TextMeshPro hinttext;
        public List<GameObject> holder1= new List<GameObject>(), holder2 = new List<GameObject>();

        public GameObject blocker1, blocker2;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        public bool Neartodestination()
        {
            if(Mathf.Abs(dropplace[no].transform.position.x - selecteddraging.transform.position.x) < Distance.x
                && Mathf.Abs(dropplace[no].transform.position.y - selecteddraging.transform.position.y) < Distance.y)
            {
           
                if(no ==8 && firstanswer)
                {
                    StartCoroutine(LevelCompleted());
                    //level Complete
                }

                
                return true;
            }
            return false;
        }
        public IEnumerator LevelCompleted()
        {
            gameCompleted_animation.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }   //    public bool 

    }
}