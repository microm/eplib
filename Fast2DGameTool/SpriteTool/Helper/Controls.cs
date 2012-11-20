using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpriteTool.Data;
using System.Collections;
using Tool.TSystem.Assist;
using System.Diagnostics;

namespace SpriteTool.Helper
{
    public class Controls : IEnumerable<ControlBase>
    {
        private List<ControlBase> m_controls = new List<ControlBase>();

        public ControlBase this[int index]
        {
            get
            {
                return m_controls[index];
            }
        }

        public int Count
        {
            get
            {
                return m_controls.Count;
            }
        }

        public void Clear()
        {
            m_controls.Clear();
        }

        public void Add(ControlBase control)
        {
            Trace.Assert(control != null);
            m_controls.Add(control);
        }

        public List<ControlBase> Get()
        {
            return new List<ControlBase>(m_controls);
        }

        public void Set(ControlBase control)
        {
            m_controls.Clear();
            m_controls.Add(control);
        }

        public void Set(List<ControlBase> entities)
        {
            if (entities == null) return;
            m_controls = new List<ControlBase>(entities);
        }

        public int Find(ControlBase control)
        {
            ReferenceFinder finder = new ReferenceFinder(control);
            return m_controls.FindIndex(finder.Find);
        }

        public bool Contains(ControlBase control)
        {
            return (Find(control) > -1);
        }

        public void Remove(ControlBase control)
        {
            int index = Find(control);
            if (index < 0) return;

            m_controls.RemoveAt(index);
        }

        public void Remove(int index)
        {
            m_controls.RemoveAt(index);
        }

        public IEnumerator<ControlBase> GetEnumerator()
        {
            foreach (ControlBase selectedcontrol in m_controls)
            {
                yield return selectedcontrol;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(List<ControlBase> entities)
        {
            foreach (ControlBase control in entities) Add(control);
        }
    }
}
