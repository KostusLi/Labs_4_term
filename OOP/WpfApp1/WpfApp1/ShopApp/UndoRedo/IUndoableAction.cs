namespace WpfApp1.ShopApp.UndoRedo
{
    public interface IUndoableAction
    {
        void Execute();
        void Undo();
    }
}