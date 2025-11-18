using NUnit.Framework;
using System.Collections.Generic;

public class DiceRollUtilTests
{
    
    public int Test_Roll_times;

    [SetUp]
    public void Setup()
    {
        // 用于在每个测试方法运行之前执行的代码
        Test_Roll_times = 1000; // 或从配置文件加载
    }

    [Test]
    public void Test_D6_Range_And_Distribution()
    {
        Dictionary<int, int> frequency = new Dictionary<int, int>();

        for (int i = 1; i <= 6; i++)
            frequency[i] = 0;

        for (int i = 0; i < Test_Roll_times; i++)
        {
            //调用函数
            int result = DiceRollUtil.D6();

            // 断言：结果在 1~6 范围内
            Assert.That(result, Is.InRange(1, 6), $"D6 结果超出范围: {result}");

            frequency[result]++;
        }

        // 打印分布信息
        TestContext.WriteLine("D6 分布:");
        foreach (var kvp in frequency)
            TestContext.WriteLine($"值 {kvp.Key}: {kvp.Value} 次");

        // 断言：至少每个面都出现过
        foreach (var count in frequency.Values)
            Assert.That(count, Is.GreaterThan(0), "某些面从未出现，随机性异常");
    }

    // ----------------------------
    // Test DfromString 基础功能
    // ----------------------------
    [Test]
    public void Test_DfromString_FixedValue()
    {
        Assert.That(DiceRollUtil.DfromString("10"), Is.EqualTo(10));
        Assert.That(DiceRollUtil.DfromString("   20   "), Is.EqualTo(20));
    }

    [Test]
    public void Test_DfromString_SingleDice()
    {
        for (int i = 0; i < Test_Roll_times; i++)
        {
            int v = DiceRollUtil.DfromString("1d6");
            Assert.That(v, Is.InRange(1, 6));
        }
    }

    [Test]
    public void Test_DfromString_DiceWithCount()
    {
        for (int i = 0; i < Test_Roll_times; i++)
        {
            int v = DiceRollUtil.DfromString("3d4");
            Assert.That(v, Is.InRange(3, 12));
        }
    }

    [Test]
    public void Test_DfromString_MixedExpression()
    {
        for (int i = 0; i < Test_Roll_times; i++)
        {
            int v = DiceRollUtil.DfromString("10 + 2d6 + 3");

            // 最小：10 + 2*1 + 3 = 15
            // 最大：10 + 2*6 + 3 = 25
            Assert.That(v, Is.InRange(15, 25));
        }
    }

    [Test]
    public void Test_DfromString_IgnoresSpaces()
    {
        for (int i = 0; i < Test_Roll_times; i++)
        {
            int v1 = DiceRollUtil.DfromString(" 10 + 2d6 ");
            int v2 = DiceRollUtil.DfromString("10+2d6");

            Assert.That(v1, Is.InRange(12, 22));
            Assert.That(v2, Is.InRange(12, 22));
        }
    }

    // ----------------------------
    // Test 省略数量 d6 = 1d6
    // ----------------------------
    [Test]
    public void Test_DfromString_OmitCount()
    {
        for (int i = 0; i < Test_Roll_times; i++)
        {
            int v = DiceRollUtil.DfromString("d8"); // should be 1d8
            Assert.That(v, Is.InRange(1, 8));
        }
    }

    // ----------------------------
    // Test + 和 - 前缀
    // ----------------------------
    [Test]
    public void Test_DfromString_WithMinus()
    {
        for (int i = 0; i < Test_Roll_times; i++)
        {
            int v = DiceRollUtil.DfromString("10 - 1d6");

            // 最小：10 - 6 = 4
            // 最大：10 - 1 = 9
            Assert.That(v, Is.InRange(4, 9));
        }
    }

    // ----------------------------
    // 多组合表达式
    // ----------------------------
    [Test]
    public void Test_DfromString_ComplexExpression()
    {
        for (int i = 0; i < Test_Roll_times; i++)
        {
            int v = DiceRollUtil.DfromString("10 + 2d8 - 3 + d4");

            // 最小：10 + 2*1 - 3 + 1 = 10
            // 最大：10 + 2*8 - 3 + 4 = 27
            Assert.That(v, Is.InRange(10, 27));
        }
    }

    // ----------------------------
    // 错误输入不崩溃
    // ----------------------------
    [Test]
    public void Test_DfromString_InvalidInput()
    {
        Assert.DoesNotThrow(() => DiceRollUtil.DfromString("abc"));
        Assert.DoesNotThrow(() => DiceRollUtil.DfromString("10 + xyz"));
        Assert.DoesNotThrow(() => DiceRollUtil.DfromString(""));
        Assert.DoesNotThrow(() => DiceRollUtil.DfromString(null));
    }
}
