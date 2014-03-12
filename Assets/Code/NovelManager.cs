using System;
using System.IO;
using System.Xml;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
//Written by Michael G. Bethke
//Thank you, Jesus
[XmlRoot("VisualNovel")]
public class VisualNovel
{
	
	[XmlElement("Resources")]
	public Resources resources;
	
	[XmlElement("PlayerStory")]
	public PlayerStory playerStory;
	
	[XmlElement("SecondStory")]
	public SecondStory[] secondStory;

}


public class Resources
{
	
	[XmlElement ( "resource" )]
	public Resource[] allResources;
}


public class Resource
{
	
	[XmlAttribute]
	public String id;
	
	[XmlText]
	public String location;
}


public class PlayerStory
{
	
	[XmlElement ( "setUp" )]
	public SetUp setUp;
	
	[XmlElement ( "dialogue" )]
	public Dialogue[] dialogue;
}


public class SetUp
{
	
	public String backgorund;
	public String music;
}


public class Dialogue
{
	
	[XmlAttribute]
	public String id;
	
	public String speaker;
	public String speakerImage;
	public String body;
	
	[XmlElement("prompt")]
	public Prompt prompt;
}


public class Prompt
{
	
	[XmlAttribute]
	public String type;
	
	[XmlElement("yes")]
	public Yes yes;
	
	[XmlElement("no")]
	public No no;
	
	[XmlElement("next")]
	public Next next;
	
//	[XmlElement("list")]
//	public Next next;
}


public class Yes
{
	
	[XmlAttribute]
	public String lead;
	
	[XmlText]
	public String text;
}


public class No
{
	
	[XmlAttribute]
	public String lead;
	
	[XmlText]
	public String text;
}


public class Next
{
	
	[XmlAttribute]
	public String lead;
		
	[XmlText]
	public String text;
}


public class Novel
{
	
	public String location;
	public String name;
	public String author;
	public String version;
	
	public VisualNovel visualNovel;
}


public class SecondStory
{
	
	
}


public class NovelManager : MonoBehaviour
{

	StartupManager startupManager;
	GUIManager guiManager;
	
	internal Novel activeNovel;
	internal int novelPlace;
	internal List<Novel> availableNovels = new List<Novel> ();
	
	SortedDictionary<string, Resource> availableResources = new SortedDictionary<string, Resource>();
	
	Texture2D backgroundTexture;
	Texture2D speakerTexture;
	
	
	void Start ()
	{
		
		startupManager = GameObject.FindGameObjectWithTag ( "StartupManager" ).GetComponent<StartupManager> ();
		guiManager = GameObject.FindGameObjectWithTag ( "GUIManager" ).GetComponent<GUIManager> ();
		
		foreach ( string directory in Directory.GetDirectories ( startupManager.parentFolder + Path.DirectorySeparatorChar + "Novels" ))
		{
			
			if ( File.Exists ( directory + Path.DirectorySeparatorChar + "Info.txt" ))
			{
			
				Novel tempNovel = new Novel ();
				tempNovel.location = directory.ToString ();
				using ( StreamReader reader = new StreamReader ( directory + Path.DirectorySeparatorChar + "Info.txt" ))
				{
					
					tempNovel.name = reader.ReadLine ();
					tempNovel.author = reader.ReadLine ();
					tempNovel.version = reader.ReadLine ();
				}
				
				if ( tempNovel.name != null && tempNovel.author != null && tempNovel.version != null && File.Exists ( directory + Path.DirectorySeparatorChar + tempNovel.name + ".xml" ))
					availableNovels.Add ( tempNovel );
				else
					UnityEngine.Debug.Log ( "Folder not added" );
			}
		}
	}
	
	
	void StartNovel ()
	{
		
		GameObject.FindGameObjectWithTag ( "LoadingText" ).GetComponent<GUIText>().text = "";
		GameObject.FindGameObjectWithTag ( "TitleText" ).GetComponent<GUIText>().text = "";
		GameObject.FindGameObjectWithTag ( "Background" ).GetComponent<GUITexture>().texture = backgroundTexture;
		GameObject.FindGameObjectWithTag ( "Speaker" ).GetComponent<GUITexture>().texture = speakerTexture;
		
		guiManager.inNovel = true;
	}
	
	
	IEnumerator LoadNovel ( Novel novel )
	{
	
		novelPlace = 0;
		activeNovel = null;
				
		System.IO.StreamReader streamReader = new System.IO.StreamReader ( novel.location + Path.DirectorySeparatorChar + novel.name + ".xml" );
		string xml = streamReader.ReadToEnd();
		streamReader.Close();
		
								
		VisualNovel currentNovel = xml.DeserializeXml<VisualNovel>();
		novel.visualNovel = currentNovel;
		activeNovel = novel;
		
		int resourcesCount = 0;
		while ( resourcesCount < novel.visualNovel.resources.allResources.Length )
		{
			
			availableResources.Add ( novel.visualNovel.resources.allResources[resourcesCount].id, novel.visualNovel.resources.allResources[resourcesCount]);
			
			resourcesCount += 1;
		}

		if ( availableResources.ContainsKey ( "Background" ))
		{
			
			WWW backgroundWWW = new WWW ( "file://" + novel.location + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + availableResources["Background"].location );
			yield return backgroundWWW;
							
			backgroundTexture = new Texture2D ( Screen.width, Screen.height );
			backgroundWWW.LoadImageIntoTexture ( backgroundTexture );
		}
		
		if ( availableResources.ContainsKey ( novel.visualNovel.playerStory.dialogue[0].speakerImage ))
		{
					
			WWW speakerWWW = new WWW ( "file://" + novel.location + Path.DirectorySeparatorChar + "Images" + Path.DirectorySeparatorChar + availableResources[novel.visualNovel.playerStory.dialogue[0].speakerImage].location );
			yield return speakerWWW;
			
			speakerTexture = new Texture2D ( Screen.width, Screen.height, TextureFormat.ARGB32, true );
			speakerWWW.LoadImageIntoTexture ( speakerTexture );
		}

		StartNovel ();
	}
}
