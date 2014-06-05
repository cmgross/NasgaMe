namespace Scraper
open System.Net
open System
open System.IO

module public Scrape = 
//    let AddNumbers (a, b) = a + b
//    let SubtractNumbers a b = a - b
//    let Subtract10 a = a - 10
//    let fetchAthleteRankingsForClassAndYear url =    //take a class and a year, screen scrape for that year    
//        let req = WebRequest.Create(Uri(url)) 
//        use resp = req.GetResponse() 
//        use stream = resp.GetResponseStream() 
//        use reader = new IO.StreamReader(stream) 
//        let html = reader.ReadToEnd() 
//        printfn "finished downloading %s" url 

//webclient example:http://en.wikibooks.org/wiki/F_Sharp_Programming/Async_Workflows
    let syncScrape (url: string, formValues: Collections.Specialized.NameValueCollection)  =
        let webclient = new System.Net.WebClient()
        webclient.UploadValues(url,formValues)
        //todo use HtmlAgilityPack here to load the HTML document and return the htmlnode
        //let document = new HtmlDocument()
  
    
//    sites 
//|> List.map fetchUrlAsync  // make a list of async tasks
//|> Async.Parallel          // set up the tasks to run in parallel
//|> Async.RunSynchronously  // start them off