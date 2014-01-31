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
                    var agent = sandbox.ExecuteAssembly<LevelCrawlerAgent, Level>(compilerResults.PathToAssembly, new Level(new []{new Location(), new Location(), }));
                    return "Level complete!";
                }
            }
        }
    }
}