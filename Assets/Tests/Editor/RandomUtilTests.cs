using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomUtilTests
{
    //NOTE: 其不是严格的单元测试
    [Test]
    public void RandomUtil_Prints_Correct()
    {
        for (int i = 0; i < 10; i++)
        {
            var data = RandomUtil.GetUniqueRandomIndexes(0, 10, 3);
            TestContext.WriteLine(string.Join(", ", data));
        }
    }

    // 测试 DrawMultiple() 方法 - 允许重复
    [Test]
    public void DrawMultiple_WithEmptyList_ReturnsEmptyList()
    {
        // Arrange
        List<string> emptyList = new List<string>();

        // Act
        var result = emptyList.GetRandomN(3, true);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [Test]
    public void DrawMultiple_WithZeroCount_ReturnsEmptyList()
    {
        // Arrange
        List<int> list = new List<int> { 1, 2, 3 };

        // Act
        var result = list.GetRandomN(0, true);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [Test]
    public void DrawMultiple_WithNegativeCount_ReturnsEmptyList()
    {
        // Arrange
        List<int> list = new List<int> { 1, 2, 3 };

        // Act
        var result = list.GetRandomN(-1, true);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [Test]
    public void DrawMultiple_AllowDuplicates_ReturnsCorrectCount()
    {
        // Arrange
        List<int> list = new List<int> { 1, 2, 3 };
        const int count = 5;

        // Act
        var result = list.GetRandomN(count, true);

        // Assert
        Assert.AreEqual(count, result.Count);
        // 由于允许重复，结果可能包含重复元素
        // 验证所有元素都在原始列表范围内
        foreach (var item in result)
        {
            Assert.IsTrue(item >= 1 && item <= 3);
        }
    }

    [Test]
    public void DrawMultiple_AllowDuplicates_OriginalListNotModified()
    {
        // Arrange
        List<int> list = new List<int> { 1, 2, 3 };
        List<int> originalList = new List<int>(list);
        const int count = 5;

        // Act
        var result = list.GetRandomN(count, true);

        // Assert
        // 原始列表不应该被修改（因为允许重复时我们不会移除元素）
        CollectionAssert.AreEqual(originalList, list);
        Assert.AreEqual(originalList.Count, list.Count);
    }

    // 测试 DrawMultiple() 方法 - 不允许重复
    [Test]
    public void DrawMultiple_DisallowDuplicates_ReturnsUniqueItems()
    {
        // Arrange
        List<string> list = new List<string> { "A", "B", "C", "D" };
        const int count = 3;

        // Act
        var result = list.GetRandomN(count, false);

        // Assert
        Assert.AreEqual(count, result.Count);

        // 验证没有重复元素
        var distinctResult = result.Distinct().ToList();
        Assert.AreEqual(count, distinctResult.Count);

        // 验证所有元素都在原始列表中
        foreach (var item in result)
        {
            Assert.IsTrue(list.Contains(item));
        }
    }

    [Test]
    public void DrawMultiple_DisallowDuplicates_WithCountGreaterThanList_ReturnsAllItems()
    {
        // Arrange
        List<int> list = new List<int> { 1, 2, 3 };
        const int count = 10; // 大于列表元素数量

        // Act
        var result = list.GetRandomN(count, false);

        // Assert
        // 应该返回列表的所有元素（不超过列表大小）
        Assert.AreEqual(list.Count, result.Count);
        CollectionAssert.AreEquivalent(list, result); // 顺序可能不同，但元素相同
    }

    [Test]
    public void DrawMultiple_DisallowDuplicates_OriginalListNotModified()
    {
        // Arrange
        List<int> list = new List<int> { 1, 2, 3, 4, 5 };
        List<int> originalList = new List<int>(list);
        const int count = 3;

        // Act
        var result = list.GetRandomN(count, false);

        // Assert
        // 原始列表不应该被修改
        CollectionAssert.AreEqual(originalList, list);
        Assert.AreEqual(originalList.Count, list.Count);
    }

    [Test]
    public void DrawMultiple_DisallowDuplicates_TempListIsModifiedCorrectly()
    {
        // Arrange
        List<int> list = new List<int> { 1, 2, 3, 4, 5 };
        const int count = 3;

        // Act
        var result = list.GetRandomN(count, false);

        // Assert
        // 结果应该包含不重复的元素
        Assert.AreEqual(count, result.Count);

        var distinctResult = result.Distinct().ToList();
        Assert.AreEqual(count, distinctResult.Count);
    }


    [Test]
    public void DrawMultiple_DefaultParameter_AllowsDuplicates()
    {
        // Arrange
        List<int> list = new List<int> { 1, 2, 3 };
        const int count = 5;

        // Act - 使用默认参数（不指定 allowDuplicates）
        var result = list.GetRandomN(count);

        // Assert - 应该允许重复
        Assert.AreEqual(count, result.Count);
        // 由于 count > list.Count，必然会有重复
        Assert.IsTrue(result.GroupBy(x => x).Any(g => g.Count() > 1));
    }
}
