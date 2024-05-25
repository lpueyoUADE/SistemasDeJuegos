using System.Collections.Generic;

public class TreeRandomNode : ITreeNode
{
    Dictionary<ITreeNode, float> _dic;

    public TreeRandomNode(Dictionary<ITreeNode, float> dic)
    {
        _dic = dic;
    }

    public void Execute()
    {
        var randomNode = CustomRandom.Roulette(_dic);
        randomNode.Execute();
    }
}
