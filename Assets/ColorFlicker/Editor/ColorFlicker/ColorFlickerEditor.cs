using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;


[CustomEditor(typeof(ColorFlicker))]
public class ColorFlickerEditor : Editor {

    public override void OnInspectorGUI()
    {
        ColorFlicker myTarget = (ColorFlicker)target;

        EditorGUILayout.LabelField("Colors", EditorStyles.boldLabel);

        myTarget.normalColor = EditorGUILayout.ColorField("Normal color", myTarget.normalColor);
        myTarget.flickColor = EditorGUILayout.ColorField("Flick color", myTarget.flickColor);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Object asignment", EditorStyles.boldLabel);

        myTarget.typeOfDetection = (ColorFlicker.Detection)EditorGUILayout.EnumPopup("Detection type", myTarget.typeOfDetection);

        switch (myTarget.typeOfDetection)
        {
            case ColorFlicker.Detection.auto:
                break;

            case ColorFlicker.Detection.image:
                myTarget.image = (Image)EditorGUILayout.ObjectField("Image", myTarget.image, typeof(Image), true);
                break;

            case ColorFlicker.Detection.text:
                myTarget.text = (Text)EditorGUILayout.ObjectField("Text", myTarget.text, typeof(Text), true);
                break;

            case ColorFlicker.Detection.material:
                myTarget.material = (Material)EditorGUILayout.ObjectField("Material", myTarget.material, typeof(Material), true);
                break;

            case ColorFlicker.Detection.light:
                myTarget.lights = (Light)EditorGUILayout.ObjectField("Light", myTarget.lights, typeof(Light), true);
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Effect customization", EditorStyles.boldLabel);

        myTarget.typeOfFlick = (ColorFlicker.Type)EditorGUILayout.EnumPopup("Type of flick", myTarget.typeOfFlick);

        switch (myTarget.typeOfFlick)
        {
            case ColorFlicker.Type.Flick:
                myTarget.Flick.flickSpeed = EditorGUILayout.Slider("Speed", myTarget.Flick.flickSpeed, 0.25f, 15f);
                myTarget.Flick.flickColorDuration = EditorGUILayout.Slider("Flick color duration", myTarget.Flick.flickColorDuration, 1f, 5f);
                break;

            case ColorFlicker.Type.PingPong:
                myTarget.PingPong.flickSpeed = EditorGUILayout.Slider("Speed", myTarget.PingPong.flickSpeed, 0.25f, 10f);
                myTarget.PingPong.flickColorDuration = EditorGUILayout.Slider("Flick color duration", myTarget.PingPong.flickColorDuration, 1f, 5f);
                break;

            case ColorFlicker.Type.Repeat:
                myTarget.Repeat.flickSpeed = EditorGUILayout.Slider("Speed", myTarget.Repeat.flickSpeed, 0.25f, 10f);
                myTarget.Repeat.flickColorDuration = EditorGUILayout.Slider("Flick color duration", myTarget.Repeat.flickColorDuration, 1f, 5f);
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Other settings", EditorStyles.boldLabel);

        myTarget.debugEnum = (ColorFlicker.Bool)EditorGUILayout.EnumPopup("Debug", myTarget.debugEnum);

        switch (myTarget.debugEnum)
        {
            case ColorFlicker.Bool.disabled:
                myTarget.debug = false;
                break;
            case ColorFlicker.Bool.enabled:
                myTarget.debug = true;
                break;
        }
    }
}
