using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z3Cam : MonoBehaviour
{
    private GameObject player;
    private Transform Plyr;
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
    public static bool camC;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        Plyr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Mathf.Clamp (player.transform.position.x, xMin, xMax);
		float y = Mathf.Clamp (player.transform.position.y, yMin, yMax);
		gameObject.transform.position = new Vector3 (x + 35, y, gameObject.transform.position.z); 

        camC = PlayerRightTwo.camClear;

        if(camC == true)
        {
            if(UnityEngine.Camera.main.orthographicSize <= 90 && UnityEngine.Camera.main.orthographicSize >= 55)
            {
                UnityEngine.Camera.main.orthographicSize += 2;
            }
            Invoke("backCam", 2.8f);
        }

        else
        {
            UnityEngine.Camera.main.orthographicSize = 55;
        }
//Dynamic cam for areas=========================================
        //BigVert/first choice path
        if(Plyr.position.x > 944f && Plyr.position.x < 1066f)
        {
            UnityEngine.Camera.main.orthographicSize = 60;
        }
        //??
        /*else if(Plyr.position.y < -164f && Plyr.position.y > -332f)
        {
            if(Plyr.position.x > 3432f && Plyr.position.y < -5657f)
            {
                UnityEngine.Camera.main.orthographicSize = 60;
            }
        }*/
        //Mine field
        /*else if(Plyr.position.y < -834f && Plyr.position.y < -671f)
        {
            if(Plyr.position.x > 1074f && Plyr.position.y < 3190f)
            {
                UnityEngine.Camera.main.orthographicSize = 60;
            }
        }

        //Min worms and blimps
        else if(Plyr.position.y < 1077f && Plyr.position.y > 344f)
        {
            if(Plyr.position.x > 2756f && Plyr.position.x < 4474f)
            {
                UnityEngine.Camera.main.orthographicSize = 60;
            }
        }*/

        //BigWorm
        else if (Plyr.position.y > 568f && Plyr.position.y < 747f)
        {
            if (Plyr.position.x > 6612f && Plyr.position.x < 7012f)
            {
                UnityEngine.Camera.main.orthographicSize = 90;
            }
            
            else
            {
                UnityEngine.Camera.main.orthographicSize = 55;
            }
        }

        else
        {
            UnityEngine.Camera.main.orthographicSize = 55;
        }
    }

    void backCam()
    {
        if(UnityEngine.Camera.main.orthographicSize > 55)
        {
            UnityEngine.Camera.main.orthographicSize -= 1;
        }
    }
}
