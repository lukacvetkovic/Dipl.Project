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

            var result = helper.GetResult("NEW YORK (GenomeWeb) – The combination of natural selection and population size may have been enough to eliminate Neanderthal genes from the human genome, according to a new study.\r\n\r\nHumans and Neanderthals interbred some 50,000 years ago, but the genomes of modern non-African humans only contain a sliver of Neanderthal-origin DNA, some 1 percent to 2 percent. In PLOS Genetics today, researchers from the University of California, Davis published the model they used to estimate the strength of natural selection on Neanderthal genes that have introgressed into the human genome.\r\n\r\nOn average, the Davis team found that Neanderthal genes appear to be weakly selected against, and these genes likely were essentially neutral within Neanderthals. But once these alleles appeared in the human population, selection against them increased.\r\n\r\n\"Our results are comparable with a scenario where the Neanderthal genome accumulated many weakly deleterious variants, because selection was not effective in the small Neanderthal populations,\" co-first author Ivan Juric from UC Davis said in a statement. \"Those variants entered the human population after hybridization. Once in the larger human population, those deleterious variants were slowly purged by natural selection.\"\r\n\r\nJuric and his colleagues developed a model that drew upon estimates of Neanderthal allele frequency within modern human genomes to estimate the strength of natural selection on those Neanderthal genes. In particular, they gauged the how often a Neanderthal-derived allele at a neutral locus was linked to a single deleterious allele. The model also took recombination rate, initial introgression proportion, and split time into account. They then fit their predictions to published estimates of Neanderthal ancestry within modern humans.\r\n\r\nFor autosomal chromosomes, the researchers found that their estimates of selection on deleterious Neanderthal alleles were low, though non-zero, in people of European or Asian ancestry. On average, the researchers estimated that less than one in 10,000 exonic basepairs harbored a deleterious Neanderthal allele. They further estimated a higher initial frequency of Neanderthal alleles among East Asians, which they noted is consistent with previous reports.\r\n\r\nIn the model, the estimated coefficients of selection against deleterious Neanderthal alleles are low, the researchers reported. This suggested to them that they could be uncovering differences in the efficacy of selection between modern humans and Neanderthals.\r\n\r\nGenetic diversity among Neanderthals suggests that they had a low long-term effective population size, as compared to modern humans, the researchers noted. This, they added, indicates that weakly deleterious alleles could have been effectively neutral among Neanderthals. However, after introgressing into modern humans — with their larger effective population size — these alleles could then slowly selected against.\r\n\r\nTo test this idea, Juric and his colleagues simulated the population split that occurred between Neanderthals and modern humans. Using this, they tracked the frequency of deleterious exonic alleles among both Neanderthals and humans at the time of admixture, for a range of estimated population sizes. Because of their smaller effective population size, Neanderthals had an excess of fixed deleterious alleles as compared to modern humans when their population size was small.\r\n\r\n\"The key finding of our study therefore is that the current levels of Neanderthal ancestry in modern humans are in part due to long‐term differences in human and Neanderthal population sizes. The human population size has historically been much larger, and this is important since selection is more efficient at removing deleterious variants in large population,\" Juric added.\r\n\r\nThe researchers also noted a decreased amount of Neanderthal ancestry on the X chromosome as compared to autosomes. Using their model, they found that this difference could be due to a sex bias in Neanderthal-human matings.");

            Console.WriteLine(JSONHelper.Serialize(result));

            Console.ReadLine();
        }
    }
}
