using System.IO; // 引入System.IO命名空间，用于读取文件
using UnityEngine; // 引入UnityEngine命名空间，用于打印日志
using System.Collections;
using System.Collections.Generic;

public class ReadText : MonoBehaviour
{
    // 定义一个数组，用于存储最后两个浮点数
    public List<float> x = new List<float>(),y = new List<float>();

    // 定义一个字符串变量，用于存储文件的路径
    private string filePath = "Assets/data/gesture.txt"; // 这里你可以根据你的文件位置修改

    void Awake()
    {
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
            // 行号加一
            lineNumber++;

            // 判断当前行是否是偶数行
            if (lineNumber % 2 == 0)
            {
                // 如果是偶数行，就对当前行的内容进行处理

                // 去掉两端的方括号
                line = line.Trim('[', ']');

                // 按照逗号分割字符串，得到一个字符串数组
                string[] numbers = line.Split(',');

                string numx=numbers[numbers.Length - 2].Trim(' ','(', ')');
                string numy=numbers[numbers.Length - 1].Trim(' ','(', ')');

                // 取出最后两个元素，转换为浮点数，并存储到数组中
                x.Add(float.Parse(numx));
                y.Add(float.Parse(numy));
                
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
