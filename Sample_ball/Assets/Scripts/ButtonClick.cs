using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {

	// Use this for initialization
	public void clickT()
    {
        if (GameObject.FindWithTag("ball").transform.position.y < 3.7 && GameObject.FindWithTag("ball").transform.position.y > 0) 
        {
            print("success");
            print(GameObject.FindWithTag("ball").transform.position.y);
            return;
        }
        print("fail");
        print(GameObject.FindWithTag("ball").transform.position.y);
        return;
    }
}
