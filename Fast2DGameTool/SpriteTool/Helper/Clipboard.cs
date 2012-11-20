using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteTool.Data;

namespace SpriteTool.Helper
{
    public class Clipboard
    {
        private readonly Controls m_controls = new Controls();
        private readonly Dictionary<string, int> m_numbers = new Dictionary<string, int>();

        public Controls Controls
        {
            get { return m_controls; }
        }

        public int Count
        {
            get { return m_controls.Count; }
        }

        public void Clear()
        {
            m_controls.Clear();
        }

        public void Set(List<ControlBase> list)
        {
            foreach (ControlBase control in list)
            {
                if (m_numbers.ContainsKey(control.Name)) continue;
                m_numbers.Add(control.Name, 0);
            }
            m_controls.Set(list);
        }

        public void Add(ControlBase control)
        {
            if (m_numbers.ContainsKey(control.Name) == false)
            {
                m_numbers.Add(control.Name, 0);
            }
            m_controls.Add(control);
        }

        public int GetNumber(string name)
        {
            if (m_numbers.ContainsKey(name) == false) return 0;

            int number = m_numbers[name];
            m_numbers[name]++;

            return number;
        }
    }
}
