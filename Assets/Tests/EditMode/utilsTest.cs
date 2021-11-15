using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class utilsTest
{
    
    [Test]
    public void testScoreReset()
    {
        utils.resetScore();
        Assert.AreEqual(0, utils.score);
    }

    [Test]
    public void testScoreMaxMultiplier()
    {
        testScoreReset();
        utils.updateScore(0.1f);
        Assert.AreEqual(13, utils.score);
    }

    [Test]
    public void testScoreMiddleMultiplier()
    {
        testScoreReset();
        utils.updateScore(3.0f);
        Assert.AreEqual(10, utils.score);
    }

    [Test]
    public void testScoreLowMultiplier()
    {
        testScoreReset();
        utils.updateScore(10.01f);
        Assert.AreEqual(7, utils.score);
    }

    [Test]
    public void testScoreMixMultiplier()
    {
        testScoreReset();
        utils.updateScore(0.1f);
        utils.updateScore(3.0f);
        utils.updateScore(10.01f);
        Assert.AreEqual(30, utils.score);
    }

    [Test]
    public void testResetBall()
    {
        utils.resetBall();
        GameObject ball = GameObject.Find("Ball");
        Assert.AreEqual(new Vector3(15, 3, 0), ball.transform.position);
    }

    [Test]
    public void testResetFirstBaseman()
    {
        utils.resetFirstBaseman();
        GameObject firstbaseman = GameObject.Find("1B");
        Assert.AreEqual(new Vector3(7, 0, 23), firstbaseman.transform.position);
    }

    [Test]
    public void testResetSecondBaseman()
    {
        utils.resetSecondsBaseman();
        GameObject secondbaseman = GameObject.Find("2B");
        Assert.AreEqual(new Vector3(-6, 0, 14), secondbaseman.transform.position);
    }

    [Test]
    public void testResetShortStop()
    {
        utils.resetShortStop();
        GameObject ss = GameObject.Find("SS");
        Assert.AreEqual(new Vector3(-6, 0, -11), ss.transform.position);

    }

    [Test]
    public void testResetThirdBaseman()
    {
        utils.resetThirdBaseman();
        GameObject third = GameObject.Find("3B");
        Assert.AreEqual(new Vector3(8, 0, -20), third.transform.position);
    }
    

    [Test]
    public void testResetCamera()
    {
        utils.resetCamera();
        GameObject cam = GameObject.Find("MainCamera");
        Assert.AreEqual(new Vector3(43, 10, 0), cam.transform.position);
    }

    [Test]
    public void testResetTimer()
    {
        utils.resetTimer();
        Assert.AreEqual(0.0f, utils.player_timer);
    }
    
    [Test]
    public void testResetPitcher()
    {
        utils.resetPitcher();
        Assert.AreEqual(0.0f, BallManager.game_time);
    }
    

}
