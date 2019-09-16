using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogCell : MonoBehaviour
{
    // Start is called before the first frame update\
    public string title;
    public Text text;
    public int type = -1;//0表示兆和 1表示兆电 2表示PPT
    public ShowManager showManager;
    public ProjectClass project;
    public List<Sprite> targetSprites;
    void Start ( )
    {
        targetSprites = new List<Sprite>();
    }

    // Update is called once per frame
    void Update ( )
    {

    }

    public void Init ( )
    {
        text.text = title;
        if (type != -1)
        {
            switch (type)
            {
                case 0:
                    GetComponent<Button>().onClick.AddListener(delegate ( ) {
                        showManager.SetShowImage(SaveData.instance.ZhLists,1);
                        CatalogPanel.Instance.shrinkPanel();
                    });
                    break;
                case 1:
                    GetComponent<Button>().onClick.AddListener(delegate ( ) {
                        showManager.SetShowImage(SaveData.instance.ZnLists,1);
                        CatalogPanel.Instance.shrinkPanel();
                    });
                    break;
                case 2:
                    GetComponent<Button>().onClick.AddListener(delegate ( ) {
                        showManager.SetShowImage(project.spriteLists , 1);
                        CatalogPanel.Instance.shrinkPanel();
                    });
                    break;

            }
        }
    }
}
