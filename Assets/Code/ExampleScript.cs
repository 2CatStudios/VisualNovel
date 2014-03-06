using System;
using System.IO;
using System.Xml;
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
//Written by Michael G. Bethke
//Please bless this project, Jesus
[XmlRoot("VisualNovel")]
public class VisualNovel
{
	
//	[XmlElement("Resources")]
//	public Resources resources;
	
	[XmlElement("PlayerStory")]
	public PlayerStory[] playerStory;
	
//	[XmlElement("MainStory")]
//	public MainStory[] mainStory;

}


public class PlayerStory
{
	
	[XmlElement ( "setUp" )]
	public SetUp setUp;
	
	[XmlElement ( "dialogue" )]
	public Dialogue dialogue;
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


public class ExampleScript : MonoBehaviour
{

	public GUISkin guiSkin;
	Vector2 scrollPosition;

	public String novelsDirectory;
 	string[] availableNovels = new string[0];


	void Start ()
	{
		
		if ( Directory.Exists ( novelsDirectory ))
		{
			
			availableNovels = Directory.GetDirectories ( novelsDirectory );
		}
	}
	
	
	void OnGUI ()
	{
		
		GUI.skin = guiSkin;
		
		GUILayout.BeginHorizontal ();
		GUILayout.Space ( Screen.width / 2 - 300 );
		GUILayout.BeginVertical ();
		GUILayout.Space ( 200 );
		scrollPosition = GUILayout.BeginScrollView ( scrollPosition, GUILayout.Width ( 600 ), GUILayout.Height (  Screen.height - ( Screen.height / 4 + 30 )));
		
		foreach ( string novel in availableNovels )
		{
			
			if ( GUILayout.Button ( novel.Remove ( 0, novel.LastIndexOf ( Path.DirectorySeparatorChar ) + 1 )))
			{
				
				LoadNovel ( novel, novel.Remove ( 0, novel.LastIndexOf ( Path.DirectorySeparatorChar ) + 1 ));
			}
		}
		
		GUILayout.EndScrollView ();
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}
	
	
	void LoadNovel ( string parentDirectory, string novelName )
	{
		
		UnityEngine.Debug.Log ( "LoadNovel" );
		
		System.IO.StreamReader streamReader = new System.IO.StreamReader ( parentDirectory + Path.DirectorySeparatorChar + novelName + ".xml" );
		string xml = streamReader.ReadToEnd();
		streamReader.Close();
						
		VisualNovel currentNovel = xml.DeserializeXml<VisualNovel>();
		
		UnityEngine.Debug.Log ( "LoadNovel Done" );
		UnityEngine.Debug.Log ( currentNovel );
		UnityEngine.Debug.Log ( currentNovel.playerStory );
		UnityEngine.Debug.Log ( currentNovel.playerStory[0] );
		UnityEngine.Debug.Log ( currentNovel.playerStory[0].dialogue );
		UnityEngine.Debug.Log ( currentNovel.playerStory[0].dialogue.id );
		UnityEngine.Debug.Log ( currentNovel.playerStory[0].dialogue.speaker );
		UnityEngine.Debug.Log ( currentNovel.playerStory[0].dialogue.speakerImage );
		UnityEngine.Debug.Log ( currentNovel.playerStory[0].dialogue.body );
		UnityEngine.Debug.Log ( currentNovel.playerStory[0].dialogue.prompt.yes.text );
	}
}










