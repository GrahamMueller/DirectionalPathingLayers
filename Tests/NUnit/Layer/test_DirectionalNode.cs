using NUnit.Framework;


public class test_DirectionalNode
{
    [Test]
    public void Test_Invert()
    {
        DirectionalNode node_all_true = new DirectionalNode(new bool[] { true, true, true, true, true, true });

        DirectionalNode node_all_false = new DirectionalNode(new bool[] { false, false, false, false, false, false });

        //Test ~
        Assert.IsTrue(node_all_false.Equals(~node_all_true));
        Assert.IsTrue(node_all_true.Equals(~node_all_false));
        Assert.IsTrue(node_all_true.Equals(~~node_all_true));
    }


    [Test]
    public void Test_Or()
    {
        DirectionalNode node_all_true = new DirectionalNode(new bool[] { true, true, true, true, true, true });

        DirectionalNode node_all_false = new DirectionalNode(new bool[] { false, false, false, false, false, false });

        DirectionalNode node_all_altTrueFalse = new DirectionalNode(new bool[] { true, false, true, false, true, false });
        DirectionalNode node_all_altFalseTrue = new DirectionalNode(new bool[] { false, true, false, true, false, true });

        //Test |
        Assert.IsTrue(node_all_true.Equals(node_all_true | node_all_true));
        Assert.IsTrue(node_all_true.Equals(node_all_true | node_all_false));
        Assert.IsTrue(node_all_true.Equals(node_all_false | node_all_true));
        Assert.IsTrue(node_all_false.Equals(node_all_false | node_all_false));
        Assert.IsTrue(node_all_true.Equals(node_all_altTrueFalse | node_all_altFalseTrue));
    }


    [Test]
    public void Test_XOr()
    {
        DirectionalNode node_all_true = new DirectionalNode(new bool[] { true, true, true, true, true, true });

        DirectionalNode node_all_false = new DirectionalNode(new bool[] { false, false, false, false, false, false });

        DirectionalNode node_all_altTrueFalse = new DirectionalNode(new bool[] { true, false, true, false, true, false });
        DirectionalNode node_all_altFalseTrue = new DirectionalNode(new bool[] { false, true, false, true, false, true });

        //Test ^
        Assert.IsTrue(node_all_true.Equals(node_all_true ^ node_all_false));
        Assert.IsTrue(node_all_true.Equals(node_all_false ^ node_all_true));
        Assert.IsTrue(node_all_false.Equals(node_all_true ^ node_all_true));
        Assert.IsTrue(node_all_false.Equals(node_all_false ^ node_all_false));
        Assert.IsTrue(node_all_true.Equals(node_all_altTrueFalse ^ node_all_altFalseTrue));
    }


    [Test]
    public void Test_And()
    {
        DirectionalNode node_all_true = new DirectionalNode(new bool[] { true, true, true, true, true, true });

        DirectionalNode node_all_false = new DirectionalNode(new bool[] { false, false, false, false, false, false });

        DirectionalNode node_all_altTrueFalse = new DirectionalNode(new bool[] { true, false, true, false, true, false });
        DirectionalNode node_all_altFalseTrue = new DirectionalNode(new bool[] { false, true, false, true, false, true });

        //Test &
        Assert.IsTrue(node_all_true.Equals(node_all_true & node_all_true));
        Assert.IsFalse(node_all_true.Equals(node_all_true & node_all_false));
        Assert.IsTrue(node_all_false.Equals(node_all_altTrueFalse & node_all_altFalseTrue));
    }


    [Test]
    public void Test_Equality()
    {
        DirectionalNode node_all_true = new DirectionalNode(new bool[] { true, true, true, true, true, true });
        DirectionalNode node_all_true_same = node_all_true;
        DirectionalNode node_all_true_alt = new DirectionalNode(new bool[] { true, true, true, true, true, true });
        DirectionalNode node_all_false = new DirectionalNode(new bool[] { false, false, false, false, false, false });

        //Test ==
        Assert.IsTrue(node_all_true == node_all_true_same);
        Assert.IsFalse(node_all_true == node_all_true_alt);
        Assert.IsFalse(node_all_true == node_all_false);

        //Test !=
        Assert.IsFalse(node_all_true != node_all_true_same);
        Assert.IsTrue(node_all_true != node_all_true_alt);
        Assert.IsTrue(node_all_true != node_all_false);

        //Test Equals
        Assert.IsTrue(node_all_true.Equals(node_all_true_same));
        Assert.IsTrue(node_all_true.Equals(node_all_true_alt));
        Assert.IsFalse(node_all_true.Equals(node_all_false));
    }
}



