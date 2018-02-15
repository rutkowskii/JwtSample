using Nancy;
using Nancy.Security;

namespace Repetitions
{
    public class DummyModule : NancyModule
    {
        public DummyModule()
        {
            this.RequiresAuthentication();
            Get["/Dummy"] = _ => GetAllCategories();
        }

        private object GetAllCategories()
        {
            return new[] {33, 3, 3, 3, 3, 3, 3, 3};
        }
    }
}