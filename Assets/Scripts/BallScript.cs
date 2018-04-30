using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallScript : MonoBehaviour {
    public GameObject left;
    public GameObject right;
    Material leftMaterial;
    Material rightMaterial;
    bool flick = false;
    float timePassed;
    public Color normalColor = new Color(1, 1, 1, 1);
    public Color flickColor = new Color(0, 0, 0, 1);
    public float leftFlickSpeed;
    public float rightFlickSpeed;
    public Effect Flick = new Effect();
    bool stoppedFlickingColourChanged = false;
    public float finishFlicking = 20f;
    public bool showIndicator = false;
    public GameObject indicator;
    int indicatorOnLeft;
    Transform indicatorTransform;

    public System.Random rand = new System.Random();
    // Use this for initialization
    void Start () {
        leftMaterial = left.GetComponent<Renderer>().material;  // Material of the left sphere
        rightMaterial = right.GetComponent<Renderer>().material;    // Material of the right sphere
        indicatorTransform = indicator.transform;   // Transform of the indicator
        StartCoroutine(TimeManager());  // Start trial
    }
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        // Terminates the program when the trial ends
        if (timePassed > finishFlicking) {
            flick = false;
        }
        // Both spheres start flickering is the flick flag is on
        if (flick)
        {
            stoppedFlickingColourChanged = false;
            //timePassed = 0;
            leftMaterial.color = Color.Lerp(normalColor, flickColor, 
                Mathf.Round(Mathf.PingPong(Time.time * leftFlickSpeed, Flick.flickColorDuration)));
            rightMaterial.color = Color.Lerp(normalColor, flickColor, 
                Mathf.Round(Mathf.PingPong(Time.time * rightFlickSpeed, Flick.flickColorDuration)));
        }
        // Sets the colors of both spheres back to normal color after they stop flickering
        else if(!stoppedFlickingColourChanged){
            stoppedFlickingColourChanged = true;
            leftMaterial.color = normalColor;
            rightMaterial.color = normalColor;
        }
	}   

    // Couroutine
    private IEnumerator TimeManager()
    {
        while (timePassed < finishFlicking)
        {
            // Turns the indicator on, and randomly put it above the left/right sphere
            showIndicator = true;
            indicatorOnLeft = rand.Next(0, 2);
            print(indicatorOnLeft);
            if (indicatorOnLeft == 1)
                indicatorTransform.position = new Vector3(-1, 0.6f, 2);
            else
                indicatorTransform.position = new Vector3(1, 0.6f, 2);
            indicator.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(1f); // Leaves the indicator on for 1 second.

            // Turn off indicator
            showIndicator = false;
            indicator.GetComponent<Renderer>().enabled = false;

            // Randomly assign low and high frequency to the left and right sphere
            int flickSpeedRandomIndex = rand.Next(0, 2);
            if(flickSpeedRandomIndex == 0)
            {
                leftFlickSpeed = Flick.lowFrequency;
                rightFlickSpeed = Flick.highFrequency;
            }
            else
            {
                leftFlickSpeed = Flick.highFrequency;
                rightFlickSpeed = Flick.lowFrequency;
            }

            // Both spheres start flickering and stop after 10 seconds
            flick = true;
            yield return new WaitForSeconds(10f);
            flick = false;
        }
    }

    //Effect class
    [Serializable]
    public class Effect
    {
        //Parameters to setup
        public float lowFrequency = 7.5f;
        public float highFrequency = 15;
        public float normalColorDuration = 1;
        public float flickColorDuration = 1;
    }
}
