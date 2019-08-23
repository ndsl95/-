using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowManager : MonoBehaviour
{
    public Button backBtn;
    public Button frontBtn;
    public Button nextBtn;
    public Image showImage;
    List<Sprite> curSpriteList;//当前展示的SpriteList
    int curIndex;//当前展示的index
    int returnTag;//0表示直接返回main，1表示返回pptPanel
    public static  ShowManager instance;

    private void Awake ( )
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(delegate ( )
        {
            curIndex = 0;
            this.gameObject.SetActive(false);
        });

        frontBtn.onClick.AddListener(delegate ( )
        {
            if(curIndex>0)
            {
                curIndex--;
                showImage.sprite = curSpriteList[curIndex]; 
            }
        });

        nextBtn.onClick.AddListener(delegate ( )
        {
            if(curIndex<curSpriteList.Count-1)
            {
                curIndex++;
                showImage.sprite = curSpriteList[curIndex];
            }
        });
    }

    //设置ShowImage需要显示哪个spriteList,tag用于表示showimage返回是返回到main还是pptPanel
    public void SetShowImage(List<Sprite> targetSprite,int tag)
    {
        returnTag = tag;
        curSpriteList = targetSprite;
        this.gameObject.SetActive(true);
        showImage.sprite = curSpriteList[0];
    }
}
