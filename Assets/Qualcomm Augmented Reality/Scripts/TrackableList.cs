using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Vuforia
{

public class TrackableList : MonoBehaviour{

	public GUISkin guiSkin;
	public GUISkin pictureSkin;
	private bool mShowGUIButton = false;
	private bool mShowGUIAbout = false;
	//We need to show picture with text in button, GUIContent for each picture, for each content.
	GUIContent aboutContent = new GUIContent();
	GUIContent startContent = new GUIContent();
	GUIContent quitContent = new GUIContent();
	GUIContent informationContent = new GUIContent();
	GUIContent galleryContent = new GUIContent();
	GUIContent backContent = new GUIContent();
	GUIContent nextContent = new GUIContent();
	GUIContent previousContent = new GUIContent();
	//All Rect variables
	private Rect mButtonRect = new Rect(Screen.width / 2-70,Screen.height / 2 - 190,Screen.width/5+30,Screen.height/10);//120,60 / Screen.height / 2 - 80
	private Rect mButton2Rect = new Rect(Screen.width / 2-70,Screen.height / 2, Screen.width/5+20,Screen.height/10);//80, 30 / Screen.height / 2 - 30
	private Rect mButton3Rect = new Rect(Screen.width / 2-70,Screen.height / 2 + 190,Screen.width/5+20,Screen.height/10); // Screen.height / 2 + 20
	private Rect mButtonStart = new Rect(Screen.width / 2-70,Screen.height / 2 - 210,Screen.width/5+20,Screen.height/10);//120,60 / Screen.height / 2 - 80
	private Rect mButtonAbout = new Rect(Screen.width / 2-70,Screen.height / 2, Screen.width/5+30,Screen.height/10);//80, 30 / Screen.height / 2 - 30
	private Rect mQuitRect = new Rect(0,0,Screen.width/5,Screen.height/16);//20,20
	private Rect mInformationRect = new Rect (0, 40, Screen.width,160);
	private Rect mPictureFrameRect = new Rect (Screen.width / 2 - 50,Screen.height / 2 - 150,500,200);
	private Rect mPictureFrameRect2 = new Rect (Screen.width / 2 - 50,120,100,100);
	//private Rect mNextPictureRect = new Rect (Screen.width / 2 + 40,Screen.height / 2 + 90,Screen.width/5+20,Screen.height/10);
	//private Rect mPreviousPictureRect = new Rect (Screen.width / 2 - 130,Screen.height / 2 + 90,Screen.width/5+20,Screen.height/10);
	private Rect mNextPictureRect = new Rect (Screen.width - 220,Screen.height / 2 + 90,Screen.width/5+20,Screen.height/10);
	private Rect mPreviousPictureRect = new Rect (50,Screen.height / 2 + 90,Screen.width/5+20,Screen.height/10);
	private Rect mredDotFrameRect = new Rect (20,20,80,30);
	private Rect mMenuBackgroundRect = new Rect (0,0,Screen.width,Screen.height);
	//Texture variables
	public Texture2D blackOriginalBackground;
	public Texture2D textureToDisplay;
	//public Texture2D mGalleryButton;
	//public Texture2D mInformationButton;
	//public Texture2D mExitButton;
	//public Texture2D mButtonsMenu;
	//public Texture2D mQuitButtonStyle;
	//public Texture2D mStartButton;
	//public Texture2D mAboutButton;
	public Texture2D mEmptyButton;//Empty for language test.
	public Texture2D mSecondEmptyButton;//Empty information menu.
	//Flag textures for language change
	public Texture2D flagLatvia;
	public Texture2D flagUnitedKingdom;
	public Texture2D flagRussia;
	public Texture2D flagFrance;
	public Texture2D flagGermany;
	private Rect mFirstFlagRect = new Rect (0,Screen.height-55,80,80);
	private Rect mSecondFlagRect = new Rect (88,Screen.height-55,80,80);
	private Rect mThirdFlagRect = new Rect (176,Screen.height-55,80,80);
	private Rect mFourthFlagRect = new Rect (264,Screen.height-55,80,80);
	private Rect mFifthFlagRect = new Rect (352,Screen.height-55,80,80);
	//Language changing variables
	private int lang = 0;
	private string[,] names = new string[5, 6] {//All languages will store in multi-dimensional array

	{"Sakums","Par applikaciju","Iziet", "Informacija", "Galerija", "Atpakal"}, //Latvian

	{ "Start", "About", "Quit", "Information", "Gallery", "Back"}, //English

	{ "Начало", "Приложение", "Bыход", "Информация","Галерея","Назад"}, //Russian

	{ "Commencer", "A propos", "Quitter", "Informations", "Galerie", "Retour"}, //France

	{ "Anwendung", "über", "Beenden", "Information", "Galerie", "Zurück"}, //German

	};

	private string[,] languageWords = new string[5, 8];
	//Bool for label showing
	private bool mMainMenu = true;
	private bool showInformation = false;
	private bool showGallery = false;
	//For now, basic information about object
	public string basicInformation;
	public string basicPictureLink;
	private string cubeTextTest = "";
	public Texture2D[] pictures;//Array where all Gallery pictures will be
	int mPictureIndexNumber = 0;//Variable for Gallery
	public Vector2 scrollPosition = new Vector2(40,40);
	public Texture2D myGUITexture;
	private List<string> picturesForLoad = new List<string>();
	private List<char> link = new List<char>();
	private List<string> allLinkInOnePlace = new List<string>();
	private int arrayPictureIndex = 0;
	private int pictureCount = 0;
	private int temp = 0;
	private bool haveAddedPictures = false;
	private bool nextButtonIsVisible = false;

	IEnumerator Start()
	{
		string languageURL = "http://vvtest.ucoz.com/languages.xml";//languageURL variable describe XML file location
		//string languageURL = "http://85.255.65.168/XML/languages.xml"; //For now all comes from Ucoz FTP, because real server is not running. Bellow link for server
		WWW languageWWW = new WWW(languageURL);

		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return languageWWW;
		if (languageWWW.error == null)
		{
			//Debug.Log("Language XML " + languageWWW.data);

			XmlDocument languageDoc = new XmlDocument();
			languageDoc.LoadXml(languageWWW.data);
			XmlNodeList languageList = languageDoc.GetElementsByTagName("language");

			int changingIndex = 0;
			int changingChildIndex = 0;

			for (int i = 0; i <languageList.Count; i++)
			{
				for (int b = 0; b <=7; b++)//For now, it`s hardcoded - count of names in app.
				{
					string words = string.Format("{0}", languageList[i].ChildNodes.Item(b).InnerText);//Drag out all necesary Language words from XML
					languageWords[i,b] = words;//Put all words in array.
					//Debug.Log(languageWords[i,b]);
				}
			}
		}
	}

		// Update is called once per frame
		void Update () {
				// Get the StateManager
				StateManager sm = TrackerManager.Instance.GetStateManager ();

				contentIdentificator();//Call contentIdentificator, what would help to change languages.
				// Query the StateManager to retrieve the list of
				// currently 'active' trackables
				//(i.e. the ones currently being tracked by Vuforia)
				IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours ();

				// Iterate through the list of active trackables
				Debug.Log ("List of trackables currently active (tracked): ");
				int numFrameMarkers = 0;
				int numImageTargets = 0;
				int numMultiTargets = 0;

				GameObject multiCamera = GameObject.Find("ARCamera");
				LoadImage pictureScript = multiCamera.GetComponent<LoadImage>();//Reference to Picture script

				foreach (TrackableBehaviour tb in activeTrackables) {
						Debug.Log("Trackable: " + tb.TrackableName);
						Debug.Log("Found ID: " + tb.Trackable.ID);

						temp = tb.Trackable.ID;

						GameObject theCamera = GameObject.Find("ARCamera");
						ProcessingBehaviour otherScript = theCamera.GetComponent<ProcessingBehaviour>();//Reference to connection script

						otherScript.descriptionLanguage = lang;
						otherScript.sendInformationRequest(tb.Trackable.ID);

						otherScript.theRealPicture = tb.Trackable.ID;
						basicInformation = otherScript.mObjectName + "\n" + otherScript.mObjectDescription;

						if(otherScript.informationLoadSuccesfull)//This is extra test, if information loaded sucessfull
						{
							mShowGUIButton = true;//If is, show recognizition menu
						}else{
							mShowGUIButton = false;
						}

						pictureCount = otherScript.pictures.Count;

						if (tb is MarkerBehaviour)
								numFrameMarkers++;
						else if (tb is ImageTargetBehaviour)
								numImageTargets++;
						else if (tb is MultiTargetBehaviour)
								numMultiTargets++;

				}

				Debug.Log ("Found " + numFrameMarkers + " frame markers in curent frame");
				Debug.Log ("Found " + numImageTargets + " image targets in curent frame");
				Debug.Log ("Found " + numMultiTargets + " multi-targets in curent frame");
		}

		void contentIdentificator()//Function what display every word in selected language
		{
			//aboutContent.text = names[lang,1];
			aboutContent.text = languageWords[lang, 1];
			startContent.text = languageWords[lang,0];
			quitContent.text = languageWords[lang,2];
			informationContent.text = languageWords[lang,3];
			galleryContent.text = languageWords[lang,4];
			backContent.text = languageWords[lang,5];
			previousContent.text = languageWords[lang,6];
			nextContent.text = languageWords[lang,7];
		}

		private IEnumerator imageInArray(int identifactor)
		{
			string urlToLink = "http://vvtest.ucoz.com/picturesReal.xml";//For now all comes from Ucoz FTP, because real server is not running. Bellow link for server
			//string urlToLink = "http://85.255.65.168/XML/pictureInformation.xml";
			WWW wLink = new WWW(urlToLink);

			//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
			yield return wLink;
			if (wLink.error == null)
			{
				//Create a new XML document out of the loaded data
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(wLink.data);
				XmlNodeList levelsList = xmlDoc.GetElementsByTagName("picture"); // array of the picture nodes.
				XmlNodeList linkList = xmlDoc.GetElementsByTagName("links"); // array of the level nodes.

				int mPictureIdentificator = identifactor - 1;

				GameObject theCamera = GameObject.Find("ARCamera");
				LoadImage otherScript = theCamera.GetComponent<LoadImage>();//Reference to XML script

				for (int i = 0; i < linkList.Count; i++)//So for certain picture, check all links
				{
					if (linkList[mPictureIdentificator].ChildNodes.Item(i) == null){//If no more link, break
						//Debug.Log("END");
						break;
					}else{
						string getLink = string.Format("{0}", linkList[mPictureIdentificator].ChildNodes.Item(i).InnerText);
						picturesForLoad.Add(getLink);
						//Debug.Log("In list have added: " + getLink);
					}
				}

				GameObject multiCamera = GameObject.Find("ARCamera");
				LoadImage pictureScript = multiCamera.GetComponent<LoadImage>();//Reference to XML scrip
				pictureScript.displayPicture(picturesForLoad[mPictureIndexNumber]);
				showInformation = false;
				showGallery = true;
		}
}
		void OnGUI() {

			GUIStyle mainButtonStyle = new GUIStyle();
			mainButtonStyle.stretchWidth = true;
			mainButtonStyle.stretchHeight = true;
			mainButtonStyle.fixedHeight = 100;
			mainButtonStyle.fixedWidth = 100;

			if (mMainMenu)
			{
				GUI.skin = guiSkin;//GUI.skin for display picture in buttons.
				GUI.skin.button.normal.background = (Texture2D)mEmptyButton;
				GUI.skin.button.hover.background = (Texture2D)mEmptyButton;
				GUI.skin.button.active.background = (Texture2D)mEmptyButton;

				GUIStyle myButtonPreStyle = new GUIStyle(GUI.skin.button);//Buttons style
				myButtonPreStyle.fontSize = 20;
				myButtonPreStyle.alignment = TextAnchor.MiddleCenter;

				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackOriginalBackground);//Both backgrounds
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), textureToDisplay);

				if(mShowGUIAbout == false)//If user haven`t push About button, you can see rest of Main menu
				{
					if (GUI.Button(mQuitRect, quitContent, myButtonPreStyle)) {
						Application.Quit();//Quit application, if user push Quit button
					}

					if (GUI.Button(mButtonStart,startContent, myButtonPreStyle)){
						mMainMenu = false; //Hide the main menu box
					}

					if (GUI.Button(mButtonAbout, aboutContent, myButtonPreStyle)){
						mShowGUIAbout= true;//Hide rest of menu and show About box.
					}
					//Just flag buttons, to choise a language
					if (GUI.Button(mFirstFlagRect, flagLatvia,mainButtonStyle)) {//Latvian language
							lang = 0;
					}else if (GUI.Button(mSecondFlagRect, flagUnitedKingdom,mainButtonStyle)) {//Latvian language
							lang = 1;
					}else if (GUI.Button(mThirdFlagRect, flagRussia,mainButtonStyle)) {//Latvian language
							lang = 2;
					}else if (GUI.Button(mFourthFlagRect, flagFrance,mainButtonStyle)) {//Latvian language
							lang = 3;
					}else if (GUI.Button(mFifthFlagRect, flagGermany,mainButtonStyle)) {//Latvian language
							lang = 4;
					}
				}

				if(mShowGUIAbout)//If user push About button
				{
					GUI.Label(mInformationRect,"Here will be Information about application");//Show information label
					if (GUI.Button(new Rect(0,Screen.height / 2 + 90, Screen.width/5,Screen.height/10), backContent)) {
						mShowGUIAbout = false;
					}
				}

			}else{

			GUI.skin = guiSkin;//Skin for recognizition menu buttons
			GUI.skin.button.normal.background = (Texture2D)mSecondEmptyButton;
			GUI.skin.button.hover.background = (Texture2D)mSecondEmptyButton;
			GUI.skin.button.active.background = (Texture2D)mSecondEmptyButton;

			GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);//Style for buttons
			myButtonStyle.fontSize = 20;
			myButtonStyle.alignment = TextAnchor.MiddleCenter;

			if (GUI.Button(mQuitRect, quitContent, myButtonStyle)) {//Quit
					Application.Quit();
			}

				if (mShowGUIButton) {//Object recognize, show Recognizition menu

					GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackOriginalBackground);//Again, both backgrounds Could make function for this stuff.
					GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), textureToDisplay);

						// draw the GUI button
						if (GUI.Button(mButtonRect, informationContent, myButtonStyle)) {//Information button
							showInformation = true;
							showGallery = false;
						}

							if (GUI.Button(mButton2Rect, galleryContent, myButtonStyle)) {//Gallery
								if (haveAddedPictures == false)//Check if picture is loaded
								{
								StartCoroutine( imageInArray( temp ) );//Load picture
								haveAddedPictures = true;
								}else{
								GameObject multiCamera = GameObject.Find("ARCamera");
								LoadImage pictureScript = multiCamera.GetComponent<LoadImage>();//Reference to picture scrip
								pictureScript.displayPicture(picturesForLoad[mPictureIndexNumber]);//Display first picture of picture array
								showInformation = false;
								showGallery = true;
							}
						}

						if (GUI.Button(mButton3Rect, backContent, myButtonStyle)) {//Back
								//Hide all menu buttons
								mShowGUIButton = false;
								showInformation = false;
								showGallery = false;
								//Clear picture array
								picturesForLoad.Clear();
								//For new object, again load picture for gallery
								haveAddedPictures = false;
								//For new object, start gallery from first picture
								mPictureIndexNumber = 0;
								//For new object also Next button is needed
								nextButtonIsVisible = false;
						}
				}
		}
		if (showInformation == true)//If user choise information button
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackOriginalBackground);//Just black background

			//VerticalScrollBar
			GUI.skin.verticalScrollbar.fixedWidth = Screen.width * 0.10f;
			GUI.skin.verticalScrollbarThumb.fixedWidth = Screen.width * 0.10f;
			GUI.skin.verticalScrollbar.fixedHeight = Screen.height * 0.10f;
			GUI.skin.verticalScrollbarThumb.fixedHeight = Screen.height * 0.10f;
			//HorizontalScrollBar
			GUI.skin.horizontalScrollbar.fixedHeight = Screen.height * 0.10f;
			GUI.skin.horizontalScrollbarThumb.fixedHeight = Screen.height * 0.10f;
			GUI.skin.horizontalScrollbar.fixedWidth = Screen.width * 0.10f;
			GUI.skin.horizontalScrollbarThumb.fixedWidth = Screen.width * 0.10f;

			GUIStyle infoLabelStyle = new GUIStyle(GUI.skin.button);//Style attribute for information window buttons
			infoLabelStyle.alignment = TextAnchor.MiddleCenter;
			infoLabelStyle.fontSize = 20;

			GUIStyle style = GUI.skin.box;//Style for information box
			style.wordWrap = true;
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = 25;
			style.fixedWidth = Screen.width - (Screen.width * 0.10f);//Subtract Scrollbar size from Box width

			GUIContent content = new GUIContent(basicInformation);
			Vector2 size = style.CalcSize(content);
			float height = style.CalcHeight(content, size.x);

			//scrollPosition = GUI.BeginScrollView(new Rect(0,0, 1000, 280), scrollPosition, new Rect(0, 0, size.x, height+10000), false, false);
			scrollPosition = GUI.BeginScrollView(new Rect(10,10, Screen.width-10, Screen.height / 2), scrollPosition, new Rect(0, 0, Screen.width+100, height+1000 ));
			GUILayout.Label(basicInformation, style);//Information will be show in scrollbar View
			GUI.EndScrollView();

			mShowGUIButton = false;
			if (GUI.Button(new Rect(0,Screen.height / 2+140, Screen.width/5,Screen.height/10), backContent, infoLabelStyle)) {//Back button to menu
				mShowGUIButton = true;
				showInformation = false;
			}
		}

		if (showGallery)//If User choise gallery
		{
			GUIStyle myButtonGalleryStyle = new GUIStyle(GUI.skin.button);
			myButtonGalleryStyle.fontSize = 20;
			myButtonGalleryStyle.alignment = TextAnchor.MiddleCenter;
			//myGUITexture = (Texture2D)Resources.Load(basicPictureLink);

			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackOriginalBackground);//Black background
			//GUI.DrawTexture(new Rect(Screen.width / 2 - 172, 0, 346, 660), mButtonsMenu);//Second background picture

			GameObject multiCamera = GameObject.Find("ARCamera");
			LoadImage pictureScript = multiCamera.GetComponent<LoadImage>();//Reference to picture script

			//pictureScript.displayPicture(picturesForLoad[mPictureIndexNumber]);//We need picture to display
			if (pictureScript.mPictureToGive != null)
			{
				myGUITexture = pictureScript.mPictureToGive;//Texture2D who will have picture
			}
			//Some attributes for picture showing
			int positionX = 0;//Position for Picture, X and Y
			int positionY = 0;
			int sizeX = 0;//Frame for Picture, X and Y
			int sizeY = 0;

			if (myGUITexture.width>myGUITexture.height)//If picture orientation is Landscape
			{
				positionX = 0;
				positionY = 0;
				sizeX = Screen.width;
				sizeY = 400;
			}else{//If picture orientation is Portrait
				positionX = 0;
				positionY = 0;
				sizeX = Screen.width;
				sizeY = Screen.height;
			}

			GUI.DrawTexture(new Rect(positionX,positionY, sizeX, sizeY), myGUITexture, ScaleMode.ScaleToFit,pictureSkin);//Display picture

			mShowGUIButton = false;
			if (!nextButtonIsVisible)//If Next button isn`t hide
			{
				if (GUI.Button(mNextPictureRect, nextContent, myButtonGalleryStyle))//Buttons for picture switch
				{
					if(mPictureIndexNumber < picturesForLoad.Count()-1)
					{
						mPictureIndexNumber = mPictureIndexNumber + 1;//Bigger picture index
						pictureScript.displayPicture(picturesForLoad[mPictureIndexNumber]);//We need picture to display
					}else{//If last picture, hide Next button
						nextButtonIsVisible = true;
					}
				}
			}

			if (GUI.Button(mPreviousPictureRect, previousContent, myButtonGalleryStyle)){//Previous button
				if(mPictureIndexNumber > 0)//If picture index is bigger than 0, load picture
				{
					mPictureIndexNumber = mPictureIndexNumber - 1;
					nextButtonIsVisible = false;
					pictureScript.displayPicture(picturesForLoad[mPictureIndexNumber]);//We need picture to display
				}
			}



			if (GUI.Button(new Rect(Screen.width/2-70,Screen.height / 2 + 250, Screen.width/5,Screen.height/10), backContent, myButtonGalleryStyle)) {//Back button also for Gallery
				mShowGUIButton = true;
				showGallery = false;
			}
		}
	}
}
}
