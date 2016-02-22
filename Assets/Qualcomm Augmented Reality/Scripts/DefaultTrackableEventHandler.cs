/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using System.Collections;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the  interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler

    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        //For GUI button when object is found
        private bool mShowGUIButton = false;
        //All Rect variables
        private Rect mButtonRect = new Rect(Screen.width / 2 - 50,Screen.height / 2 - 180,Screen.width/5,Screen.height/16);//120,60 / Screen.height / 2 - 80
        private Rect mButton2Rect = new Rect(Screen.width / 2 - 50,Screen.height / 2 - 30, Screen.width/5,Screen.height/16);//80, 30 / Screen.height / 2 - 30
        private Rect mButton3Rect = new Rect(Screen.width / 2 - 50,Screen.height / 2 + 120,Screen.width/5,Screen.height/16); // Screen.height / 2 + 20
        private Rect mButtonStart = new Rect(Screen.width / 2 - 50,Screen.height / 2 - 180,Screen.width/5,Screen.height/16);//120,60 / Screen.height / 2 - 80
        private Rect mButtonAbout = new Rect(Screen.width / 2 - 50,Screen.height / 2 - 30, Screen.width/5,Screen.height/16);//80, 30 / Screen.height / 2 - 30
        private Rect mQuitRect = new Rect(20,20,Screen.width/5,Screen.height/16);
        private Rect mInformationRect = new Rect (0, 40, Screen.width,160);
        private Rect mPictureFrameRect = new Rect (Screen.width / 2 - 110,40,200,200);
        private Rect mPictureFrameRect2 = new Rect (Screen.width / 2 - 50,120,100,100);
        private Rect mNextPictureRect = new Rect (Screen.width / 2 + 20,180,80,30);
        private Rect mPreviousPictureRect = new Rect (Screen.width / 2 - 110,180,80,30);
        private Rect mredDotFrameRect = new Rect (20,20,80,30);
        private Rect mMenuBackgroundRect = new Rect (0,0,Screen.width,Screen.height);
        //Texture variables
        public Texture2D textureToDisplay;
        public Texture2D mFiveEiroGallery1;
        public Texture2D mFiveEiroGallery2;
        //Bool for label showing
        private bool mMainMenu = true;
        public bool showInformation = false;
        public bool showGallery = false;
        //For now, basic information about object
        public string basicInformation;
        public string cubeTextTest = "";
        public Texture2D[] pictures;//Array where all Gallery pictures will be
        int mPictureIndexNumber = 0;//Variable for Gallery

        #endregion // PRIVATE_MEMBER_VARIABLES

        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
        //  mMainMenu = true;

            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

            for (int i = 0; i < pictures.Length; i++)
            {
              Debug.Log("Bildes numurs: "+i+"is named "+pictures[i].name);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the  function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                Debug.Log("Atradu");
                mShowGUIButton = true;
                OnTrackingFound();
            }
            else
            {
              Debug.Log("Mango");
              //mShowGUIButton = false;
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
          Debug.Log(" Nekas nestrada ");
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");

        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        void Update () {

        }

        void OnGUI() {

          if (mMainMenu)
          {
            GUIStyle myMainMenuStyle = new GUIStyle(GUI.skin.box);
            //myMainMenuStyle.fixedHeight = 10000;
            //myMainMenuStyle.fixedWidth = 10000;
            myMainMenuStyle.stretchWidth = true;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), textureToDisplay);
            //GUI.Box(mMenuBackgroundRect, textureToDisplay, myMainMenuStyle);

            if (GUI.Button(mQuitRect, "Quit")) {
                // do something on button click
                Application.Quit();
            }

            if (GUI.Button(mButtonStart, "Start")) {

                mMainMenu = false; //Hide the main menu box
            }

            if (GUI.Button(mButtonAbout, "About")) {
                // do something on button click
            }

          }else{


          GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
          myButtonStyle.normal.textColor = Color.green;
          myButtonStyle.hover.textColor = Color.grey;
          myButtonStyle.fontSize = 20;
          myButtonStyle.alignment = TextAnchor.UpperLeft;

          if (GUI.Button(mQuitRect, "Quit")) {
              // do something on button click
              Application.Quit();
          }

            if (mShowGUIButton) {
              Debug.Log("Poga is TRUE");
              //GUI.Label(mInformationRect,"5Eiro vieta kadreiz bija 5 Lati");
                // draw the GUI button
                if (GUI.Button(mButtonRect, "Information")) {
                    // do something on button click
                    Debug.Log("Nopietni?");
                    showInformation = true;
                    showGallery = false;
                }

                if (GUI.Button(mButton2Rect, "Gallery")) {
                    // do something on button click
                    Debug.Log("Galerija!");
                    //mShowGUIButton = false;
                    showInformation = false;
                    showGallery = true;
                }

                if (GUI.Button(mButton3Rect, "Exit")) {
                    // do something on button click
                    Debug.Log("Close this window!");
                    mShowGUIButton = false;
                    showInformation = false;
                    showGallery = false;
                }

                if (showInformation == true)
                {
                  Debug.Log("Informacijas bool mainigais ir uzstadits pareizi");

                  //GUI.Label(new Rect(0,0,Screen.width,Screen.height),textureToDisplay);

                  GUI.Label(mInformationRect,mTrackableBehaviour.TrackableName + "\n" + basicInformation, myButtonStyle);
                }

                if (showGallery)
                {
                  Debug.Log("Bildes tiek attelotas");
                  //For now manual picture showing. Later MySQL DB with pictures
                  GUI.Label(mPictureFrameRect,pictures[mPictureIndexNumber]);
                  //GUI.Label(mPictureFrameRect2,mFiveEiroGallery2);
                  if (GUI.Button(mNextPictureRect, "Next"))
                  {
                    if(mPictureIndexNumber < pictures.Length-1)
                    {
                      mPictureIndexNumber = mPictureIndexNumber + 1;
                    }
                  }else if (GUI.Button(mPreviousPictureRect, "Previous")){
                    if(mPictureIndexNumber > 0)
                    {
                      mPictureIndexNumber = mPictureIndexNumber - 1;
                    }
                  }
                }
            }
        }
      }
        #endregion // PRIVATE_METHODS
    }
}
