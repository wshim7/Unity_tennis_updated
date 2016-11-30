using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {
    Renderer render;
    private float t = 0;
    private float duration = 1;
	// Use this for initialization
	void Start () {
        render = GetComponent<Renderer> ();
	}
    void Update()
    {

        if (transform.position.y < 3.7 && transform.position.y >3)
        {
            render.material.color = Color.Lerp(Color.green,Color.red,t);
            if (t < 1)
            {
                t += Time.deltaTime / duration;
            }
        }
        if (transform.position.y < 1.5 && transform.position.y > 1.39)
        {
            t = 0;
            render.material.color = Color.Lerp(Color.red, Color.green,t);
            if (t < 1)
            {
                t += Time.deltaTime / duration;
            }
        } 
    }
    // Update is called once per frame
    
    //void OnCollisionEnter(Collision col)
    //{
        /*if (col.gameObject.tag == "court")
        {
            render.material.color = Color.Lerp(Color.red, Color.green, 0);
            print(transform.lossyScale.y);
        }
        */

    //}
   
}
