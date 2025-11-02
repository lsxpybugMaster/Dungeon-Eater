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

}
