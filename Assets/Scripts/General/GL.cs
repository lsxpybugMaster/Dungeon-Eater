using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GL
{
    /// <summary>
    /// 字符 → 向量 映射表, 避免之后繁琐的
    /// </summary>
    //NOTE: const仅适用于简单类型用于编译时确定,对于复杂类型需要使用readonly代替,在运行时确定
    public static readonly Dictionary<char, Vector3> Direct = new Dictionary<char, Vector3>
    {
        { 'U', Vector3.up },     // 上：Y+
        { 'D', Vector3.down },   // 下：Y-
        { 'L', Vector3.left },   // 左：X-
        { 'R', Vector3.right }   // 右：X+
    };
}
