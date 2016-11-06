using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Watson.Project.Models;

namespace IBM.Watson.Project.Helpers.WatsonHelper
{
    public interface IWatsonHelper
    {
        AlchemyResponse GetResult(string input);
    }
}
