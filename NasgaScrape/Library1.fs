namespace NasgaScraper
open System.Net
open System
open System.IO

module public NasgaScrape=
//    let AddNumbers (a, b) = a + b
//    let SubtractNumbers a b = a - b
//    let Subtract10 a = a - 10
    let fetchAthleteRankingsForClassAndYear url =    //take a class and a year, screen scrape for that year    
        let req = WebRequest.Create(Uri(url)) 
        use resp = req.GetResponse() 
        use stream = resp.GetResponseStream() 
        use reader = new IO.StreamReader(stream) 
        let html = reader.ReadToEnd() 
        printfn "finished downloading %s" url 


    let fetchAllAthleteRankings //takes a list of strings (classes), and a string year

//    sites 
//|> List.map fetchUrlAsync  // make a list of async tasks
//|> Async.Parallel          // set up the tasks to run in parallel
//|> Async.RunSynchronously  // start them off