namespace Klockmann.BotHelper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CommandList : IList<Command>
    {
        #region member vars

        private readonly IList<Command> _data;

        #endregion

        #region constructors and destructors

        public CommandList()
        {
            _data = new List<Command>();
        }

        public CommandList(IEnumerable<Command> commands)
        {
            _data = new List<Command>(commands);
        }

        #endregion

        #region explicit interfaces

        public void Add(Command item)
        {
            if (_data.Any(c => c.Name == item.Name))
            {
                throw new ArgumentException($"Command with name \"{item.Name}\" already in the list!", nameof(item));
            }
            _data.Add(item);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(Command item)
        {
            return _data.Contains(item);
        }

        public void CopyTo(Command[] array, int arrayIndex)
        {
            _data.CopyTo(array, arrayIndex);
        }

        public int Count => _data.Count;

        public bool IsReadOnly => _data.IsReadOnly;

        public bool Remove(Command item)
        {
            return _data.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_data).GetEnumerator();
        }

        public IEnumerator<Command> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public int IndexOf(Command item)
        {
            return _data.IndexOf(item);
        }

        public void Insert(int index, Command item)
        {
            _data.Insert(index, item);
        }

        public Command this[int index]
        {
            get
            {
                return _data[index];
            }
            set
            {
                _data[index] = value;
            }
        }

        public void RemoveAt(int index)
        {
            _data.RemoveAt(index);
        }

        #endregion
    }
}