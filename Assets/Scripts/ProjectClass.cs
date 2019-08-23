using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectClass 
{
    public string projectName;
    public List<Sprite> spriteLists;
    public ProjectClass()
    {
        spriteLists = new List<Sprite>();
        projectName = "";
    }


}
