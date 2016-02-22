using UnityEngine;
using System.Collections;

public class LoadImage : MonoBehaviour {
		public GameObject go;
		public Texture2D mPictureToGive;
		private Rect mPictureFrameRect = new Rect (Screen.width / 2 - 50,Screen.height / 2 - 150,500,200);

		private IEnumerator loadImage( GameObject page, string url ) {
				WWW www = new WWW( url );
				yield return www;
				//page.renderer.material.mainTexture = www.texture;
				mPictureToGive = www.texture;
		}

		public void displayPicture(string cat)//This function is call from another script to Display picture
		{
			StartCoroutine( loadImage( go, cat ) );
		}

		void OnGUI() {
		}
}
