using NUnit.Framework;

//IMPORTANT: 这是一个测试脚本,必须放置在Assets/Tests/Editor下
/// <summary>
/// 测试 RNGSystem 随机数生成器的可控性与一致性
/// </summary>
public class RNGSystemTests
{
    //代表一个测试单元
    [Test]
    public void SameSeed_Should_Produce_SameResults()
    {
        // 两个系统使用同一个种子
        RNGSystem rngA = new RNGSystem(12345);
        RNGSystem rngB = new RNGSystem(12345);

        // 获取同名子流
        var streamA = rngA.GetStream("MapGen");
        var streamB = rngB.GetStream("MapGen");

        // 生成一组随机数
        int[] resultsA = new int[5];
        int[] resultsB = new int[5];

        for (int i = 0; i < 5; i++)
        {
            resultsA[i] = streamA.Next(0, 100);
            resultsB[i] = streamB.Next(0, 100);
        }

        // 比较每个结果是否一致
        for (int i = 0; i < 5; i++)
        {
            Assert.AreEqual(resultsA[i], resultsB[i], $"Mismatch at index {i}");
        }
    }

    [Test]
    public void DifferentSeed_Should_Produce_DifferentResults()
    {
        RNGSystem rngA = new RNGSystem(11111);
        RNGSystem rngB = new RNGSystem(22222);

        var streamA = rngA.GetStream("BattleReward");
        var streamB = rngB.GetStream("BattleReward");

        int valueA = streamA.Next(0, 10000);
        int valueB = streamB.Next(0, 10000);

        // 理论上不同种子应生成不同结果（概率极低相同）
        Assert.AreNotEqual(valueA, valueB, "Different seeds unexpectedly produced identical result");
    }

    [Test]
    public void SameStreamName_Should_Return_SameInstance()
    {
        RNGSystem rng = new RNGSystem(99999);

        var stream1 = rng.GetStream("Loot");
        var stream2 = rng.GetStream("Loot");

        Assert.AreSame(stream1, stream2, "GetStream should return same instance for same stream name");
    }

}
