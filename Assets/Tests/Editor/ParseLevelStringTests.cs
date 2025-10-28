using NUnit.Framework;
using System;

//字符串解析函数的单元测试
public class ParseLevelStringTests
{
    private LevelData testLevel;

    [SetUp]
    public void Setup()
    {
        testLevel = new LevelData();
    }


    [Test]
    public void NormalInput_ExpandsCorrectly()
    {
        Assert.AreEqual("LLLLRRRR", StringUtil.ParseLevelString("L4R4"));
    }

    [Test]
    public void MultiDigit_WorksCorrectly()
    {
        Assert.AreEqual(new string('R', 12) + "LL", StringUtil.ParseLevelString("R12L2"));
    }

    [Test]
    public void LowercaseLetters_ConvertedToUpper()
    {
        Assert.AreEqual("LLLLRRRR", StringUtil.ParseLevelString("l4r4"));
    }

    [Test]
    public void SpacesAndTabs_Ignored()
    {
        Assert.AreEqual("LLLLRRRR", StringUtil.ParseLevelString(" L4\tR4 "));
    }

    [Test]
    public void NoDigits_ReturnsOriginal()
    {
        Assert.AreEqual("LRUD", StringUtil.ParseLevelString("LRUD"));
    }

    [Test]
    //是否兼容单字符?
    public void Mixed_WorksCorrectly()
    {
        Assert.AreEqual("LLRULLLD", StringUtil.ParseLevelString("L2RUL3D"));
        Assert.AreEqual("LLRULLLDLR", StringUtil.ParseLevelString("L2RUL3DLR"));
    }

    [Test]
    public void MixedWhitespaceAndLowercase_StillWorks()
    {
        Assert.AreEqual("UUUUUUUUUUUUDDDDLLLL", StringUtil.ParseLevelString("  u12 d4 l4 "));
    }

}
