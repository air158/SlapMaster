using System.IO; // 引入System.IO命名空间，用于读取文件
using UnityEngine; // 引入UnityEngine命名空间，用于打印日志
using System.Collections;
using System.Collections.Generic;

public class ReadText : MonoBehaviour
{
    public string name = "gesture";
    // 定义一个数组，用于存储最后两个浮点数
    public List<float> x = new List<float>(),y = new List<float>();

    // 定义一个字符串变量，用于存储文件的路径
    private string filePath = ""; // 这里你可以根据你的文件位置修改

    void Awake()
    {
        filePath = "Assets/data/"+name+".txt";
        // 调用ReadFile方法，读取文件内容
        ReadFile(filePath);
    }

    void ReadFile(string path)
    {
        // 使用StreamReader类来读取文件
        StreamReader reader = new StreamReader(path);

        // 定义一个字符串变量，用于存储当前行的内容
        string line;

        // 定义一个整数变量，用于记录当前行的序号
        int lineNumber = 0;

        // 使用while循环，逐行读取文件，直到文件末尾
        while ((line = reader.ReadLine()) != null)
        {

            if(!line.Contains('[')){
                continue;
            }

            // 按照逗号分割字符串，得到一个字符串数组
            string[] numbers = line.Split(',');

            string numx=numbers[numbers.Length - 2].Trim(' ','(', ')','[', ']',',');
            string numy=numbers[numbers.Length - 1].Trim(' ','(', ')','[', ']',',');

            float numberx=0,numbery=0;

            // Debug.Log(numx+" "+numy);
            if(float.TryParse(numx, out numberx)&&float.TryParse(numy, out numbery))
            {
                //转换成功, 输出数字
                Debug.Log ("数字是:" + numberx+" "+numbery);
                // 取出最后两个元素，转换为浮点数，并存储到数组中
                x.Add(numberx);
                y.Add(numbery);
            }else{
                //转换失败, 字符串不是只是数字
                Debug.Log("这个不是数字");
            }

            
        }

        // 关闭文件流
        reader.Close();

        int len = x.Count;

        for(int i=0;i<len;i++){
            // 打印数组中的元素，验证结果是否正确
                Debug.Log("The last two numbers are: " + x[i] + " and " + y[i]);
        }
        
    }
}
