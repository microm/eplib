using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Tool.TSystem.Res
{
    public class Script : AbstractResource
    {
		List<string> m_lstValue = new List<string>();

		public Script()
		{
            m_type = EResType.SCRIPT;
		}

        public List<string> lstValue
		{
            get { return m_lstValue; }
		}

        public override bool Load(string path)
        {
			try
			{
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
                        string strKey = line.TrimEnd(null);
                        if (strKey != "")
                        {
                            m_lstValue.Add(strKey);
                        }
					}
				}				
			}
            catch 
            {
             	return false;
			}
            return true;
        }

        public override bool Save()
        {			
			try
			{
                string[] strContent = new string[m_lstValue.Count];
                int i = 0;
                foreach( string line in m_lstValue )
                {
                    strContent[i++] = line;
                }
                
				File.WriteAllLines(Path, strContent);
			}
            catch (Exception e)
            {
                MessageBox.Show(String.Format("{0} \r\n{1}", Path, e.Message), "Save Error");
				return false;	
			}
            return true;
        }        
	}
}
