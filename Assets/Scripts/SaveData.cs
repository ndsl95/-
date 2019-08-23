using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//该脚本用于保存从FileManager读取取出来的图片

//小屏幕文件夹的格式固定为:ZH,ZN,PPT
//PPT里面会有多个项目介绍

public class SaveData : MonoBehaviour
{
    [SerializeField]
    List<Sprite> zhLists;
    [SerializeField]
    List<Sprite> znLists;
    [SerializeField]
     List<ProjectClass> projectLists;
    public static SaveData instance;

    public List<Sprite> ZnLists { get => znLists; set => znLists = value; }
    public List<Sprite> ZhLists { get => zhLists; set => zhLists = value; }
    public List<ProjectClass> ProjectLists { get => projectLists; set => projectLists = value; }

    public void Awake ( )
    {
        instance = this;
        ZhLists = new List<Sprite>();
        ZnLists = new List<Sprite>();
        ProjectLists = new List<ProjectClass>();
    }

    
}
