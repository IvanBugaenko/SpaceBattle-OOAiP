namespace SpaceBattle.Lib;

public interface IReciver
{
    ICommand Recieve();
    bool isEmpty();
}

