using System;
using System.Collections.Generic;
using System.Text;

namespace Tool.TSystem.Res
{
    public enum EResType
    {
        SCRIPT = 0,
        SCRIPTCSV,
        TEXPACK,
    };

	public interface IResourceUnit
	{
		void Release();

        EResType type { get; }
        string Path { get; }

		bool Load( string strName );
		bool Save();
		bool SaveAs(string strName);
	}

    public abstract class AbstractResource : IResourceUnit
    {
        protected EResType m_type;
        protected string m_path;
        protected string m_rootPath = "texture\\";

        public AbstractResource()
        {
        }

        ~AbstractResource()
        {
            Release();
        }

        public EResType type
        {
            get { return m_type; }
        }

        public string Path
        {
            get { return m_path; }
            set { m_path = value; }
        }

        public virtual void Release() {}
        public abstract bool Load(string path);
        public abstract bool Save();

        public virtual bool SaveAs(string strName)
        {
            m_path = strName;
            return Save();
        }
    }
}
