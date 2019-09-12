using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PPTManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PPTManager Instance;

    public GameObject rootGrid;
    [SerializeField]
    public List<ProjectClass> prjClassLists;//Data读取的字典
    [SerializeField]
    List<GameObject> cellLists;//实例GameObject
    public GameObject cell;//单个Cell
    public GameObject panel;//单个Panel
    public ShowManager showManager;
    public ScrollRect scrollView;
    public Scrollbar horScrollBar;
    public Button frontBtn;
    public Button nextBtn;
    public Button backBtn;
    public int result = 1;//Panel个数

    public float stepPanel=0;//scroll步进

    [Header("Random PPT")]
    public bool playRandomPPT = false;
    [SerializeField]
    float currentTime = 0.0f;
    public int limitTime = 5;
    
    public int currentPPTIndex = 0;//当前播放的下标
    [SerializeField]
    ProjectClass currentPPT;//当前播放的PPT项目

    private void Awake ( )
    {

        cellLists = new List<GameObject>();
        backBtn.onClick.AddListener(delegate ( )
        {
            gameObject.SetActive(false);
        });
        frontBtn.onClick.AddListener(LeftPanel);
        nextBtn.onClick.AddListener(RightPanel);
        Instance = this;

    }

    void Update()
    {
        if (playRandomPPT)//开始随机播放PPT
        {
            Debug.Log("Project:" + currentPPT.projectName);
            gameObject.SetActive(true);
            currentTime += Time.deltaTime;
            if (currentTime >= limitTime)
            {
                if (currentPPTIndex < currentPPT.spriteLists.Count - 1)//还有下一张
                {
                    showManager.NextPanel();
                    currentPPTIndex++;
                }
                else
                {
                    PlayRandomPPT(limitTime);
                }
                currentTime = 0.0f;
            }

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playRandomPPT = false;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayRandomPPT(1);
        }
    }

    public void setPPTValue (bool value)
    {
        playRandomPPT = value;
    }


    //随机播放一个项目APP
    public void PlayRandomPPT (int time = 5)
    {
        this.gameObject.SetActive(true);
        limitTime = time;
        int projectIndex = UnityEngine.Random.Range(0 , SaveData.instance.ProjectLists.Count - 1);//随机选择一个项目
        currentPPT = SaveData.instance.ProjectLists[projectIndex];
        showManager.SetShowImage(currentPPT.spriteLists,1);
        currentPPTIndex = 0;
        playRandomPPT = true;
    }


    public void Init()
    {
        if (prjClassLists.Count == 0)
        {
            prjClassLists = new List<ProjectClass>();
            prjClassLists = SaveData.instance.ProjectLists;
            int curIndex = 0;//当curIndex==8时，需要重新新建一个Panel
            GameObject newPanel = GameObject.Instantiate(panel , rootGrid.transform);

    
            foreach (ProjectClass prj in prjClassLists)
            {
                if (curIndex == 8)
                {
                    curIndex = 0;
                    newPanel = GameObject.Instantiate(panel , rootGrid.transform);
                    rootGrid.GetComponent<RectTransform>().sizeDelta -= new Vector2(-1480 , 0);
                    result++;
                }
                curIndex++;
                GameObject newCell = GameObject.Instantiate(cell , newPanel.transform);
                newCell.GetComponent<Cell>().setCurLists(prj.spriteLists);
                newCell.GetComponent<Cell>().name = prj.projectName;
                newCell.GetComponent<Cell>().showManager = showManager;
                newCell.GetComponent<Cell>().content.text = prj.projectName;
                newCell.GetComponent<Cell>().image.sprite = prj.spriteLists[0];
                cellLists.Add(newCell);
            }
        }
        gameObject.SetActive(true);
        scrollView.horizontalNormalizedPosition = 0;
        stepPanel = 1 / result;
    }

    public void LeftPanel( )
    {

        scrollView.horizontalNormalizedPosition -= stepPanel;
        if (scrollView.horizontalNormalizedPosition < 0)
            scrollView.horizontalNormalizedPosition = 0;
    }

    public void RightPanel( )
    {

        scrollView.horizontalNormalizedPosition  +=stepPanel;
        if (scrollView.horizontalNormalizedPosition > 1)
            scrollView.horizontalNormalizedPosition = 1;
    }


    
}
