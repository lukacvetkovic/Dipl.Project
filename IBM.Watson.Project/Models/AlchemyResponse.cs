using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Watson.Project.Models
{

    public class Concept
    {
        public string text { get; set; }
        public string relevance { get; set; }
        public string website { get; set; }
        public string dbpedia { get; set; }
        public string freebase { get; set; }
        public string opencyc { get; set; }
        public string yago { get; set; }
    }

    public class Sentiment
    {
        public string type { get; set; }
        public string score { get; set; }
        public string mixed { get; set; }
    }

    public class Entity
    {
        public string type { get; set; }
        public string relevance { get; set; }
        public Sentiment sentiment { get; set; }
        public string count { get; set; }
        public string text { get; set; }
    }


    public class Keyword
    {
        public string relevance { get; set; }
        public Sentiment sentiment { get; set; }
        public string text { get; set; }
    }

    public class AlchemyResponse
    {
        public string status { get; set; }
        public string usage { get; set; }
        public string url { get; set; }
        public string totalTransactions { get; set; }
        public string language { get; set; }
        public List<Concept> concepts { get; set; }
        public List<Entity> entities { get; set; }
        public List<Keyword> keywords { get; set; }
    }

}
