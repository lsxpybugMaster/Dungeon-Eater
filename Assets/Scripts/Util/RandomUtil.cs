using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ṩ��������ɹ���
/// </summary>
public static class RandomUtil
{
    /// <summary>
    /// ��ָ����Χ������ n �����ظ����������������
    /// </summary>
    /// <param name="min">��Сֵ��������</param>
    /// <param name="max">���ֵ����������</param>
    /// <param name="count">Ҫ���ɵ�����</param>
    /// <returns>���ظ������б�</returns>
    public static List<int> GetUniqueRandomIndexes(int min, int max, int count)
    {
        if (max <= min)
            throw new ArgumentException("max ������� min��");
        if (count > max - min)
            throw new ArgumentException("�����������ܳ�����Χ���ȡ�");

        List<int> result = new List<int>(count);
        HashSet<int> used = new HashSet<int>();

        while (result.Count < count)
        {
            int value = UnityEngine.Random.Range(min, max);
            if (used.Add(value))
                result.Add(value);
        }

        return result;
    }
}
