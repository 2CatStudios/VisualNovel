using System;
using System.IO;
using UnityEngine;
using System.Collections;
//Written by Michael G. Bethke
//Thank you for your love, Jesus
public class StartupManager : MonoBehaviour
{

	static String mac = Path.DirectorySeparatorChar + "Users" + Path.DirectorySeparatorChar  + Environment.UserName + Path.DirectorySeparatorChar + "Library" + Path.DirectorySeparatorChar  + "Application Support" + Path.DirectorySeparatorChar + "2Cat Studios" + Path.DirectorySeparatorChar + "UnityVisualNovel";
	static String windows = Environment.GetFolderPath ( Environment.SpecialFolder.CommonApplicationData ) + Path.DirectorySeparatorChar  + "2Cat Studios" + Path.DirectorySeparatorChar + "UnityVisualNovel";// Environment.SpecialFolder.ApplicationData + Path.DirectorySeparatorChar  + "2Cat Studios" + Path.DirectorySeparatorChar + "UnityVisualNovel";
	internal String parentFolder;
	

	void Start ()
	{
		
		if ( Environment.OSVersion.Platform.ToString () == "Unix" )
			parentFolder = mac;
		else
			parentFolder = windows;
	
		if ( !Directory.Exists ( parentFolder ))
			Directory.CreateDirectory ( parentFolder );
			
		GameObject.FindGameObjectWithTag ( "Background" ).GetComponent<GUITexture>().pixelInset = new Rect ( 0, 0, Screen.width, Screen.height );
		GameObject.FindGameObjectWithTag ( "Speaker" ).GetComponent<GUITexture>().pixelInset = new Rect ( 0, 0, Screen.width/3, Screen.height  );
	}
}
