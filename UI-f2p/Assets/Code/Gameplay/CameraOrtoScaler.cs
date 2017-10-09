using UnityEngine;
using System.Collections;

public class CameraOrtoScaler : MonoBehaviour {
	public float baseSize = 1f;
    
	void Awake () {
        GetComponent<Camera>().orthographicSize = baseSize * (16f / 9f) / Camera.main.aspect;      
    }
}
