using UnityEngine;
using System.Collections;

namespace Vuforia
{
public class Button : MonoBehaviour, ITrackableEventHandler {

		private TrackableBehaviour mTrackableBehaviour;

		private bool mShowGUIButton = false;
		private Rect mButtonRect = new Rect(10,10,60,60);

		void Start () {
			Debug.Log("Pogas sakums");
				mTrackableBehaviour = GetComponent<TrackableBehaviour>();
				if (mTrackableBehaviour)
				{
						mTrackableBehaviour.RegisterTrackableEventHandler(this);
				}
		}

		public void OnTrackableStateChanged(
																		TrackableBehaviour.Status previousStatus,
																		TrackableBehaviour.Status newStatus)
		{
				if (newStatus == TrackableBehaviour.Status.DETECTED ||
						newStatus == TrackableBehaviour.Status.TRACKED)
				{
					Debug.Log("NEkas noteikti nestrada");
						mShowGUIButton = true;
				}
				else
				{
						mShowGUIButton = false;
				}
		}

		void Update()
		{

		}

		//void OnGUI() {
			//GUI.Button(mButtonRect, "Hello");

						// draw the GUI button
						/*
						if (GUI.Button(mButtonRect, "Informācija")) {
							Debug.Log("Poga nostrada");
							informationAboutSculpture();

				}
		}

		void informationAboutSculpture()
		{
			Debug.Log("Method is working!");
			Debug.Log("Informacija par:  ");
		}
		*/
}
}
