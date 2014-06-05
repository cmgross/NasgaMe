using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace NasgaMe.Models
{
    public class AthleteRanking
    {
        public string Id
        {
            get { return Year + "/" + Class + "/" + Rank; }
        }
        public string Year { get; set; }
        public string Class { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public string BraemarThrow { get; set; } //save in the format it needs to be in
        public int BraemarPoints { get; set; }
        public string OpenThrow { get; set; }
        public int OpenPoints { get; set; }
        public string HeavyWeightThrow { get; set; }
        public int HeavyWeightPoints { get; set; }
        public string LightWeightThrow { get; set; }
        public int LightWeightPoints { get; set; }
        public string HeavyHammerThrow { get; set; }
        public int HeavyHammerPoints { get; set; }
        public string LightHammerThrow { get; set; }
        public int LightHammerPoints { get; set; }
        public int CaberPoints { get; set; }
        public string SheafThrow { get; set; }
        public int SheafPoints { get; set; }
        public string WfhThrow { get; set; }
        public int WfhPoints { get; set; }

        public static List<string[]> GetResults(string athleteClass, string year) //example C# synchronous 
        {
            using (var client = new WebClient())
            {
                var formValues = new NameValueCollection
                        {
                            {"class", athleteClass},
                            {"rankyear", year},
                            {"x", "26"},
                            {"y", "10"}
                        };
                byte[] byteArray = client.UploadValues("http://nasgaweb.com/dbase/rank_overall.asp", formValues);
                var document = new HtmlDocument();
                document.LoadHtml(Encoding.ASCII.GetString(byteArray));
                var body = document.DocumentNode.Descendants().FirstOrDefault(n => n.Name == "body");
                var table = body.Descendants("table").FirstOrDefault(t => t.Attributes.Contains("cellpadding") && t.Attributes["cellpadding"].Value == "1");
                var rows =
                    table.Descendants("tr")
                        .Where(r => r.Attributes.Contains("bgcolor") && r.Attributes["bgcolor"].Value != "#99ccff");
                List<string[]> athleteDatas =
                    rows.Select(t => t.Descendants("td").Select(d => d.InnerText).ToArray()).ToList();

                return athleteDatas;
            }
        }
    }
}