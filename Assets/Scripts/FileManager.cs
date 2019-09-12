using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FileManager : MonoBehaviour
{
    public List<string> dirLists;
    public List<string> pptLists;
    string currentFilePath;
    string document = "Picture";
    string fullPath;

    bool fileReadState = false;
    static public FileManager Instance;
     

    // Start is called before the first frame update
    private void Awake ( )
    {
        dirLists = new List<string>();
        pptLists = new List<string>();
        Instance = this;
    }
    void Start ( )
    {
        SearchDirectory();//加载文件夹，加载图片到SaveData
    }

    // Update is called once per frame
    void Update ( )
    {

    }

    void SearchDirectory ( )//加载文件夹
    {
        currentFilePath = System.Environment.CurrentDirectory;
        fullPath = currentFilePath + "\\" + document;
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo folder = new DirectoryInfo(fullPath);
            foreach (DirectoryInfo file in folder.GetDirectories())//主文件夹
            {
                dirLists.Add(file.FullName);//保存 兆和，兆能，PPT三个主文件夹的路径
            }
        }

        if (dirLists.Count != 0)//存在，则分别读取路径里面的图片
        {
            foreach (string s in dirLists)
            {
                DirectoryInfo folder = new DirectoryInfo(s);
                foreach (FileInfo file in folder.GetFiles())
                {
                    Sprite newSprite = Sprite.Create(new Texture2D(1920 , 1080) , new Rect(0 , 0 , 1920 , 1080) , new Vector2(0.5f , 0.5f));
                    newSprite.name = file.Name;
                    LoadImageByPath(file.FullName , out newSprite);
                    if (s.Contains("兆和"))
                    {
                        SaveData.instance.ZhLists.Add(newSprite);
                    }
                    else if (s.Contains("兆能"))
                    {
                        SaveData.instance.ZnLists.Add(newSprite);
                    }
                }

            }
        }
    }
    void LoadImageByPath (string path , out Sprite resultSprite)
    {
        double startTime = (double) Time.time;
        FileStream fileStream = new FileStream(path , FileMode.Open , FileAccess.Read);
        fileStream.Seek(0 , SeekOrigin.Begin);
        byte[] bytes = new byte[fileStream.Length];
        fileStream.Read(bytes , 0 , (int) fileStream.Length);
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;
        //创建Tex
        Texture2D texture = new Texture2D(1920 , 1080);
        texture.LoadImage(bytes);
        resultSprite = Sprite.Create(texture , new Rect(0 , 0 , texture.width , texture.height) , new Vector2(0.5f , 0.5f));
    }
    //PPT文件展示是保存有多种项目的目录，一个项目对应一个目录，该目录的名称中的第一个图片为展示图片
    public void SearchPPTDirectory ( )//加载PPT文件夹
    {
        if (dirLists.Count != 0 && SaveData.instance.ProjectLists.Count==0)//存在，则分别读取路径里面的图片
        {
            foreach (string s in dirLists)
            {
                if (s.Contains("项目展示"))
                {
                    DirectoryInfo folder = new DirectoryInfo(s);
                    foreach (DirectoryInfo directory in folder.GetDirectories())
                    {
                        pptLists.Add(directory.FullName);
                        ProjectClass newPrj = new ProjectClass();
                        newPrj.projectName = directory.Name;
                        foreach(FileInfo file in directory.GetFiles())
                        {
                            Sprite newSprite = Sprite.Create(new Texture2D(1920 , 1080) , new Rect(0 , 0 , 1920 , 1080) , new Vector2(0.5f , 0.5f));
                            newSprite.name = file.Name;
                            LoadImageByPath(file.FullName , out newSprite);
                            newPrj.spriteLists.Add(newSprite);
                        }
                        SaveData.instance.ProjectLists.Add(newPrj);
                    }
                }
            }
        }
        fileReadState = true;
    }

    public bool GetFileReadState()
    {
        return fileReadState;
    }

}
