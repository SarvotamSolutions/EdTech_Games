using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Maths.BeadStair.ColorSlection
{
    public class GameController : Singleton<GameController>
    {
        public Totorial totorialcheck;
        public ColorSelection[] all_CollerSelection;
        public int level;
        public AllCollors selectedcollor;
        public Marble[] allmarble;
        public TextMeshProUGUI Hinttext;

        [Space(10)]
        public GameObject Sketchpen;
        public GameObject DrawnNO;
        public ExampleGestureHandler Draw;
        public Sprite NormalInput, CurrectAnswerInput, WrongInput;
        public SpriteRenderer InputBox;

        [Space(10)]
        public GameObject gameCompleted_animation;
        public GameObject wrongAnswer_animtion;
        public GameObject Party_pop;
        private void Awake()
        {
            Hinttext.color = allmarble[level].color;
            Hinttext.text = "<color=white>Colour " + (level + 1) + " bead "+ "</color>" + allmarble[level].thiscolor;
        }
        public void ResetColler()
        {

            for (int i = 0; i < all_CollerSelection.Length; i++)
            {
                all_CollerSelection[i].Selectedobj.SetActive(false);
            }
        }
    }
}
public enum AllCollors
{
    green=1,
    red=0,
    pink=2,
    White=6,
    lightblue=4,
    yellow=3,
    Darkblue=8,
    Purple=5,
    Gold=9,
    Brown=7
}