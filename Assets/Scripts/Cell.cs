using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//PPT Cell
public class Cell : MonoBehaviour
{
    public ShowManager showManager;
    public Text content;
    public Button jumpBtn;
    public Image image;
    private List<Sprite> curSpriteList;


    
    // Start is called before the first frame update
    void Start()
    {
        jumpBtn.onClick.AddListener(delegate ( )
        {
            showManager.SetShowImage(curSpriteList , 1);
            Debug.Log(gameObject.name);
        });
    }

    public void setCurLists(List<Sprite> targetLists)
    {
        curSpriteList = targetLists;
    }
}
