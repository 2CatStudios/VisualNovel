using UnityEngine;
using System.Collections;
//Written by Michael G. Bethke
//Please bless us, Lord
public class GUIManager : MonoBehaviour
{
	
	NovelManager novelManager;

	public GUISkin guiSkin;
	GUIStyle clearStyle;
	Vector2 scrollPosition;
	
	internal bool inNovel = false;
	internal bool showNovelList = true;

	
	void Start ()
	{
		
		novelManager = GameObject.FindGameObjectWithTag ( "NovelManager" ).GetComponent <NovelManager> ();
		GameObject.FindGameObjectWithTag ( "TitleText" ).GetComponent<GUIText>().text = "UnityVisualNovel";
		
		clearStyle = new GUIStyle ();
		clearStyle.font = guiSkin.font;
		clearStyle.normal.background = null;
		clearStyle.hover.background = guiSkin.button.normal.background;
		clearStyle.border = new RectOffset ( 10, 10, 10, 10 );
		clearStyle.padding = new RectOffset ( 6, 6, 3, 3 );
	}
	
	
	void OnGUI ()
	{
		
		GUI.skin = guiSkin;
		
		if ( showNovelList == true )
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
					
					showNovelList = false;
					novelManager.SendMessage ( "LoadNovel", novel );
				}
			}
			
			GUILayout.EndScrollView ();
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
		} else
		{
			
			if ( inNovel == true )
			{
				
				if ( Input.GetKeyDown ( KeyCode.Escape ))
				{
					
					UnityEngine.Debug.Log ( "Pause Menu" );
				}
			
				GUI.Label ( new Rect ( 24, Screen.height - 420, Screen.width - 48, 400 ), novelManager.activeNovel.visualNovel.playerStory.dialogue[novelManager.novelPlace].body );
			}
		}
	}
}
