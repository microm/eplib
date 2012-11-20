using System;
using System.Collections.Generic;
using Tool.TSystem.Pattern;
using Tool.TSystem.Primitive;
using SpriteTool.Data;
using SpriteTool.Control;

namespace SpriteTool.Command.Create
{
    public class CreatePanel : BaseCreateControl
    {
        public CreatePanel(StagePictureBox editor)
            :base(editor)
        {
        }

        private CreatePanel(StagePictureBox editor, TPoint startPosition, TPoint endPosition, ControlBase control)
            :base(editor,startPosition,endPosition,control)
        {
        }

        public override string Name
        {
            get { return "CreatePanel"; }
        }

        public override ICommand Clone()
        {
            return new CreatePanel(m_editor, m_startPosition, m_endPosition, m_createControl);
        }

        protected override ControlBase CreateControl()
        {
            if (m_editor.LayerInfo == null) return null;
            return m_editor.LayerInfo.CreateControl(ControlType.Panel, m_startPosition, m_endPosition);
        }
    }
}