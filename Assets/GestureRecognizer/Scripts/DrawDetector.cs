﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

namespace GestureRecognizer {

	public class DrawDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

		public AudioSource dragsound;
		public UnityEvent called;
		public Recognizer recognizer;
		public Text infotext;
		public UILineRenderer line;
		private List<UILineRenderer> lines;
		public ExampleGestureHandler handler;
		[Range(0f,1f)]
		public float scoreToAccept = 0.8f;

		[Range(1,10)]
		public int minLines = 1;
		public int MinLines { set { minLines = Mathf.Clamp (value, 1, 10); } }

		[Range(1,10)]
		public int maxLines = 2;
		public int MaxLines { set { maxLines = Mathf.Clamp (value, 1, 10); } }

		public enum RemoveStrategy { RemoveOld, ClearAll }
		public RemoveStrategy removeStrategy;

		public bool clearNotRecognizedLines;

		public bool fixedArea = false;

		GestureData data = new GestureData();

		[System.Serializable]
		public class ResultEvent : UnityEvent<RecognitionResult> {}
		public ResultEvent OnRecognize;

		RectTransform rectTransform;


		void Start(){
			handler = GetComponent<ExampleGestureHandler>();
			line.relativeSize = true;
			line.LineList = false;
			lines = new List<UILineRenderer> (){ line };
			rectTransform = transform as RectTransform;
			UpdateLines ();
		}

		void OnValidate(){
			maxLines = Mathf.Max (minLines, maxLines);
		}

		public void UpdateLines()
		{
			
			while (lines.Count < data.lines.Count)
            {
				
                var newLine = Instantiate(line, line.transform.parent);
                lines.Add(newLine);
            }
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].Points = new Vector2[] { };			
                lines[i].SetAllDirty();
            }
            int n = Mathf.Min (lines.Count, data.lines.Count);
			for (int i = 0; i < n; i++) 
			{
				lines [i].Points = data.lines [i].points.Select (p => RealToLine (p)).ToArray ();
				lines [i].SetAllDirty ();
			}
		}

		Vector2 RealToLine(Vector2 position){
			
			var local = rectTransform.InverseTransformPoint(position);
			
			var normalized = Rect.PointToNormalized (rectTransform.rect, local);
			
			return normalized;
		}

		Vector2 FixedPosition(Vector2 position){
			return position;
		}
		public int vibtation;
		public int power;
		public void ClearLines()
		{
			
			data.lines.Clear ();
			UpdateLines ();
			if(infotext)
			infotext.gameObject.SetActive(true);
			GetComponent<ExampleGestureHandler>().no = 0;
			GetComponent<ExampleGestureHandler>().notext = "";
            if (GetComponent<ExampleGestureHandler>().textResult)
                GetComponent<ExampleGestureHandler>().textResult.text = "?";
            else if (GetComponent<ExampleGestureHandler>().textResultUI)
                GetComponent<ExampleGestureHandler>().textResultUI.text = "?";

        }

		public void OnPointerClick (PointerEventData eventData) {

		}
		IEnumerator storecorotine;
		public void OnBeginDrag (PointerEventData eventData)
		{
			dragsound.Play();
			Debug.Log("drag started");
			infotext.gameObject.SetActive(false);
			if (data.lines.Count >= maxLines) {
				switch (removeStrategy)
				{
					case RemoveStrategy.RemoveOld:
						data.lines.RemoveAt (0);
					break;
					case RemoveStrategy.ClearAll:
						data.lines.Clear ();
					break;
				}
            }
            else
            {
				if (storecorotine != null)
					StopCoroutine(storecorotine);
			}

			data.lines.Add (new GestureLine ());

			var fixedPos = FixedPosition (eventData.position);
			if (data.LastLine.points.Count == 0 || data.LastLine.points.Last () != fixedPos) {
				data.LastLine.points.Add (fixedPos);
				UpdateLines ();
			}
		}

		public void OnDrag (PointerEventData eventData) {
			var fixedPos = FixedPosition (eventData.position);
			if (data.LastLine.points.Count == 0 || data.LastLine.points.Last () != fixedPos) {
				data.LastLine.points.Add (fixedPos);
				UpdateLines ();
			}
			
		}

		public void OnEndDrag (PointerEventData eventData)
		{
			StartCoroutine (OnEndDragCoroutine (eventData));

			dragsound.Stop();
		}

		IEnumerator OnEndDragCoroutine(PointerEventData eventData){

			data.LastLine.points.Add (FixedPosition(eventData.position));
			UpdateLines ();

			for (int size = data.lines.Count; size >= 1 && size >= minLines; size--) {
				var sizedData = new GestureData () {
					lines = data.lines.GetRange (data.lines.Count - size, size)
				};

				var sizedNormalizedData = sizedData;

				if (fixedArea) {
					var rect = this.rectTransform.rect;
					sizedNormalizedData = new GestureData (){
						lines = sizedData.lines.Select( line => new GestureLine(){
							closedLine = line.closedLine,
							points = line.points.Select( p => Rect.PointToNormalized(rect, this.rectTransform.InverseTransformPoint(p) ) ).ToList()
						} ).ToList()
					};
				}

				RecognitionResult result = null;

				var thread = new System.Threading.Thread (()=>{
					result = recognizer.Recognize (sizedNormalizedData, normalizeScale: !fixedArea);
				});
				thread.Start ();
				while (thread.IsAlive) {
					yield return null;
				}

				if (result.gesture != null && result.score.score >= scoreToAccept) {
					OnRecognize.Invoke (result);
					if (clearNotRecognizedLines) {
						data = sizedData;
						UpdateLines ();
					}
					break;
				} else {
					OnRecognize.Invoke (RecognitionResult.Empty);
				}
			}
			storecorotine = waitForClear();
			StartCoroutine(storecorotine);
			yield return null;
		}


		IEnumerator waitForClear()
        {
			yield return new WaitForSeconds(1);
			if (transform.GetComponent<ExampleGestureHandler>().notext != "")
				called.Invoke();
			else
			{
					transform.GetComponent<ExampleGestureHandler>().notext = "?";
				transform.DOShakePosition(1, power, vibtation);
			}
			ClearLines();
        }
	}
}