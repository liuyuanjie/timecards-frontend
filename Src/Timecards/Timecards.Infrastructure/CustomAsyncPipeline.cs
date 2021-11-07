using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Timecards.Infrastructure
{
    public class CustomAsyncPipeline
    {
        public delegate void BeforeExecute();

        public delegate void AsyncExecuteBody();

        public delegate void ExecuteSuccess();

        public delegate void ExecuteFailed();
    }
}
