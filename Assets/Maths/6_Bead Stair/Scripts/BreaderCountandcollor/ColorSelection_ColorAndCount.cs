using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Maths.BeadStair.ColorandCount
{
    public class ColorSelection_ColorAndCount : MonoBehaviour
    {

        public GameObject Selectedobj;
        public AllCollors thiscolor;

        private void Awake()
        {
            Selectedobj = transform.GetChild(0).gameObject;
            GetComponent<Button>().onClick.AddListener(() => SelectedColor());

        }

        public void SelectedColor()
        {
            GameController_ColorAndCount.instace.ResetColler();
            GameController_ColorAndCount.instace.selectedcollor = thiscolor;
            Selectedobj.SetActive(true);

        }

    }
}