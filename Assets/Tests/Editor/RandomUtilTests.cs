using NUnit.Framework;

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
}
