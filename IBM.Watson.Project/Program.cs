using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IBM.Watson.Project.Helpers;
using IBM.Watson.Project.Helpers.WatsonHelper;

namespace IBM.Watson.Project
{
    class Program
    {
        private static readonly string APIKey = "ea4cb11ced2395985e21914549b176c68111faac";
        static void Main(string[] args)
        {
            IWatsonHelper helper = new WatsonHelperText(APIKey);

            var result = helper.GetResult("Ove je srijede, 30. studenoga, dovršena velika operacija koju su zajednički provodile policije i tužiteljstva Njemačke i SAD-a, FBI, Eurojust, Europol, kao i stručnjaci iz 30 zemalja, a u kojoj je uspješno ugašena velika botnet struktura naziva Avalanche. Ova je mreža botova još od 2009. godine služila za distribuciju zloćudnih programa, phishing napade i regrutiranje ljudi u razne malverzacije, tj. njihovo pretvaranje u financijske mule.\r\n \r\nŠtete prouzročene putem ovog sustava procjenjuju se na više stotina milijuna eura, a samo u Njemačkoj bankovni su sektor izravno koštale oko 6 milijuna eura. Putem Avalanche mreže napadači su tjedno prosječno žrtvama slali preko milijun zaraženih ili phishing e-mailova.\r\n \r\nEuropolova istraga ovog slučaja započela je 2012. godine u Njemačkoj, a ovotjedna je akcija rezultirala uhićenjem pet osoba, te zapljenom 39 servera u istom danu. Dodatno, više od 800.000 domena koje su služile za distribuciju ilegalnih programa i lansiranje napada je ugašeno, zaplijenjeno ili blokirano tijekom ove policijske operacije, javlja Europol.\r\n \r\nAkcija suzbijanja ove malware mreže bila je iznimno zahtjevna jer su svi serveri bili zaštićeni sofisticiranom tehnikom \"double fast flux\" koja vrlo brzo mijenja DNS zapise i promet preusmjerava kroz višestruke proxyje, te tako skriva tragove napadača.");
            Console.WriteLine(JSONHelper.Serialize(result));

            Console.WriteLine();
            Console.WriteLine("-- ------------------------------- --");
            Console.WriteLine();

            IWatsonHelper helpUrl = new WatsonHelperUrl(APIKey);

            var resultUrl= helpUrl.GetResult("http://www.baynews9.com/alerts.html");

            Console.WriteLine(JSONHelper.Serialize(result));

            Console.ReadLine();
        }
    }
}
