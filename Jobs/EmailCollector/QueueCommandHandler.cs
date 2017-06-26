using Jobs.Core;

namespace EmailCollector
{
    public class QueueCommandHandler
    {
        public QueueCommandHandler(JobCommand jobCommand)
        {
            Command = jobCommand;
        }

        public JobCommand Command { get; private set; }
    }
}
