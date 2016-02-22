using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


public class ProcessingBehaviour : MonoBehaviour // the Class
{
public TextAsset GameAsset;

public string mObjectName = "";
public string mObjectDescription = "";
//public string mObjectPictureLink = "";
public int theRealPicture = 0;
public bool informationLoadSuccesfull = false;
public Texture2D Picture;
public List<string> pictures = new List<string>();
public int descriptionLanguage = 0;

//List<Dictionary<string,string>> pictures = new List<Dictionary<string,string>>();
//Dictionary<string,string> obj;

IEnumerator Start()
{
	//Load XML data from a URL
	//string url = "http://vvtest.ucoz.com/picturesReal.xml";
	string url = "http://85.255.65.168/XML/pictureInformation.xml"; //For now all comes from Ucoz FTP, because real server is not running. Bellow link for server

	WWW www = new WWW(url);

	//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
	yield return www;
	if (www.error == null)
	{
		//Sucessfully loaded the XML
		Debug.Log("Loaded following XML " + www.data);

		//Create a new XML document out of the loaded data
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(www.data);
		XmlNodeList levelsList = xmlDoc.GetElementsByTagName("picture"); // array of the level nodes.
		XmlNodeList linkList = xmlDoc.GetElementsByTagName("links"); // array of the level nodes.

		int mPictureIdentificator = theRealPicture - 1;

	}
	else
	{//Error

		Debug.Log("ERROR: " + www.error);
	}
}
/*
public void GetLevel()
{
XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
xmlDoc.LoadXml(GameAsset.text); // load the file.
XmlNodeList levelsList = xmlDoc.GetElementsByTagName("picture"); // array of the level nodes.
//XmlNodeList xnList = xmlDoc.SelectNodes("levels/level[position() <= 2]");

for (int i = 0; i < levelsList.Count; i++)
{
				string str = string.Format("name: {0}", levelsList[i].ChildNodes.Item(0).InnerText, levelsList[i].ChildNodes.Item(1).InnerText);
				//Debug.Log(str);
}

foreach (XmlNode xn in levelsList)
{
	//print("NODE select nestrada");
	//Debug.Log(xn.InnerText);
}

foreach (XmlNode levelInfo in levelsList)
{
	XmlNodeList levelcontent = levelInfo.ChildNodes;
	obj = new Dictionary<string,string>(); // Create a object(Dictionary) to colect the both nodes inside the level node and then put into levels[] array.

	foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
	{
	if(levelsItens.Name == "name")
	{
		obj.Add("name",levelsItens.InnerText); // put this in the dictionary.
	}

	if(levelsItens.Name == "description")
	{
		obj.Add("description",levelsItens.InnerText); // put this in the dictionary.
	}
	}

		}
	}

	public void getInformation(int ID){
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(GameAsset.text); // load the file.
		XmlNodeList levelsList = xmlDoc.GetElementsByTagName("picture");

		int realID = ID - 1;

		string objectName = string.Format("{0}", levelsList[realID].ChildNodes.Item(0).InnerText);
		Debug.Log(objectName);
		mObjectName = objectName;

		string objectDescription = string.Format("{0}", levelsList[realID].ChildNodes.Item(1).InnerText);
		Debug.Log(objectDescription);
		mObjectDescription = objectDescription;

		string objectPictureLink = string.Format("{0}", levelsList[realID].ChildNodes.Item(2).InnerText);
		Debug.Log(objectPictureLink);
		mObjectPictureLink = objectPictureLink;
	}

	private void ProcessPictures(XmlNodeList nodes)
	{

		string objectName = string.Format("{0}", nodes[theRealPicture].ChildNodes.Item(0).InnerText);
		Debug.Log(objectName);
		mObjectName = objectName;

		string objectDescription = string.Format("{0}", nodes[theRealPicture].ChildNodes.Item(1).InnerText);
		Debug.Log(objectDescription);
		mObjectDescription = objectDescription;

		string objectPictureLink = string.Format("{0}", nodes[theRealPicture].ChildNodes.Item(2).InnerText);
		Debug.Log(objectPictureLink);
		mObjectPictureLink = objectPictureLink;

		foreach (XmlNode node in nodes)
		{
			//Debug.Log("Here`s something");
		}
	}
*/

public void sendInformationRequest(int id)//Send request with picture ID
{
	StartCoroutine("WaitForRequest", id);//We need this, because of use of IEnumerator
}

	IEnumerator WaitForRequest(int someParameter)
	{
		//Load XML data from a URL
		string url = "http://vvtest.ucoz.com/picturesReal.xml"; //For now all comes from Ucoz FTP, because real server is not running. Bellow link for server
		//string url = "http://85.255.65.168/XML/pictureInformation.xml";
		WWW www = new WWW(url);

		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
		yield return www;
		if (www.error == null)
		{
			//Create a new XML document out of the loaded data
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(www.data);
			XmlNodeList levelsList = xmlDoc.GetElementsByTagName("picture"); // array of the picture nodes.
			XmlNodeList linkList = xmlDoc.GetElementsByTagName("links"); // array of the level nodes.
			XmlNodeList descriptionList = xmlDoc.GetElementsByTagName("descriptions");
			XmlNodeList pictureName = xmlDoc.GetElementsByTagName("names");

			int mPictureIdentificator = someParameter - 1;

			//string objectName = string.Format("{0}", levelsList[mPictureIdentificator].ChildNodes.Item(0).InnerText);
			string objectName = string.Format("{0}", pictureName[mPictureIdentificator].ChildNodes.Item(descriptionLanguage).InnerText);
			//Debug.Log(objectName);
			mObjectName = objectName;//Object full name

			string objectDescription = string.Format("{0}", descriptionList[mPictureIdentificator].ChildNodes.Item(descriptionLanguage).InnerText);
			//Debug.Log(objectDescription);
			mObjectDescription = objectDescription;//Object description

			informationLoadSuccesfull = true;
		}
		else
		{
			informationLoadSuccesfull = false;
			Debug.Log("ERROR: " + www.error);
		}
}

void OnGUI() {

	GUI.Label(new Rect(150,0,199,199),Picture);

}

}
