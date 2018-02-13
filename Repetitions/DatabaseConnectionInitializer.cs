using System.Data.Entity;
using System.Linq;

namespace Repetitions
{
    public class DatabaseConnectionInitializer
    {
        public void Start()
        {
            DbConfiguration.SetConfiguration(new DefaultDbConfig());
            using (var context = new SamplesContext())
            {
                var xx = context.Players.ToArray();

                context.SaveChanges();
            }
        }
    }
}