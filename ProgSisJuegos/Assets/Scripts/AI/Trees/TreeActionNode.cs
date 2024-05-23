using System;

public class TreeActionNode : ITreeNode
{
    Action _action;

    public TreeActionNode(Action action)
    {
        _action = action;
    }

    public void Execute()
    {
        _action();
    }
}
