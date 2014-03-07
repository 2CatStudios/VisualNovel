using System;
using System.IO;
using UnityEngine;
using System.Collections;
//Written by Michael G. Bethke
//Thank you for your love, Jesus
public class StartupManager : MonoBehaviour
{

	static String mac = Path.DirectorySeparatorChar + "Users" + Path.DirectorySeparatorChar  + Environment.UserName + Path.DirectorySeparatorChar + "Library" + Path.DirectorySeparatorChar  + "Application Support" + Path.DirectorySeparatorChar + "2Cat Studios" + Path.DirectorySeparatorChar + "UnityVisualNovel";
	static String windows = Environment.SpecialFolder.ApplicationData + Path.DirectorySeparatorChar  + "2Cat Studios" + Path.DirectorySeparatorChar + "UnityVisualNovel";
	internal String parentFolder;
	

	void Start ()
	{
		
		if ( Environment.OSVersion.Platform.ToString () == "Unix" )
			parentFolder = mac;
		else
			parentFolder = windows;
	
		if ( !Directory.Exists ( parentFolder ))
			Directory.CreateDirectory ( parentFolder );
	}
}
