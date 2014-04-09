using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace CSharpWarrior.Controllers
{
    public class LevelController : ApiController
    {
        [HttpPost]
        public object Post([FromBody]JToken code)
        {
            var codeToCompile = (string)code["code"];
            using (var compiler = new PlayerCompiler())
            {
                var compilerResults = compiler.Compile(codeToCompile);
                var level = new LevelFactory().MakeLevel1();
                using (var sandbox = new Sandbox())
                {
                    return new
                    {
                        Log = (string)sandbox.ExecuteAssembly<LevelCrawlerAgent, Level>(compilerResults.PathToAssembly, level)
                    };
                }
            }
        }
    }
}