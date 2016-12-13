using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Watson.Project.Models
{

    public class DocSentiment
    {
        public string type { get; set; }
        public string score { get; set; }
        public string mixed { get; set; }
    }

    public class Concept
    {
        public string text { get; set; }
        public string relevance { get; set; }
        public string dbpedia { get; set; }
        public string freebase { get; set; }
        public string yago { get; set; }
        public string opencyc { get; set; }
    }

    public class Disambiguated
    {
        public List<object> subType { get; set; }
        public string name { get; set; }
        public string website { get; set; }
        public string dbpedia { get; set; }
        public string freebase { get; set; }
        public string yago { get; set; }
        public string ciaFactbook { get; set; }
        public string opencyc { get; set; }
    }

    public class Entity
    {
        public string type { get; set; }
        public string relevance { get; set; }
        public string count { get; set; }
        public string text { get; set; }
        public Disambiguated disambiguated { get; set; }
    }

    public class Keyword
    {
        public string text { get; set; }
    }

    public class Subject
    {
        public string text { get; set; }
        public List<Keyword> keywords { get; set; }
    }

    public class Verb
    {
        public string text { get; set; }
        public string tense { get; set; }
    }

    public class Action
    {
        public string text { get; set; }
        public string lemmatized { get; set; }
        public Verb verb { get; set; }
    }

    public class Disambiguated2
    {
        public List<string> subType { get; set; }
        public string name { get; set; }
        public string dbpedia { get; set; }
        public string ciaFactbook { get; set; }
        public string freebase { get; set; }
        public string opencyc { get; set; }
        public string yago { get; set; }
        public string website { get; set; }
    }

    public class Entity2
    {
        public string type { get; set; }
        public string text { get; set; }
        public Disambiguated2 disambiguated { get; set; }
    }

    public class Keyword2
    {
        public string text { get; set; }
    }

    public class Object
    {
        public string text { get; set; }
        public List<Entity2> entities { get; set; }
        public List<Keyword2> keywords { get; set; }
    }

    public class Location
    {
        public string text { get; set; }
    }

    public class Relation
    {
        public string sentence { get; set; }
        public Subject subject { get; set; }
        public Action action { get; set; }
        public Object @object { get; set; }
        public Location location { get; set; }
    }

    public class Taxonomy
    {
        public string confident { get; set; }
        public string label { get; set; }
        public string score { get; set; }
    }

    public class Keyword3
    {
        public string relevance { get; set; }
        public string text { get; set; }
    }

    public class Entity3
    {
        public string id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Argument
    {
        public List<Entity3> entities { get; set; }
        public string part { get; set; }
        public string text { get; set; }
    }

    public class TypedRelation
    {
        public List<Argument> arguments { get; set; }
        public string score { get; set; }
        public string sentence { get; set; }
        public string type { get; set; }
    }

    public class Date
    {
        public string date { get; set; }
        public string text { get; set; }
    }

    public class DocEmotions
    {
        public string anger { get; set; }
        public string disgust { get; set; }
        public string fear { get; set; }
        public string joy { get; set; }
        public string sadness { get; set; }
    }

    public class AlchemyResponse
    {
        public string status { get; set; }
        public string warningMessage { get; set; }
        public string usage { get; set; }
        public string totalTransactions { get; set; }
        public string language { get; set; }
        public DocSentiment docSentiment { get; set; }
        public List<Concept> concepts { get; set; }
        public List<Entity> entities { get; set; }
        public List<Relation> relations { get; set; }
        public List<Taxonomy> taxonomy { get; set; }
        public List<Keyword3> keywords { get; set; }
        public string model { get; set; }
        public List<TypedRelation> typedRelations { get; set; }
        public List<Date> dates { get; set; }
        public DocEmotions docEmotions { get; set; }
    }

}
