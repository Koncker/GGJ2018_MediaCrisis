using System;
using System.Collections.Generic;

namespace Assets.Scripts.UI.UIBinder
{
    public class PropertyBinder<T>
    {
        private List<Action<T>> _bindList = new List<Action<T>>();
        private T _value = default(T);

        public T Value
        {
            get { return _value; }
            set
            {
                if (!value.Equals(_value))
                {
                    Set(value);
                }
            }
        }

        public void Bind(Action<T> bind, bool callOnBind = true)
        {
            if (!_bindList.Contains(bind))
            {
                _bindList.Add(bind);
                bind(_value);
            }
        }


        public void Unbind(Action<T> bind)
        {
            if (_bindList.Contains(bind))
                _bindList.Remove(bind);
        }

        private void Set(T value)
        {
            _value = value;
            foreach(Action<T> binding in _bindList)
            {
                binding(value);
            }
        }
    }
}
