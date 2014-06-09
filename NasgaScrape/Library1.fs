namespace Scraper
open System
open System.Net
open System.Text
open HtmlAgilityPack.FSharp
open FSharp.Data

module public Scrape = 
    //for a given year and class, get the resulting page as a string    
//    let yearAndClassResults (url: string, formValues: Collections.Specialized.NameValueCollection)  =
//        let webclient = new WebClient()
//        Encoding.ASCII.GetString(webclient.UploadValues(url,formValues))

//    let scrapeYearAndClass (url: string, formValues: Collections.Specialized.NameValueCollection)  =
//        yearAndClassResults (url,formValues)
//        |> resultsBody

    let yearAndClassResultsAsync url formValues = Http.AsyncRequestString(url, body = FormValues formValues)

    let getTitle node = 
        let title = 
            node
            |> descendants "table"
            |> Seq.filter (hasAttr "width" "100%")
            |> Seq.head
            |> descendants "b"
            |> Seq.head
            |> innerText
        title

    let getAthleteCount node =
        let athleteCount =
            node
            |> descendants "table"
            |> Seq.filter (hasAttr "cellpadding" "1")
            |> Seq.head
            |> descendants "tr"
            |> Seq.filter(doesntHaveAttr "bgcolor" "#99ccff")
            |> Seq.length
        athleteCount.ToString()

    //get all the row's data as a string array from the downloaded page's HTML into string
    let resultsBody resultsPage =
        let htmlPage = createDoc resultsPage
        let title = getTitle htmlPage
        let rankCount = getAthleteCount htmlPage
        htmlPage
        |> descendants "table"
        |> Seq.filter (hasAttr "cellpadding" "1")
        |> Seq.head
        |> descendants "tr"
        |> Seq.filter(doesntHaveAttr "bgcolor" "#99ccff")
        //|> Seq.map (descendants "td" >> Seq.map innerText >> Array.ofSeq) //same as lines below
        |> Seq.map (fun line ->
            line
            |> descendants "td"
            |> Seq.map innerText
            |> Array.ofSeq)
        |> Seq.map (Array.append [|title; rankCount|])

    let asyncScrape url allParameters =
        allParameters
        |> Seq.map(fun v -> yearAndClassResultsAsync url v)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> Array.map resultsBody
        |> Seq.concat