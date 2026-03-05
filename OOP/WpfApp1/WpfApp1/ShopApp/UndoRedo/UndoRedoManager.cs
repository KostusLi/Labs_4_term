using System.Collections.Generic;

namespace WpfApp1.ShopApp.UndoRedo
{
    public class UndoRedoManager
    {
        private readonly Stack<IUndoableAction> _undoStack = new Stack<IUndoableAction>();

        private readonly Stack<IUndoableAction> _redoStack = new Stack<IUndoableAction>();

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public void ExecuteAction(IUndoableAction action)
        {
            action.Execute();
            _undoStack.Push(action);
            _redoStack.Clear();
        }

        public void Undo()
        {
            if (!CanUndo) return;
            var action = _undoStack.Pop();
            action.Undo();
            _redoStack.Push(action);
        }

        public void Redo()
        {
            if (!CanRedo) return;
            var action = _redoStack.Pop();
            action.Execute();
            _undoStack.Push(action);
        }
    }
}