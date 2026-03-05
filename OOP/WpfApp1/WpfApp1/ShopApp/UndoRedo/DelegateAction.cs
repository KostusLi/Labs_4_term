using System;

namespace WpfApp1.ShopApp.UndoRedo
{
    public class DelegateAction : IUndoableAction
    {
        private readonly Action _execute;
        private readonly Action _undo;

        public DelegateAction(Action execute, Action undo)
        {
            _execute = execute;
            _undo = undo;
        }

        public void Execute() => _execute();
        public void Undo() => _undo();
    }
}