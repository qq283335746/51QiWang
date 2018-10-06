using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotterySln.TaskProcessor;

namespace TaskProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            RunLotteryProcessor.TaskStart();

            OrderProcessor.TaskStart();
        }
    }
}
