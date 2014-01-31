using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace CSharpWarrior.Controllers
{
    public class LevelController : ApiController
    {
        [HttpPost]
        public string Post([FromBody]JToken code)
        {
            var codeToCompile = (string)code["code"];
            using (var compiler = new PlayerCompiler())
            {
                var compilerResults = compiler.Compile(codeToCompile);
                using (var sandbox = new Sandbox())
                {
                    sandbox.ExecuteAssembly<LevelCrawlerAgent, Level>(compilerResults.PathToAssembly, new LevelFactory().MakeLevel1());
                    return "Level complete!";
                }
            }
        }
    }
}