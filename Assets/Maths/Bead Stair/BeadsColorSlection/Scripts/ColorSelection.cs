using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Maths.BeadStair.ColorSlection
{
    public class ColorSelection : MonoBehaviour
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
            GameController.instace.ResetColler();
            GameController.instace.selectedcollor = thiscolor;
            Selectedobj.SetActive(true);

        }
    }

}