using System;
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
            using (var sandbox = new Sandbox())
            {
                return sandbox.Execute(codeToCompile);
            }
        }
    }
}