using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z1Cam : MonoBehaviour
{
    private GameObject player;
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
    public static bool camC;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Mathf.Clamp (player.transform.position.x, xMin, xMax);
		float y = Mathf.Clamp (player.transform.position.y, yMin, yMax);
		gameObject.transform.position = new Vector3 (x, y + 20, gameObject.transform.position.z); 

        camC = PlayerUp.camClear;

        if(camC == true)
        {
            if(UnityEngine.Camera.main.orthographicSize <= 70 && UnityEngine.Camera.main.orthographicSize >= 45)
            {
                UnityEngine.Camera.main.orthographicSize += 2;
            }
            Invoke("backCam", 2.8f);
        }

        else
        {
            UnityEngine.Camera.main.orthographicSize = 45;
            /*if(UnityEngine.Camera.main.orthographicSize <= 70 && UnityEngine.Camera.main.orthographicSize >= 45)
            {
                if(camC == false)
                {
                    UnityEngine.Camera.main.orthographicSize -= 5;
                }
            }*/
        }
    }

    void backCam()
    {
        if(UnityEngine.Camera.main.orthographicSize > 45)
        {
            UnityEngine.Camera.main.orthographicSize -= 1;
        }
    }
}
