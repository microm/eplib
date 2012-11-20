using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Tool.TSystem.Assist;

namespace SpriteTool.Data.Control
{
    using LayeredControl = SortedDictionary<int, List<ControlBase>>;
    using NamedControl = Dictionary<string, ControlBase>;
    using System.Xml;
    using Tool.TSystem.IO;
   

    public class LayeredCollection : IEnumerable<ControlBase>
    {
        private LayeredControl m_layeredControls = new LayeredControl(new ReverseComparer<int>());
        private NamedControl m_namedControls = new NamedControl();

        public int Count
        {
            get { return m_namedControls.Count; }
        }

        public int LayerCount
        {
            get { return m_layeredControls.Count; }
        }

        public void Add(ControlBase control)
        {
            if (control == null) return;
            m_namedControls.Add(control.Name, control);

            if (m_layeredControls.ContainsKey(control.Anchor.ZOrder))
            {
                m_layeredControls[control.Anchor.ZOrder].Add(control);
            }
            else
            {
                List<ControlBase> childControl = new List<ControlBase>();
                childControl.Add(control);
                m_layeredControls.Add(control.Anchor.ZOrder, childControl);
            }
        }

        public ControlBase Find(string name)
        {
            ControlBase control;
            if (m_namedControls.TryGetValue(name, out control)) return control;
            return null;
        }

        #region IEnumerable<ControlBase> members

        public IEnumerator<ControlBase> GetEnumerator()
        {
            foreach (List<ControlBase> controls in m_layeredControls.Values)
            {
                foreach (ControlBase control in controls)
                {
                    yield return control;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public bool Remove(ControlBase control)
        {
            if (control == null) return false;
            ControlBase findControl = Find(control.Name);
            if (control != findControl) return false;

            m_namedControls.Remove(control.Name);
            m_layeredControls[control.Anchor.ZOrder].Remove(control);

            if (m_layeredControls[control.Anchor.ZOrder].Count == 0)
            {
                m_layeredControls.Remove(control.Anchor.ZOrder);
            }
            return true;
        }

        public void Clear()
        {
            m_namedControls.Clear();
            m_layeredControls.Clear();
        }

        
    }
}
