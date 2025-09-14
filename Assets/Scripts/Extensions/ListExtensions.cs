using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//静态工具类,无需挂载脚本,直接使用
public static class ListExtensions 
{
    //从list中抽取数据的泛型类
    //this 关键字指明这是一个扩展方法,通过myList.Draw()调用
    public static T Draw<T>(this List<T> list)
    {
        //default 关键字返回T类型的默认值
        if (list.Count == 0) return default;
        int r = Random.Range(0, list.Count);
        T t = list[r];
        list.Remove(t);
        return t;
    }
}
