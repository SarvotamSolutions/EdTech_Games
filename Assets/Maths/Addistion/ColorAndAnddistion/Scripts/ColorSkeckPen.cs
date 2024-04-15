using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maths.Addision.AddisitonwithColors
{
    public class ColorSkeckPen : MonoBehaviour
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
            GameCondtroller.instace.ResetColler();
            GameCondtroller.instace.selectedcollor = thiscolor;
            Selectedobj.SetActive(true);

        }
    }
}