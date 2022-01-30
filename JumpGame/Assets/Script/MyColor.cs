using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyColor : MonoBehaviour
{
    public static Color GreenNoWhite = new Color((float)0/255f, (float)181f/255f, (float)0f/255f);
    public static Color BlueNoWhite = new Color((float)0f/255f, (float)71f/255f, (float)171f/255f);

    public static Color R   = Color.red*0.5f + Color.white * 0.5f;
    public static Color G   = (GreenNoWhite*0.5f) + (Color.white * 0.5f);
    public static Color B   = (BlueNoWhite * 0.5f)+ (Color.white * 0.5f);
    public static Color RB  = new Color(0.6666667f , 0.4261438f, 0.5568628f);
    public static Color RG = new Color(0.6666667f, 0.5699347f, 0.3333333f);
    public static Color GB = new Color(0.3333333f, 0.6627451f, 0.5568628f);
    public static Color RGB = new Color(0.5f, 0.4970588f, 0.4176471f);
    public static Color RRB = new Color(0.75f, 0.3196079f, 0.4176471f);
    public static Color RRG = new Color(0.75f, 0.427451f, 0.25f);
    public static Color RGG = new Color(0.5f, 0.604902f, 0.25f);
    public static Color BBG = new Color(0.25f, 0.5666667f, 0.5852941f);
    public static Color BGG = new Color(0.25f, 0.6745098f, 0.4176471f);
    public static Color RBB = new Color(0.5f, 0.3892157f, 0.5852941f);

    public static List<Color> colors = new List<Color>{ R, G, B,RB,RG,GB,RGB,RRB,RRG,BBG,BGG,RBB };

    private void Start()
    {
        for(int i=0;i<colors.Count;i++)
        {
            Debug.Log(i + " : " + colors[i]);
        }
    }
}
