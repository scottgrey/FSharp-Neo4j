module FSharpNeo4j

open System
open Neo4j.Driver.V1
open System.Linq

[<EntryPoint>]
let main argv =
    let driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic("neo4j", "neo4j"))
    let session = driver.Session()
    
    let followers twitterHandle = 
        let results = session.Run("MATCH (n)<-[follows]-(e) WHERE n.Twitter = {th} return e.Name as name", dict["th",twitterHandle])
        results |> List.ofSeq
        //results
                
    let showFollowersinUI (flls: IRecord list)=
        flls |> List.iter (fun i-> printfn "%A" (i.Item("name")))
        //flls |> Seq.iter (fun i-> printfn "%A" (i.Item("name")))
    
    let pAfollowers = followers "tA"
        
    printfn "tAfollowers"
    pAfollowers |> showFollowersinUI
        
    //printfn "tA followers"        
    //for record in pAfollowers do
    //    printfn "%A" (record.Item("name"))
    
    //printfn "%A" argv
    0 // return an integer exit code
