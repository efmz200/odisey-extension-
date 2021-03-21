var auth='BQASRcMUZUYakvurHQ1Df55aI0NezfWEvUgCY9hz24y8xCsXwjYocY06AqAKdXILMaBupKRJry0oxeiHv_Oq9QJmjiugL6B3eXePX6JNz4zkrn_YhJK6gO6V' 
chrome.omnibox.onInputEntered.addListener(function(text){
    
    if(text[0]=="#"){
      chrome.identity.getProfileUserInfo(function(userInfo) {
        fetch("https://localhost:5001/api/songs/id/"+text.substring(1),{method:"DELETE",headers:{"UserName":userInfo.id,"email":userInfo.email}}).then(result=>{
        console.log(result)
      })
      })
      
    }
    if (text.includes('spot_fy') && text.substring(0,3)!=="***"){
      
      if (text.includes("s_ng")){
        var texto=text.substring(13)
        for(i=0;i<texto.length;i++){
          if(texto[i]==" "){
            texto[i]="%20"
          }
        }
        var envio;
        fetch('https://api.spotify.com/v1/search?q='+texto+'&type=track&market=US&limit=1',{
            headers:{
              Authorization:'Bearer ' + auth
            }}).then(r => r.json()).then(result => {
              console.log(typeof result)
              cancion=result.tracks.items[0]
              envio=
              {
                name :result.tracks.items[0].name,
                artist:result.tracks.items[0].artists[0].name,
                album: result.tracks.items[0].album.name,
                lyrics:"example",
                imageURL: result.tracks.items[0].album.images[1].url,
                spotifyID:result.tracks.items[0].id
              }
              console.log(JSON.stringify(envio).length) 
              chrome.identity.getProfileUserInfo(function(userInfo) {
                console.log("hola")
                console.log(userInfo.email)
                fetch("https://localhost:5001/api/songs/",{
                method:'POST',
                body:JSON.stringify(envio),
                
                headers:{
                  "Content-Type":"application/json",
                  "Content-Length":JSON.stringify(envio).length,
                  "Host":"https://localhost:5001",
                  "Accept":"*/*",
                  "UserName":userInfo.id,
                  "email":userInfo.email
                }
              })
              })
                      
              })           

      }
    }
    
    var tex=text.split("*")

    if(!text.includes('spot_fy') && text.substring(0,3)!=="***")
     chrome.storage.local.set({"spotifyID":text[4]})
      chrome.runtime.sendMessage(
        {
          name:tex[0],
          artist:tex[1],
          album: tex[2],
          imageURL: tex[3],
          spotifyID:tex[4]})

    })

chrome.omnibox.onInputChanged.addListener(
  
  function(text, suggest)
  {   
      if(text.substring(0,6)==="*users"){
        
        fetch("https://localhost:5001/api/users/").then(r=>r.json()).then(result=>{
          var suggestions = [];  
          
          for(i=0;i<result.length;i++){                         
            suggestions.push({ 
              content: "***"+result[i].name, 
              description: "Id: "+result[i].name
            });
            
          }
          suggest(suggestions);  
        })        
      }
      if(text.substring(0,7)==="*userID"){
        chrome.identity.getProfileUserInfo(function(userInfo) {
          fetch("https://localhost:5001/api/users/"+text.substring(7)).then(r=>r.json()).then(result=>{
          var suggestions = [];  
          
                                   
            suggestions.push({ 
              content: "***"+result.name, 
              description: "Id: "+result.name+" Data Base Id: "+result.id
            });
            
          
          suggest(suggestions);  
        })
        })
                
      }
      if (text[0]==="#"){
        console.log("aaaaaaaaah")        
          var suggestions = []; 
          suggestions.push({ 
            content: text, 
            description: "Eliminar la cancion de id: "+text.substring(1)
          });        
          suggest(suggestions);
          
      }
      if(text.substring(0,7)==="*userDL"){        
      
        chrome.identity.getProfileUserInfo(function(userInfo) {
          fetch("https://localhost:5001/api/users/"+text.substring(7),{method:"DELETE",headers:{"UserName":userInfo.id,"email":userInfo.email}}).then(result=>{
          console.log(result)
        })

        })
                
      }
      if (text==="all" || text==="ALL"){        
        
        chrome.identity.getProfileUserInfo(function(userInfo) {
                    
          fetch('https://localhost:5001/api/songs/',{headers:{"UserName":userInfo.id,"email":userInfo.email}}).then(r=>r.json()).then(result => {
          var suggestions = [];     
          if(result.title!=="Not Found"){
             for(i=0;i<result.length;i++){
                         
              suggestions.push({ 
                content: result[i].name+"*"+result[i].artist+"*"+result[i].album+"*"+result[i].imageURL+"*"+result[i].spotifyID, 
                description: "id: "+result[i].id+" Reproducir "+ result[i].name +" de "+result[i].artist+" album "+ result[i].album
              });
              
            }
          }
          suggest(suggestions);
        })
        });
          
      }
      if(text[0]==="@"){
        chrome.identity.getProfileUserInfo(function(userInfo) {
          
          fetch('https://localhost:5001/api/songs/'+text.substring(1),{headers:{"UserName":userInfo.id,"email":userInfo.email}}).then(r=>r.json()).then(result => {
            var suggestions = [];    
            if(result.title!=="Not Found"){
               for(i=0;i<result.length;i++){
                           
                suggestions.push({ 
                  content: result[i].name+"*"+result[i].artist+"*"+result[i].album+"*"+result[i].imageURL+"*"+result[i].spotifyID, 
                  description: "id: "+result[i].id+" Reproducir "+ result[i].name +" de "+result[i].artist+" album "+ result[i].album
                });
                
              }
            }
  
            suggestions.push({ content: "spot_fy/s_ng/" + text.substring(1), description: "Agregar la cancion "+text.substring(1)+" desde spotify" });
            suggest(suggestions);
          })
          

        })
      }
        

        
      }           
  
);

