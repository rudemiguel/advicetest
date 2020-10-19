using System.Collections.Generic;
using advicetest.Advices;

namespace advicetest.Infrastructure
{
    /// <summary>
    /// Сервис бесполезной работы для <see cref="LoadTestingServiceAdvice"/>
    /// </summary>
    public class LoadTestingMockJob
    {
        private readonly List<int> _jobs;

        public LoadTestingMockJob()
        {
            _jobs = new List<int>();
        }

        public void Add()
        {
            // _jobs.Add(1);
        }
    }
}