using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//右边目录,固定显示5个项目，其他PPT，所有的视频
public class CatalogPanel : MonoBehaviour
{
    static public CatalogPanel Instance;
    [SerializeField]
    int num = 0;//项目个数+PPT个数+视频个数
    public CatalogCell btnPrefabs;
    public Transform btnPrefabParent;
    public List<GameObject> btnLists;

    [Header("Panel")]
    public ShowManager showManager;
    public PPTManager PPTManager;

    [Header("Lists")]
    [SerializeField]
    List<Sprite> zhLists;
    [SerializeField]
    List<Sprite> znLists;
    List<ProjectClass> pptLists;

    [Header("Animator")]
    public Animator catalogButton;

    public GameObject tipsPanel;
    bool animatorState = false;

    void Awake()
    {
        Instance = this;
        catalogButton.GetComponent<Button>().onClick.AddListener(delegate ( ) {
            if (FileManager.Instance.GetFileReadState())
            {
                
                animatorState = !animatorState;
                catalogButton.SetBool("Start" , animatorState);
                GetComponent<Animator>().SetBool("Start" , animatorState);
            }
        });


    }


    void Start ( )
    {
        if (btnLists.Count <= 0)
        {
            zhLists = new List<Sprite>();
            znLists = new List<Sprite>();
            pptLists = new List<ProjectClass>();
        }
        StartCoroutine(Init());
    }

    void Update ( )
    {

    }

    IEnumerator Init ( )
    {
        yield return new WaitUntil(FileManager.Instance.GetFileReadState);
        num = pptLists.Count  + 10;
        pptLists = SaveData.instance.ProjectLists;
        zhLists = SaveData.instance.ZhLists;
        znLists = SaveData.instance.ZnLists;
        btnLists = new List<GameObject>();

        //生成兆和项目
        GameObject znBtn = GameObject.Instantiate(btnPrefabs.gameObject , btnPrefabParent);
        znBtn.SetActive(true);
        znBtn.GetComponent<CatalogCell>().title = "广东市兆能有限公司介绍";
        znBtn.GetComponent<CatalogCell>().showManager = this.showManager;
        znBtn.GetComponent<CatalogCell>().type = 0;
        znBtn.GetComponent<CatalogCell>().targetSprites = znLists;
        znBtn.GetComponent<CatalogCell>().Init();
        btnLists.Add(znBtn);
        //兆能项目
        GameObject zhBtn = GameObject.Instantiate(btnPrefabs.gameObject , btnPrefabParent);
        zhBtn.SetActive(true);
        zhBtn.GetComponent<CatalogCell>().title = "广东市兆和电力技术有限公司介绍";
        zhBtn.GetComponent<CatalogCell>().showManager = this.showManager;
        zhBtn.GetComponent<CatalogCell>().type = 1;
        zhBtn.GetComponent<CatalogCell>().targetSprites = zhLists;
        zhBtn.GetComponent<CatalogCell>().Init();
        btnLists.Add(zhBtn);


        for (int i = 0 ; i <= pptLists.Count - 1 ; i++)//生成PPT项目
        {
            GameObject newBtn = GameObject.Instantiate(btnPrefabs.gameObject , btnPrefabParent);
            newBtn.SetActive(true);
            newBtn.GetComponent<CatalogCell>().title = pptLists[i].projectName;
            newBtn.GetComponent<CatalogCell>().showManager = this.showManager;
            newBtn.GetComponent<CatalogCell>().type = 2;
            newBtn.GetComponent<CatalogCell>().project = pptLists[i];
            newBtn.GetComponent<CatalogCell>().Init();
            btnLists.Add(newBtn);
        }
    }
    
    public void shrinkPanel()
    {
        catalogButton.SetBool("Start" , false);
        GetComponent<Animator>().SetBool("Start" , false);
    }
}
