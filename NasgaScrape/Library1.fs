namespace Scraper
open System
open System.Net
open System.Text
open HtmlAgilityPack.FSharp
open FSharp.Data

module public Scrape = 
    //for a given year and class, get the resulting page as a string    
    let yearAndClassResults (url: string, formValues: Collections.Specialized.NameValueCollection)  =
        let webclient = new WebClient()
        Encoding.ASCII.GetString(webclient.UploadValues(url,formValues))

    let yearAndClassResultsAsync (url: string, formValues: Collections.Specialized.NameValueCollection)  =
            let webclient = new WebClient()
            Encoding.ASCII.GetString(webclient.UploadValues(url,formValues))
            |> Http.AsyncRequestString


           
    //get all the row's data as a string array from the downloaded page's HTML into string
    let resultsBody resultsPage =
        resultsPage
        |> createDoc
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

    let scrapeYearAndClass (url: string, formValues: Collections.Specialized.NameValueCollection)  =
        yearAndClassResults (url,formValues)
        |> resultsBody

    let scrapeClassesForYear (url: string, multiFormValues: ResizeArray<Collections.Specialized.NameValueCollection>)  =
        multiFormValues
        |> Seq.map(fun fv -> 
            yearAndClassResultsAsync (url,fv))
            |> Async.Parallel
            |> Async.RunSynchronously
      