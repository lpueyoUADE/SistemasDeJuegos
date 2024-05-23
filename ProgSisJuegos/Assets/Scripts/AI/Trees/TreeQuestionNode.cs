using System;

public class TreeQuestionNode : ITreeNode
{
    Func<bool> _question;
    ITreeNode _fNode;
    ITreeNode _tNode;

    private Func<bool> questionIdleTimeout;

    public TreeQuestionNode(Func<bool> question, ITreeNode fNode, ITreeNode tNode)
    {
        _question = question;
        _fNode = fNode;
        _tNode = tNode;
    }

    public void Execute()
    {
        if (_question())
            _tNode.Execute();
        else
            _fNode.Execute();
    }
}
