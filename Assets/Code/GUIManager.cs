using UnityEngine;
using System.Collections;
//Written by Michael G. Bethke
//Please bless us, Lord
public class GUIManager : MonoBehaviour
{
	
	NovelManager novelManager;

	public GUISkin guiSkin;
	Vector2 scrollPosition;
	
	internal bool inNovel = false;

	
	void Start ()
	{
		
		novelManager = GameObject.FindGameObjectWithTag ( "NovelManager" ).GetComponent <NovelManager> ();
		
		GameObject.FindGameObjectWithTag ( "TitleText" ).GetComponent<GUIText>().text = "UnityVisualNovel";
	}
	
	
	void OnGUI ()
	{
		
		GUI.skin = guiSkin;
		
		if ( inNovel == false )
		{
			
			GUILayout.BeginHorizontal ();
			GUILayout.Space ( Screen.width / 2 - 300 );
			GUILayout.BeginVertical ();
			GUILayout.Space ( 200 );
			scrollPosition = GUILayout.BeginScrollView ( scrollPosition, GUILayout.Width ( 600 ), GUILayout.Height (  Screen.height - ( Screen.height / 4 + 30 )));
			
			foreach ( Novel novel in novelManager.availableNovels )
			{
				
				if ( GUILayout.Button ( novel.name ))
				{
					
					GameObject.FindGameObjectWithTag ( "LoadingText" ).GetComponent<GUIText>().text = "Loading " + novel.name;
					inNovel = true;
					
					novelManager.SendMessage ( "LoadNovel", novel );
				}
			}
			
			GUILayout.EndScrollView ();
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
		} else
		{
			
			GUI.Label ( new Rect ( 24, Screen.height - 420, Screen.width - 48, 400 ), novelManager.activeNovel.visualNovel.playerStory.dialogue[novelManager.novelPlace].body );
		}
	}
}
