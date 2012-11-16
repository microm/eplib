using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Tool.TSystem.Res
{
    public class ScriptCsv : AbstractResource
    {
        private List< string[] > m_lstValue = new List< string[] >();
		private string m_strTitle;
        private int m_nRows = 0;
        private int m_nCols = 0;

        public ScriptCsv()
		{
			m_type = EResType.SCRIPTCSV;
		}

        public List<string[] > lstValue
		{
            get { return m_lstValue; }
		}

		public string strTitle
		{
			get { return m_strTitle; }
			set { m_strTitle = value; }
		}

        public override bool Load(string path)
        {  
			try
			{
				using (StreamReader sr = new StreamReader(path , Encoding.Default ))
				{
					string line;
					char[] Separators = new char[] { ',' };

                    string[] arLine;
					while ((line = sr.ReadLine()) != null)
					{
                        arLine = line.Split(Separators, StringSplitOptions.None );
                        if (m_nRows == 0)
                        {
                            m_nRows++;
                            m_nCols = arLine.Length;
                            continue;
                        }
                        m_nRows ++;
                        if (arLine.Length < m_nCols)
                        {
                            MessageBox.Show( string.Format( "Err Line {0} : cols {1} - {2} \n", m_nRows, m_nCols, line ));
                            continue;
                        }
                        m_lstValue.Add( arLine );
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
				string[] strContent = new string[ m_lstValue.Count + 1];
				strContent[0] = m_strTitle;
				for ( int line=0; line < m_lstValue.Count ; line++ )
				{
					for (int i = 0; i < m_lstValue[line].Length; i++)
					{
						strContent[line+1] += m_lstValue[line][i] + ",";
					}
				}
				File.WriteAllLines( Path, strContent);
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
