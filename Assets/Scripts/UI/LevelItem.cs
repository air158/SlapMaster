using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelItem : MonoBehaviour
{
    /// <summary>
    /// 关卡ID
    /// </summary>
    private int LevelId;
    /// <summary>
    /// 创建按钮
    /// </summary>
    private Button btn;

    // Start is called before the first frame update
    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="id">关卡ID</param>
    /// <param name="isLock">是否锁住关卡</param>
    public void Init(int id, bool isLock)
    {
        LevelId = id;
        if (isLock)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

    /// <summary>
    /// 点击监听
    /// </summary>
    private void OnClick()
    {
        //场景加载，进入关卡
        //确保BuildSetting中的场景编号没有问题
        SceneManager.LoadScene(LevelId);
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Box");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
