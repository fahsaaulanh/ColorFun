using UnityEngine;

public class ColorPalatteData : ScriptableObject
{
    [System.Serializable]
    public enum ColorsState
    {
        none,
        lightBlue,
        red,
        purple,
        pink,
        orange,
        yellow,
        lightGreen,
        green,
        darkBlue,
        black,
        grey,
        custom,
    }

    [System.Serializable]
    public struct Colors
    {
        public Color color;
        public ColorsState state;
    }
}
