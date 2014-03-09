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
	
	internal Novel activeNovel;
	internal int novelPlace;
	internal List<Novel> availableNovels = new List<Novel> ();
	
	
	void Start ()
	{
		
		startupManager = GameObject.FindGameObjectWithTag ( "StartupManager" ).GetComponent<StartupManager> ();
		
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
	
	
	void LoadNovel ( Novel novel )
	{
	
		novelPlace = 0;
		activeNovel = null;
				
		System.IO.StreamReader streamReader = new System.IO.StreamReader ( novel.location + Path.DirectorySeparatorChar + novel.name + ".xml" );
		string xml = streamReader.ReadToEnd();
		streamReader.Close();
								
		VisualNovel currentNovel = xml.DeserializeXml<VisualNovel>();
		novel.visualNovel = currentNovel;
		activeNovel = novel;
		
		GameObject.FindGameObjectWithTag ( "LoadingText" ).GetComponent<GUIText>().text = "";
		GameObject.FindGameObjectWithTag ( "TitleText" ).GetComponent<GUIText>().text = "";
/*
		UnityEngine.Debug.Log ( currentNovel );
		UnityEngine.Debug.Log ( currentNovel.playerStory );
		UnityEngine.Debug.Log ( currentNovel.playerStory.dialogue );
		UnityEngine.Debug.Log ( currentNovel.playerStory.dialogue.Length );
		UnityEngine.Debug.Log ( currentNovel.playerStory.dialogue[6] );
		UnityEngine.Debug.Log ( currentNovel.playerStory.dialogue[6].id );
		UnityEngine.Debug.Log ( currentNovel.playerStory.dialogue[6].speaker );
		UnityEngine.Debug.Log ( currentNovel.playerStory.dialogue[6].speakerImage );
		UnityEngine.Debug.Log ( currentNovel.playerStory.dialogue[6].body );
		if ( currentNovel.playerStory.dialogue[6].prompt.type == "next" )
			UnityEngine.Debug.Log ( currentNovel.playerStory.dialogue[6].prompt.next.text );
		else
			UnityEngine.Debug.Log ( "Prompt not displayed- not same type!" );
			
		UnityEngine.Debug.Log ( availableNovels[0].visualNovel );
*/	
	}
}
