using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GestureRecognizer;
using System.Linq;
using TMPro;

public class ExampleGestureHandler : MonoBehaviour {

	public TextMeshPro textResult;
	public TextMeshProUGUI textResultUI;
	public string notext;
	public int no;

	GesturePatternDraw[] references;

	void Start () {
		//references = referenceRoot.GetComponentsInChildren<GesturePatternDraw> ();
	}

	//void ShowAll(){
	//	for (int i = 0; i < references.Length; i++) {
	//		references [i].gameObject.SetActive (true);
	//	}
	//}

	public void OnRecognize(RecognitionResult result)
	{
		StopAllCoroutines ();
		//ShowAll ();
		if (result != RecognitionResult.Empty) 
		{

			notext += result.gesture.id;
			if (int.TryParse(notext, out no))
			{


				//no = int.Parse(notext);
				if (textResult)
				{
					textResult.text = no.ToString();
				}
				else if (textResultUI)
					textResultUI.text = no.ToString();
            }
            else
            {
				if (textResult)
				{
					textResult.text = notext;
				}
				else if (textResultUI)
					textResultUI.text =notext;
			}
			//StartCoroutine (Blink (result.gesture.id));
		} else 
		{
			if (textResult)
				textResult.text = "?";
			else if(textResultUI)
				textResultUI.text = "?";
		}
	}

	//IEnumerator Blink(string id){
	//	var draw = references.Where (e => e.pattern.id == id).FirstOrDefault ();
	//	if (draw != null) {
	//		var seconds = new WaitForSeconds (0.1f);
	//		for (int i = 0; i <= 20; i++) {
	//			draw.gameObject.SetActive (i % 2 == 0);
	//			yield return seconds;
	//		}
	//		draw.gameObject.SetActive (true);
	//	}
	//}

}
