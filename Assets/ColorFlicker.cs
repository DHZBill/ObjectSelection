using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ColorFlicker : MonoBehaviour {

    //Types of detections
    public enum Detection { auto, image, text, material, light }
    //Types of effects
    public enum Type { Flick, PingPong, Repeat}

    //Used to setup the debug option
    public enum Bool
    {
        disabled, enabled,
    }
    public Bool debugEnum;

    //Colors to flick
    public Color normalColor = new Color(1,1,1,1);
    public Color flickColor = new Color(0,0,0,1);

    //Components to asign
    public Image image;
    public Text text;
    public Material material;
    public Light lights;

    //Debug state bool
    public bool debug = false;

    public Detection typeOfDetection;
    public Type typeOfFlick;

    private bool _image = false;
    private bool _text = false;
    private bool _material = false;
    private bool _lights = false;

    //Effect class
    [Serializable]
    public class Effect
    {
        //Parameters to setup
        public float flickSpeed = 1;
        public float normalColorDuration = 1;
        public float flickColorDuration = 1;
    }

    //Create the effects
    public Effect Flick = new Effect();
    public Effect PingPong = new Effect();
    public Effect Repeat = new Effect();

    void Awake()
    {
        //Check which components have the object
        CheckForComponents();
    }

    //Abstract function to check components
    protected virtual bool CheckComponent <T>()
        where T : Component
    {
        if(GetComponent<T>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void Update() {

        //Check components to avoid errors and dont display debug msg each frame
        CheckForComponentsRunTime();

        //Apply the type of effect on desired components
        switch (typeOfFlick) {

            case Type.Flick:
                if (_text) text.color = Color.Lerp(normalColor, flickColor, Mathf.Round(Mathf.PingPong(Time.time * Flick.flickSpeed, Flick.flickColorDuration)));
                if (_image) image.color = Color.Lerp(normalColor, flickColor, Mathf.Round(Mathf.PingPong(Time.time * Flick.flickSpeed, Flick.flickColorDuration)));
                if (_material) material.color = Color.Lerp(normalColor, flickColor, Mathf.Round(Mathf.PingPong(Time.time * Flick.flickSpeed, Flick.flickColorDuration)));
                if (_lights) lights.color = Color.Lerp(normalColor, flickColor, Mathf.Round(Mathf.PingPong(Time.time * Flick.flickSpeed, Flick.flickColorDuration)));


                break;

            case Type.PingPong:
                if (_text) text.color = Color.Lerp(normalColor, flickColor, Mathf.PingPong(Time.time * PingPong.flickSpeed, PingPong.flickColorDuration));
                if (_image) image.color = Color.Lerp(normalColor, flickColor, Mathf.PingPong(Time.time * PingPong.flickSpeed, PingPong.flickColorDuration));
                if (_material) material.color = Color.Lerp(normalColor, flickColor, Mathf.PingPong(Time.time * PingPong.flickSpeed, PingPong.flickColorDuration));
                if (_lights) lights.color = Color.Lerp(normalColor, flickColor, Mathf.PingPong(Time.time * PingPong.flickSpeed, PingPong.flickColorDuration));

                break;

            case Type.Repeat:
                if (_text) text.color = Color.Lerp(normalColor, flickColor, Mathf.Repeat(Time.time * Repeat.flickSpeed, Repeat.flickColorDuration));
                if (_image) image.color = Color.Lerp(normalColor, flickColor, Mathf.Repeat(Time.time * Repeat.flickSpeed, Repeat.flickColorDuration));
                if (_material) material.color = Color.Lerp(normalColor, flickColor, Mathf.Repeat(Time.time * Repeat.flickSpeed, Repeat.flickColorDuration));
                if (_lights) lights.color = Color.Lerp(normalColor, flickColor, Mathf.Repeat(Time.time * Repeat.flickSpeed, Repeat.flickColorDuration));

                break;
        }
    }

    void CheckForComponents()
    {
        _text = false;
        _image = false;
        _material = false;
        _lights = false;
        switch (typeOfDetection)
        {
            case Detection.auto:
                //Check if the gameobject have desired components
                if (CheckComponent<Image>())
                {
                    if (debug) Debug.Log("Color Flicker: Image detected on " + this.gameObject.name);
                    _image = true;
                    image = GetComponent<Image>();
                }
                if (CheckComponent<Text>())
                {
                    if (debug) Debug.Log("Color Flicker: Text detected on " + this.gameObject.name);
                    _text = true;
                    text = GetComponent<Text>();
                }
                if (CheckComponent<Renderer>())
                {
                    if (debug) Debug.Log("Color Flicker: Material detected on " + this.gameObject.name);
                    _material = true;
                    material = GetComponent<Renderer>().material;
                }
                if (CheckComponent<Light>())
                {
                    if (debug) Debug.Log("Color Flicker: Light detected on " + this.gameObject.name);
                    _lights = true;
                    lights = GetComponent<Light>();
                }

                break;
            case Detection.image:
                if (image != null)
                {
                    _image = true;
                }
                else
                {
                    if (debug)
                    {
                        Debug.Log("Color Flicker: Image component is missing. Your flick its not working");
                    }
                }
                _text = false;
                _material = false;
                _lights = false;
                break;
            case Detection.text:
                _image = false;
                if (text != null)
                {
                    _text = true;
                }
                else
                {
                    if (debug)
                    {
                        Debug.Log("Color Flicker: Text component is missing. Your flick its not working");
                    }
                }
                _material = false;
                _lights = false;
                break;
            case Detection.material:
                _image = false;
                _text = false;
                if (material != null)
                {
                    _material = true;
                }
                else
                {
                    if (debug)
                    {
                        Debug.Log("Color Flicker: Material component is missing. Your flick its not working");
                    }
                }
                _lights = false;
                break;
            case Detection.light:
                _image = false;
                _text = false;
                _material = false;
                if (lights != null)
                {
                    _lights = true;
                }
                else
                {
                    if (debug)
                    {
                        Debug.Log("Color Flicker: Light component is missing. Your flick its not working");
                    }
                }
                break;
            }
        }

        void CheckForComponentsRunTime()
        {
            _text = false;
            _image = false;
            _material = false;
            _lights = false;
            switch (typeOfDetection)
            {
                case Detection.auto:
                    //Check if the gameobject have desired components
                    if (CheckComponent<Image>())
                    {
                        _image = true;
                        image = GetComponent<Image>();
                    }
                    if (CheckComponent<Text>())
                    {
                        _text = true;
                        text = GetComponent<Text>();
                    }
                    if (CheckComponent<Renderer>())
                    {
                        _material = true;
                        material = GetComponent<Renderer>().material;
                    }
                    if (CheckComponent<Light>())
                    {
                        _lights = true;
                        lights = GetComponent<Light>();
                    }

                    break;
                case Detection.image:
                    if (image != null)
                    {
                        _image = true;
                    }
                    _text = false;
                    _material = false;
                    _lights = false;
                    break;
                case Detection.text:
                    //Take the text as public
                    _image = false;
                    if (text != null)
                    {
                        _text = true;
                    }
                    _material = false;
                    _lights = false;
                    break;
                case Detection.material:
                    //Take the material as public
                    _image = false;
                    _text = false;
                    if (material != null)
                    {
                        _material = true;
                    }
                    _lights = false;
                    break;
                case Detection.light:
                    _image = false;
                    _text = false;
                    _material = false;
                    if (lights != null)
                    {
                        _lights = true;
                    }
                    break;
                }
        }
}
