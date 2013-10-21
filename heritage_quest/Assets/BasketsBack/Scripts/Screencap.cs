using UnityEngine;
using System.Collections;
using System;

public class Screencap : MonoBehaviour {
	
	bool takeScreen = false;
	Action callback;
	
	Material material;
	Texture2D texture;
	
	void OnPostRender(){
		if (takeScreen){
			int height = Screen.height;
			int width = Screen.width;
			texture = new Texture2D(width, height);
        	texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			texture.Apply();
			
			material = new Material (Shader.Find("Self-Illumin/Diffuse"));
			material.SetTextureScale("Tiling", new Vector2(100,0));
			material.mainTexture = texture;
			
			takeScreen = false;
			try{
				TImer time = GameObject.FindGameObjectWithTag("Timer").GetComponent<TImer>();
			}
			catch (NullReferenceException){
				Debug.Log ("ughhhh");
			}
			callback();
		}
	}
	
	public Material GetMaterial(){
		return material;
	}
	
	public void TakeScreenshot(Action action){
		takeScreen = true;
		callback = action;
	}

}
