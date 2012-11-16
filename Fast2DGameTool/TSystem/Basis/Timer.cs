
using System.Diagnostics;

namespace Tool.TSystem.Basis
{
    public class Timer
    {
        private bool m_isUsingQPF;
        private bool m_isTimerStopped;
        private long m_ticksPerSecond;
        private long m_stopTime;
        private long m_lastElapsedTime;
        private long m_baseTime;

        public Timer()
        {
            m_isTimerStopped = true;
            m_ticksPerSecond = 0;
            m_stopTime = 0;
            m_lastElapsedTime = 0;
            m_baseTime = 0;

            m_isUsingQPF = API.QueryPerformanceFrequency(ref m_ticksPerSecond);

            try
            {
                API.timeBeginPeriod(1);
            }
            catch
            {
                Debugger.Log(9, string.Empty, "Could not set time period.\r\n");
            }
        }

        public bool IsStopped
        {
            get { return m_isTimerStopped; }
        }

        public void Reset()
        {
            if (!m_isUsingQPF) return;
            long time = GetQPFTime();

            m_baseTime = time;
            m_lastElapsedTime = time;
            m_stopTime = 0;
            m_isTimerStopped = false;
        }

        public void Stop()
        {
            if (!m_isUsingQPF) return;
            if (m_isTimerStopped) return;

            long time = GetQPFTime();

            m_stopTime = time;
            m_lastElapsedTime = time;
            m_isTimerStopped = true;
        }

        public void Start()
        {
            if (!m_isUsingQPF) return;
            long time = GetQPFTime();

            if (m_isTimerStopped)
            {
                m_baseTime += (time - m_stopTime);
            }
            m_stopTime = 0;
            m_lastElapsedTime = time;
            m_isTimerStopped = false;
        }

        private long GetQPFTime()
        {
            long time = 0;
            if (m_stopTime != 0)
            {
                time = m_stopTime;
            }
            else
            {
                API.QueryPerformanceCounter(ref time);
            }
            return time;
        }

        public void Advance()
        {
            if (!m_isUsingQPF) return;
            m_stopTime += m_ticksPerSecond / 10;
        }

        public double GetAbsoluteTime()
        {
            if (!m_isUsingQPF) return -1.0;
            long time = GetQPFTime();

            double absolueTime = time / (double)m_ticksPerSecond;
            return absolueTime;
        }

        public double GetTime()
        {
            if (!m_isUsingQPF) return -1.0;
            long time = GetQPFTime();

            return (time - m_baseTime) / (double)m_ticksPerSecond;
        }

        public double GetElapsedTime()
        {
            if (!m_isUsingQPF) return -1.0;
            long time = GetQPFTime();

            double elapsedTime = (time - m_lastElapsedTime) / (double)m_ticksPerSecond;
            m_lastElapsedTime = time;
            return elapsedTime;
        }
    }
}
