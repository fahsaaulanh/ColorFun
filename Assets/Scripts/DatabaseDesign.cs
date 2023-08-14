using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DesignObject
{

    [System.Serializable]
    public struct Blok
    {
        public string nameBlok;
        public Color colorBlok;
    }

    [System.Serializable]
    public struct BlokHexa
    {
        public string nameBlok;
        public string hexaBlok;
    }

    [System.Serializable]
    public struct Object
    {
        public string id;
        public int tamplateId;
        public string name;
        public string author;
        public List<Blok> blok;
    }

    [System.Serializable]
    public struct ObjectHexa
    {
        public string id;
        public int tamplateId;
        public string name;
        public string author;
        public List<BlokHexa> blok;
    }

    public Object designObj;
    public ObjectHexa objectHexa;
}


[System.Serializable]
public class DatabaseDesign 
{
    public List<DesignObject.ObjectHexa> dataHexaList;
}
